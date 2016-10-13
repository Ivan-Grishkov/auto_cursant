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
            CreateMap<Instructor, InstructorManageViewModel>()
                .ForMember(x => x.MetadataInfo, opt => opt.ResolveUsing(x => new MetadataInfoViewModel
                {
                    Id = x.Metadata?.Id ?? default(int),
                    Description = x.Metadata?.Description,
                    Info = x.Metadata?.Info,
                    Keywords = x.Metadata?.Keywords
                }));
            CreateMap<InstructorManageViewModel, Instructor>()
                .ForMember(x => x.Metadata, opt => opt.Ignore())
                .ForMember(x => x.ThumbnailImage, opt => opt.Ignore());

            CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.InstructorName, opt => opt.ResolveUsing(x => $"{x.Instructor?.LastName} {x.Instructor?.FirstName}"))
                .ForMember(x => x.InstructorId, opt => opt.ResolveUsing(x => x.Instructor?.Id));
            CreateMap<CommentViewModel, Comment>()
                .ForMember(x => x.Instructor, opt => opt.Ignore());

            CreateMap<Metadata, MetadataInfoViewModel>();
            CreateMap<MetadataInfoViewModel, Metadata>();

        }
    }
}
