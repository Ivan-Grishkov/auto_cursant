using System.Threading.Tasks;
using AutoCadet.Models;

namespace AutoCadet.Services
{
    public interface IHomeControllerService
    {
        Task<HomePageViewModel> GetHomePageViewModelAsync();
        Task<InstructorManageViewModel> GetInstructorViewModelAsync(string instructorUrl);

    }
}