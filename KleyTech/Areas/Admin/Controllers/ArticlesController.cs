using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

                    articleVM.Article.ImageURL = @"images\articles\" + fileName + extension;
                    articleVM.Article.CreationDate = DateTime.Now.ToString();

                    _workContainer.Article.Add(articleVM.Article);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
            }

            articleVM.CategoryList = _workContainer.Category.GetCategoryList();
            return View(articleVM);
            
        }

        #region Call to API

        [HttpGet]
        public IActionResult GetAll()
        {
            //Opcion1
            return Json(new { data = _workContainer.Article.GetAll() });
        }

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var objFromDB = _workContainer.Article.Get(id);
        //    if (objFromDB == null)
        //    {
        //        return Json(new { success = false, Message = "Error erasing article" });
        //    }

        //    _workContainer.Article.Remove(objFromDB);
        //    _workContainer.Save();
        //    return Json(new { success = true, Message = "Article erased correctly" });
        //}

        #endregion
    }
}
