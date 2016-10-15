using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IList<InstructorGridItemViewModel>> GetAllUsersViewModelsAsync()
        {
            var instructors = await _autoCadetDbContext.Instructors.Include(x => x.ThumbnailImage).ToListAsync().ConfigureAwait(false);
            return instructors?.Select(i => _mapper.Map<InstructorGridItemViewModel>(i)).ToList();
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

            if (pageVm.Instructor.UploadedThumbnail != null)
            {
                if (instructor.ThumbnailImage == null)
                {
                    instructor.ThumbnailImage = new ImageFile();
                }

                instructor.ThumbnailImage.Bytes = pageVm.Instructor.UploadedThumbnail;
            }

            var instructorDetails = await _autoCadetDbContext.InstructorDetailses
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
            await _autoCadetDbContext.SaveChangesAsync();
        }

        public async Task<IList<CommentViewModel>> GetAllCommentViewModelsAsync()
        {
            var comments = await _autoCadetDbContext.Comments.Include(x => x.Instructor).ToListAsync().ConfigureAwait(false);
            return comments?.Select(i => _mapper.Map<CommentViewModel>(i)).ToList();
        }

        public async Task SaveInstructorsAttributesAsync(IList<InstructorGridItemViewModel> instructorGridItemViewModels)
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
                }
            }
            await _autoCadetDbContext.SaveChangesAsync();
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
            await _autoCadetDbContext.SaveChangesAsync();
        }
    }
}