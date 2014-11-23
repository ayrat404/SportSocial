using System.Linq;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.IdentityConfig;
using BLL.Login.ViewModels;
using BLL.Sms;
using DAL.DomainModel;
using DAL.Repository.Interfaces;
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
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public LoginService(ISmsService smsService, AppUserManager appUserManager, IAuthenticationManager authManager, IRepository repository, ICurrentUser currentUser)
        {
            _smsService = smsService;
            _appUserManager = appUserManager;
            _authManager = authManager;
            _repository = repository;
            _currentUser = currentUser;
        }

        public LoginServiceResult SignIn(SignInModel signInModel)
        {
            var result = new LoginServiceResult
            {
                Success = true
            };
            var user = _appUserManager.Find(signInModel.Phone, signInModel.Pass);
            if (user == null)
            {
                result.Success = false;
                result.ErrorMessage = "Не верный логин или пароль".Resource(this);
            }
            else
            {
                var ident = _appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                _authManager.SignOut();
                _authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
                result.ReturnUrl = ""; //TODO return url
            }
            return result;
        }

        public LoginServiceResult PreRegister(RegistratioinModel regModel, string url)
        {
            var result = new LoginServiceResult
            {
                Success = true,
                ReturnUrl = url,
            };
            var user = _appUserManager.FindByName(regModel.Phone);
            if (user != null && user.PhoneNumberConfirmed)
            {
                result.ErrorMessage = "Пользователь с указанным номером телефона уже зарегистрирован".Resource(this);
                result.Success = false;
                return result;
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
                    return result;
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
                    var profile = new Profile
                    {
                        Id = user.Id,
                        Lang = LanguageHelper.GetCurrentCulture(),
                        Avatar = "/Content/Images/default-avatar.png",
                    };
                    _repository.Add(profile);
                    _repository.SaveChanges();
                    var ident = _appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    _authManager.SignOut();
                    _authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
                }
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "Не найден пользователь".Resource(this);
            }
            return result;
        }

        public ServiceResult ChangePassword(ChangePaswdModel changePaswdModel)
        {
            var result = new ServiceResult() {Success = false};
            var chPswdResult = _appUserManager.ChangePassword(_currentUser.UserId, 
                changePaswdModel.Old, changePaswdModel.New);
            if (chPswdResult.Succeeded)
            {
                result.Success = true;
                return result;
            }
            result.ErrorMessage = chPswdResult.Errors.FirstOrDefault();
            return result;
        }

        public ServiceResult RestorePassword(string phone)
        {
            var result = new ServiceResult()
            {
                Success = false,
            };
            var user = _appUserManager.FindByName(phone);
            if (user == null)
            {
                result.ErrorMessage = "Пользователь не найден".Resource(this);
                return result;
            }
            return _smsService.GenerateAndSendCode(user.Id, user.UserName);
        }

        public ServiceResult RestorePasswordConfirm(ConfirmSmsCode confirmModel)
        {
            var result = new ServiceResult {Success = false};
            var user = _appUserManager.FindByName(confirmModel.Phone);
            if (user == null)
            {
                result.ErrorMessage = "Пользователь не найден".Resource(this);
                return result;
            }
            var verifyResult = _smsService.VerifyCode(user.Id, confirmModel.Code);
            if (verifyResult.Success)
            {
                _appUserManager.RemovePassword(user.Id);
                var chPswdResult = _appUserManager.AddPassword(user.Id, confirmModel.Password);
                if (chPswdResult.Succeeded)
                {
                    result.Success = true;
                    return result;
                }
                result.ErrorMessage = "Ошибка на сервере".Resource(this); 
                //TODO нетипичная ситуация, надо както уведомлять админа
            }
            return verifyResult;
        }

        public ServiceResult ChangePhone(string phone)
        {
            return _smsService.GenerateAndSendCode(_currentUser.UserId, _currentUser.Phone);
        }

        public ServiceResult ChangePhoneConfirm(ChangePhoneModel chPhoneModel)
        {
            var verifyResult = _smsService.VerifyCode(_currentUser.UserId, chPhoneModel.Code);
            if (!verifyResult.Success)
            {
                return verifyResult;
            }

            var user = _repository.Find<AppUser>(_currentUser.UserId);
            user.UserName = chPhoneModel.Phone;
            _repository.SaveChanges();
            var result = new ServiceResult {Success = true};
            return result;
        }

        public ServiceResult LogOut()
        {
            var result = new ServiceResult {Success = false};
            if (!_currentUser.IsAnonimous && !string.IsNullOrEmpty(_currentUser.UserId))
            {
                _authManager.SignOut();
                result.Success = true;
                return result;
            }
            result.ErrorMessage = "Ошибка".Resource(this);
            return result;
        }

        public ServiceResult ResendSmsCode(string phone)
        {
            var user = _repository
                .Queryable<AppUser>()
                .SingleOrDefault(u => u.UserName == phone && !u.PhoneNumberConfirmed);
            if (user != null)
            {
                return _smsService.GenerateAndSendCode(user.Id, phone);
            }
            return new ServiceResult
            {
                Success = false,
                ErrorMessage = "Ошибка".Resource(this),
            };
        }
    }
}