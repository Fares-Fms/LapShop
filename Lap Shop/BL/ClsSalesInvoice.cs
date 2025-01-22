using Lap_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;
namespace Lap_Shop.BL
{
    public interface IsalesInvoice
    {
        public List<VwSalesInvoice> GetAll();
        public decimal GetSales();
        public int GetOrders();

        public Task<bool> Save(TbSalesInvoice item, List<TbSalesInvoiceItem> lstItems,ClaimsPrincipal user,bool isnew);
        public bool Delete(int Id);
        public TbSalesInvoice GetById(int Id);
        public List<TbSalesInvoice> GetInvoice(Guid Id);
        public TbSalesInvoice GetByInvoiceId(int Id);

    }
    public class ClsSalesInvoice : IsalesInvoice
    {
        LapShopContext CTX;
        ISalesInvoiceItems salesInvoiceItems;
        UserManager<ApplicationUser> _userManager;
        public ClsSalesInvoice(LapShopContext context, ISalesInvoiceItems isalesInvoiceItems,UserManager<ApplicationUser> userManager)
        {
            CTX = context;
            salesInvoiceItems = isalesInvoiceItems;
           _userManager = userManager;  

        }
        public bool Delete(int Id)
        {
            try
            {
                var cate = GetById(Id);
                cate.CurrentState = 0;
                CTX.Entry(cate).State = EntityState.Modified;
                CTX.SaveChanges();
                return true;

            }
            catch { return false; }
        }

        public List<VwSalesInvoice> GetAll()
        {

            try
            {
                return CTX.VwSalesInvoices.ToList();
            }
            catch
            {
                return new List<VwSalesInvoice>();
            }
        }

        public TbSalesInvoice GetById(int Id)
        {
            try
            {

             
                var SalesInvoices = CTX.TbSalesInvoices.FirstOrDefault(a => a.InvoiceId == Id);
                if (SalesInvoices == null)
                {
                    return new TbSalesInvoice();
                }
                else
                return SalesInvoices;
            }
            catch
            {
                return new TbSalesInvoice();
            }
        }

        public List<TbSalesInvoice> GetInvoice(Guid Id)
        {
            try
            {
                var invoice= CTX.TbSalesInvoices.Where(a=>a.CustomerId==Id).OrderByDescending(a=>a.InvoiceDate).ToList();
                if (invoice==null)
                {
                    return new List<TbSalesInvoice>();
                }
                else return invoice;
            }
            catch (Exception e)
            {
                return new List<TbSalesInvoice>();
            }
        }

        public TbSalesInvoice GetByInvoiceId(int Id)
        {
            try
            {
                var invoice = CTX.TbSalesInvoices.FirstOrDefault(a => a.InvoiceId == Id);
                if (invoice == null)
                {
                    return new TbSalesInvoice();
                }
                else return invoice;
            }
            catch (Exception e)
            {
                return new TbSalesInvoice();
            }
        }
            
        public int GetOrders()
        {
            int orders = CTX.TbSalesInvoices.Count(x => x.CurrentState != 0);
            return orders;
        }

        public decimal GetSales()
        {
            var sales = CTX.TbSalesInvoiceItems.Sum(a => a.InvoicePrice);
            return sales;
        }

        public async Task<bool> Save(TbSalesInvoice? item, List<TbSalesInvoiceItem>? lstItems,ClaimsPrincipal principal,bool isnew)
        {
            using var transaction = CTX.Database.BeginTransaction();

            var user = await _userManager.GetUserAsync(principal);
            try
            {



                item.CurrentState = 1;
                if (isnew)
                {
                   item.CreatedBy = user?.UserName ?? "Anonymous";
                    item.CreatedDate = DateTime.Now;
                    CTX.TbSalesInvoices.Add(item);
                }
                else
                {
                    item.UpdatedBy = user?.UserName ?? "Anonymous";
                    item.UpdatedDate = DateTime.Now;

                    CTX.Entry(item).State = EntityState.Modified;
                }
                CTX.SaveChanges();
                salesInvoiceItems.save(lstItems, item.InvoiceId,isnew);
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }

        }
    }
}
