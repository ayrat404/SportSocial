using BLL.Social.UserProfile.Objects;

namespace BLL.Social.UserProfile
{
    public interface IProfileService
    {
        ProfileFull GetProfileFull(int id);
    }
}