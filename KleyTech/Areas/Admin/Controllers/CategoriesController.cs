using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;
using KleyTech.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Data;

namespace KleyTech.Areas.Admin.Controllers
{
    [Authorize(Roles = CNT.Admin)]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IWorkContainer _workContainer;
        //private readonly ApplicationDbContext _context;

        public CategoriesController(IWorkContainer workContainer,ApplicationDbContext context)
        {
            _workContainer = workContainer;
            //_context = context;
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
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid) { 
                _workContainer.Category.Add(category);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = _workContainer.Category.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _workContainer.Category.Update(category);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        #region Call to API

        [HttpGet]
        public IActionResult GetAll() {
            //Opcion1
            return Json(new { data = _workContainer.Category.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDB = _workContainer.Category.Get(id);
            if (objFromDB == null)
            {
                return Json(new { success = false,Message = "Error borrando categoría" });
            }

            _workContainer.Category.Remove(objFromDB);
            _workContainer.Save();
            return Json(new { success = true,Message = "Categoría borrada correctamente" });
        }

        #endregion
    }
}
