using System;
using System.Linq;
using AutoMapper;
using BLL.Admin.Conference.ViewModels;
using BLL.Common.Objects;

namespace BLL.Admin.Conference.MapProfiles
{
    public class ConferenceProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<DAL.DomainModel.ConferenceEntities.Conference, ConfModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Stamp, opt => opt.MapFrom(src => (int)(src.Date - DateTime.Now).TotalMilliseconds));

            CreateMap<DAL.DomainModel.ConferenceEntities.Conference, ProcessConfModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Comments, 
                    opt => opt.MapFrom(src => src.Comments.Skip(src.Comments.Count - 3).ToList().Take(3)))
                .ForMember(dest => dest.CommentsCount, 
                    opt => opt.MapFrom(src => src.Comments.Count <= 3 ? 0 : src.Comments.Count - 3))
                .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => CommentItemType.Conference))
                .ForMember(dest => dest.Stamp, opt => opt.MapFrom(src => (int)(src.Date - DateTime.Now).TotalMilliseconds));

            CreateMap<DAL.DomainModel.ConferenceEntities.Conference, CreateConfModel>();
            CreateMap<CreateConfModel, DAL.DomainModel.ConferenceEntities.Conference>();
        }
    }
}