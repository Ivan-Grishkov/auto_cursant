using System.Threading.Tasks;
using System.Web.Helpers;
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
            HomePageViewModel pageViewModel = await _homeControllerService.GetHomePageViewModelAsync().ConfigureAwait(true);
            return View(pageViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(string instructorUrl)
        {
            var vm = new DetailsPageViewModel
            {
                Instructor = await _homeControllerService.GetInstructorViewModelAsync(instructorUrl).ConfigureAwait(true)
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(CommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult {Data = new {error = true}};
            }

            var isSuccess = await _homeControllerService.SaveCommentAsync(comment).ConfigureAwait(true);
            if (isSuccess)
            {
                return new JsonResult {Data = new {success = true}};
            }

            return new JsonResult {Data = new {isSame = true}};
        }
    }
}
