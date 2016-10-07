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

        public async Task<ActionResult> Index()
        {
            IList<InstructorGridItemBaseViewModel> instructorsVs = await _adminControllerService.GetAllUsersViewModelsAsync().ConfigureAwait(true);
            return View(instructorsVs);
        }
    }
}
