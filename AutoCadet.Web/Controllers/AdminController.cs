using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IList<InstructorViewModel> instructorsVs = await _adminControllerService.GetAllUsersViewModelsAsync().ConfigureAwait(true);
            return View(instructorsVs);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Index(IList<InstructorViewModel> instructorGridItemViewModels)
        {
            if (ModelState.IsValid)
            {
                await _adminControllerService.SaveInstructorsAttributesAsync(instructorGridItemViewModels).ConfigureAwait(true);
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
            if (!ModelState.IsValid)
            {
                return View(instructorVm);
            }

            instructorVm.Instructor.ThumbnailImage = GetFileContent(itemFile);
            instructorVm.InstructorDetails.DetailsImage = GetFileContent(detailsFile);
            instructorVm.InstructorDetails.VehicleImage = GetFileContent(vehicleFile);

            await _adminControllerService.SaveInstructorAsync(instructorVm).ConfigureAwait(true);
            return RedirectToAction("Index");
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
            if (ModelState.IsValid)
            {
                await _adminControllerService.SaveCommentsAttributesAsync(commentViewModels).ConfigureAwait(true);
            }
            return View(commentViewModels);
        }
    }
}
