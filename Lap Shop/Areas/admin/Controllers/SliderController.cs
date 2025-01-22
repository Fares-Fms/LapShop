using Lap_Shop.BL;
using Lap_Shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lap_Shop.Areas.admin.Controllers
{
    [Area("admin")]
    public class SliderController : Controller
    {
        ISliders oClsSliders;
        public SliderController(ISliders sliders)
        {
            oClsSliders = sliders;   
        }
        public IActionResult Index()
        {
            return View(oClsSliders.GetAll());
        }
        [Route("admin/Slider/Edit")]
        public IActionResult Edit(int? SliderId)
        {
            var category = new TbSlider();
            if (SliderId != null)
            {
                category = oClsSliders.GetById(Convert.ToInt32(SliderId));
            }
            return View(category);
        }
        [HttpPost]
      
        public async Task<IActionResult> Save(TbSlider slider, List<IFormFile> file)
        {

            if (!ModelState.IsValid)
                return View("Edit", slider);
            slider.ImageName = await UploadImage(file);
            oClsSliders.Save(slider);
            return RedirectToAction("Index");

        }
        async Task<string> UploadImage(List<IFormFile> Files)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    var filepaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Sliders", ImageName);
                    using (var stream = System.IO.File.Create(filepaths))
                    {
                        await file.CopyToAsync(stream);
                        return ImageName;
                    }

                }
            }
            return "";
        }
        public IActionResult Delete(int SliderId)
        {
            oClsSliders.Delete(SliderId);
            return RedirectToAction("Index");
        }
    }
}
