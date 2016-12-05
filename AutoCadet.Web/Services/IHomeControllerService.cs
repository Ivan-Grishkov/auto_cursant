using System.Collections.Generic;
using System.Threading.Tasks;
using AutoCadet.Models;

namespace AutoCadet.Services
{
    public interface IHomeControllerService
    {
        Task<HomePageViewModel> GetHomePageViewModelAsync();
        Task<InstructorsListPageViewModel> GetInstructorsPageViewModelAsync();
        Task<VideoPageViewModel> GetVideoPageViewModelAsync();
        Task<VideoViewModel> GetVideoViewModelAsync(string prettyUrl);

        Task<ObuchenieListPageViewModel> GetObucheniePageViewModelAsync();
        Task<ObuchenieViewModel> GetObuchenieViewModelAsync(string prettyUrl);

        Task<BlogListPageViewModel> GetBlogListPageViewModelAsync();
        Task<BlogViewModel> GetBlogViewModelAsync(string prettyUrl);

        Task<InstructorDetailsPageViewModel> GetInstructorDetailsViewModelAsync(string instructorUrl);
        Task<bool> SaveCommentAsync(CommentViewModel commentVm);
        Task<bool> ProcessCallMeAsync(CallMeViewModel callMe);

        Task<IList<CommentViewModel>> GetCommentsForListAsync();
    }
}