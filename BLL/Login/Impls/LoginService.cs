﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Common.Services.Cookies;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.IdentityConfig;
using BLL.Login.ViewModels;
using BLL.Sms;
using BLL.Social.Journals.Objects;
using BLL.Storage;
using BLL.Storage.Impls;
using BLL.Storage.Objects.Enums;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using Knoema.Localization;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;

namespace BLL.Login.Impls
{
    public class LoginService: ILoginService
    {
        private readonly ISmsService _smsService;
        private readonly AppUserManager _appUserManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;
        private readonly ICookiesService _cookiesService;
        private readonly IMediaService _mediaService;

        public const string DefaultAvatarUrl = "/Content/Images/default-avatar.png";
        public const string DefaultFortressAvatar = "/Content/Images/fortress-avatar.jpg";
        public const int TrialDays = 15;

        public LoginService(ISmsService smsService, AppUserManager appUserManager, IAuthenticationManager authManager, IRepository repository, ICurrentUser currentUser, ICookiesService cookiesService, IMediaService mediaService)
        {
            _smsService = smsService;
            _appUserManager = appUserManager;
            _authManager = authManager;
            _repository = repository;
            _currentUser = currentUser;
            _cookiesService = cookiesService;
            _mediaService = mediaService;
        }

        public ServiceResult<SignInResult> SignIn(SignInModel signInModel, string returnUrl)
        {
            var user = _appUserManager.Find(signInModel.Phone, signInModel.Password);
            if (user == null)
            {
                return ServiceResult.ErrorResult<SignInResult>("Не верный логин или пароль".Resource(this));
            }
            else
            {
                var ident = _appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                _authManager.SignOut();
                _authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
                var result = new SignInResult()
                {
                    Id = user.Id,
                    Name = user.FullName(),
                    Avatar = user.Profile.Avatar
                };
                return ServiceResult.SuccessResult(result);
            }
        }

        public ServiceResult<SignInResult> SignInExternal(RegisterType registerType, string externalId)
        {
            var user = _appUserManager.FindByName(ExternalUser(registerType, externalId));
            if (user == null)
            {
                return ServiceResult.ErrorResult<SignInResult>("");
            }
            _authManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            _authManager.SignOut();
            var ident = _appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            _authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
            var result = new SignInResult()
            {
                Id = user.Id,
                Name = user.FullName(),
                Avatar = user.Profile.Avatar
            };
            return ServiceResult.SuccessResult(result);
        }

        public ServiceResult PreRegister(RegistratioinModel regModel, string url)
        {
            var result = new ServiceResult
            {
                Success = true,
            };
            if (!IsValidPhone(regModel.Phone))
            {
                result.ErrorMessage = "Номер телефона должен содержать только цифры в формате <код страны><номер> без сивола \"+\".".Resource(this);
                result.Success = false;
                return result;
            }
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
                    Status = UserStatus.Normal,
                    Created = DateTime.Now,
                    Modified = DateTime.Now
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
            {
                result.Success = false;
                result.ErrorMessage = smsResult.ErrorMessage;
            }
            return smsResult;
        }

        public ServiceResult<SignInResult> ConfirmSmsCode(ConfirmSmsCode confirmModel)
        {
            var user = _appUserManager.FindByName(confirmModel.Phone);
            if (user != null && !user.PhoneNumberConfirmed)
            {
                var smsResult = _smsService.VerifyCode(user.Id, confirmModel.Code);
                if (smsResult.Success)
                {
                    user.PhoneNumberConfirmed = true;
                    _appUserManager.AddPassword(user.Id, confirmModel.Password);
                    user.RegisterType = (int)RegisterType.Internal;
                    var userUpdateResult = _appUserManager.Update(user);
                    if (!userUpdateResult.Succeeded)
                    {
                        string errorMessage = userUpdateResult.Errors.First();
                        var httpContext = HttpContext.Current;
                        var signal = Elmah.ErrorSignal.FromContext(httpContext);
                        signal.Raise(new Exception(errorMessage + ' ' + confirmModel.Phone), httpContext);
                        return ServiceResult.ErrorResult<SignInResult>("Ошибка при регистрации. Попробуйте пройти регистрацию заново.");
                    }
                    //var img = _repository.Find<UserAvatarPhoto>(confirmModel.ImgId);
                    //var profile = new Profile
                    //{
                    //    Id = user.Id,
                    //    Lang = LanguageHelper.GetCurrentCulture(),
                    //    Avatar = img != null ? img.Url : DefaultAvatarUrl,
                    //    ReadedNews = _cookiesService.GetReadedNews(),
                    //    FirstName = confirmModel.Name,
                    //    LastName = confirmModel.LastName,
                    //    Sex = confirmModel.Gender,
                    //    Experience = confirmModel.SportTime,
                    //    BirthDate = confirmModel.BirthDay,
                    //    IsTrial = true,
                    //    LastPaymentDate = DateTime.Now,
                    //    LastPaidDaysCount = TrialDays,
                    //};
                    //_repository.Add(profile);
                    //_repository.SaveChanges();
                    //if (img != null)
                    //{
                    //    _mediaService.AttachMediaToEntity(new List<MediaVm> {new MediaVm() {Id = img.Id} },
                    //        user.Id, UploadType.Avatar);
                    //}
                    CreateProfile(confirmModel, user.Id);
                    var ident = _appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    _authManager.SignOut();
                    _authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
                }
                else
                {
                    return ServiceResult.ErrorResult<SignInResult>(smsResult.ErrorMessage);
                }
            }
            else
            {
                return ServiceResult.ErrorResult<SignInResult>("Не найден пользователь".Resource(this));
            }
            var signInModel = new SignInResult
            {
                Id = user.Id,
                Avatar = user.Profile.Avatar,
                Name = user.FullName()
            };
            return ServiceResult.SuccessResult(signInModel);
        }

