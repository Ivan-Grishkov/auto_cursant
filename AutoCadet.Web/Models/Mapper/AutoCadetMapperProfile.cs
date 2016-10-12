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
            CreateMap<Instructor, InstructorManageViewModel>();
            CreateMap<InstructorManageViewModel, Instructor>()
                .ForMember(x => x.ThumbnailImage, opt => opt.Ignore());
            CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.InstructorName, opt => opt.ResolveUsing(x => $"{x.Instructor?.LastName} {x.Instructor?.FirstName}"))
                .ForMember(x => x.InstructorId, opt => opt.ResolveUsing(x => x.Instructor?.Id));
            CreateMap<CommentViewModel, Comment>()
                .ForMember(x => x.Instructor, opt => opt.Ignore());
        }
    }
}
