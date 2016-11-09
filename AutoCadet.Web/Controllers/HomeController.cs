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

        public HomeController(IHomeControllerService homeControllerService)
        {
            _homeControllerService = homeControllerService;
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

            ViewData["MetaInfo"] = vm.InstructorDetails.MetadataInfo?.Info;
            ViewData["MetaDescription"] = vm.InstructorDetails.MetadataInfo?.Description;
            ViewData["MetaKeywords"] = vm.InstructorDetails.MetadataInfo?.Keywords;
            ViewData["Title"] = vm.InstructorDetails.MetadataInfo?.Title;
            ViewData["H1"] = vm.InstructorDetails.MetadataInfo?.H1;
            return View(vm);
        }

        [HttpGet]
        [RequireRequestValue("")]
        public async Task<ActionResult> VideoLessons()
        {
            var lessonsPageViewModel = await _homeControllerService.GetVideoLessonsPageViewModelAsync().ConfigureAwait(true);
            return View("VideoLessonsList", lessonsPageViewModel);
        }

        [HttpGet]
        [RequireRequestValue("prettyUrl")]
        public async Task<ActionResult> VideoLessons(string prettyUrl)
        {
            VideoLessonViewModel vm = await _homeControllerService
                .GetVideoLessonViewModelAsync(prettyUrl)
                .ConfigureAwait(true) ?? new VideoLessonViewModel();

            ViewData["MetaInfo"] = vm.Metadata?.Info;
            ViewData["MetaDescription"] = vm.Metadata?.Description;
            ViewData["MetaKeywords"] = vm.Metadata?.Keywords;
            ViewData["Title"] = vm.Metadata?.Title;
            ViewData["H1"] = vm.Metadata?.H1;
            return View(vm);
        }

        [HttpGet]
        [RequireRequestValue("")]
        public async Task<ActionResult> Services()
        {
            var pageVm = await _homeControllerService.GetServicesPageViewModelAsync().ConfigureAwait(true);
            return View("ServicesList", pageVm);
        }

        [HttpGet]
        [RequireRequestValue("prettyUrl")]
        public async Task<ActionResult> Services(string prettyUrl)
        {
            ServiceViewModel vm = await _homeControllerService
                .GetServiceViewModelAsync(prettyUrl)
                .ConfigureAwait(true) ?? new ServiceViewModel();

            ViewData["MetaInfo"] = vm.Metadata?.Info;
            ViewData["MetaDescription"] = vm.Metadata?.Description;
            ViewData["MetaKeywords"] = vm.Metadata?.Keywords;
            ViewData["Title"] = vm.Metadata?.Title;
            ViewData["H1"] = vm.Metadata?.H1;
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(CommentViewModel comment)
        {
            if (!ModelState.IsValid || comment == null)
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
    }
}
