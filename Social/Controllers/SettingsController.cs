using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Common.Extensions;
using BLL.Common.Services.CurrentUser;
using BLL.Core.Services.Settings;
using BLL.Core.Services.Settings.Objects;
using BLL.Login;
using BLL.Login.ViewModels;
using DAL.DomainModel.EnumProperties;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/settings")]
    public class SettingsController : BaseApiController
    {
        private readonly ILoginService _loginService;
        private readonly ICurrentUser _currentUser;
        private readonly ISettingsService _settingsService;

        public SettingsController(ILoginService loginService, ICurrentUser currentUser, ISettingsService settingsService)
        {
            _loginService = loginService;
            _currentUser = currentUser;
            _settingsService = settingsService;
        }

        [HttpGet]
        [Authorize]
        [Route("account")]
        public ApiResult AccountSettings()
        {
            string phone = _loginService.GetPhoneForSettings();
            return ApiResult(new {phone = phone});
        }

        [HttpPost]
        [Authorize]
        [Route("password")]
        public ApiResult ChangePassword(NewPasswordVm newPassword)
        {
            if (ModelState.IsValid)
            {

                return ApiResult(_loginService.ChangePassword(new ChangePaswdModel {ConfirmNew =  newPassword.NewRepeatPassword, New = newPassword.NewPassword, Old = newPassword.OldPassword}));
            }
            return ModelStateErrors();
        }

        [HttpPost]
        [Authorize]
        [Route("change_phone_one")]
        public ApiResult ChangePhone(PhoneVm phone)
        {
            if (ModelState.IsValid)
            {
                return ApiResult(_loginService.ChangePhone(phone.Phone));
            }
            return ModelStateErrors();
        }

        [HttpPost]
        [Authorize]
        [Route("change_phone_two")]
        public ApiResult ChangePhoneConfirm(ChangePhoneModel phoneConfirm)
        {
            if (ModelState.IsValid)
            {
                return ApiResult(_loginService.ChangePhoneConfirm(phoneConfirm));
            }
            return ModelStateErrors();
        }

        [HttpGet]
        [Authorize]
        [Route("profile")]
        public ApiResult GetProfile()
        {
            var profileSettings = _settingsService.GetProfileSettings();
            var prop= new
            {
                genders = ((IEnumerable<Sex>) Enum.GetValues(typeof (Sex))).Select(o => new SexVm
                {
                    Label = o.GetDescription(),
                    Value = o
                }),
                sportTimes = ((IEnumerable<SportExperience>) Enum.GetValues(typeof (SportExperience))).Select(o => new SportExpirienceVm()
                {
                    Label = o.GetDescription(),
                    Value = (int)o
                }),
            };
            return ApiResult(new {model = profileSettings, prop});
        }

        [HttpPut]
        [Authorize]
        [Route("profile")]
        public ApiResult ChangeProfile(ProfileSettings settings)
        {
            return ApiResult(_settingsService.SaveSettings(settings));
        }
    }
}