        private void CreateProfile(RegistrationConfirm confirmModel, int userId)
        {
            var img = _repository.Find<UserAvatarPhoto>(confirmModel.ImgId);
            var profile = new Profile
            {
                Id = userId,
                Lang = LanguageHelper.GetCurrentCulture(),
                Avatar = img != null ? img.Url : DefaultAvatarUrl,
                ReadedNews = _cookiesService.GetReadedNews(),
                FirstName = confirmModel.Name,
                LastName = confirmModel.LastName,
                Sex = confirmModel.Gender,
                Experience = confirmModel.SportTime,
                BirthDate = confirmModel.BirthDay,
                IsTrial = true,
                LastPaymentDate = DateTime.Now,
                LastPaidDaysCount = TrialDays,
            };
            _repository.Add(profile);
            _repository.SaveChanges();
            if (img != null)
            {
                _mediaService.AttachMediaToEntity(new List<MediaVm> {new MediaVm() {Id = img.Id} },
                    userId, UploadType.Avatar);
            }
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
            var usera = _repository.GetAll<AppUser>();
            var user = _appUserManager.FindByName(phone);
            if (user == null)
            {
                result.ErrorMessage = "Пользователь не найден".Resource(this);
                return result;
            }
            return _smsService.GenerateAndSendCode(user.Id, user.UserName);
        }

        public ServiceResult RestorePasswordConfirm(RestorePasswordInfo confirmModel)
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
            var result = new ServiceResult();
            if (!IsValidPhone(phone))
            {
                result.ErrorMessage = "Номер телефона должен содержать только цифры в формате <код страны><номер> без сивола \"+\".".Resource(this);
                result.Success = false;
                return result;
            }
            if (UserExist(phone))
            {
                ServiceResult.ErrorResult("Пользователь с указанным номером телефона уже зарегистрирован");
            }
            return _smsService.GenerateAndSendCode(_currentUser.UserId, _currentUser.Phone);
        }

        private bool UserExist(string phone)
        {
            var user = _appUserManager.FindByName(phone);
            if (user != null && user.PhoneNumberConfirmed)
                return true;
            return false;
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
            var ident = _appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            _authManager.SignOut();
            _authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
            var result = new ServiceResult {Success = true};
            return result;
        }

        public ServiceResult LogOut()
        {
            var result = new ServiceResult {Success = false};
            if (!_currentUser.IsAnonimous && _currentUser.UserId > 0)
            {
                _authManager.SignOut();
                result.Success = true;
                return result;
            }
            result.ErrorMessage = "Ошибка".Resource(this);
            return result;
        }

        public ServiceResult<ImageUploadResult> RemoveAvatar()
        {
            //var userProfile = _repository.Find<Profile>(_currentUser.UserId);
            //userProfile.Avatar = DefaultAvatarUrl;
            _currentUser.User.Profile.Avatar = DefaultAvatarUrl;
            _repository.SaveChanges();
            return new ServiceResult<ImageUploadResult>
            {
                Success = true,
                Result = new ImageUploadResult
                {
                    Url = DefaultAvatarUrl
                }
            };
        }

        public ServiceResult ResendSmsCode(string phone)
        {
            var user = _repository
                .Queryable<AppUser>()
                .SingleOrDefault(u => u.UserName == phone);
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

        public string GetPhoneForSettings()
        {
            string phone = _currentUser.Phone;
            string phoneCountryPrefix = phone.Substring(0, phone.Length - 10);
            string phonePart = phone.Substring(phone.Length - 10, phone.Length - 3);
            string phoneEnd = phone.Substring(phone.Length - 2, 2);
            foreach (char dig in "0123456789")
            {
                phonePart = phonePart.Replace(dig, '*');
            }
            return string.Concat(phoneCountryPrefix, phonePart, phoneEnd);
        }

        public ServiceResult<SignInResult> RegisterExternal(ExternalRegistrationModel external, RegisterType registerType, string externalId, string externalEmail)
        {
            var extUser = ExternalUser(registerType, externalId);
            var user = new AppUser()
            {
                UserName = extUser,
                Email = externalEmail,
                PhoneNumberConfirmed = false,
                Status = UserStatus.Normal,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                RegisterType = (int)registerType
            };
            var createUserResult = _appUserManager.Create(user);
            if (!createUserResult.Succeeded)
            {
                return ServiceResult.ErrorResult<SignInResult>("Ошибка при регистрации".Resource(this));
            }
            CreateProfile(external, user.Id);
            _authManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            _authManager.SignOut();
            var identity = _appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            _authManager.SignIn(identity);
            var signInResult = new SignInResult
            {
                Id = user.Id,
                Name = user.FullName(),
                Avatar = user.Profile.Avatar
            };
            return ServiceResult.SuccessResult(signInResult);
        }

        private string ExternalUser(RegisterType registerType, string extId)
        {
            return string.Format("{0}-{1}", (int)registerType, extId);
        }

        //public ExternalLoginInfo GetExternalLogin()
        //{
        //    var loginInfo = _authManager.GetExternalLoginInfoAsync().Result;
        //}

        private bool IsValidPhone(string phone)
        {
            if (phone.Length == 11 && phone[0] == '8')
            {
                return false;
            }
            return true;
        }
    }

    public class ExternalLoginInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class SignInResult
    {
        //SignInResult()
        //{
            
        //}
        public int Id { get; set; } 

        public string Avatar { get; set; } 

        public string Name { get; set; } 
    }
}