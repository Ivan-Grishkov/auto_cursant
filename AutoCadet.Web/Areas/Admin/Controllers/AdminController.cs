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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IList<InstructorViewModel> instructorsVs = await _adminControllerService.GetAllInstructorsViewModelsAsync().ConfigureAwait(true);
            return View(instructorsVs);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> ManageInstructor(InstructorManagePageViewModel instructorVm, HttpPostedFileBase itemFile, HttpPostedFileBase detailsFile, HttpPostedFileBase vehicleFile)
        {
            if (!ModelState.IsValid || instructorVm == null)
            {
                return View(instructorVm);
            }

            instructorVm.Instructor.ThumbnailImage = GetFileContent(itemFile);
            instructorVm.InstructorDetails.DetailsImage = GetFileContent(detailsFile);
            instructorVm.InstructorDetails.VehicleImage = GetFileContent(vehicleFile);

            await _adminControllerService.SaveInstructorAsync(instructorVm).ConfigureAwait(true);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> VideoLessons()
        {
            IList<VideoLessonViewModel> instructorsVs = await _adminControllerService.GetAllVideoLessonViewModelsAsync().ConfigureAwait(true);
            return View(instructorsVs);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> VideoLessons(IList<VideoLessonViewModel> videoLessonsGridItemViewModels)
        {
            if (ModelState.IsValid && videoLessonsGridItemViewModels != null)
            {
                await _adminControllerService.SaveVideoLessonsAttributesAsync(videoLessonsGridItemViewModels).ConfigureAwait(true);
                ViewBag.IsSuccess = true;
            }
            return View(videoLessonsGridItemViewModels);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> ManageVideoLesson(int? lessonId)
        {
            VideoLessonsManagePageViewModel videoLessonVm = null;
            if (lessonId.HasValue)
            {
                videoLessonVm = await _adminControllerService.GetVideoLessonViewModelAsync(lessonId.Value).ConfigureAwait(true);
            }
            if (videoLessonVm == null)
            {
                videoLessonVm = new VideoLessonsManagePageViewModel();
            }

            videoLessonVm.VideoLessonViewModel = videoLessonVm.VideoLessonViewModel ?? new VideoLessonViewModel();
            videoLessonVm.MetadataInfo = videoLessonVm.MetadataInfo ?? new MetadataInfoViewModel();

            return View(videoLessonVm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> ManageVideoLesson(VideoLessonsManagePageViewModel lessonVm, HttpPostedFileBase itemFile)
        {
            if (!ModelState.IsValid || lessonVm == null)
            {
                return View(lessonVm);
            }
            lessonVm.VideoLessonViewModel.ThumbnailImageFile = GetFileContent(itemFile);

            await _adminControllerService.SaveVideoLessonAsync(lessonVm).ConfigureAwait(true);
            return RedirectToAction("VideoLessons");
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Comments()
        {
            IList<CommentViewModel> commentViewModels = await _adminControllerService.GetAllCommentViewModelsAsync().ConfigureAwait(true);
            return View(commentViewModels);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> CallMeRequests()
        {
            IList<CallMeViewModel> callMeViewModels = await _adminControllerService.GetAllCallMeViewModelsAsync().ConfigureAwait(true);
            return View(callMeViewModels);
        }

        [Authorize(Roles = "Admin")]
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
