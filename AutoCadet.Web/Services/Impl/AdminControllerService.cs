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

        public async Task<InstructorManageViewModel> GetInstructorViewModelAsync(int instructorId)
        {
            var instructor = await _autoCadetDbContext
                .Instructors
                .Include(x => x.Metadata)
                .FirstOrDefaultAsync(x => x.Id == instructorId)
                .ConfigureAwait(false);
            return _mapper.Map<InstructorManageViewModel>(instructor);
        }

        public async Task SaveInstructorAsync(InstructorManageViewModel instructorVm)
        {
            var instructor = await _autoCadetDbContext.Instructors.FirstOrDefaultAsync(x => x.Id == instructorVm.Id).ConfigureAwait(false) ?? new Instructor();
            _mapper.Map<InstructorManageViewModel, Instructor>(instructorVm, instructor);
            if (instructorVm.UploadedThumbnail != null)
            {
                if (instructor.ThumbnailImage == null)
                {
                    instructor.ThumbnailImage = new ImageFile();
                }

                instructor.ThumbnailImage.Bytes = instructorVm.UploadedThumbnail;
            }

            instructor.Metadata = _mapper.Map<Metadata>(instructorVm.MetadataInfo);
            if (instructorVm.MetadataInfo != null && instructorVm.MetadataInfo.Id != 0)
            {
                var meta = await _autoCadetDbContext.Metadatas.FirstOrDefaultAsync(x => x.Id == instructorVm.MetadataInfo.Id).ConfigureAwait(false);
                _mapper.Map(instructorVm.MetadataInfo, meta);
                instructor.Metadata = meta;
            }

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