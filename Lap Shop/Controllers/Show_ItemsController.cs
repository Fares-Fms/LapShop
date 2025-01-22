using Lap_Shop.BL;
using Lap_Shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lap_Shop.Controllers
{
    public class Show_ItemsController : Controller
    {
        IItems oclsitem;
        ICategory oclsCategory;
        public Show_ItemsController(IItems item,ICategory category)
        {

            oclsitem = item;
            oclsCategory = category;

        }
        public IActionResult Main(int Id)
        {
            VmShowItem vm = new VmShowItem();

            vm.itemsDetail = oclsitem.GetItemById(Id);
            vm.RecommendedItems = oclsitem.GetRecommendedItem(Id).Take(12).ToList();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Result(string name) {
           return View( oclsitem.GetItemByName(name));

        }
        public IActionResult List(int? id)
        {
            try
            {
                VmCategoryItem categoryItem = new VmCategoryItem();
                categoryItem.LstItems = oclsitem.GetAllItemsDta(id).Take(22).ToList();

                categoryItem.CategoryName = oclsCategory.GetById(id).CategoryName;
                return View(categoryItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(new VmCategoryItem());
            }
        }
        }
}
