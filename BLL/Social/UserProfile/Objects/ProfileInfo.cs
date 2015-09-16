using System.Collections.Generic;
using BLL.Social.Journals.Objects;
using DAL.DomainModel.EnumProperties;

namespace BLL.Social.UserProfile.Objects
{
    public class ProfileInfo
    {
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int SportTime { get; set; }
        public string Location { get; set; }
        public bool IsOwner { get; set; }
    }

    public class ProfileFull : ProfileInfo
    {
        public IEnumerable<JournalPreviewVm> Journals { get; set; }

        public UserInfos Followers { get; set; }

        public UserInfos Subscribe { get; set; }
    }

    public class UserInfos
    {
        public UserInfos()
        {
            List = new List<UserInfo>();
        }
        public List<UserInfo> List { get; set; }

        public int Count { get; set; }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
    }
}