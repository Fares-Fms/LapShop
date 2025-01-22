using Microsoft.AspNetCore.Mvc;
using Lap_Shop.Models;
using Lap_Shop.BL;
namespace Lap_Shop.Controllers
{
    public class HomeController : Controller
    {
        IItems oclsitems;
        ICategory oclscategory;
        IclsSettings oclscsettings;
        ISliders oClsSliders;
        public HomeController(IItems items,ICategory category,IclsSettings settings,ISliders sliders)
        {
            oclsitems=items;
            oclscategory=category;
            oclscsettings=settings;
            oClsSliders=sliders;
        }
        public IActionResult Index()
        {
            VmHome vm=new VmHome();
            vm.LstAllItems = oclsitems.GetAllItemsDta(null).Take(8).ToList();
            vm.LstBestSellers = oclsitems.GetAllItemsDta(null).Take(8).ToList();
            vm.LstFeatured = oclsitems.GetAllItemsDta(null).Where(x => x.SalesPrice > 800).Take(8).ToList();
            vm.LstCategory = oclscategory.GetAll();
            vm.LstSlider = oClsSliders.GetAll();
            vm.settings = oclscsettings.GetById();
            return View(vm);
        }       
    }
}
