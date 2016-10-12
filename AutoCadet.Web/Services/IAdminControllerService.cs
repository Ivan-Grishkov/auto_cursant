using System.Collections.Generic;
using System.Threading.Tasks;
using AutoCadet.Models;

namespace AutoCadet.Services
{
    public interface IAdminControllerService
    {
        Task<IList<InstructorGridItemViewModel>> GetAllUsersViewModelsAsync();
        Task<InstructorManageViewModel> GetInstructorViewModelAsync(int instructorId);
        Task SaveInstructorAsync(InstructorManageViewModel instructorVm);
        Task<IList<CommentViewModel>> GetAllCommentViewModelsAsync();
    }
}