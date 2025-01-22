using Lap_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lap_Shop.BL
{
    public interface ISalesInvoiceItems
    {
        public List<TbSalesInvoiceItem> GetSalesInvoicesId(int id);
        public bool save(IList<TbSalesInvoiceItem> items, int invoiceid, bool isnew);
        public List<TbItem> GetItems(List<TbSalesInvoiceItem> invoiceItems);
    }
    public class ClsSalesInvoiceItems : ISalesInvoiceItems
    {
        LapShopContext CTX;
        public ClsSalesInvoiceItems(LapShopContext context)
        {
            CTX = context;
        }

        public List<TbItem> GetItems(List<TbSalesInvoiceItem> invoiceItems)
        {
            try
            {
                List<TbItem> tbItems = new List<TbItem>();  
                foreach (var item in invoiceItems)
                {
                    tbItems.Add(CTX.TbItems.FirstOrDefault(a => a.ItemId == item.ItemId));
                }
          
                if (tbItems == null)
                {
                    return new List<TbItem>();
                }
                else
                    return tbItems;
            }
            catch
            {
                return new List<TbItem>();
            }
        }

        public List<TbSalesInvoiceItem> GetSalesInvoicesId(int id)
        {
            try
            {

                var SalesInvoices = CTX.TbSalesInvoiceItems.Where(a => a.InvoiceId == id).ToList();
                if (SalesInvoices == null)
                {
                    return new List<TbSalesInvoiceItem>();
                }
                else
                    return SalesInvoices;
            }
            catch
            {
                return new List<TbSalesInvoiceItem>();
            }
        }

        public bool save(IList<TbSalesInvoiceItem> items,int invoiceid, bool isnew)
        {
            List<TbSalesInvoiceItem> salesInvoiceItems = GetSalesInvoicesId(items[0].InvoiceId);
            foreach (var interfaceitems in items)
            {
                var dbObject = salesInvoiceItems.Where(a => a.InvoiceItemId == interfaceitems.InvoiceItemId).FirstOrDefault();
                if (dbObject != null)
                {
                    CTX.Entry(dbObject).State= EntityState.Modified;
                }
                else
                {
                    interfaceitems.InvoiceId= invoiceid;
                    CTX.TbSalesInvoiceItems.Add(interfaceitems);
                }
            }
            foreach (var item in salesInvoiceItems)
            {
                var interfaceObj = items.Where(a => a.InvoiceItemId == item.InvoiceItemId).FirstOrDefault();
                if (interfaceObj==null)
                {
                    CTX.TbSalesInvoiceItems.Remove(item);
                }
            }
                CTX.SaveChanges();
                return true;    
        }
    }
}
