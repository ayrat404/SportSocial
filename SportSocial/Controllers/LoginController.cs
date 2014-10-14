using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.LoginService;
using DAL;
using DAL.DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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

        public LoginController(AppUserManager appUserManager, AppRoleManager appRoleManager, IAuthenticationManager authManager, ILoginService loginService)
        {
            _appUserManager = appUserManager;
            _appRoleManager = appRoleManager;
            _authManager = authManager;
            _loginService = loginService;
        }

        public ActionResult Index()
        {
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
                _authManager.SignIn(new AuthenticationProperties { IsPersistent =  true }, ident);
                return Json(new {success = true, ReturnUrl = ""}, "application/json");
            }
            else
                return Json(new {success = false, ErrorMessage = "Не введены немер телефона или пароль"}, jsonContentType);
        }

        [HttpPost]
        public ActionResult Register(RegistratioinModel model, string url = "")
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Name = model.Name,
                    PhoneNumber = model.Phone,
                    UserName = model.Phone,
                    PhoneNumberConfirmed = false,
                };
                var result = _appUserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    ClaimsIdentity ident =
                        _appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    _authManager.SignOut();
                    _authManager.SignIn(new AuthenticationProperties { IsPersistent =  true }, ident);
                    return Json(new { success = true, returnUrl = url, canResendSms = 40});
                }
                else
                    return Json(new {success = false, errorMessage = "Ошибка при регистрации"});
            }   
            else
            {
                return Json(new {success = false, ErrorMessage = "Не введены немер телефона или пароль"}, jsonContentType);
            }
        }

        [HttpGet]
        public async Task<ActionResult> ConfirmCode(int code)
        {
            if (code == 1111)
            {
                var user = await _appUserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    user.PhoneNumberConfirmed = true;
                    return Json(new {success = true});
                }
                else
                    return Json(new {success = true, ErrorMessage = "Не найден пользователь"});
            }
            else
                return Json(new {success = true, ErrorMessage = "Не верный код"});
        }

        [HttpPost]
        public async Task<ActionResult> FinalRegistration(string code)
        {
            throw new NotImplementedException();
        }
    }
}