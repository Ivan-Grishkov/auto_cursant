using System.Threading.Tasks;
using AutoCadet.Models;

namespace AutoCadet.Services
{
    public interface IHomeControllerService
    {
        Task<HomePageViewModel> GetHomePageViewModelAsync();
        Task<InstructorDetailsPageViewModel> GetInstructorDetailsViewModelAsync(string instructorUrl);
        Task<bool> SaveCommentAsync(CommentViewModel commentVm);
        Task<bool> ProcessCallMeAsync(CallMeViewModel callMe);
    }
}