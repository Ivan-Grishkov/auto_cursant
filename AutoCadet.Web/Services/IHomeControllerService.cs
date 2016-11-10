﻿using System.Threading.Tasks;
using AutoCadet.Models;

namespace AutoCadet.Services
{
    public interface IHomeControllerService
    {
        Task<HomePageViewModel> GetHomePageViewModelAsync();
        Task<InstructorsListPageViewModel> GetInstructorsPageViewModelAsync();
        Task<VideoLessonsPageViewModel> GetVideoLessonsPageViewModelAsync();
        Task<VideoLessonViewModel> GetVideoLessonViewModelAsync(string prettyUrl);

        Task<TrainingListPageViewModel> GetTrainingPageViewModelAsync();
        Task<TrainingViewModel> GetTrainingViewModelAsync(string prettyUrl);

        Task<InstructorDetailsPageViewModel> GetInstructorDetailsViewModelAsync(string instructorUrl);
        Task<bool> SaveCommentAsync(CommentViewModel commentVm);
        Task<bool> ProcessCallMeAsync(CallMeViewModel callMe);
    }
}