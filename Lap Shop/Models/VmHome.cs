using Lap_Shop.BL;

namespace Lap_Shop.Models
{
    public class VmHome
    {
        public VmHome()
        {
            LstAllItems = new List<VwItem>();
                LstNewData = new List<VwItem>();
            LstBestSellers = new List<VwItem>();
            LstFeatured = new List<VwItem>();
            LstCategory = new List<TbCategory>();
            settings= new TbSettings();
        }
        public List<VwItem> LstAllItems { get; set; }
        public List<VwItem> LstNewData { get; set; }
        public List<VwItem> LstBestSellers { get; set; }
        public List<VwItem> LstFeatured { get; set; }
        public List<TbCategory> LstCategory { get; set; }
        public List<TbSlider> LstSlider { get; set; }
        public TbSettings settings { get; set; }

    }
}
