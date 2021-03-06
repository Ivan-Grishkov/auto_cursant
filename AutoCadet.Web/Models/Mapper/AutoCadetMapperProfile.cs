﻿using AutoCadet.Domain.Entities;
using AutoMapper;

namespace AutoCadet.Models.Mapper
{
    public class AutoCadetMapperProfile : Profile
    {
        public AutoCadetMapperProfile()
        {
            CreateMap<Instructor, InstructorViewModel>()
                .ForMember(x => x.AverageScore, opt => opt.Ignore());
            CreateMap<InstructorViewModel, Instructor>();

            CreateMap<InstructorDetails, InstructorDetailsViewModel>()
                .ForMember(x => x.MetadataInfo, opt => opt.MapFrom(x => x.Metadata));

            CreateMap<InstructorDetailsViewModel, InstructorDetails>()
                .ForMember(x => x.Metadata, opt => opt.Ignore());

            CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.InstructorName, opt => opt.ResolveUsing(x => $"{x.Instructor?.LastName} {x.Instructor?.FirstName}"))
                .ForMember(x => x.InstructorId, opt => opt.ResolveUsing(x => x.Instructor?.Id));
            CreateMap<CommentViewModel, Comment>()
                .ForMember(x => x.Instructor, opt => opt.Ignore());

            CreateMap<Video, VideoViewModel>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata));
            CreateMap<VideoViewModel, Video>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata));

            CreateMap<Obuchenie, ObuchenieViewModel>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata));
            CreateMap<ObuchenieViewModel, Obuchenie>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata));

            CreateMap<Blog, BlogViewModel>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata));
            CreateMap<BlogViewModel, Blog>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata));

            CreateMap<Metadata, MetadataInfoViewModel>();
            CreateMap<MetadataInfoViewModel, Metadata>();

            CreateMap<ShareEvent, ShareEventViewModel>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata));
            CreateMap<ShareEventViewModel, ShareEvent>()
                .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata));

            CreateMap<CallMe, CallMeViewModel>()
                .ForMember(x => x.InstructorName, opt => opt.ResolveUsing(x => $"{x.Instructor?.LastName} {x.Instructor?.FirstName}"))
                .ForMember(x => x.InstructorId, opt => opt.ResolveUsing(x => x.Instructor?.Id));
            CreateMap<CallMeViewModel, CallMe>()
                .ForMember(x => x.Instructor, opt => opt.Ignore());

        }
    }
}
