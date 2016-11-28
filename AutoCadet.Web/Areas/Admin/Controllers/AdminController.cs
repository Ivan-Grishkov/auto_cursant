using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoCadet.Areas.Admin.Models;
using AutoCadet.Models;
using AutoCadet.Services;

namespace AutoCadet.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminControllerService _adminControllerService;
        // GET: Admin
        public AdminController(IAdminControllerService adminControllerService)
        {
            _adminControllerService = adminControllerService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IList<InstructorViewModel> instructorsVs = await _adminControllerService.GetAllInstructorsViewModelsAsync().ConfigureAwait(true);
            return View(instructorsVs?.OrderByDescending(x => x.SortingNumber).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Index(IList<InstructorViewModel> instructorGridItemViewModels)
        {
            if (ModelState.IsValid && instructorGridItemViewModels != null)
            {
                await _adminControllerService.SaveInstructorsAttributesAsync(instructorGridItemViewModels).ConfigureAwait(true);
                ViewBag.IsSuccess = true;
            }
            return View(instructorGridItemViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> ManageInstructor(int? instructorId)
        {
            InstructorManagePageViewModel instructorVm = null;
            if (instructorId.HasValue)
            {
                instructorVm = await _adminControllerService.GetInstructorViewModelAsync(instructorId.Value).ConfigureAwait(true);
            }
            if (instructorVm == null)
            {
                instructorVm = new InstructorManagePageViewModel();
            }

            instructorVm.Instructor = instructorVm.Instructor ?? new InstructorViewModel();
            instructorVm.MetadataInfo = instructorVm.MetadataInfo ?? new MetadataInfoViewModel();
            instructorVm.InstructorDetails = instructorVm.InstructorDetails ?? new InstructorDetailsViewModel();

            return View(instructorVm);
        }

        [HttpPost]
        public async Task<ActionResult> ManageInstructor(InstructorManagePageViewModel instructorVm, HttpPostedFileBase itemFile)
        {
            if (!ModelState.IsValid || instructorVm == null)
            {
                return View(instructorVm);
            }

            instructorVm.Instructor.ThumbnailImage = GetFileContent(itemFile);

            await _adminControllerService.SaveInstructorAsync(instructorVm).ConfigureAwait(true);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> Video()
        {
            IList<VideoViewModel> instructorsVs = await _adminControllerService.GetAllVideoViewModelsAsync().ConfigureAwait(true);
            return View(instructorsVs?.OrderByDescending(x => x.SortingNumber).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Video(IList<VideoViewModel> VideoGridItemViewModels)
        {
            if (ModelState.IsValid && VideoGridItemViewModels != null)
            {
                await _adminControllerService.SaveVideoAttributesAsync(VideoGridItemViewModels).ConfigureAwait(true);
                ViewBag.IsSuccess = true;
            }
            return View(VideoGridItemViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> ManageVideo(int? lessonId)
        {
            VideoManagePageViewModel videoVm = null;
            if (lessonId.HasValue)
            {
                videoVm = await _adminControllerService.GetVideoViewModelAsync(lessonId.Value).ConfigureAwait(true);
            }
            if (videoVm == null)
            {
                videoVm = new VideoManagePageViewModel();
            }

            videoVm.VideoViewModel = videoVm.VideoViewModel ?? new VideoViewModel();
            videoVm.MetadataInfo = videoVm.MetadataInfo ?? new MetadataInfoViewModel();

            return View(videoVm);
        }

        [HttpPost]
        public async Task<ActionResult> ManageVideo(VideoManagePageViewModel lessonVm, HttpPostedFileBase itemFile)
        {
            if (!ModelState.IsValid || lessonVm == null)
            {
                return View(lessonVm);
            }
            lessonVm.VideoViewModel.ThumbnailImageFile = GetFileContent(itemFile);

            await _adminControllerService.SaveVideoAsync(lessonVm).ConfigureAwait(true);
            return RedirectToAction("Video");
        }


        [HttpGet]
        public async Task<ActionResult> Obuchenie()
        {
            IList<ObuchenieViewModel> items = await _adminControllerService.GetAllObuchenieViewModelsAsync().ConfigureAwait(true);
            return View(items?.OrderByDescending(x => x.SortingNumber).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Obuchenie(IList<ObuchenieViewModel> ObuchenieGridItemViewModels)
        {
            if (ModelState.IsValid && ObuchenieGridItemViewModels != null)
            {
                await _adminControllerService.SaveObuchenieAttributesAsync(ObuchenieGridItemViewModels).ConfigureAwait(true);
                ViewBag.IsSuccess = true;
            }
            return View(ObuchenieGridItemViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> ManageObuchenie(int? obuchenieId)
        {
            ObuchenieManagePageViewModel videoVm = null;
            if (obuchenieId.HasValue)
            {
                videoVm = await _adminControllerService.GetObuchenieViewModelAsync(obuchenieId.Value).ConfigureAwait(true);
            }
            if (videoVm == null)
            {
                videoVm = new ObuchenieManagePageViewModel();
            }

            videoVm.ObuchenieViewModel = videoVm.ObuchenieViewModel ?? new ObuchenieViewModel();
            videoVm.MetadataInfo = videoVm.MetadataInfo ?? new MetadataInfoViewModel();

            return View(videoVm);
        }

        [HttpPost]
        public async Task<ActionResult> ManageObuchenie(ObuchenieManagePageViewModel obuchenieVm)
        {
            if (!ModelState.IsValid || obuchenieVm == null)
            {
                return View(obuchenieVm);
            }

            await _adminControllerService.SaveObuchenieAsync(obuchenieVm).ConfigureAwait(true);
            return RedirectToAction("Obuchenie");
        }


        [HttpGet]
        public async Task<ActionResult> Blog()
        {
            IList<BlogViewModel> items = await _adminControllerService.GetAllBlogsViewModelsAsync().ConfigureAwait(true);
            return View(items?.OrderByDescending(x => x.SortingNumber).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Blog(IList<BlogViewModel> blogGridItemViewModels)
        {
            if (ModelState.IsValid && blogGridItemViewModels != null)
            {
                await _adminControllerService.SaveBlogsAttributesAsync(blogGridItemViewModels).ConfigureAwait(true);
                ViewBag.IsSuccess = true;
            }
            return View(blogGridItemViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> ManageBlog(int? blogId)
        {
            BlogManagePageViewModel videoVm = null;
            if (blogId.HasValue)
            {
                videoVm = await _adminControllerService.GetBlogViewModelAsync(blogId.Value).ConfigureAwait(true);
            }
            if (videoVm == null)
            {
                videoVm = new BlogManagePageViewModel();
            }

            videoVm.BlogViewModel = videoVm.BlogViewModel ?? new BlogViewModel();
            videoVm.MetadataInfo = videoVm.MetadataInfo ?? new MetadataInfoViewModel();

            return View(videoVm);
        }

        [HttpPost]
        public async Task<ActionResult> ManageBlog(BlogManagePageViewModel vm, HttpPostedFileBase itemFile)
        {
            if (!ModelState.IsValid || vm == null)
            {
                return View(vm);
            }
            vm.BlogViewModel.ThumbnailImageFile = GetFileContent(itemFile);

            await _adminControllerService.SaveBlogAsync(vm).ConfigureAwait(true);
            return RedirectToAction("Blog");
        }

        private byte[] GetFileContent(HttpPostedFileBase file)
        {
            byte[] uploadedFile = null;
            if (file != null && file.ContentLength > 0)
            {
                uploadedFile = new byte[file.InputStream.Length];
                file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
            }
            return uploadedFile;
        }

        [HttpGet]
        public async Task<ActionResult> Comments()
        {
            IList<CommentViewModel> commentViewModels = await _adminControllerService.GetAllCommentViewModelsAsync().ConfigureAwait(true);
            return View(commentViewModels?.OrderByDescending(x => x.CreatedDate).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Comments(IList<CommentViewModel> commentViewModels)
        {
            if (ModelState.IsValid && commentViewModels != null)
            {
                await _adminControllerService.SaveCommentsAttributesAsync(commentViewModels).ConfigureAwait(true);
                ViewBag.IsSuccess = true;
            }
            return View(commentViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> CallMeRequests()
        {
            IList<CallMeViewModel> callMeViewModels = await _adminControllerService.GetAllCallMeViewModelsAsync().ConfigureAwait(true);
            return View(callMeViewModels?.OrderBy(x => x.IsHandled).ThenByDescending(x => x.CreatedDate).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> CallMeRequests(IList<CallMeViewModel> callMeViewModels)
        {
            if (ModelState.IsValid && callMeViewModels != null && callMeViewModels.Any())
            {
                await _adminControllerService.SaveCallMeViewModelsAsync(callMeViewModels).ConfigureAwait(true);
                ViewBag.IsSuccess = true;
            }
            return View(callMeViewModels);
        }
    }
}
