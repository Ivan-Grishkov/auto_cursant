using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoCadet.Domain;
using AutoCadet.Domain.Entities;
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
            var instructors = await _autoCadetDbContext.Instructors.Include(x => x.ThumbnailImage).ToListAsync().ConfigureAwait(false);
            var vms = instructors.Select(x => _mapper.Map<InstructorGridItemViewModel>(x)).ToList();
            return new HomePageViewModel
            {
                InstructorGridItemViewModels = vms
            };
        }

        public async Task<InstructorManageViewModel> GetInstructorViewModelAsync(string instructorUrl)
        {
            var instructor = await _autoCadetDbContext.Instructors
                .Include(x => x.ThumbnailImage)
                .FirstOrDefaultAsync(x => x.UrlName == instructorUrl)
                .ConfigureAwait(false);
            return _mapper.Map<InstructorManageViewModel>(instructor);
        }

        public async Task<bool> SaveCommentAsync(CommentNewViewModel commentVm)
        {
            var isSameExists = await _autoCadetDbContext.Comments.AnyAsync(x => x.Text == commentVm.Text && x.Name == commentVm.Name).ConfigureAwait(false);
            if (!isSameExists)
            {
                var comment = _mapper.Map<Comment>(commentVm);
                _autoCadetDbContext.Comments.Add(comment);
                _autoCadetDbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}