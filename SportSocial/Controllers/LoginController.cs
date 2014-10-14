using System.Web.Mvc;
using BLL.LoginService;
using DAL;
using Microsoft.Owin.Security;

namespace SportSocial.Controllers
{
    public class LoginController : Controller
    {
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
	}
}