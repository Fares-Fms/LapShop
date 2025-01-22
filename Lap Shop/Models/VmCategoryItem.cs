namespace Lap_Shop.Models
{
    public class VmCategoryItem
    {
        public VmCategoryItem()
        {
            LstItems=new List<VwItem>();
            LstRecommended=new List<TbItem>();
        }
        public List<VwItem> LstItems { get; set; }
        public List<TbItem> LstRecommended { get; set; }
        public string? CategoryName { get; set; }
    }
}
