using System.Collections.Generic;
using System.Threading.Tasks;
using AutoCadet.Models;

namespace AutoCadet.Services
{
    public interface IAdminControllerService
    {
        Task<IList<InstructorGridItemViewModel>> GetAllUsersViewModelsAsync();
        Task<InstructorManagePageViewModel> GetInstructorViewModelAsync(int instructorId);
        Task SaveInstructorAsync(InstructorManagePageViewModel pageVm);
        Task<IList<CommentViewModel>> GetAllCommentViewModelsAsync();
        Task SaveInstructorsAttributesAsync(IList<InstructorGridItemViewModel> instructorGridItemViewModels);
        Task SaveCommentsAttributesAsync(IList<CommentViewModel> commentViewModels);
    }
}