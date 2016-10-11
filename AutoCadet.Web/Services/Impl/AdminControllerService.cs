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

        public async Task<InstrucrorManageViewModel> GetInstructorViewModelAsync(int instructorId)
        {
            var instructor = await _autoCadetDbContext.Instructors.FirstOrDefaultAsync(x => x.Id == instructorId).ConfigureAwait(false);
            return _mapper.Map<InstrucrorManageViewModel>(instructor);
        }

        public async Task SaveInstructorAsync(InstrucrorManageViewModel instructorVm)
        {
            var instructor = await _autoCadetDbContext.Instructors.FirstOrDefaultAsync(x => x.Id == instructorVm.Id).ConfigureAwait(false) ?? new Instructor();
            _mapper.Map<InstrucrorManageViewModel, Instructor>(instructorVm, instructor);
            if (instructorVm.UploadedThumbnail != null)
            {
                if (instructor.ThumbnailImage == null)
                {
                    instructor.ThumbnailImage = new ImageFile();
                }

                instructor.ThumbnailImage.Bytes = instructorVm.UploadedThumbnail;
            }

            _autoCadetDbContext.Instructors.AddOrUpdate(instructor);
            await _autoCadetDbContext.SaveChangesAsync();
        }
    }
}