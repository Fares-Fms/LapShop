namespace Lap_Shop.Models
{
    public class VmShowItem
    {
        public VmShowItem()
        {
            itemsDetail = new VwItem();
            RecommendedItems = new List<TbItem>();
        }
        public VwItem itemsDetail { get; set; }
        public List<TbItem> RecommendedItems { get; set; }

    }
}
