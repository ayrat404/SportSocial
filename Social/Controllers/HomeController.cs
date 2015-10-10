using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using BLL.Infrastructure.IdentityConfig;
using BLL.Login;
using Microsoft.Owin.Security;
using Social.Models;

namespace Social.Controllers
{
    public class HomeController : Controller
    {
        private IAuthenticationManager _authManager;
        private AppUserManager _userManager;
        private ILoginService _loginService;

        public HomeController(AppUserManager userManager, ILoginService loginService)
        {
            _userManager = userManager;
            _loginService = loginService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ExternalLogin(string provider)
        {
            _authManager = HttpContext.GetOwinContext().Authentication;
            var loginInfo = _authManager.GetExternalLoginInfoAsync().Result;
            if (loginInfo == null)
            {
                return new ChallengeWebResult(provider);
            }
            if (loginInfo.Login.LoginProvider != provider)
            {
                return new ChallengeWebResult(provider);
            }
            var signInResult = _loginService.SignInExternal(RegisterTypeFromString(loginInfo.Login.LoginProvider),
                loginInfo.Login.ProviderKey);
            if (signInResult.Success)
            {
                return RedirectToAction("Index");
            }
            return Redirect("/registration/external");
        }

        private RegisterType RegisterTypeFromString(string provider)
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
