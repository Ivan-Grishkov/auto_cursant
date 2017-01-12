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
        public ActionResult M()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Mobile(string prettyUrl)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Akciya()
        {
            ShareEventViewModel pageViewModel = await _homeControllerService
                .GetShareEventViewModelAsync()
                .ConfigureAwait(true) ?? new ShareEventViewModel();
            return View(pageViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> GetAkciya()
        {
            ShareEventViewModel pageViewModel = await _homeControllerService
                .GetShareEventViewModelAsync()
                .ConfigureAwait(true);
            if (pageViewModel == null || !pageViewModel.IsActive)
            {
                return null;
            }

            return PartialView("_GetAkciya", pageViewModel);
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

        #region API

        [HttpGet]
        public async Task<ActionResult> GetObuchenieItems()
        {
            var pageVm = await _homeControllerService.GetObucheniePageViewModelAsync().ConfigureAwait(true);
            return PartialView("_IndexObuchenieList", pageVm.ItemsViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> GetVideoItems()
        {
            var pageVm = await _homeControllerService.GetVideoPageViewModelAsync().ConfigureAwait(true);
            return PartialView("_IndexVideoList", pageVm.VideoViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> GetBlogItems()
        {
            var pageVm = await _homeControllerService.GetBlogListPageViewModelAsync().ConfigureAwait(true);
            return PartialView("_IndexBlogList", pageVm.ItemsViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> GetCommentsItems()
        {
            var commentsVms = await _homeControllerService.GetCommentsForListAsync().ConfigureAwait(true);
            return PartialView("_IndexCommentsList", commentsVms);
        }

        #endregion API
    }
}
