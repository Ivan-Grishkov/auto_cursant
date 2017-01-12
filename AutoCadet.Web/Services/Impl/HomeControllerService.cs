using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;
using AutoCadet.Domain;
using AutoCadet.Domain.Entities;
using AutoCadet.Models;
using AutoCadet.Notificators;
using AutoMapper;

namespace AutoCadet.Services.Impl
{
    public class HomeControllerService: IHomeControllerService
    {
        private readonly AutoCadetDbContext _autoCadetDbContext;
        private readonly IMapper _mapper;
        private readonly ICallMeNotificator _callMeNotificator;

        public HomeControllerService(AutoCadetDbContext autoCadetDbContext, IMapper mapper, ICallMeNotificator callMeNotificator)
        {
            _autoCadetDbContext = autoCadetDbContext;
            _mapper = mapper;
            _callMeNotificator = callMeNotificator;
        }

        public async Task<HomePageViewModel> GetHomePageViewModelAsync()
        {
            var instructors = await _autoCadetDbContext.Instructors
                .Where(x => x.IsActive)
                .Select(x => new {Instr = x, AvS = x.Comments.Where(c => c.IsActive).Average(c => (double?)c.Score)})
                .ToListAsync()
                .ConfigureAwait(false);

            var vms = instructors.Select(x =>
            {
                var vm = new InstructorViewModel
                {
                    AverageScore =  x.AvS
                };
                _mapper.Map(x.Instr, vm);
                return vm;
            }).ToList();

            Random rnd = new Random();
            var randomInstructors = vms.Where(x => x.IsPrimary).OrderBy(x => rnd.Next()).ToList();
            randomInstructors.AddRange(vms.Where(x => !x.IsPrimary).OrderByDescending(x => x.SortingNumber));

            return new HomePageViewModel
            {
                InstructorGridItems = randomInstructors
            };
        }

        public async Task<ShareEventViewModel> GetShareEventViewModelAsync()
        {
            var share = await _autoCadetDbContext.ShareEvents
                .Include(x => x.Metadata)
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return _mapper.Map<ShareEventViewModel>(share);
        }

        public async Task<InstructorsListPageViewModel> GetInstructorsPageViewModelAsync()
        {
            var instructors = await _autoCadetDbContext.Instructors
                .Where(x => x.IsActive)
                .Select(x => new { Instr = x, AvS = x.Comments.Where(c => c.IsActive).Average(c => (double?)c.Score) })
                .ToListAsync()
                .ConfigureAwait(false);

            var vms = instructors.Select(x =>
            {
                var vm = new InstructorViewModel
                {
                    AverageScore = x.AvS
                };
                _mapper.Map(x.Instr, vm);
                return vm;
            }).ToList();

            Random rnd = new Random();
            var randomInstructors = vms.Where(x => x.IsPrimary).OrderBy(x => rnd.Next()).ToList();
            randomInstructors.AddRange(vms.Where(x => !x.IsPrimary).OrderByDescending(x => x.SortingNumber));

            return new InstructorsListPageViewModel
            {
                InstructorGridItems = randomInstructors
            };
        }

        public async Task<VideoPageViewModel> GetVideoPageViewModelAsync()
        {
            var video = await _autoCadetDbContext.Video
                .Where(x => x.IsActive)
                .Include(x => x.Metadata)
                .ToListAsync()
                .ConfigureAwait(false);

            return new VideoPageViewModel
            {
                VideoViewModels = video?.Select(x => _mapper.Map<VideoViewModel>(x)).ToList()
            };
        }

        public async Task<VideoViewModel> GetVideoViewModelAsync(string prettyUrl)
        {
            var video = await _autoCadetDbContext.Video
                .Include(x => x.Metadata)
                .FirstOrDefaultAsync(x => x.UrlName.ToLower() == prettyUrl.ToLower())
                .ConfigureAwait(false);
            return _mapper.Map<VideoViewModel>(video);
        }

        public async Task<ObuchenieListPageViewModel> GetObucheniePageViewModelAsync()
        {
            var items = await _autoCadetDbContext.Obuchenie
                .Where(x => x.IsActive)
                .Include(x => x.Metadata)
                .ToListAsync()
                .ConfigureAwait(false);

            return new ObuchenieListPageViewModel
            {
                ItemsViewModels = items?.Select(x => _mapper.Map<ObuchenieViewModel>(x)).ToList()
            };
        }

