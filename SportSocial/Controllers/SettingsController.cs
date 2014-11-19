using System.Web.Mvc;
using BLL.Login;
using BLL.Login.ViewModels;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class SettingsController : SportSocialControllerBase
    {
        private readonly ILoginService _loginService;

        public SettingsController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword(ChangePaswdModel chPaswdModel)
        {
            return Json(_loginService.ChangePassword(chPaswdModel));
        }

        [HttpPost]
        public ActionResult RequestCode(string phone)
        {
            return Json(_loginService.ChangePhone(phone));
        }

        [HttpPost]
        public ActionResult ChangePhone(ChangePhoneModel chPhoneModel)
        {
            return Json(_loginService.ChangePhoneConfirm(chPhoneModel));
        }
    }
}