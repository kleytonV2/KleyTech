using KleyTech.Areas.User.Controllers;
using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;
using KleyTech.Models.ViewModels;
using KleyTech.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace KleyTech.Areas.Admin.Controllers
{
    [Authorize(Roles = CNT.Admin)]
    [Area("Admin")]
    public class PageClassController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PageClassController(IWorkContainer workContainer, IWebHostEnvironment webHostEnvironment)
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
        public IActionResult Create(PageClass pageClass)
        {

            if (ModelState.IsValid)
            {
                _workContainer.PageClass.Add(pageClass);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(pageClass);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PageClass pageClass = _workContainer.PageClass.Get(id);
            if (pageClass == null)
            {
                //return NotFound();
                pageClass = new PageClass { Name = "Main pageClass" };
                _workContainer.PageClass.Add(pageClass);
                _workContainer.Save();
            }

            return View(pageClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PageClass pageClass)
        {

            if (ModelState.IsValid)
            {
                _workContainer.PageClass.Update(pageClass);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(pageClass);

        }

        #region Call to API

        [HttpGet]
        public IActionResult GetAll()
        {
            //Opcion1
            return Json(new { data = _workContainer.PageClass.GetAll() });
        }

        [HttpGet]
        public IActionResult ViewPage(int id)
        {
            HttpContext.Session.SetInt32("idPage",id);
            ViewData["IdPage"] = id;
            //HomeController home = new HomeController(_workContainer);
            return RedirectToAction("Index","Home",new { area = "User" });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pageClassFromDb = _workContainer.PageClass.Get(id);
            if (pageClassFromDb == null)
            {
                return Json(new { success = false, Message = "Error erasing pageClass" });
            }

            _workContainer.PageClass.Remove(pageClassFromDb);
            _workContainer.Save();
            return Json(new { success = true, Message = "PageClass erased correctly" });
        }

        #endregion
    }
}
