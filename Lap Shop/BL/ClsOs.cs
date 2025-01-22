using Lap_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lap_Shop.BL
{
        public interface IOS
        {
            public List<TbO> GetAll();
            public bool Save(TbO IO);
            public bool Delete(int Id);
            public TbO GetById(int Id);
           
        }
    public class ClsOs : IOS
    {
        LapShopContext CTX;
        public ClsOs(LapShopContext context)
        {
            CTX = context;
        }

        public List<TbO> GetAll()
        {
            try
            {
                var LstCategores = CTX.TbOs.ToList();
                return LstCategores;
            }
            catch
            {
                return new List<TbO>();
            }
        }
        public bool Save(TbO Os)
        {

            try
            {



                if (Os.OsId == 0)
                {
                    Os.CreatedBy = "me";
                    Os.UpdatedDate = DateTime.Now;
                    CTX.TbOs.Add(Os);
                }
                else
                {
                    Os.UpdatedBy = "me";
                    Os.UpdatedDate = DateTime.Now;
                    CTX.Entry(Os).State = EntityState.Modified;
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
                CTX.TbOs.Remove(cate);
                CTX.SaveChanges();
                return true;

            }
            catch { return false; }
        }

        public TbO GetById(int Id)
        {

            try
            {

                var Os = CTX.TbOs.FirstOrDefault(a => a.OsId == Id);
                return Os;
            }
            catch
            {
                return new TbO();
            }
        }
    }
}
