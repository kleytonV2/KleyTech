using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;
using KleyTech.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace KleyTech.Areas.Admin.Controllers
{
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
                _workContainer.Slider.Add(slider);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(slider);
            
        }

        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{

        //    ArticleVM articleVM = new ArticleVM()
        //    {
        //        Article = new KleyTech.Models.Article(),
        //        CategoryList = _workContainer.Category.GetCategoryList()
        //    };

        //    if (id != null) {
        //        articleVM.Article = _workContainer.Article.Get(id.GetValueOrDefault());
        //    }

        //    return View(articleVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(ArticleVM articleVM)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        string MainRoute = _webHostEnvironment.WebRootPath;
        //        var files = HttpContext.Request.Form.Files;
        //        var articleFromDB = _workContainer.Article.Get(articleVM.Article.Id);

        //        if (files.Count > 0)
        //        {
        //            string fileName = Guid.NewGuid().ToString();
        //            var extension = Path.GetExtension(files[0].FileName);
        //            var uploads = Path.Combine(MainRoute, @"images\sliders");
        //            var nuevaExtension = Path.GetExtension(files[0].FileName);

        //            var imageRoute = Path.Combine(MainRoute, articleFromDB.ImageURL.TrimStart('\\'));
        //            if (System.IO.File.Exists(imageRoute))
        //            {
        //                System.IO.File.Delete(imageRoute);
        //            }

        //            using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
        //            {
        //                files[0].CopyTo(fileStream);
        //            }

        //            articleVM.Article.ImageURL = @"\images\sliders\" + fileName + extension;

        //        }
        //        else {
        //            articleVM.Article.ImageURL = articleFromDB.ImageURL;
        //        }

        //        _workContainer.Article.Update(articleVM.Article);
        //        _workContainer.Save();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    articleVM.CategoryList = _workContainer.Category.GetCategoryList();
        //    return View(articleVM);

        //}

        #region Call to API

        [HttpGet]
        public IActionResult GetAll()
        {
            //Opcion1
            return Json(new { data = _workContainer.Slider.GetAll() });
        }

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var sliderFromDb = _workContainer.Slider.Get(id);
        //    string imageMainRoute = _webHostEnvironment.WebRootPath;
        //    var imageRoute = Path.Combine(imageMainRoute, sliderFromDb.ImageUrl.TrimStart('\\'));
        //    if (System.IO.File.Exists(imageRoute))
        //    {
        //        System.IO.File.Delete(imageRoute);
        //    }
        //    if (sliderFromDb == null) {
        //        return Json(new { success = false, Message = "Error erasing slider" });
        //    }

        //    _workContainer.Slider.Remove(sliderFromDb);
        //    _workContainer.Save();
        //    return Json(new { success = true, Message = "Slider erased correctly" });
        //}

        #endregion
    }
}
