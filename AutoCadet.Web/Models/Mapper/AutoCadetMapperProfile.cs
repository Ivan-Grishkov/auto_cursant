using AutoCadet.Domain.Entities;
using AutoMapper;

namespace AutoCadet.Models.Mapper
{
    public class AutoCadetMapperProfile : Profile
    {
        public AutoCadetMapperProfile()
        {
            CreateMap<Instructor, InstructorGridItemViewModel>()
                .ForMember(vm => vm.ThumbnailImage, opt => opt.ResolveUsing(x => x.ThumbnailImage?.Bytes));
            CreateMap<Instructor, InstructorGridItemBaseViewModel>();
            CreateMap<Instructor, InstrucrorManageViewModel>();
            CreateMap<InstrucrorManageViewModel, Instructor>()
                .ForMember(x => x.ThumbnailImage, opt => opt.Ignore());
        }
    }
}