        public async Task<ObuchenieViewModel> GetObuchenieViewModelAsync(string prettyUrl)
        {
            var items = await _autoCadetDbContext.Obuchenie
                .Include(x => x.Metadata)
                .FirstOrDefaultAsync(x => x.UrlName.ToLower() == prettyUrl.ToLower())
                .ConfigureAwait(false);
            return _mapper.Map<ObuchenieViewModel>(items);
        }

        public async Task<BlogListPageViewModel> GetBlogListPageViewModelAsync()
        {
            var items = await _autoCadetDbContext.Blogs
                .Where(x => x.IsActive)
                .Include(x => x.Metadata)
                .ToListAsync()
                .ConfigureAwait(false);

            return new BlogListPageViewModel
            {
                ItemsViewModels = items?.Select(x => _mapper.Map<BlogViewModel>(x)).ToList()
            };
        }

        public async Task<BlogViewModel> GetBlogViewModelAsync(string prettyUrl)
        {
            var items = await _autoCadetDbContext.Blogs
                .Include(x => x.Metadata)
                .FirstOrDefaultAsync(x => x.UrlName.ToLower() == prettyUrl.ToLower())
                .ConfigureAwait(false);
            return _mapper.Map<BlogViewModel>(items);
        }

        public async Task<InstructorDetailsPageViewModel> GetInstructorDetailsViewModelAsync(string instructorUrl)
        {
            var instructor = await _autoCadetDbContext.Instructors
                .Include(x => x.Comments)
                .Include(x => x.InstructorDetails)
                .Include(x => x.InstructorDetails.Metadata)
                .FirstOrDefaultAsync(x => x.UrlName.ToLower() == instructorUrl.ToLower())
                .ConfigureAwait(false);

            var vm = new InstructorDetailsPageViewModel
            {
                Instructor = _mapper.Map<InstructorViewModel>(instructor),
                InstructorDetails = _mapper.Map<InstructorDetailsViewModel>(instructor.InstructorDetails),
                Comments = instructor.Comments?.Where(x=>x.IsActive).Select(_mapper.Map<CommentViewModel>).ToList(),
                NewComment = new CommentViewModel()
            };

            if (vm.Instructor == null)
            {
                vm.Instructor = new InstructorViewModel();
            }
            if (vm.Comments.Any())
            {
                vm.Instructor.AverageScore = vm.Comments?.Average(x => x.Score);
            }
            return vm;
        }

        public async Task<bool> SaveCommentAsync(CommentViewModel commentVm)
        {
            var isSameExists = await _autoCadetDbContext.Comments
                .AnyAsync(x => x.Text == commentVm.Text && x.Name == commentVm.Name)
                .ConfigureAwait(false);
            if (!isSameExists)
            {
                var instructor = await _autoCadetDbContext.Instructors.FirstOrDefaultAsync(x => x.Id == commentVm.InstructorId).ConfigureAwait(false);
                if (instructor != null)
                {
                    var comment = _mapper.Map<Comment>(commentVm);
                    comment.CreatedDate = DateTime.Now;
                    comment.Instructor = instructor;
                    _autoCadetDbContext.Comments.Add(comment);
                    await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> ProcessCallMeAsync(CallMeViewModel callMeVm)
        {
            var isExists = await _autoCadetDbContext.CallMes
                .AnyAsync(
                    x => x.Phone == callMeVm.Phone 
                        && !x.IsHandled 
                        && SqlFunctions.DateDiff("hh", x.CreatedDate, DateTime.Now) < 1 )
                .ConfigureAwait(false);
            if (!isExists)
            {
                var callMe = _mapper.Map<CallMe>(callMeVm);
                Instructor instructor = null;
                if (callMeVm.InstructorId != null)
                {
                    instructor = await _autoCadetDbContext.Instructors.FirstOrDefaultAsync(x => x.Id == callMeVm.InstructorId).ConfigureAwait(false);
                    callMe.Instructor = instructor;
                }

                _autoCadetDbContext.CallMes.Add(callMe);
                await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);

                return _callMeNotificator.Notify(callMe.Phone, callMe.RequesterName, instructor);
            }
            return false;
        }

        public async Task<IList<CommentViewModel>> GetCommentsForListAsync()
        {
            var comments = await _autoCadetDbContext.Comments
                .Include(x => x.Instructor)
                .Where(x => x.IsActive)
                .Where(x => x.IsVisibleInList)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync()
                .ConfigureAwait(false);
            return comments.Select(x => _mapper.Map<CommentViewModel>(x)).ToList();
        }
    }
}