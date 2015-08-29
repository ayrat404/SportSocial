using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Common.Objects;
using BLL.Login;
using BLL.Login.ViewModels;
using Knoema.Localization;
using SportSocial.Controllers.Base;
using WebGrease.Css.Extensions;

namespace SportSocial.Controllers
{
    [Authorize]
    public class LoginController : SportSocialControllerBase
    {
        private const string jsonContentType = "application/json";

        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public ActionResult Index()
        {
            //return Content("adfsfsd");
            return View();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            _loginService.LogOut();
            return Redirect("/");
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult SignIn()
        //{
        //    return View();
        //}

        [HttpPost]
        [AllowAnonymous]
        [CustomAntiForgeryValidator]
        public JsonResult SignIn(SignInModel model, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                return Json(_loginService.SignIn(model, returnUrl));
            }
            return Json(new {success = false, ErrorMessage = GetModelStateErrors(), Redirect = returnUrl}, jsonContentType);
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomAntiForgeryValidator]
        public JsonResult Register(RegistratioinModel model, string url = "/")
        {
            if (ModelState.IsValid)
            {
                return Json(_loginService.PreRegister(model, url));
            }   
            return Json(new {success = false, ErrorMessage = GetModelStateErrors()}, jsonContentType);
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomAntiForgeryValidator]
        public JsonResult ConfirmPhone(ConfirmSmsCode confirmModel)
        {
            if (ModelState.IsValid)
            {
                return Json(_loginService.ConfirmSmsCode(confirmModel));
            }
            return Json(new { success = false, errorMessage = GetModelStateErrors() });
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomAntiForgeryValidator]
        public JsonResult ResendCode(string phone)
        {
            return Json(_loginService.ResendSmsCode(phone));
        }

        //[HttpPost]
        //public async Task<ActionResult> FinalRegistration(string code)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RestorePassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomAntiForgeryValidator]
        public ActionResult RestorePassword(string phone)
        {
            return Json(_loginService.RestorePassword(phone));
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomAntiForgeryValidator]
        public ActionResult RestorePasswordConfirm(ConfirmSmsCode confirmModel)
        {
            if (ModelState.IsValid)
                return Json(_loginService.RestorePasswordConfirm(confirmModel));
            return
                Json(new ServiceResult
                {
                    Success = false,
                    ErrorMessage = "Одно или несколько полей заполнены неверно".Resource(this)
                });
        }



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