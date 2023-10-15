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
    public class BlocksController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlocksController(IWorkContainer workContainer, IWebHostEnvironment webHostEnvironment)
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
        public IActionResult Create(Block block)
        {

            if (ModelState.IsValid)
            {
                _workContainer.Block.Add(block);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(block);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Block block = _workContainer.Block.Get(id);

            if (block == null)
            {
                return NotFound();
            }

            return View(block);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Block Block)
        {

            if (ModelState.IsValid)
            {
                _workContainer.Block.Update(Block);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(Block);

        }

        #region Call to API

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workContainer.Block.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var BlockFromDb = _workContainer.Block.Get(id);
            if (BlockFromDb == null)
            {
                return Json(new { success = false, Message = "Error erasing Block" });
            }

            _workContainer.Block.Remove(BlockFromDb);
            _workContainer.Save();
            return Json(new { success = true, Message = "Block erased correctly" });
        }

        #endregion
    }
}
