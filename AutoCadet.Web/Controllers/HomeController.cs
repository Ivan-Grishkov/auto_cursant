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
            HomePageViewModel pageViewModel = await _homeControllerService
                .GetHomePageViewModelAsync()
                .ConfigureAwait(true);
            return View(pageViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> InstructorDetails(string instructorUrl)
        {
            InstructorDetailsPageViewModel vm = await _homeControllerService.GetInstructorDetailsViewModelAsync(instructorUrl).ConfigureAwait(true) ?? new InstructorDetailsPageViewModel();
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
    }
}
