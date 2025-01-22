using Lap_Shop.BL;
using Lap_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
namespace Lap_Shop.Controllers
{
    public class ShopingCartController : Controller
    {
        IItems oclsitems;
        UserManager<ApplicationUser> _userManager;
        IsalesInvoice osalesInvoice;
        ISalesInvoiceItems _invoiceItems;
        public ShopingCartController(IItems items,UserManager<ApplicationUser> userManager,IsalesInvoice isalesInvoice,ISalesInvoiceItems salesInvoiceItems)
        {
            _userManager = userManager;
            oclsitems = items;
            osalesInvoice = isalesInvoice;
    _invoiceItems = salesInvoiceItems;
        }
        public IActionResult ShowInvoice(int id) 
        {
            VmInvoice vmInvoice=new VmInvoice();
            var lstInvoiceItem=_invoiceItems.GetSalesInvoicesId(id);
           vmInvoice.Items=_invoiceItems.GetItems(lstInvoiceItem);
            vmInvoice.Invoice = osalesInvoice.GetByInvoiceId(id);
            return View(vmInvoice);
        }
        public IActionResult cart()
        {
            var sesstion = HttpContext.Request.Cookies["cart"];

            if (sesstion != null)
            {
                var cart = JsonConvert.DeserializeObject<ShopingCart>(sesstion);
                return View(cart);

            }
       
            return View();
            
        }
        [Authorize]
        public IActionResult MyOrders()
        {

            var user = _userManager.GetUserAsync(User).Result;
            var UserId =Guid.Parse( user.Id);
       
            return View(osalesInvoice.GetInvoice(UserId));

        }
        [Authorize]
        public async Task <IActionResult> OrderSuccess()
        {
            string sessionCart = "";
            if (HttpContext.Request.Cookies["cart"] != null)
                sessionCart = HttpContext.Request.Cookies["cart"];
            var cart = JsonConvert.DeserializeObject<ShopingCart>(sessionCart);
            
            await SaveOrder(cart);
            return View(cart);

        }
        public IActionResult AddTocart(int itemId)
        {
            ShopingCart cart;
         
            if (HttpContext.Request.Cookies["cart"] != null)
                cart = JsonConvert.DeserializeObject<ShopingCart>(HttpContext.Request.Cookies["cart"]);
            else
                cart = new ShopingCart();
            var item = oclsitems.GetById(itemId);
            var iteminlist = cart.LstItems.Where(a => a.itemId == itemId).FirstOrDefault();
            if (iteminlist != null)
            {
                iteminlist.quantity++;
                iteminlist.Total = iteminlist.price * iteminlist.quantity;
            }

            else
            {
                cart.LstItems.Add(new ShopingCartItem
                {
                    itemId = item.ItemId,
                    itemName = item.ItemName,
                    imageName = item.ImageName,
                    price = item.SalesPrice,
                    quantity = 1,
                    Total = item.SalesPrice
                });
            }
            cart.total = cart.LstItems.Sum(a => a.Total);
            HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("cart");
        }
        public IActionResult Remove(int itemId)
        {
            ShopingCart cart;

            if (HttpContext.Request.Cookies["cart"] == null)
                return RedirectToAction("cart");
            else
                cart = JsonConvert.DeserializeObject<ShopingCart>(HttpContext.Request.Cookies["cart"]);
            var item= oclsitems.GetById(itemId);
            if (item != null)
            {
                var iteminlist = cart.LstItems.Where(a => a.itemId == itemId).FirstOrDefault();
                if (iteminlist != null)
                {
                    cart.LstItems.Remove(iteminlist);
                }

                    
            }
            cart.total = cart.LstItems.Sum(a => a.Total);
            HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("cart");
        }
            async Task SaveOrder(ShopingCart shopingCart)
        {
            try
            {
                if (shopingCart == null || shopingCart.LstItems == null || !shopingCart.LstItems.Any())
                    throw new Exception("ShoppingCart or LstItems is null or empty");

                List<TbSalesInvoiceItem> lstinvoiceItems = shopingCart.LstItems.Select(item => new TbSalesInvoiceItem
                {
                    ItemId = item.itemId,
                    Qty = item.quantity,
                    InvoicePrice = item.price,
                }).ToList();

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    throw new Exception("User not found");

                TbSalesInvoice oSalesInvoice = new TbSalesInvoice
                {
                    InvoiceDate = DateTime.Now,
                    CustomerId = Guid.Parse(user.Id),
                    DelivryDate = DateTime.Now.AddDays(3),
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now
                };

                if (!lstinvoiceItems.Any())
                    throw new Exception("LstinvoiceItems is empty");

                var invoiceService = osalesInvoice.Save(oSalesInvoice, lstinvoiceItems, User, true);
             
            }
            catch (Exception ex)
            {
             
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
