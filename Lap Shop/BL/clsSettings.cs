using Lap_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lap_Shop.BL
{
   public interface IclsSettings
    {
        public bool Save(TbSettings settings);
        public TbSettings GetById();
    }
    public class clsSettings : IclsSettings
    {
        LapShopContext CTX;
        public clsSettings(LapShopContext context)
        {
            CTX = context;
        }
        public bool Save(TbSettings settings)
        {

            try
            {


            
                CTX.TbHomeSettings.Entry(settings).State = EntityState.Modified;
                
                CTX.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public TbSettings GetById()
        {

            try
            {

                var category = CTX.TbHomeSettings.First();
                return category;
            }
            catch
            {
                return new TbSettings();
            }
        }

    }
}
