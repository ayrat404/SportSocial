using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.LoginService;
using BLL.Sms;
using DAL;
using DAL.DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SportSocial.IdentityConfig;
using SportSocial.Models;

namespace SportSocial.Controllers
{
    public class LoginController : Controller
    {
        private const string jsonContentType = "application/json";

        private AppUserManager _appUserManager;
        private AppRoleManager _appRoleManager;
        private IAuthenticationManager _authManager;
        private ILoginService _loginService;
        private ISmsService _smsService;

        public LoginController(AppUserManager appUserManager, AppRoleManager appRoleManager, IAuthenticationManager authManager, ILoginService loginService, ISmsService smsService)
        {
            _appUserManager = appUserManager;
            _appRoleManager = appRoleManager;
            _authManager = authManager;
            _loginService = loginService;
            _smsService = smsService;
        }

        public ActionResult Index()
        {
            //return Content("adfsfsd");
            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _appUserManager.FindAsync(model.Phone, model.Password);
                if (user == null)
                {
                    return Json(new {success = false, ErrorMessage = "Не верный логин или пароль"}, jsonContentType);
                }
                ClaimsIdentity ident =
                    await _appUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                _authManager.SignOut();
                _authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
                return Json(new {success = true, ReturnUrl = ""}, "application/json");
            }
            else
                return Json(new {success = false, ErrorMessage = "Не введены немер телефона или пароль"}, jsonContentType);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistratioinModel model, string url = "")
        {
            if (ModelState.IsValid)
            {
                var user = await _appUserManager.FindByNameAsync(model.Phone);
                if (user != null && user.PhoneNumberConfirmed)
                    return Json(new { success = false, errorMessage = "Пользователь с указанным номером телефона уже зарегистрирован"});

                if (user == null)
                {
                    user = new AppUser
                    {
                        Name = model.Name,
                        PhoneNumber = model.Phone,
                        UserName = model.Phone,
                        PhoneNumberConfirmed = false,
                    };
                    var result = _appUserManager.Create(user);
                    if (!result.Succeeded)
                    {
                        return Json(new { success = false, errorMessage = "Ошибка при регистрации" });
                    }
                }
                var smsResult = _smsService.GenerateAndSendCode(user.Id);
                if (smsResult.Success)
                    return Json(new { success = true, retunUrl = url});
                else
                    return Json(new { success = false, errorMessage = smsResult.ErrorMessage});
            }   
            else
            {
                return Json(new {success = false, ErrorMessage = "Не введены немер телефона или пароль"}, jsonContentType);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmPhone(ConfirmSmsCode confirm)
        {
            var user = await _appUserManager.FindByNameAsync(confirm.Phone);
            if (user != null && !user.PhoneNumberConfirmed)
            {
                var result = _smsService.VerifyCode(user.Id, confirm.Code);
                if (result.Success)
                {
                    user.PhoneNumberConfirmed = true;
                    await _appUserManager.UpdateAsync(user);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, errorMessage = result.ErrorMessage });
                }
            }
            else
                return Json(new { success = true, ErrorMessage = "Не найден пользователь" });
        }

        [HttpPost]
        public async Task<ActionResult> ResendCode()
        {
            throw new Exception();
        }

        [HttpPost]
        public async Task<ActionResult> FinalRegistration(string code)
        {
            throw new NotImplementedException();
        }
    }
}