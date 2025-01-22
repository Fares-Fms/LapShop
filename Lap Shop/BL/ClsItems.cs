using Lap_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lap_Shop.BL
{
    public interface IItems
    {
        public List<TbItem> GetAll(int? id);
        public List<TbItem> GetRecommendedItem(int? id);
        public bool Save(TbItem item);
        public bool Delete(int Id);
        public TbItem GetById(int? Id);
        public List<VwItem> GetAllItemsDta(int? id);
        public VwItem GetItemById(int Id);
        public List<VwItem> GetItemByName(string name);
    }
    public class ClsItems : IItems
    {
        LapShopContext CTX;
        public ClsItems(LapShopContext context)
        {
            CTX = context;
        }

        public List<TbItem> GetAll(int? id)
        {
            try
            {
                var LstItems = CTX.TbItems.Where(a => (a.CategoryId == id || id == null || id == 0)&&(a.CurrentState==1)).ToList();
                return LstItems;
            }
            catch
            {
                return new List<TbItem>();
            }
        }
        public List<VwItem> GetAllItemsDta(int? id)
        {
            try
            {
                var LstItems = CTX.VwItems.Where(a=>(a.CategoryId==id || id==null||id==0)&&(a.CurrentState == 1)).ToList();
                return LstItems;
            }
            catch
            {
                return new List<VwItem>();
            }
        }
        public bool Save(TbItem Items)
        {

            try
            {



                    Items.CurrentState = 1;
                if (Items.ItemId == 0)
                {
                    Items.CreatedBy = "me";
                    Items.UpdatedDate = DateTime.Now;
                    CTX.TbItems.Add(Items);
                }
                else
                {
                    Items.UpdatedBy = "me";
                    Items.UpdatedDate = DateTime.Now;
                   
                    CTX.Entry(Items).State = EntityState.Modified;
                }
                CTX.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

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

        public TbItem GetById(int? Id)
        {

            try
            {

                var Items = CTX.TbItems.FirstOrDefault(a => a.ItemId == Id);
                return Items;
            }
            catch
            {
                return new TbItem();
            }
        }

        public VwItem GetItemById(int Id)
        {

           

                try
                {

                    var Items = CTX.VwItems.FirstOrDefault(a => a.ItemId == Id);
                    return Items;
                }
                catch
                {
                    return new VwItem();
                }
          
        }

        public List<TbItem> GetRecommendedItem(int? id)

        {
            try
            {
                var item = GetById(id);
                var LstItems = CTX.TbItems.Where(a => (a.ItemTypeId==item.ItemTypeId )&&
                (a.SalesPrice > item.SalesPrice-50 && a.SalesPrice < item.SalesPrice + 50) &&

                (a.CurrentState == 1)).ToList();
                return LstItems;
            }
            catch
            {
                return new List<TbItem>();
            }
        }

        public List<VwItem> GetItemByName(string name)
        {
            try
            {

                var Items = CTX.VwItems.Where(a => (a.ItemName.Contains(name)||a.CategoryName.Contains(name))&&(a.CurrentState!=0)).Take(25).ToList();
                return Items;
            }
            catch
            {
                return new List<VwItem>();
            }
        }
    }
}
