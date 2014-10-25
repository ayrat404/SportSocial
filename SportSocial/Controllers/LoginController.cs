using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Infrastrcture.IdentityConfig;
using BLL.Login;
using BLL.Login.ViewModels;
using Microsoft.Owin.Security;
using SportSocial.Controllers.Base;
using WebGrease.Css.Extensions;

namespace SportSocial.Controllers
{
    public class LoginController : SportSocialControllerBase
    {
        private const string jsonContentType = "application/json";

        private AppUserManager _appUserManager;
        private IAuthenticationManager _authManager;
        private ILoginService _loginService;

        public LoginController(AppUserManager appUserManager, IAuthenticationManager authManager, ILoginService loginService)
        {
            _appUserManager = appUserManager;
            _authManager = authManager;
            _loginService = loginService;
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
                return Json(_loginService.SignIn(model));
            }
            return Json(new {success = false, ErrorMessage = GetErrors()}, jsonContentType);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistratioinModel model, string url = "")
        {
            if (ModelState.IsValid)
            {
                return Json(_loginService.PreRegister(model, url));
            }   
            return Json(new {success = false, ErrorMessage = GetErrors()}, jsonContentType);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmPhone(ConfirmSmsCode confirmModel)
        {
            if (ModelState.IsValid)
            {
                return Json(_loginService.ConfirmSmsCode(confirmModel));
            }
            return Json(new { success = true, errorMessage = "Не валидные значения полей" });
        }

        //[HttpPost]
        //public async Task<ActionResult> ResendCode()
        //{
        //    throw new Exception();
        //}

        //[HttpPost]
        //public async Task<ActionResult> FinalRegistration(string code)
        //{
        //    throw new NotImplementedException();
        //}

        private Dictionary<string, List<string>> GetErrors()
        {
            var errors = new Dictionary<string, List<string>>();
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Any())
                {
                    errors[key] = new List<string>();
                    ModelState[key].Errors.ForEach(f => errors[key].Add(f.ErrorMessage));
                }
                    
            }
            return null;
        }
    }
}