using System;
using System.Web.Http;
using BLL.Login;
using BLL.Social.Achievements.Objects;
using BLL.Social.UserProfile;
using BLL.Social.UserProfile.Objects;
using Ninject.Web.WebApi;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/profile")]
    public class ProfileController : BaseApiController
    {
        private readonly IProfileService _profileService;
        private readonly ILoginService _loginService;

        public ProfileController(IProfileService profileService, ILoginService loginService)
        {
            _profileService = profileService;
            _loginService = loginService;
        }

        public ApiResult GetProfile(int id)
        {
            return ApiResult(_profileService.GetProfileFull(id));
        }

        [Route("~/api/users")]
        [HttpGet]
        public ApiResult GetUsers(int page = 1, int count = 20)
        {
            var users = _profileService.GetUsers(new ProfileSearch {Page = page, Count = count});
            return ApiResult(users);
        }

        [Route("~/api/users/filter")]
        [HttpGet]
        public object GetFilter()
        {
            return ApiResult(new object());
        }

        [Route("avatar")]
        [HttpDelete]
        public ApiResult DeleteAvatar()
        {
            return ApiResult(_loginService.RemoveAvatar());
        }
    }
}
