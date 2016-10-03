using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoCadet.Domain;
using AutoCadet.Models;
using AutoMapper;

namespace AutoCadet.Services.Impl
{
    public class HomeControllerService: IHomeControllerService
    {
        private readonly AutoCadetDbContext _autoCadetDbContext;
        private readonly IMapper _mapper;

        public HomeControllerService(AutoCadetDbContext autoCadetDbContext, IMapper mapper)
        {
            _autoCadetDbContext = autoCadetDbContext;
            _mapper = mapper;
        }

        public async Task<HomePageViewModel> GetHomePageViewModelAsync()
        {
            using (_autoCadetDbContext)
            {
                var instructors = await _autoCadetDbContext.Instructors.ToListAsync();
                var vms = instructors.Select(x => _mapper.Map<InstructorGridItemViewModel>(x)).ToList();
                return new HomePageViewModel
                {
                    InstructorGridItemViewModels = vms
                };
            }
        }
    }
}