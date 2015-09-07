using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using BLL.Blog.ViewModels;
using BLL.Common.Services.CurrentUser;
using BLL.Rating;
using BLL.Rating.Enums;
using BLL.Social.Journals.Objects;
using BLL.Storage.Objects;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;
using DAL.DomainModel.JournalEntities;

namespace BLL.Social.Journals.MapProfiles
{
    public class JournalPreviewProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<Journal, JournalPreviewVm>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => RatingMapper.MapRating(src)))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created));

            CreateMap<Journal, JournalDisplayVm>()
                .IncludeBase<Journal, JournalPreviewVm>()
                .ForMember(dest => dest.Media, opt => opt.MapFrom(src => src.Media))
                .ForMember(dest => dest.Themes, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));

            Mapper.CreateMap<JournalTag, string>().ConvertUsing(source => source.Tag.Label);

            CreateMap<JournalMedia, MediaVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (MediaType)src.Type))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));
        }
    }
}