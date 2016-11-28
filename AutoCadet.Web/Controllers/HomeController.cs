using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoCadet.Attributes;
using AutoCadet.Models;
using AutoCadet.Services;

namespace AutoCadet.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeControllerService _homeControllerService;
        private readonly ISiteMapService _siteMapService;

        public HomeController(IHomeControllerService homeControllerService, ISiteMapService siteMapService)
        {
            _homeControllerService = homeControllerService;
            _siteMapService = siteMapService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            HomePageViewModel pageViewModel = await _homeControllerService
                .GetHomePageViewModelAsync()
                .ConfigureAwait(true);
            return View(pageViewModel);
        }

        [HttpGet]
        [RequireRequestValue("")]
        public async Task<ActionResult> Instructors()
        {
            var instructorsVm = await _homeControllerService.GetInstructorsPageViewModelAsync().ConfigureAwait(true);
            return View("InstructorsList", instructorsVm);
        }

        [HttpGet]
        [RequireRequestValue("prettyUrl")]
        public async Task<ActionResult> Instructors(string prettyUrl)
        {
            InstructorDetailsPageViewModel vm = await _homeControllerService
                .GetInstructorDetailsViewModelAsync(prettyUrl)
                .ConfigureAwait(true) ?? new InstructorDetailsPageViewModel();
            if (vm.InstructorDetails == null)
            {
                vm.InstructorDetails = new InstructorDetailsViewModel();
            }
            if (vm.Instructor == null)
            {
                vm.Instructor = new InstructorViewModel();
            }

            return View(vm);
        }

        [HttpGet]
        [RequireRequestValue("")]
        public async Task<ActionResult> Video()
        {
            var lessonsPageViewModel = await _homeControllerService.GetVideoPageViewModelAsync().ConfigureAwait(true);
            return View("VideoList", lessonsPageViewModel);
        }

        [HttpGet]
        [RequireRequestValue("prettyUrl")]
        public async Task<ActionResult> Video(string prettyUrl)
        {
            VideoViewModel vm = await _homeControllerService
                .GetVideoViewModelAsync(prettyUrl)
                .ConfigureAwait(true) ?? new VideoViewModel();

            return View(vm);
        }

        [HttpGet]
        [RequireRequestValue("")]
        public async Task<ActionResult> Obuchenie()
        {
            var pageVm = await _homeControllerService.GetObucheniePageViewModelAsync().ConfigureAwait(true);
            return View("ObuchenieList", pageVm);
        }

        [HttpGet]
        [RequireRequestValue("prettyUrl")]
        public async Task<ActionResult> Obuchenie(string prettyUrl)
        {
            ObuchenieViewModel vm = await _homeControllerService
                .GetObuchenieViewModelAsync(prettyUrl)
                .ConfigureAwait(true) ?? new ObuchenieViewModel();

            return View(vm);
        }

        [HttpGet]
        [RequireRequestValue("")]
        public async Task<ActionResult> Blog()
        {
            var pageVm = await _homeControllerService.GetBlogListPageViewModelAsync().ConfigureAwait(true);
            return View("BlogList", pageVm);
        }

        [HttpGet]
        [RequireRequestValue("prettyUrl")]
        public async Task<ActionResult> Blog(string prettyUrl)
        {
            BlogViewModel vm = await _homeControllerService
                .GetBlogViewModelAsync(prettyUrl)
                .ConfigureAwait(true) ?? new BlogViewModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(CommentViewModel comment)
        {
            if (!ModelState.IsValid || comment == null)
            {
                return new JsonResult {Data = new {error = true}};
            }

            bool isSuccess = await _homeControllerService.SaveCommentAsync(comment).ConfigureAwait(true);
            if (isSuccess)
            {
                return new JsonResult {Data = new {success = true}};
            }

            return new JsonResult {Data = new {isSame = true}};
        }

        [HttpPost]
        public async Task<ActionResult> CallMe(CallMeViewModel callMe)
        {
            if (!ModelState.IsValid || callMe == null)
            {
                return new JsonResult {Data = new {error = true}};
            }

            var isSuccess = await _homeControllerService.ProcessCallMeAsync(callMe).ConfigureAwait(true);
            if (isSuccess)
            {
                return new JsonResult {Data = new {success = true}};
            }

            return new JsonResult {Data = new {isSame = true}};
        }

        [HttpGet]
        public async Task<ActionResult> SiteMap()
        {
            var sitemap = await _siteMapService.GenetateSiteMapAsync().ConfigureAwait(true);
            return Content(sitemap, "xml", Encoding.UTF8);
        }
    }
}
