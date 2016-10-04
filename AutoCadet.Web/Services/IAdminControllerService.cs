using System.Collections.Generic;
using System.Threading.Tasks;
using AutoCadet.Models;

namespace AutoCadet.Services
{
    public interface IAdminControllerService
    {
        Task<IList<InstructorGridItemBaseViewModel>> GetAllUsersViewModelsAsync();
    }
}