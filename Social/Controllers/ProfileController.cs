using System;
using System.Web.Http;
using BLL.Login;
using BLL.Social.Achievements.Objects;
using BLL.Social.Tape;
using BLL.Social.UserProfile;
using BLL.Social.UserProfile.Objects;
using DAL.DomainModel.EnumProperties;
using Ninject.Web.WebApi;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/profile")]
    public class ProfileController : BaseApiController
    {
        private readonly IProfileService _profileService;
        private readonly ILoginService _loginService;
        private readonly ITapeService _tapeService;

        public ProfileController(IProfileService profileService, ILoginService loginService, ITapeService tapeService)
        {
            _profileService = profileService;
            _loginService = loginService;
            _tapeService = tapeService;
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
        public ApiResult GetFilter()
        {
            var filter = new
            {
                gender = Enum.GetNames(typeof(Sex)),
                sportTime = Enum.GetValues(typeof(SportExperience)),
            };
            return ApiResult(filter);
        }

        [Route("avatar")]
        [HttpDelete]
        public ApiResult DeleteAvatar()
        {
            return ApiResult(_loginService.RemoveAvatar());
        }

        [Route("~/api/subscribe")]
        [HttpPost]
        public ApiResult Subscribe(SubcribeModel model)
        {
            return ApiResult(_profileService.Subscribe(model));
        }

        [Route("~/api/tape")]
        [HttpGet]
        public ApiResult Tape(int page = 1, int count = 20)
        {
            return ApiResult(_tapeService.GetTape(page, count));
        }
    }
}
