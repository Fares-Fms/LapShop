using Lap_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lap_Shop.BL;
using Microsoft.AspNetCore.Authorization;

namespace Lap_Shop.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]

    public class CategoresController : Controller
    {
        public CategoresController(ICategory category)
        {
            oclsCategory = category;
        }
        ICategory oclsCategory ;


        public IActionResult List()
        {

             return View(oclsCategory.GetAll());
        }


        public IActionResult Edit1(int? categoryId)
        {
            var category = new TbCategory();
            if (categoryId != null)
            {
                category = oclsCategory.GetById(Convert.ToInt32(categoryId));
            }
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/Categores/Save_with_image")]
        public async Task<IActionResult> Save(TbCategory category, List<IFormFile> Files)
        {

            if (!ModelState.IsValid)
                return View("Edit1", category);
            category.ImageName = await UploadImage(Files);
            oclsCategory.Save(category);
            return RedirectToAction("List");

        }

        public IActionResult Delete(int categoryId)
        {
            oclsCategory.Delete(categoryId);
            return RedirectToAction("List");
        }
        async Task<string> UploadImage(List<IFormFile> Files)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    var filepaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Category", ImageName);
                    using (var stream = System.IO.File.Create(filepaths))
                    {
                        await file.CopyToAsync(stream);
                        return ImageName;
                    }

                }
            }
            return "";
        }
        public IActionResult Show(int categoryId)
        {
            return View(oclsCategory.GetById(categoryId));
        }
    }
}
