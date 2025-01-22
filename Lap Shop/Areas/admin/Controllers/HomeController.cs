using Lap_Shop.BL;
using Lap_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lap_shop.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        IUser _user;
        IsalesInvoice _isalesInvoice;
        public HomeController(IUser user,IsalesInvoice invoice)
        {
            _isalesInvoice = invoice;
            _user = user;
        }
        public IActionResult Index()
        {
            VmAdmin vmAdmin = new VmAdmin();
            vmAdmin.Sales=_isalesInvoice.GetSales();
            vmAdmin.NumUsers = _user.UserCount();
            vmAdmin.NumOrders=_isalesInvoice.GetOrders();
            return View(vmAdmin);
        }
    }
}
