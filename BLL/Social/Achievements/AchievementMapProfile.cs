using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using BLL.Comments.MapProfiles;
using BLL.Common.Services.CurrentUser;
using BLL.Rating;
using BLL.Social.Achievements.Objects;
using BLL.Social.Journals.Objects;
using BLL.Storage.MapProfiles;
using DAL;
using DAL.DomainModel.Achievement;
using DAL.DomainModel.Base;
using DAL.DomainModel.EnumProperties;

namespace BLL.Social.Achievements
{
    public class AchievementMapProfile: Profile
    {
        protected T GetService<T>() where T: class
        {
            return DependencyResolver.Current.GetService<T>() ??
                    (T)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(T));
        }

        protected override void Configure()
        {
            CreateMap<Achievement, AchievementCreateVm>()
                .ForMember(dest => dest.Step, opt => opt.MapFrom(src => src.Step))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Video,
                    opt => opt.MapFrom(src => MediaMapper.Map(src.AchievementMedia.FirstOrDefault())));

            CreateMap<Achievement, ChoosedAchievmentType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AchievementType.Id))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value.Value));;

            CreateMap<AchievementType, AchievementTypeVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Img, opt => opt.MapFrom(src => src.ImgUrl))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl))
                .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.GetValues()));

            CreateMap<Achievement, AchievementPreviewVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Started ?? src.Created))
                .ForMember(dest => dest.CupImage, opt => opt.MapFrom(src => src.Value.CupImage))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.AchievementType.Title))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.GetTimeStamp()))
                .ForMember(dest => dest.Voice, opt => opt.MapFrom(src => src));

            CreateMap<Achievement, AchievementDisplayVm>()
                .IncludeBase<Achievement, AchievementPreviewVm>()
                .ForMember(dest => dest.TypeImage, opt => opt.MapFrom(src => src.AchievementType.ImgUrl))
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => RatingMapper.MapRating(src)))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => CommentsVmMapper.Map(src)))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.AchievementMedia.First().Url))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.GetStatus()));

            //CreateMap<AchievementCreateVm, Achievement>()
            //    //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Step, opt => opt.MapFrom(src => src.HasVideo() ? 2 : 1))
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AchievementStatus.InCreating))
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AchievementStatus.InCreating))

            CreateMap<Achievement, AchievmentVoiceVm>()
                .ForMember(dest => dest.For, opt => opt.MapFrom(src => src.Voices.Count(r => r.VoteFor)))
                .ForMember(dest => dest.Against, opt => opt.MapFrom(src => src.Voices.Count(r => !r.VoteFor)))
                .ForMember(dest => dest.IsVoited, opt => opt.MapFrom(src => src.Voices.Any(r => r.UserId == GetService<ICurrentUser>().UserId)));
        }
    }
}