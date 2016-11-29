using System.Collections.Generic;
using System.Threading.Tasks;
using AutoCadet.Areas.Admin.Models;
using AutoCadet.Models;

namespace AutoCadet.Services
{
    public interface IAdminControllerService
    {
        Task<IList<InstructorViewModel>> GetAllInstructorsViewModelsAsync();
        Task<InstructorManagePageViewModel> GetInstructorViewModelAsync(int instructorId);
        Task SaveInstructorAsync(InstructorManagePageViewModel pageVm);
        Task SaveInstructorsAttributesAsync(IList<InstructorViewModel> instructorGridItemViewModels);

        Task<IList<VideoViewModel>> GetAllVideoViewModelsAsync();
        Task<VideoManagePageViewModel> GetVideoViewModelAsync(int lessonId);
        Task SaveVideoAttributesAsync(IList<VideoViewModel> VideoGridItemViewModels);
        Task SaveVideoAsync(VideoManagePageViewModel lessonVm);

        Task<IList<ObuchenieViewModel>> GetAllObuchenieViewModelsAsync();
        Task<ObuchenieManagePageViewModel> GetObuchenieViewModelAsync(int obuchenieId);
        Task SaveObuchenieAttributesAsync(IList<ObuchenieViewModel> obuchenieGridItemVms);
        Task SaveObuchenieAsync(ObuchenieManagePageViewModel obuchenieVm);

        Task<IList<BlogViewModel>> GetAllBlogsViewModelsAsync();
        Task<BlogManagePageViewModel> GetBlogViewModelAsync(int blogId);
        Task SaveBlogsAttributesAsync(IList<BlogViewModel> blogGridItemVms);
        Task SaveBlogAsync(BlogManagePageViewModel blogVm);

        Task<IList<CommentViewModel>> GetAllCommentViewModelsAsync();
        Task SaveCommentsAttributesAsync(IList<CommentViewModel> commentViewModels);
        Task<IList<CallMeViewModel>> GetAllCallMeViewModelsAsync();
        Task SaveCallMeViewModelsAsync(IList<CallMeViewModel> callMeViewModels);

        Task<ShareEventViewModel> GetShareEventViewModelAsync();
        Task SaveShareEventViewModelAsync(ShareEventViewModel shareEventViewModel);
    }
}