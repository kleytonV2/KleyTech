using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;
using KleyTech.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace KleyTech.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public HomeController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session != null) {
                int? idPage = HttpContext.Session.GetInt32("idPage");
                PageClass pageClass = _workContainer.PageClass.GetFirstOrDefault();
                if (idPage == 0 && pageClass != null) {
                    idPage = _workContainer.PageClass.GetFirstOrDefault().Id;
                }

                if (idPage > 0) { 
            
                }
            
            }

            HomeVM homeVM = new HomeVM()
            {
                Slider = _workContainer.Slider.GetAll(),
                ArticleList = _workContainer.Article.GetAll()
            };

            ViewBag.IsHome = true;

            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            return View(_workContainer.Article.Get(id));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}