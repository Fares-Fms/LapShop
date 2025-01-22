using Lap_Shop.BL;
using Lap_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lap_Shop.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]

    public class ItemsController : Controller
    {
            
        public ItemsController(IItems items,ICategory category,IOS oS,IItemType itemType)
        {
            oclsItems = items;
            oclsCategory = category;
            oclsIOS = oS;
            oclsItemType = itemType;
        }
        #region CLS
        IItems oclsItems;
        ICategory oclsCategory;
IOS oclsIOS;
        IItemType oclsItemType;

        #endregion
        public IActionResult List()
        {
            ViewBag.category = oclsCategory.GetAll();
            return View(oclsItems.GetAllItemsDta(null));
        }


    
        public IActionResult Edit1(int? itemId)
        {
            ViewBag.osbag = oclsIOS.GetAll();
            ViewBag.ItemTypeBag = oclsItemType.GetAll();
            ViewBag.CategoryBag = oclsCategory.GetAll();
            var item = new TbItem();
            if (itemId != null)
            {
                item = oclsItems.GetById(Convert.ToInt32(itemId));
            }
            return View(item);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/Items/Save_with_image")]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {

            if (!ModelState.IsValid)
                return View("Edit1", item);
            item.ImageName  = await UploadImage(Files);
            oclsItems.Save(item);
            return RedirectToAction("List");

        }

        public IActionResult Delete(int itemId)
        {
            oclsItems.Delete(itemId);
            return RedirectToAction("List");
        }
 
        public IActionResult Show(int itemId)
        {
            return View(oclsItems.GetById(itemId));
        }
        public IActionResult Search(int id) 
        {
          
            ViewBag.category = oclsCategory.GetAll();
            return View("List", oclsItems.GetAllItemsDta(id));
        }
        async Task<string> UploadImage(List<IFormFile> Files)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    var filepaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Items", ImageName);
                    using (var stream = System.IO.File.Create(filepaths))
                    {
                        await file.CopyToAsync(stream);
                        return ImageName;
                    }

                }
            }
            return "";
        }
    }
}
