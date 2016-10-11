using System.Threading.Tasks;
using System.Web.Mvc;
using AutoCadet.Models;
using AutoCadet.Services;

namespace AutoCadet.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeControllerService _homeControllerService;

        public HomeController(IHomeControllerService homeControllerService)
        {
            _homeControllerService = homeControllerService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var pageViewModel = await _homeControllerService.GetHomePageViewModelAsync().ConfigureAwait(true);
            return View(pageViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(string instructorUrl)
        {
            InstrucrorManageViewModel instrucrorManageViewModel = await _homeControllerService.GetInstructorViewModelAsync(instructorUrl).ConfigureAwait(true);
            return View(instrucrorManageViewModel);
        }
    }
}
