using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoCadet.Models;
using AutoCadet.Services;

namespace AutoCadet.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminControllerService _adminControllerService;
        // GET: Admin
        public AdminController(IAdminControllerService adminControllerService)
        {
            _adminControllerService = adminControllerService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IList<InstructorGridItemBaseViewModel> instructorsVs = await _adminControllerService.GetAllUsersViewModelsAsync().ConfigureAwait(true);
            return View(instructorsVs);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Manage(int? instructorId)
        {
            InstrucrorManageViewModel instructorVm;
            if (instructorId.HasValue)
            {
                instructorVm = await _adminControllerService.GetInstructorViewModelAsync(instructorId.Value).ConfigureAwait(true);
            }
            else
            {
                instructorVm = new InstrucrorManageViewModel();
            }
            return View(instructorVm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Manage(InstrucrorManageViewModel instructorVm)
        {
            if (!ModelState.IsValid)
            {
                return View(instructorVm);
            }

            await _adminControllerService.SaveInstructorAsync(instructorVm).ConfigureAwait(true);
            return RedirectToAction("Index");
        }
    }
}
