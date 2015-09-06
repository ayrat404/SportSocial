using System.Collections.Generic;
using BLL.Social.Journals.Objects;
using DAL.DomainModel.EnumProperties;

namespace BLL.Social.UserProfile.Objects
{
    public class ProfileInfo
    {
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string Age { get; set; }
        public SportExperience SportTime { get; set; }
        public string Location { get; set; }
        public bool IsOwner { get; set; }
    }

    public class ProfileFull : ProfileInfo
    {
        public IEnumerable<JournalPreviewVm> Journals { get; set; }
    }
}