namespace Lap_Shop.Models
{
    public class ShopingCart
    {
        public ShopingCart()
        {
            LstItems = new List<ShopingCartItem>();
        }
        public List<ShopingCartItem>? LstItems { get; set; }
        public decimal? total { get; set; }
    }
}
