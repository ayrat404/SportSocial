using System.Security.Claims;
using BLL.Common.Objects;
using BLL.Infrastructure.IdentityConfig;
using BLL.Login.ViewModels;
using BLL.Sms;
using DAL.DomainModel;
using Knoema.Localization;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BLL.Login.Impls
{
    public class LoginService: ILoginService
    {
        private readonly ISmsService _smsService;
        private readonly AppUserManager _appUserManager;
        private readonly IAuthenticationManager _authManager;

        public LoginService(ISmsService smsService, AppUserManager appUserManager, IAuthenticationManager authManager)
        {
            _smsService = smsService;
            _appUserManager = appUserManager;
            _authManager = authManager;
        }

        public LoginServiceResult SignIn(SignInModel signInModel)
        {
            var result = new LoginServiceResult()
            {
                Success = true
            };
            AppUser user = _appUserManager.Find(signInModel.Phone, signInModel.Pass);
            if (user == null)
            {
                result.Success = false;
                result.ErrorMessage = "Не верный логин или пароль".Resource(this);
            }
            else
            {
                ClaimsIdentity ident = _appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                _authManager.SignOut();
                _authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
                result.ReturnUrl = ""; //TODO return url
            }
            return result;
        }

        public LoginServiceResult PreRegister(RegistratioinModel regModel, string url)
        {
            var result = new LoginServiceResult()
            {
                Success = true,
                ReturnUrl = url,
            };
            var user = _appUserManager.FindByName(regModel.Phone);
            if (user != null && user.PhoneNumberConfirmed)
            {
                result.ErrorMessage = "Пользователь с указанным номером телефона уже зарегистрирован".Resource(this);
                result.Success = false;
            }

            if (user == null)
            {
                user = new AppUser
                {
                    Name = regModel.Name,
                    PhoneNumber = regModel.Phone,
                    UserName = regModel.Phone,
                    PhoneNumberConfirmed = false,
                };
                var createUserResult = _appUserManager.Create(user);
                if (!createUserResult.Succeeded)
                {
                    result.ErrorMessage = "Ошибка при регистрации".Resource(this);
                    result.Success = false;
                }
            }
            var smsResult = _smsService.GenerateAndSendCode(user.Id, regModel.Phone);
            if (!smsResult.Success)
                result.ErrorMessage = smsResult.ErrorMessage;
            return result;
        }

        public ServiceResult ConfirmSmsCode(ConfirmSmsCode confirmModel)
        {
            var result = new ServiceResult
            {
                Success = true
            };
            var user = _appUserManager.FindByName(confirmModel.Phone);
            if (user != null && !user.PhoneNumberConfirmed)
            {
                result = _smsService.VerifyCode(user.Id, confirmModel.Code);
                if (result.Success)
                {
                    user.PhoneNumberConfirmed = true;
                    _appUserManager.AddPassword(user.Id, confirmModel.Password);
                    _appUserManager.Update(user);
                }
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "Не найден пользователь".Resource(this);
            }
            return result;
        }

        public ServiceResult ResendSmsCode()
        {
            throw new System.NotImplementedException();
        }
    }
}