using Lap_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lap_Shop.BL
{
        public interface ICategory
        {
            public List<TbCategory> GetAll();
            public bool Save(TbCategory category);
            public bool Delete(int Id);
            public TbCategory GetById(int? Id);
        }
    public class ClsCategory : ICategory
    {
        LapShopContext CTX;
        public ClsCategory(LapShopContext context)
        {
            CTX = context;
        }

        public List<TbCategory> GetAll()
        {
            try
            {
                var LstCategores = CTX.TbCategories.Where(x=>x.CurrentState!=0).ToList();
                return LstCategores;
            }
            catch
            {
                return new List<TbCategory>();
            }
        }
        public bool Save(TbCategory category)
        {
          
            try
            {

               
         
                if (category.CategoryId == 0)
                {
                    category.CreatedBy = "me";
                    category.UpdatedDate = DateTime.Now;
                    CTX.TbCategories.Add(category);
                }
                else
                {
                    category.CurrentState= 1;
                    category.UpdatedBy = "me";
                    category.UpdatedDate = DateTime.Now;
                    CTX.Entry(category).State = EntityState.Modified;
                }
                CTX.SaveChanges();
                return true;    
            }
            catch
            {
                return false;
            }
            
        }
        public bool Delete(int Id) {
            try
            {
      
               
                var cate =GetById(Id);
                cate.CurrentState = 0;
                CTX.Entry(cate).State=EntityState.Modified;
                CTX.SaveChanges();

                return true;

            }
            catch { return false; }
        }
  
        public TbCategory GetById(int? Id) {
   
            try
            {
              
                var category = CTX.TbCategories.FirstOrDefault(a => a.CategoryId == Id);
                if (category == null)
                {
                    return category;
                }
                return category;
            }
            catch
            {
                return new TbCategory();
            }
        }
    }
}
