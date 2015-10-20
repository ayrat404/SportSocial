using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BLL.Common.Extensions;
using BLL.Core.Services.Settings.Objects;
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
        public ApiResult GetUsers(AgeSearch? age = null, SportExperience? inSport = null, Sex? gender =null, string city = null,
                                  string country = null, string query = null, int page = 1, int count = 20)
        {
            var users = _profileService.GetUsers(new ProfileSearch {Age = age, SportTime = inSport, Gender = gender, City = city, Country = country,
                Page = page, Count = count});
            return ApiResult(users);
        }

        [Route("~/api/users/filter")]
        [HttpGet]
        public ApiResult GetFilter()
        {
            var filter = new
            {
                gender = ((IEnumerable<Sex>) Enum.GetValues(typeof (Sex))).Select(o => new SexVm
                {
                    Label = o.GetDescription(),
                    Value = o
                }),
                inSport = ((IEnumerable<SportExperience>) Enum.GetValues(typeof (SportExperience))).Select(o => new SportExpirienceVm()
                {
                    Label = o.GetDescription(),
                    Value = (int)o
                }),
                age = ((IEnumerable<AgeSearch>) Enum.GetValues(typeof (AgeSearch))).Select(o => new SportExpirienceVm()
                {
                    Label = o.GetDescription(),
                    Value = (int)o
                }),

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
