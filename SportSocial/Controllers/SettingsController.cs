using System.Web.Mvc;
using BLL.Common.Services.CurrentUser;
using BLL.Login;
using BLL.Login.ViewModels;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class SettingsController : SportSocialControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ICurrentUser _currentUser;

        public SettingsController(ILoginService loginService, ICurrentUser currentUser)
        {
            _loginService = loginService;
            _currentUser = currentUser;
        }

        public ActionResult Index()
        {
            var model = new SettingsModel();
            model.Avatar = _currentUser.User.Profile.Avatar;
            string phone = _currentUser.Phone;
            string phoneCountryPrefix = phone.Substring(0, phone.Length - 10);
            string phonePart = phone.Substring(phone.Length - 10, phone.Length - 3);
            string phoneEnd = phone.Substring(phone.Length - 2, 2);
            foreach (char dig in "0123456789")
            {
                phonePart = phonePart.Replace(dig, '*');
            }
            model.Phone = string.Concat("+", phoneCountryPrefix, phonePart, phoneEnd);
            return View(model);
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

    public class SettingsModel
    {
        public string Avatar { get; set; } 
        public string Phone { get; set; }
    }
}