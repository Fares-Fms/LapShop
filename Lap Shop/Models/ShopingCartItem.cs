namespace Lap_Shop.Models
{
    public class ShopingCartItem
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string imageName { get; set; }
        public int quantity { get; set; }
      public  decimal  price { get; set; }
       public decimal  Total { get; set; }
    }
}
