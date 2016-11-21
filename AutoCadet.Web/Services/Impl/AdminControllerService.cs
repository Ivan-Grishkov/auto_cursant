using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using AutoCadet.Areas.Admin.Models;
using AutoCadet.Domain;
using AutoCadet.Domain.Entities;
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

        public async Task<IList<InstructorViewModel>> GetAllInstructorsViewModelsAsync()
        {
            var instructors = await _autoCadetDbContext.Instructors.Include(x => x.ThumbnailImage).ToListAsync().ConfigureAwait(false);
            return instructors?.Select(i => _mapper.Map<InstructorViewModel>(i)).ToList();
        }

        public async Task<InstructorManagePageViewModel> GetInstructorViewModelAsync(int instructorId)
        {
            var instructor = await _autoCadetDbContext
                .Instructors
                .Include(x => x.InstructorDetails)
                .Include(x => x.InstructorDetails.VehicleImage)
                .Include(x => x.InstructorDetails.DetailsImage)
                .Include(x => x.InstructorDetails.Metadata)
                .Include(x => x.ThumbnailImage)
                .FirstOrDefaultAsync(x => x.Id == instructorId)
                .ConfigureAwait(false);

            return new InstructorManagePageViewModel
            {
                Instructor = _mapper.Map<InstructorViewModel>(instructor),
                InstructorDetails = _mapper.Map<InstructorDetailsViewModel>(instructor?.InstructorDetails),
                MetadataInfo = _mapper.Map<MetadataInfoViewModel>(instructor?.InstructorDetails?.Metadata)
            };
        }

        public async Task SaveInstructorAsync(InstructorManagePageViewModel pageVm)
        {
            if (pageVm?.Instructor == null)
            {
                throw new ArgumentNullException(nameof(pageVm));
            }

            var instructor = await _autoCadetDbContext.Instructors
                .FirstOrDefaultAsync(x => x.Id == pageVm.Instructor.Id)
                .ConfigureAwait(false) 
                    ?? new Instructor();
            _mapper.Map(pageVm.Instructor, instructor);

            if (pageVm.Instructor.ThumbnailImage != null)
            {
                if (instructor.ThumbnailImage == null)
                {
                    instructor.ThumbnailImage = new ImageFile();
                }

                instructor.ThumbnailImage.Bytes = pageVm.Instructor.ThumbnailImage;
            }

            var instructorDetails = await _autoCadetDbContext.InstructorDetails
                .FirstOrDefaultAsync(x => x.Id == pageVm.InstructorDetails.Id)
                .ConfigureAwait(false)
                                    ?? new InstructorDetails();
            _mapper.Map(pageVm.InstructorDetails, instructorDetails);

            if (pageVm.InstructorDetails.DetailsImage != null)
            {
                if (instructorDetails.DetailsImage == null)
                {
                    instructorDetails.DetailsImage = new ImageFile();
                }

                instructorDetails.DetailsImage.Bytes = pageVm.InstructorDetails.DetailsImage;
            }

            if (pageVm.InstructorDetails.VehicleImage != null)
            {
                if (instructorDetails.VehicleImage == null)
                {
                    instructorDetails.VehicleImage = new ImageFile();
                }

                instructorDetails.VehicleImage.Bytes = pageVm.InstructorDetails.VehicleImage;
            }

            instructorDetails.Metadata = _mapper.Map<Metadata>(pageVm.MetadataInfo);
            if (pageVm.MetadataInfo != null && pageVm.MetadataInfo.Id != 0)
            {
                var meta = await _autoCadetDbContext.Metadatas.FirstOrDefaultAsync(x => x.Id == pageVm.MetadataInfo.Id).ConfigureAwait(false);
                _mapper.Map(pageVm.MetadataInfo, meta);
                instructorDetails.Metadata = meta;
            }

            instructor.InstructorDetails = instructorDetails;
            _autoCadetDbContext.Instructors.AddOrUpdate(instructor);
            await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task SaveInstructorsAttributesAsync(IList<InstructorViewModel> instructorGridItemViewModels)
        {
            var ids = instructorGridItemViewModels.Where(x => x != null).Select(i => i.Id).ToList();
            var instructors = await _autoCadetDbContext.Instructors
                .Where(x => ids.Contains(x.Id))
                .ToListAsync()
                .ConfigureAwait(false);
            foreach (var instructor in instructors)
            {
                var vm = instructorGridItemViewModels.FirstOrDefault(x => x.Id == instructor.Id);
                if (vm != null)
                {
                    instructor.IsActive = vm.IsActive;
                    instructor.SortingNumber = vm.SortingNumber;
                    instructor.Email = vm.Email;
                    instructor.UrlName = vm.UrlName;
                    instructor.Price = vm.Price;
                    instructor.Phone = vm.Phone;
                }
            }
            await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IList<VideoViewModel>> GetAllVideoViewModelsAsync()
        {
            var Video = await _autoCadetDbContext.Video
                .Include(x => x.Metadata)
                .Include(x => x.ThumbnailImageFile)
                .ToListAsync()
                .ConfigureAwait(false);
            return Video?.Select(i => _mapper.Map<VideoViewModel>(i)).ToList();
        }

        public async Task<VideoManagePageViewModel> GetVideoViewModelAsync(int lessonId)
        {
            var instructor = await _autoCadetDbContext
                .Video
                .Include(x => x.Metadata)
                .Include(x => x.ThumbnailImageFile)
                .FirstOrDefaultAsync(x => x.Id == lessonId)
                .ConfigureAwait(false);

            return new VideoManagePageViewModel
            {
                VideoViewModel = _mapper.Map<VideoViewModel>(instructor),
                MetadataInfo = _mapper.Map<MetadataInfoViewModel>(instructor?.Metadata)
            };
        }

        public async Task SaveVideoAttributesAsync(IList<VideoViewModel> VideoGridItemViewModels)
        {
            var ids = VideoGridItemViewModels.Where(x => x != null).Select(i => i.Id).ToList();
            var lessons = await _autoCadetDbContext.Video
                .Where(x => ids.Contains(x.Id))
                .ToListAsync()
                .ConfigureAwait(false);
            foreach (var instructor in lessons)
            {
                var vm = VideoGridItemViewModels.FirstOrDefault(x => x.Id == instructor.Id);
                if (vm != null)
                {
                    instructor.ListHeader = vm.ListHeader;
                    instructor.YoutubeUrl = vm.YoutubeUrl;
                    instructor.UrlName = vm.UrlName;
                    instructor.Text = vm.Text;
                    instructor.IsActive = vm.IsActive;
                    instructor.SortingNumber = vm.SortingNumber;
                }
            }
            await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task SaveVideoAsync(VideoManagePageViewModel pageVm)
        {
            if (pageVm?.VideoViewModel == null)
            {
                throw new ArgumentNullException(nameof(pageVm));
            }

            var lesson = await _autoCadetDbContext.Video
                .FirstOrDefaultAsync(x => x.Id == pageVm.VideoViewModel.Id)
                .ConfigureAwait(false)
                    ?? new Video();
            _mapper.Map(pageVm.VideoViewModel, lesson);

            if (pageVm.VideoViewModel.ThumbnailImageFile != null)
            {
                if (lesson.ThumbnailImageFile == null)
                {
                    lesson.ThumbnailImageFile = new ImageFile();
                }

                lesson.ThumbnailImageFile.Bytes = pageVm.VideoViewModel.ThumbnailImageFile;
            }

            lesson.Metadata = _mapper.Map<Metadata>(pageVm.MetadataInfo);
            if (pageVm.MetadataInfo != null && pageVm.MetadataInfo.Id != 0)
            {
                var meta = await _autoCadetDbContext.Metadatas.FirstOrDefaultAsync(x => x.Id == pageVm.MetadataInfo.Id).ConfigureAwait(false);
                _mapper.Map(pageVm.MetadataInfo, meta);
                lesson.Metadata = meta;
            }
            
            lesson.CreatedDate = DateTime.Now;
            _autoCadetDbContext.Video.AddOrUpdate(lesson);
            await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IList<ObuchenieViewModel>> GetAllObuchenieViewModelsAsync()
        {
            var entities = await _autoCadetDbContext.Obuchenie
               .Include(x => x.Metadata)
               .Include(x => x.ThumbnailImageFile)
               .ToListAsync()
               .ConfigureAwait(false);
            return entities?.Select(i => _mapper.Map<ObuchenieViewModel>(i)).ToList();
        }

        public async Task<ObuchenieManagePageViewModel> GetObuchenieViewModelAsync(int obuchenieId)
        {
            var entity = await _autoCadetDbContext
               .Obuchenie
               .Include(x => x.Metadata)
               .Include(x => x.ThumbnailImageFile)
               .FirstOrDefaultAsync(x => x.Id == obuchenieId)
               .ConfigureAwait(false);

            return new ObuchenieManagePageViewModel
            {
                ObuchenieViewModel = _mapper.Map<ObuchenieViewModel>(entity),
                MetadataInfo = _mapper.Map<MetadataInfoViewModel>(entity?.Metadata)
            };
        }

        public async Task SaveObuchenieAttributesAsync(IList<ObuchenieViewModel> obuchenieGridItemVms)
        {
            var ids = obuchenieGridItemVms.Where(x => x != null).Select(i => i.Id).ToList();
            var entities = await _autoCadetDbContext.Obuchenie
                .Where(x => ids.Contains(x.Id))
                .ToListAsync()
                .ConfigureAwait(false);
            foreach (var entity in entities)
            {
                var vm = obuchenieGridItemVms.FirstOrDefault(x => x.Id == entity.Id);
                if (vm != null)
                {
                    entity.ListHeader = vm.ListHeader;
                    entity.ListDescription = vm.ListDescription;
                    entity.ListIcon = vm.ListIcon;
                    entity.UrlName = vm.UrlName;
                    entity.IsActive = vm.IsActive;
                    entity.SortingNumber = vm.SortingNumber;
                }
            }
            await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task SaveObuchenieAsync(ObuchenieManagePageViewModel pageVm)
        {
            if (pageVm?.ObuchenieViewModel == null)
            {
                throw new ArgumentNullException(nameof(pageVm));
            }

            var entity = await _autoCadetDbContext.Obuchenie
                .FirstOrDefaultAsync(x => x.Id == pageVm.ObuchenieViewModel.Id)
                .ConfigureAwait(false)
                    ?? new Obuchenie();
            _mapper.Map(pageVm.ObuchenieViewModel, entity);

            if (pageVm.ObuchenieViewModel.ThumbnailImageFile != null)
            {
                if (entity.ThumbnailImageFile == null)
                {
                    entity.ThumbnailImageFile = new ImageFile();
                }

                entity.ThumbnailImageFile.Bytes = pageVm.ObuchenieViewModel.ThumbnailImageFile;
            }

            entity.Metadata = _mapper.Map<Metadata>(pageVm.MetadataInfo);
            if (pageVm.MetadataInfo != null && pageVm.MetadataInfo.Id != 0)
            {
                var meta = await _autoCadetDbContext.Metadatas.FirstOrDefaultAsync(x => x.Id == pageVm.MetadataInfo.Id).ConfigureAwait(false);
                _mapper.Map(pageVm.MetadataInfo, meta);
                entity.Metadata = meta;
            }

            entity.CreatedDate = DateTime.Now;
            _autoCadetDbContext.Obuchenie.AddOrUpdate(entity);
            await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IList<BlogViewModel>> GetAllBlogsViewModelsAsync()
        {
            var entities = await _autoCadetDbContext.Blogs
               .Include(x => x.Metadata)
               .Include(x => x.ThumbnailImageFile)
               .Include(x => x.DetailsImageFile)
               .ToListAsync()
               .ConfigureAwait(false);
            return entities?.Select(i => _mapper.Map<BlogViewModel>(i)).ToList();
        }

        public async Task<BlogManagePageViewModel> GetBlogViewModelAsync(int blogId)
        {
            var entity = await _autoCadetDbContext
                .Blogs
                .Include(x => x.Metadata)
                .Include(x => x.ThumbnailImageFile)
                .Include(x => x.DetailsImageFile)
                .FirstOrDefaultAsync(x => x.Id == blogId)
                .ConfigureAwait(false);

            return new BlogManagePageViewModel
            {
                BlogViewModel = _mapper.Map<BlogViewModel>(entity),
                MetadataInfo = _mapper.Map<MetadataInfoViewModel>(entity?.Metadata)
            };
        }

        public async Task SaveBlogsAttributesAsync(IList<BlogViewModel> blogGridItemVms)
        {
            var ids = blogGridItemVms.Where(x => x != null).Select(i => i.Id).ToList();
            var entities = await _autoCadetDbContext.Blogs
                .Where(x => ids.Contains(x.Id))
                .ToListAsync()
                .ConfigureAwait(false);
            foreach (var entity in entities)
            {
                var vm = blogGridItemVms.FirstOrDefault(x => x.Id == entity.Id);
                if (vm != null)
                {
                    entity.ListHeader = vm.ListHeader;
                    entity.ListDescription = vm.ListDescription;
                    entity.UrlName = vm.UrlName;
                    entity.YoutubeUrl = vm.YoutubeUrl;
                    entity.IsActive = vm.IsActive;
                    entity.SortingNumber = vm.SortingNumber;
                }
            }
            await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task SaveBlogAsync(BlogManagePageViewModel pageVm)
        {
            if (pageVm?.BlogViewModel == null)
            {
                throw new ArgumentNullException(nameof(pageVm));
            }

            var entity = await _autoCadetDbContext.Blogs
                .FirstOrDefaultAsync(x => x.Id == pageVm.BlogViewModel.Id)
                .ConfigureAwait(false)
                    ?? new Blog();
            _mapper.Map(pageVm.BlogViewModel, entity);

            if (pageVm.BlogViewModel.ThumbnailImageFile != null)
            {
                if (entity.ThumbnailImageFile == null)
                {
                    entity.ThumbnailImageFile = new ImageFile();
                }

                entity.ThumbnailImageFile.Bytes = pageVm.BlogViewModel.ThumbnailImageFile;
            }

            if (pageVm.BlogViewModel.DetailsImageFile != null)
            {
                if (entity.DetailsImageFile == null)
                {
                    entity.DetailsImageFile = new ImageFile();
                }

                entity.DetailsImageFile.Bytes = pageVm.BlogViewModel.DetailsImageFile;
            }

            entity.Metadata = _mapper.Map<Metadata>(pageVm.MetadataInfo);
            if (pageVm.MetadataInfo != null && pageVm.MetadataInfo.Id != 0)
            {
                var meta = await _autoCadetDbContext.Metadatas.FirstOrDefaultAsync(x => x.Id == pageVm.MetadataInfo.Id).ConfigureAwait(false);
                _mapper.Map(pageVm.MetadataInfo, meta);
                entity.Metadata = meta;
            }

            entity.CreatedDate = DateTime.Now;
            _autoCadetDbContext.Blogs.AddOrUpdate(entity);
            await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IList<CommentViewModel>> GetAllCommentViewModelsAsync()
        {
            var comments = await _autoCadetDbContext.Comments.Include(x => x.Instructor).ToListAsync().ConfigureAwait(false);
            return comments?.Select(i => _mapper.Map<CommentViewModel>(i)).ToList();
        }

        public async Task SaveCommentsAttributesAsync(IList<CommentViewModel> commentViewModels)
        {
            var ids = commentViewModels.Where(x => x != null).Select(i => i.Id).ToList();
            var comments = await _autoCadetDbContext.Comments
                .Include(x => x.Instructor)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync()
                .ConfigureAwait(false);
            foreach (var comment in comments)
            {
                var vm = commentViewModels.FirstOrDefault(x => x.Id == comment.Id);
                if (vm != null)
                {
                    _mapper.Map(vm, comment);
                }
            }
            await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IList<CallMeViewModel>> GetAllCallMeViewModelsAsync()
        {
            var callMes = await _autoCadetDbContext.CallMes.Include(x => x.Instructor).ToListAsync().ConfigureAwait(false);
            return callMes?.Select(i => _mapper.Map<CallMeViewModel>(i)).ToList();
        }

        public async Task SaveCallMeViewModelsAsync(IList<CallMeViewModel> callMeViewModels)
        {
            var ids = callMeViewModels.Where(x => x != null).Select(i => i.Id).ToList();
            var callMes = await _autoCadetDbContext.CallMes
                .Where(x => ids.Contains(x.Id))
                .ToListAsync()
                .ConfigureAwait(false);
            foreach (var callMe in callMes)
            {
                var vm = callMeViewModels.FirstOrDefault(x => x.Id == callMe.Id);
                if (vm != null)
                {
                    _mapper.Map(vm, callMe);
                }
            }
            await _autoCadetDbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}