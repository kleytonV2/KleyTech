using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace KleyTech.Areas.Admin.Controllers
{
    [Authorize(Roles = CNT.Admin)]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public UsersController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //return View(_workContainer.User.GetAll());
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var actualUser = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_workContainer.User.GetAll(u=>u.Id != actualUser.Value));
        }
        
        [HttpGet]
        public IActionResult Lock(string id)
        {
            if (id == null) {
                return NotFound();
            }

            _workContainer.User.LockUser(id);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult Unlock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _workContainer.User.UnlockUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
