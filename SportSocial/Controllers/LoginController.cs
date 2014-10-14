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
                    return Json(new {Success = false, ErrorMessage = "Не верный логин или пароль"}, jsonContentType);
                }
                ClaimsIdentity ident =
                    await _appUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                _authManager.SignOut();
                _authManager.SignIn(new AuthenticationProperties { IsPersistent =  true }, ident);
                return Json(new {Success = true, ReturnUrl = ""}, "application/json");
            }
            else
                return Json(new {Success = false, ErrorMessage = "Не введены немер телефона или пароль"}, jsonContentType);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistratioinModel model, string url)
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
                var result = await _appUserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ClaimsIdentity ident =
                        await _appUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    _authManager.SignOut();
                    _authManager.SignIn(new AuthenticationProperties { IsPersistent =  true }, ident);
                    return Json(new {Success = true, returnUrl = url});
                }
                else
                    return Json(new {Success = false, errorMessage = "Ошибка при регистрации"});
            }   
            else
            {
                return Json(new {Success = false, ErrorMessage = "Не введены немер телефона или пароль"}, jsonContentType);
            }
        }

        [HttpGet]
        [ActionName("Register")]
        public async Task<ActionResult> ConfirmCode(int code)
        {
            if (code == 1111)
            {
                var user = await _appUserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    user.PhoneNumberConfirmed = true;
                    return Json(new {Success = true});
                }
                else
                    return Json(new {Success = true, ErrorMessage = "Не найден пользователь"});
            }
            else
                return Json(new {Success = true, ErrorMessage = "Не верный код"});
            
        }

        [HttpPost]
        [ActionName("Register")]
        public async Task<ActionResult> FinalRegistration(string code)
        {
            throw new NotImplementedException();
        }
    }
}