using System.Web.Http;
using BLL.Social.UserProfile;
using BLL.Social.UserProfile.Objects;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/profile")]
    public class ProfileController : BaseApiController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public ProfileFull GetProfile(int id)
        {
            return _profileService.GetProfileFull(id);
        }
    }
}
