using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Common.Extensions;
using BLL.Common.Services.CurrentUser;
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

        public SettingsController(ILoginService loginService, ICurrentUser currentUser)
        {
            _loginService = loginService;
            _currentUser = currentUser;
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
            var profileSettings = new ProfileSettings
            {
                FirstName = _currentUser.User.Profile.FirstName,
                LastName = _currentUser.User.Profile.LastName,
                SportTime =
                    new SportExpirienceVm()
                    {
                        Label = _currentUser.User.Profile.Experience.GetDescription(),
                        Value = (int) _currentUser.User.Profile.Experience
                    },
                Birthday = _currentUser.User.Profile.BirthDate,
                Gender =
                    new SexVm()
                    {
                        Label = _currentUser.User.Profile.Experience.GetDescription(),
                        Value = _currentUser.User.Profile.Sex
                    },
            };
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

        [HttpPost]
        [Authorize]
        [Route("profile")]
        public ApiResult ChangeProfile()
        {
            return new ApiResult();
        }
    }

    public class ProfileSettings
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public SexVm Gender { get; set; }
        public SportExpirienceVm SportTime { get; set; }
    }

    public class SexVm
    {
        public Sex Value { get; set; }
        public string Label { get; set; }
    }

    public class SportExpirienceVm
    {
        public int Value { get; set; }
        public string Label { get; set; }
    }

    public class NewPasswordVm
    {
        [Required(ErrorMessage = "Необходимо ввести старый пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Необходимо ввести новый пароль")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Необходимо ввести подтверждение пароля")]
#pragma warning disable 618
        [System.Web.Mvc.Compare("New", ErrorMessage = "Пароли не совпадают")]
#pragma warning restore 618
        public string NewRepeatPassword { get; set; }

    }

    public class PhoneVm
    {
        [Required(ErrorMessage = "Необходимо ввести номер телефона")]
        public string Phone { get; set; }
    }
}
