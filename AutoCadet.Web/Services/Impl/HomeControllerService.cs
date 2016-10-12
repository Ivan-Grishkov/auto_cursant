using System;
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
            var instructors = await _autoCadetDbContext.Instructors
                .Include(x => x.ThumbnailImage)
                .ToListAsync()
                .ConfigureAwait(false);
            var vms = instructors.Select(x => _mapper.Map<InstructorGridItemViewModel>(x)).ToList();
            var comments = await _autoCadetDbContext.Comments
                .Include(x => x.Instructor)
                .Where(x => x.IsActive)
                .Where(x => x.IsVisibleInList)
                .OrderByDescending(x => x.CreatedDate)
                .Take(6)
                .ToListAsync()
                .ConfigureAwait(false);
            var commentsVms = comments.Select(x => _mapper.Map<CommentViewModel>(x)).ToList();

            return new HomePageViewModel
            {
                InstructorGridItems = vms,
                Comments = commentsVms
            };
        }

        public async Task<InstructorDetailsPageViewModel> GetInstructorDetailsViewModelAsync(string instructorUrl)
        {
            var instructor = await _autoCadetDbContext.Instructors
                .Include(x => x.ThumbnailImage)
                .FirstOrDefaultAsync(x => x.UrlName == instructorUrl)
                .ConfigureAwait(false);

            var vm = new InstructorDetailsPageViewModel
            {
                Instructor = _mapper.Map<InstructorManageViewModel>(instructor)
            };
            return vm;
        }

        public async Task<bool> SaveCommentAsync(CommentViewModel commentVm)
        {
            var isSameExists = await _autoCadetDbContext.Comments.AnyAsync(x => x.Text == commentVm.Text && x.Name == commentVm.Name).ConfigureAwait(false);
            if (!isSameExists)
            {
                var instructor = await _autoCadetDbContext.Instructors.FirstOrDefaultAsync(x => x.Id == commentVm.InstructorId);
                if (instructor != null)
                {
                    var comment = _mapper.Map<Comment>(commentVm);
                    comment.CreatedDate = DateTime.Now;
                    comment.Instructor = instructor;
                    _autoCadetDbContext.Comments.Add(comment);
                    _autoCadetDbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}