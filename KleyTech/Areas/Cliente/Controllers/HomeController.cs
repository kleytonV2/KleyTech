using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;
using KleyTech.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KleyTech.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public HomeController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM() {
                Slider = _workContainer.Slider.GetAll(),
                ArticleList = _workContainer.Article.GetAll()
            };
            return View(homeVM);
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