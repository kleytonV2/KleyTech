using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;
using KleyTech.Models.ViewModels;
using KleyTech.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Data;

namespace KleyTech.Areas.Admin.Controllers
{
    [Authorize(Roles = CNT.Admin)]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlidersController(IWorkContainer workContainer, IWebHostEnvironment webHostEnvironment)
        {
            _workContainer = workContainer;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {

            if (ModelState.IsValid)
            {
                string MainRoute = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var sliderFromDB = _workContainer.Slider.Get(slider.Id);

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var uploads = Path.Combine(MainRoute, @"images\sliders");

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    slider.ImageUrl = @"\images\sliders\" + fileName + extension;

                }

                _workContainer.Slider.Add(slider);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(slider);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Slider slider = _workContainer.Slider.Get(id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {

            if (ModelState.IsValid)
            {
                string MainRoute = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var sliderFromDB = _workContainer.Slider.Get(slider.Id);

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var uploads = Path.Combine(MainRoute, @"images\sliders");

                    if (sliderFromDB.ImageUrl.Length > 0)
                    {
                        var imageRoute = Path.Combine(MainRoute, sliderFromDB.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imageRoute))
                        {
                            System.IO.File.Delete(imageRoute);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    slider.ImageUrl = @"\images\sliders\" + fileName + extension;

                }
                else
                {
                    slider.ImageUrl = sliderFromDB.ImageUrl;
                }

                _workContainer.Slider.Update(slider);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(slider);

        }

        #region Call to API

        [HttpGet]
        public IActionResult GetAll()
        {
            //Opcion1
            return Json(new { data = _workContainer.Slider.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sliderFromDb = _workContainer.Slider.Get(id);
            string imageMainRoute = _webHostEnvironment.WebRootPath;
            if (sliderFromDb.ImageUrl != null)
            {
                var imageRoute = Path.Combine(imageMainRoute, sliderFromDb.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imageRoute))
                {
                    System.IO.File.Delete(imageRoute);
                }
            }
            if (sliderFromDb == null)
            {
                return Json(new { success = false, Message = "Error erasing slider" });
            }

            _workContainer.Slider.Remove(sliderFromDb);
            _workContainer.Save();
            return Json(new { success = true, Message = "Slider erased correctly" });
        }

        #endregion
    }
}