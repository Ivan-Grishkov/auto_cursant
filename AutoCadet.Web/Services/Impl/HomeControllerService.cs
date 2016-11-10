using System;
using System.Data.Entity;
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
            var instructors =  await _autoCadetDbContext.Instructors
                .Include(x => x.ThumbnailImage)
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

            var comments = await _autoCadetDbContext.Comments
                .Include(x => x.Instructor)
                .Where(x => x.IsActive)
                .Where(x => x.IsVisibleInList)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync()
                .ConfigureAwait(false);
            var commentsVms = comments.Select(x => _mapper.Map<CommentViewModel>(x)).ToList();


            var videoLessons = await _autoCadetDbContext.VideoLessons
                .Include(x => x.ThumbnailImageFile)
                .Include(x => x.Metadata)
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync()
                .ConfigureAwait(false);
            var videoLessonVms = videoLessons.Select(x => _mapper.Map<VideoLessonViewModel>(x)).ToList();

            var services = await _autoCadetDbContext.Services
                .Include(x => x.ThumbnailImageFile)
                .Include(x => x.Metadata)
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync()
                .ConfigureAwait(false);
            var servicesVms = services.Select(x => _mapper.Map<TrainingViewModel>(x)).ToList();

            return new HomePageViewModel
            {
                InstructorGridItems = vms,
                Comments = commentsVms,
                VideosGridItems = videoLessonVms,
                Services = servicesVms
            };
        }

        public async Task<InstructorsListPageViewModel> GetInstructorsPageViewModelAsync()
        {
            var instructors = await _autoCadetDbContext.Instructors
                .Include(x => x.ThumbnailImage)
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

            return new InstructorsListPageViewModel
            {
                InstructorGridItems = vms
            };
        }

        public async Task<VideoLessonsPageViewModel> GetVideoLessonsPageViewModelAsync()
        {
            var videoLessons = await _autoCadetDbContext.VideoLessons
                .Include(x => x.Metadata)
                .Include(x => x.ThumbnailImageFile)
                .ToListAsync()
                .ConfigureAwait(false);

            return new VideoLessonsPageViewModel
            {
                VideoLessonViewModels = videoLessons?.Select(x => _mapper.Map<VideoLessonViewModel>(x)).ToList()
            };
        }

        public async Task<VideoLessonViewModel> GetVideoLessonViewModelAsync(string prettyUrl)
        {
            var videoLesson = await _autoCadetDbContext.VideoLessons
                .Include(x => x.Metadata)
                .Include(x => x.ThumbnailImageFile)
                .FirstOrDefaultAsync(x => x.UrlName.ToLower() == prettyUrl.ToLower())
                .ConfigureAwait(false);
            return _mapper.Map<VideoLessonViewModel>(videoLesson);
        }

        public async Task<TrainingListPageViewModel> GetTrainingPageViewModelAsync()
        {
            var items = await _autoCadetDbContext.Services
                .Include(x => x.Metadata)
                .Include(x => x.ThumbnailImageFile)
                .ToListAsync()
                .ConfigureAwait(false);

            return new TrainingListPageViewModel
            {
                ItemsViewModels = items?.Select(x => _mapper.Map<TrainingViewModel>(x)).ToList()
            };
        }

        public async Task<TrainingViewModel> GetTrainingViewModelAsync(string prettyUrl)
        {
            var items = await _autoCadetDbContext.Services
                .Include(x => x.Metadata)
                .Include(x => x.ThumbnailImageFile)
                .FirstOrDefaultAsync(x => x.UrlName.ToLower() == prettyUrl.ToLower())
                .ConfigureAwait(false);
            return _mapper.Map<TrainingViewModel>(items);
        }

        public async Task<InstructorDetailsPageViewModel> GetInstructorDetailsViewModelAsync(string instructorUrl)
        {
            var instructor = await _autoCadetDbContext.Instructors
                .Include(x => x.ThumbnailImage)
                .Include(x => x.Comments)
                .Include(x => x.InstructorDetails)
                .Include(x => x.InstructorDetails.Metadata)
                .Include(x => x.InstructorDetails.VehicleImage)
                .Include(x => x.InstructorDetails.DetailsImage)
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
            var isSameExists = await _autoCadetDbContext.Comments.AnyAsync(x => x.Text == commentVm.Text && x.Name == commentVm.Name).ConfigureAwait(false);
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
            var isExists = await _autoCadetDbContext.CallMes.AnyAsync(x => x.Phone == callMeVm.Phone).ConfigureAwait(false);
            if (!isExists)
            {
                var callMe = _mapper.Map<CallMe>(callMeVm);

                _autoCadetDbContext.CallMes.Add(callMe);
                await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);

                var instructor =
                    await
                        _autoCadetDbContext.Instructors.Where(x => x.Id == callMeVm.InstructorId)
                            .FirstOrDefaultAsync()
                            .ConfigureAwait(false);
                return _callMeNotificator.Notify(callMe.Phone, callMe.RequesterName, instructor);
            }
            return false;
        }
    }
}