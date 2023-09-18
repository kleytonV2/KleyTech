using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace KleyTech.Areas.Admin.Controllers
{
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
            return View(_workContainer.User.GetAll());
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
