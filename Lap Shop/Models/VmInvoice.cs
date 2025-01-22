namespace Lap_Shop.Models
{
    public class VmInvoice
    {
        public List<TbItem> Items { get; set; }
        public TbSalesInvoice Invoice { get; set; }
    }
}
