using System.Collections.Generic;
using BLL.Social.Journals.Objects;
using BLL.Social.UserProfile.MapProfiles;
using DAL.DomainModel.EnumProperties;

namespace BLL.Social.UserProfile.Objects
{
    public class ProfileInfo
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int SportTime { get; set; }
        public string Location { get; set; }
    }

    public class ProfileFull : ProfileInfo
    {
        public bool IsOwner { get; set; }

        public IEnumerable<JournalPreviewVm> Journals { get; set; }

        public ListVm<UserInfoVm> Followers { get; set; }

        public ListVm<UserInfoVm> Subscribe { get; set; }
    }

    public class ProfilePreview : ProfileInfo
    {
        public ProfilePreview()
        {
            Subscribers = new SubcribersVm();
        }

        public int AchievementsCount { get; set; }
        public int RecordsCount { get; set; }
        public SubcribersVm Subscribers { get; set; }
    }

    public class ListVm<T>
    {
        public List<UserInfoVm> List { get; set; }

        public int Count { get; set; }
    }

}