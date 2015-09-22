using System.Collections.Generic;

namespace BLL.Social.UserProfile.Objects
{
    public class ProfileListVm
    {
        public ProfileListVm()
        {
            List = new List<ProfilePreview>();
        }

        public bool IsMore { get; set; }
        public IEnumerable<ProfilePreview> List { get; set; }
    }
}