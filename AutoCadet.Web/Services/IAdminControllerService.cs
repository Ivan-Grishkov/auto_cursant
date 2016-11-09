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

        Task<IList<VideoLessonViewModel>> GetAllVideoLessonViewModelsAsync();
        Task<VideoLessonsManagePageViewModel> GetVideoLessonViewModelAsync(int lessonId);
        Task SaveVideoLessonsAttributesAsync(IList<VideoLessonViewModel> videoLessonsGridItemViewModels);
        Task SaveVideoLessonAsync(VideoLessonsManagePageViewModel lessonVm);

        Task<IList<ServiceViewModel>> GetAllServicesViewModelsAsync();
        Task<ServicesManagePageViewModel> GetServiceViewModelAsync(int serviceId);
        Task SaveServicesAttributesAsync(IList<ServiceViewModel> serviceGridItemVms);
        Task SaveServiceAsync(ServicesManagePageViewModel serviceVm);

        Task<IList<CommentViewModel>> GetAllCommentViewModelsAsync();
        Task SaveCommentsAttributesAsync(IList<CommentViewModel> commentViewModels);
        Task<IList<CallMeViewModel>> GetAllCallMeViewModelsAsync();
        Task SaveCallMeViewModelsAsync(IList<CallMeViewModel> callMeViewModels);
    }
}