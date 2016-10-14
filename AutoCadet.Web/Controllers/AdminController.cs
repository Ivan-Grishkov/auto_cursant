using System.Collections.Generic;
using System.IO;
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
            IList<InstructorGridItemViewModel> instructorsVs = await _adminControllerService.GetAllUsersViewModelsAsync().ConfigureAwait(true);
            return View(instructorsVs);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Index(IList<InstructorGridItemViewModel> instructorGridItemViewModels)
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
            InstructorManageViewModel instructorVm;
            if (instructorId.HasValue)
            {
                instructorVm = await _adminControllerService.GetInstructorViewModelAsync(instructorId.Value).ConfigureAwait(true);
            }
            else
            {
                instructorVm = new InstructorManageViewModel();
            }
            return View(instructorVm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> ManageInstructor(InstructorManageViewModel instructorVm, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(instructorVm);
            }

            byte[] uploadedFile = null;
            if (file != null && file.ContentLength > 0)
            {
                uploadedFile = new byte[file.InputStream.Length];
                file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
            }
            instructorVm.UploadedThumbnail = uploadedFile;

            await _adminControllerService.SaveInstructorAsync(instructorVm).ConfigureAwait(true);
            return RedirectToAction("Index");
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
