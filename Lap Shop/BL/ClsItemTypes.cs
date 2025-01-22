using Lap_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lap_Shop.BL
{
        public interface IItemType
        {
            public List<TbItemType> GetAll();
            public bool Save(TbItemType ItemType);
            public bool Delete(int Id);
            public TbItemType GetById(int Id);

        }
    public class ClsItemTypes : IItemType
    {
        LapShopContext CTX;
        public ClsItemTypes(LapShopContext context)
        {
            CTX = context;
        }

        public List<TbItemType> GetAll()
        {
            try
            {
                var LstCategores = CTX.TbItemTypes.ToList();
                return LstCategores;
            }
            catch
            {
                return new List<TbItemType>();
            }
        }
        public bool Save(TbItemType ItemType)
        {

            try
            {



                if (ItemType.ItemTypeId == 0)
                {
                    ItemType.CreatedBy = "me";
                    ItemType.UpdatedDate = DateTime.Now;
                    CTX.TbItemTypes.Add(ItemType);
                }
                else
                {
                    ItemType.UpdatedBy = "me";
                    ItemType.UpdatedDate = DateTime.Now;
                    CTX.Entry(ItemType).State = EntityState.Modified;
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
                CTX.TbItemTypes.Remove(cate);
                CTX.SaveChanges();
                return true;

            }
            catch { return false; }
        }

        public TbItemType GetById(int Id)
        {

            try
            {

                var ItemType = CTX.TbItemTypes.FirstOrDefault(a => a.ItemTypeId == Id);
                return ItemType;
            }
            catch
            {
                return new TbItemType();
            }
        }
    }
}
