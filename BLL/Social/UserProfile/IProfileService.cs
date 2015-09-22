using BLL.Common.Objects;
using BLL.Social.Achievements.Objects;
using BLL.Social.UserProfile.Objects;

namespace BLL.Social.UserProfile
{
    public interface IProfileService
    {
        ProfileFull GetProfileFull(int id);
        ProfileListVm GetUsers(ProfileSearch search);
    }
}