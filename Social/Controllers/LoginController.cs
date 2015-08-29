using System;
using System.Web.Http;
using BLL.Common.Objects;
using BLL.Login;
using BLL.Login.ViewModels;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : BaseApiController
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpGet]
        [Route("")]
        public ApiResult SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                return ApiResult(_loginService.SignIn(model, ""));
            }
            return ModelStateErrors();
            //return Json(new {success = false, ErrorMessage = GetModelStateErrors(), Redirect = returnUrl}, jsonContentType);
        }


        [HttpPost]
        [Route("~/api/register/step1")]
        public ApiResult Register(RegistratioinModel regModel)
        {
            if (ModelState.IsValid)
            {
                return ApiResult(_loginService.PreRegister(regModel, ""));
            }
            return ModelStateErrors();
        }

        [HttpPost]
        [Route("~/api/register/step2")]
        public ApiResult ConfirmRegistration(ConfirmSmsCode confirm)
        {
            if (ModelState.IsValid)
            {
                return ApiResult(_loginService.ConfirmSmsCode(confirm));
            }
            return ModelStateErrors();
        }

        [Route("~/api/resendCode")]
        [HttpPost]
        public ApiResult ResendCode(string phone)
        {
            return ApiResult(_loginService.ResendSmsCode(phone));
        }

        [HttpPost]
        [Route("~/api/restorePassword")]
        public ApiResult RestorePassword(string phone)
        {
            return ApiResult(_loginService.RestorePassword(phone));
        }

        [HttpPost]
        [Route("~/api/restorePassword/confirm")]
        public ApiResult RestorePasswordConfirm(RestorePasswordInfo restoreInfo)
        {
            return ApiResult(_loginService.RestorePasswordConfirm(restoreInfo));
        }
    }

}
