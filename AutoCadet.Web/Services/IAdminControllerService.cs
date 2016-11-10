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

        Task<IList<TrainingViewModel>> GetAllTrainingsViewModelsAsync();
        Task<TrainingManagePageViewModel> GetTrainingViewModelAsync(int serviceId);
        Task SaveTrainingsAttributesAsync(IList<TrainingViewModel> trainingGridItemVms);
        Task SaveTrainingAsync(TrainingManagePageViewModel trainingVm);

        Task<IList<CommentViewModel>> GetAllCommentViewModelsAsync();
        Task SaveCommentsAttributesAsync(IList<CommentViewModel> commentViewModels);
        Task<IList<CallMeViewModel>> GetAllCallMeViewModelsAsync();
        Task SaveCallMeViewModelsAsync(IList<CallMeViewModel> callMeViewModels);
    }
}