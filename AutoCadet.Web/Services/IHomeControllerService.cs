using System.Threading.Tasks;
using AutoCadet.Models;

namespace AutoCadet.Services
{
    public interface IHomeControllerService
    {
        Task<HomePageViewModel> GetHomePageViewModelAsync();
        Task<InstrucrorManageViewModel> GetInstructorViewModelAsync(string instructorUrl);

    }
}