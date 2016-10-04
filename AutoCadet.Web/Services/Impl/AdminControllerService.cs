using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoCadet.Domain;
using AutoCadet.Models;
using AutoMapper;

namespace AutoCadet.Services.Impl
{
    public class AdminControllerService : IAdminControllerService
    {
        private readonly AutoCadetDbContext _autoCadetDbContext;
        private readonly IMapper _mapper;

        public AdminControllerService(AutoCadetDbContext autoCadetDbContext, IMapper mapper)
        {
            _autoCadetDbContext = autoCadetDbContext;
            _mapper = mapper;
        }

        public async Task<IList<InstructorGridItemBaseViewModel>> GetAllUsersViewModelsAsync()
        {
            var instructors = await _autoCadetDbContext.Instructors.ToListAsync();
            return instructors?.Select(i => _mapper.Map<InstructorGridItemBaseViewModel>(i)).ToList();
        }
    }
}