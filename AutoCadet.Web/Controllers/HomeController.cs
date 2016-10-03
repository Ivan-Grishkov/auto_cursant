using System.Threading.Tasks;
using System.Web.Mvc;
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

        public async Task<ActionResult> Index()
        {
            var pageViewModel = await _homeControllerService.GetHomePageViewModelAsync();
            return View(pageViewModel);
        }
    }
}
