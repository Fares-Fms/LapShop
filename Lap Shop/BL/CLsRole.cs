using Lap_Shop.Models;

namespace Lap_Shop.BL
{
   public interface IRole
    {
        public List<AspNetRole> GetAll();
    }
    public class CLsRole : IRole
    {
        ApplicationDbContext CTX;
        public CLsRole(ApplicationDbContext context)
        {
            CTX = context;
        }
        public List<AspNetRole> GetAll()
        {
            try
            {
                var LstItems = CTX.AspNetRoles.ToList();

                return LstItems;
            }
            catch
            {
                return new List<AspNetRole>();
            }
        }
    }
}
