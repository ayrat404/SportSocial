using System;
using AutoMapper;
using BLL.Admin.Conference.ViewModels;

namespace BLL.Admin.Conference.MapProfiles
{
    public class ConferenceProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<DAL.DomainModel.Conference, ConfModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Stamp, opt => opt.MapFrom(src => (int)(src.Date - DateTime.Now).TotalMilliseconds));

            CreateMap<DAL.DomainModel.Conference, CreateConfModel>();
            CreateMap<CreateConfModel, DAL.DomainModel.Conference>();
        }
    }
}