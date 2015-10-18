using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using BLL.Common.Objects;
using BLL.Infrastructure.IdentityConfig;
using BLL.Login;
using BLL.Login.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : BaseApiController
    {
        private readonly ILoginService _loginService;
        private readonly AppUserManager _appUserManager;
        private readonly IAuthenticationManager _authManager;

        public LoginController(ILoginService loginService, AppUserManager appUserManager, IAuthenticationManager authManager)
        {
            _loginService = loginService;
            _appUserManager = appUserManager;
            _authManager = authManager;
        }


        [HttpPost]
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
        [Route("~/api/logout")]
        public ApiResult LogOut()
        {
            return ApiResult(_loginService.LogOut());
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

        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/register/external-info")]
        public ApiResult ExternalInfo()
        {
            var loginInfo = _authManager.GetExternalLoginInfoAsync().Result;
            var externalInfo = GetExternalInfo(loginInfo);
            //loginInfo.ExternalIdentity.Claims.Where(c => c.Value):
            return ApiResult(externalInfo);
        }

        private object GetExternalInfo(ExternalLoginInfo loginInfo)
        {
            var provider = ParseRegType(loginInfo.Login.LoginProvider);
            var claims = loginInfo.ExternalIdentity.Claims;
            string firstName = string.Empty;
            string lastName = string.Empty;
            switch (provider)
            {
                case RegisterType.Vkontakte:
                    string fullName = claims.SingleOrDefault(c => c.Type == "urn:vkontakte:name").Value;
                    var splittedFullName = fullName.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    firstName = splittedFullName[0];
                    if (splittedFullName.Length > 0)
                    {
                        lastName = string.Join(" ", splittedFullName.Skip(1));
                    }
                    break;
                case RegisterType.Google:
                    var surNameClaim = claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname);
                    if (surNameClaim != null)
                    {
                        lastName = surNameClaim.Value;
                    }
                    var givNameClaim = claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName);
                    if (givNameClaim != null)
                    {
                        firstName = givNameClaim.Value;
                    }
                    break;
            }
            return new {firstName, lastName};
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("~/api/register/external")]
        public ApiResult ExternalInfo(ExternalRegistrationModel external)
        {
            var loginInfo = _authManager.GetExternalLoginInfoAsync().Result;
            var result = _loginService.RegisterExternal(external, ParseRegType(loginInfo.Login.LoginProvider),
                loginInfo.Login.ProviderKey, loginInfo.Email);
            return ApiResult(result);
        }
        private RegisterType ParseRegType(string provider)
        {
            RegisterType registerType;
            if (Enum.TryParse(provider, out registerType))
            {
                return registerType;
            }
            return RegisterType.Internal;
        }
    }
}
