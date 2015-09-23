using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using BLL.Social.Journals;
using BLL.Social.Journals.Objects;
using BLL.Social.UserProfile.Objects;
using DAL.DomainModel;
using Profile = AutoMapper.Profile;

namespace BLL.Social.UserProfile.MapProfiles
{
    public class UserMapProfile:Profile
    {
        protected override void Configure()
        {
            CreateMap<AppUser, ProfileInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Profile.Avatar))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName()))
                .ForMember(dest => dest.SportTime, opt => opt.MapFrom(src => (int) src.Profile.Experience))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => (int) src.Profile.GetAge()))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => (int) src.Profile.GetAge()));

            CreateMap<AppUser, ProfileFull>()
                .IncludeBase<AppUser, ProfileInfo>()
                .ForMember(dest => dest.IsOwner, opt => opt.MapFrom(src => src.Id == GetService<ICurrentUser>().UserId))
                .ForMember(dest => dest.Followers, opt => opt.MapFrom(src => MapFolowers(src)))
                .ForMember(dest => dest.Subscribe, opt => opt.MapFrom(src => MapSubscribes(src)))
                .ForMember(dest => dest.Journal,
                    opt => opt.MapFrom(src => GetService<IJournalService>().GetJournals(src.Id, 1, 20)));

            CreateMap<AppUser, ProfilePreview>()
                .IncludeBase<AppUser, ProfileInfo>()
                .ForMember(dest => dest.AchievementsCount, opt => opt.MapFrom(src => src.Achievements.Count))
                .ForMember(dest => dest.RecordsCount, opt => opt.MapFrom(src => src.Journals.Count))
                .ForMember(dest => dest.Subscribers, opt => opt.MapFrom(src => MapSubscribersVm(src)));

            //CreateMap<ICollection<Subscribe>, ListVm<UserInfoVm>>()
            //    .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
            //    .ForMember(dest => dest.List, opt => opt.MapFrom(src => src.ToList()));
        }

        private ListVm<UserInfoVm> MapSubscribes(AppUser src)
        {
            return new ListVm<UserInfoVm>
            {
                Count = src.Subscribes.Count,
                List = src.Subscribes.Select(s => s.ToUser.MapTo<UserInfoVm>()).ToList()
            };
        }

        private SubcribersVm MapSubscribersVm(AppUser src)
        {
            return new SubcribersVm()
            {
                Count = src.Folowers.Count,
                List = src.Folowers.Select(s => s.FolowerUser.MapTo<UserInfoVm>()).ToList(),
                IsSubscribed = src.Folowers.Any(s => s.FolowerUserId == GetService<ICurrentUser>().UserId)
            };
        }

        private ListVm<UserInfoVm> MapFolowers(AppUser src)
        {
            return new ListVm<UserInfoVm>
            {
                Count = src.Folowers.Count,
                List = src.Folowers.Select(s => s.FolowerUser.MapTo<UserInfoVm>()).ToList()
            };
        }

        protected T GetService<T>() where T : class
        {
            return DependencyResolver.Current.GetService<T>() ??
                    (T)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(T));
        }
    }

    public class SubcribersVm: ListVm<UserInfoVm>
    {
        public bool IsSubscribed { get; set; }
    }
}