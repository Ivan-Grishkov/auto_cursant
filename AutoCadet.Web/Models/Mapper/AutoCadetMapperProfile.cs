using AutoCadet.Domain.Entities;
using AutoMapper;

namespace AutoCadet.Models.Mapper
{
    public class AutoCadetMapperProfile : Profile
    {
        public AutoCadetMapperProfile()
        {
            CreateMap<Instructor, InstructorViewModel>()
                .ForMember(x => x.ThumbnailImage, opt => opt.ResolveUsing(x => x.ThumbnailImage?.Bytes))
                .ForMember(x => x.AverageScore, opt => opt.Ignore());
            CreateMap<InstructorViewModel, Instructor>()
                .ForMember(x => x.ThumbnailImage, opt => opt.Ignore());

            CreateMap<InstructorDetails, InstructorDetailsViewModel>()
                .ForMember(x => x.MetadataInfo, opt => opt.MapFrom(x => x.Metadata))
                .ForMember(x => x.DetailsImage, opt => opt.ResolveUsing(x => x.DetailsImage?.Bytes))
                .ForMember(x => x.VehicleImage, opt => opt.ResolveUsing(x => x.VehicleImage?.Bytes));

            CreateMap<InstructorDetailsViewModel, InstructorDetails>()
                .ForMember(x => x.Metadata, opt => opt.Ignore())
                .ForMember(x => x.DetailsImage, opt => opt.Ignore())
                .ForMember(x => x.VehicleImage, opt => opt.Ignore());

            CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.InstructorName, opt => opt.ResolveUsing(x => $"{x.Instructor?.LastName} {x.Instructor?.FirstName}"))
                .ForMember(x => x.InstructorId, opt => opt.ResolveUsing(x => x.Instructor?.Id));
            CreateMap<CommentViewModel, Comment>()
                .ForMember(x => x.Instructor, opt => opt.Ignore());

            CreateMap<VideoLesson, VideoLessonViewModel>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata));
            CreateMap<VideoLessonViewModel, VideoLesson>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata));

            CreateMap<Metadata, MetadataInfoViewModel>();
            CreateMap<MetadataInfoViewModel, Metadata>();

            CreateMap<CallMe, CallMeViewModel>();
            CreateMap<CallMeViewModel, CallMe>();

        }
    }
}
