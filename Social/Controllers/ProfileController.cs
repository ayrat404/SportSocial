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

        public ApiResult GetProfile(int id)
        {
            return ApiResult(_profileService.GetProfileFull(id));
        }
    }
}
