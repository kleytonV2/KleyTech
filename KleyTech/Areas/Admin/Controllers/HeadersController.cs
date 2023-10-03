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
    public class HeadersController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HeadersController(IWorkContainer workContainer, IWebHostEnvironment webHostEnvironment)
        {
            _workContainer = workContainer;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //return View();
            return RedirectToAction("Edit", new { Id = 1 }); ;
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Header header)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        string MainRoute = _webHostEnvironment.WebRootPath;
        //        var files = HttpContext.Request.Form.Files;
        //        var headerFromDB = _workContainer.Header.Get(header.Id);

        //        if (files.Count > 0)
        //        {
        //            string fileName = Guid.NewGuid().ToString();
        //            var extension = Path.GetExtension(files[0].FileName);
        //            var uploads = Path.Combine(MainRoute, @"images\headers");

        //            using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
        //            {
        //                files[0].CopyTo(fileStream);
        //            }

        //            header.LogoURL = @"\images\headers\" + fileName + extension;

        //        }

        //        _workContainer.Header.Add(header);
        //        _workContainer.Save();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(header);

        //}

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Header header = _workContainer.Header.Get(id);
            if (header == null)
            {
                //return NotFound();
                header = new Header { Name = "Main header" };
                _workContainer.Header.Add(header);
                _workContainer.Save();
            }

            return View(header);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Header header)
        {

            if (ModelState.IsValid)
            {
                string MainRoute = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var headerFromDB = _workContainer.Header.Get(header.Id);

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var uploads = Path.Combine(MainRoute, @"images\headers");

                    if (headerFromDB.LogoURL != null && headerFromDB.LogoURL.Length > 0)
                    {
                        var imageRoute = Path.Combine(MainRoute, headerFromDB.LogoURL.TrimStart('\\'));
                        if (System.IO.File.Exists(imageRoute))
                        {
                            System.IO.File.Delete(imageRoute);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    header.LogoURL = @"\images\headers\" + fileName + extension;

                }
                else
                {
                    header.LogoURL = headerFromDB.LogoURL;
                }

                _workContainer.Header.Update(header);
                _workContainer.Save();

                return RedirectToAction(nameof(Areas.User.Controllers.HomeController.Index));
            }

            return View(header);

        }

        #region Call to API

        [HttpGet]
        public IActionResult GetAll()
        {
            //Opcion1
            return Json(new { data = _workContainer.Header.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var headerFromDb = _workContainer.Header.Get(id);
            string imageMainRoute = _webHostEnvironment.WebRootPath;
            if (headerFromDb.LogoURL != null)
            {
                var imageRoute = Path.Combine(imageMainRoute, headerFromDb.LogoURL.TrimStart('\\'));
                if (System.IO.File.Exists(imageRoute))
                {
                    System.IO.File.Delete(imageRoute);
                }
            }
            if (headerFromDb == null)
            {
                return Json(new { success = false, Message = "Error erasing header" });
            }

            _workContainer.Header.Remove(headerFromDb);
            _workContainer.Save();
            return Json(new { success = true, Message = "Header erased correctly" });
        }

        #endregion
    }
}
