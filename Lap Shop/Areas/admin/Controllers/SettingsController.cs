using Lap_Shop.BL;
using Lap_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Lap_Shop.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]

    public class SettingsController : Controller
    {
        IclsSettings oclsSettings;
        public SettingsController(IclsSettings settings)
        {
            oclsSettings= settings;
        }
        public IActionResult Edit()
        {
            TbSettings settings = new TbSettings();
          settings=  oclsSettings.GetById();
            return View(settings);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Save(TbSettings settings, List<IFormFile> HomeSlide)
        {

            if (!ModelState.IsValid)
                return View("Edit", settings);
            settings.HomeSlide = await UploadImage(HomeSlide);
 
            settings.id = 1;
            oclsSettings.Save(settings);
            return RedirectToAction("Edit");

        }
        async Task<string> UploadImage(List<IFormFile> Files)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    var filepaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Slides", ImageName);
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
