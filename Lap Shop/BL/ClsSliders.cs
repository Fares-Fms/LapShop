using Lap_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lap_Shop.BL
{
    public interface ISliders
    {
        public List<TbSlider> GetAll();
        public bool Save(TbSlider IO);
        public bool Delete(int Id);
        public TbSlider GetById(int Id);

    }
    public class ClsSliders : ISliders
    {
        LapShopContext CTX;
        public ClsSliders(LapShopContext context)
        {
            CTX = context;   
        }
        public List<TbSlider> GetAll()
        {
            try
            {
                var LstSliders = CTX.TbSliders.ToList();
                return LstSliders;
            }
            catch
            {
                return new List<TbSlider>();
            }
        }
        public bool Save(TbSlider Slider)
        {

            try
            {



                if (Slider.SliderId == 0)
                {
               
                    CTX.TbSliders.Add(Slider);
                }
                else
                {
                  
                    CTX.Entry(Slider).State = EntityState.Modified;
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
                CTX.TbSliders.Remove(cate);
                CTX.SaveChanges();
                return true;

            }
            catch { return false; }
        }

        public TbSlider GetById(int Id)
        {

            try
            {

                var Slider = CTX.TbSliders.FirstOrDefault(a => a.SliderId == Id);
                return Slider;
            }
            catch
            {
                return new TbSlider();
            }
        }
    }
}
