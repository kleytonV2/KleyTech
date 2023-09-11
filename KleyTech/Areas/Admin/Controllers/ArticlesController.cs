using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace KleyTech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticlesController(IWorkContainer workContainer, IWebHostEnvironment webHostEnvironment)
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

            ArticleVM articleVM = new ArticleVM() {
                Article = new KleyTech.Models.Article(),
                CategoryList = _workContainer.Category.GetCategoryList()
            };
            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleVM articleVM)
        {

            if (ModelState.IsValid)
            {
                string MainRoute = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (articleVM.Article.Id == 0) { 
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(MainRoute, @"images\articles");
                    var extension = Path.GetExtension(files[0].FileName );

                    using (var fileStream = new FileStream(Path.Combine(uploads,fileName + extension ),FileMode.Create ) ) {
                        files[0].CopyTo(fileStream);
                    }

                    articleVM.Article.ImageURL = @"\images\articles\" + fileName + extension;
                    articleVM.Article.CreationDate = DateTime.Now.ToString();

                    _workContainer.Article.Add(articleVM.Article);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
            }

            articleVM.CategoryList = _workContainer.Category.GetCategoryList();
            return View(articleVM);
            
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            ArticleVM articleVM = new ArticleVM()
            {
                Article = new KleyTech.Models.Article(),
                CategoryList = _workContainer.Category.GetCategoryList()
            };

            if (id != null) {
                articleVM.Article = _workContainer.Article.Get(id.GetValueOrDefault());
            }

            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleVM articleVM)
        {

            if (ModelState.IsValid)
            {
                string MainRoute = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var articleFromDB = _workContainer.Article.Get(articleVM.Article.Id);

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var uploads = Path.Combine(MainRoute, @"images\articles");
                    var nuevaExtension = Path.GetExtension(files[0].FileName);

                    if (articleFromDB.ImageURL != null) {
                        var imageRoute = Path.Combine(MainRoute, articleFromDB.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(imageRoute))
                        {
                            System.IO.File.Delete(imageRoute);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    articleVM.Article.ImageURL = @"\images\articles\" + fileName + extension;

                }
                else {
                    articleVM.Article.ImageURL = articleFromDB.ImageURL;
                }

                _workContainer.Article.Update(articleVM.Article);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }

            articleVM.CategoryList = _workContainer.Category.GetCategoryList();
            return View(articleVM);

        }

        #region Call to API

        [HttpGet]
        public IActionResult GetAll()
        {
            //Opcion1
            return Json(new { data = _workContainer.Article.GetAll(includeProperties:"Category") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articleFromDb = _workContainer.Article.Get(id);
            string imageMainRoute = _webHostEnvironment.WebRootPath;
            var imageRoute = Path.Combine(imageMainRoute, articleFromDb.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(imageRoute))
            {
                System.IO.File.Delete(imageRoute);
            }
            if (articleFromDb == null) {
                return Json(new { success = false, Message = "Error erasing article" });
            }

            _workContainer.Article.Remove(articleFromDb);
            _workContainer.Save();
            return Json(new { success = true, Message = "Article erased correctly" });
        }

        #endregion
    }
}
