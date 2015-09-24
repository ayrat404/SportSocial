using System.Collections.Generic;
using BLL.Social.Achievements.Objects;
using BLL.Social.Journals.Objects;
using BLL.Social.UserProfile.MapProfiles;
using BLL.Storage.Objects;
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

        public PagedListVm<JournalPreviewVm> Journal { get; set; }

        public IEnumerable<ProfileJournalMediaVm> Media { get; set; }

        public ListVm<UserInfoVm> Followers { get; set; }

        public ListVm<UserInfoVm> Subscribe { get; set; }
    }

    public class ProfileJournalMediaVm
    {
        public int RecordId { get; set; }
        public int Index { get; set; }
        public string Url { get; set; }
        public MediaType Type { get; set; }
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