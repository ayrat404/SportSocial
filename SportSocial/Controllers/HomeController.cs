using System.Web.Mvc;
using BLL.Login;
using BLL.Login.ViewModels;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    public class HomeController : SportSocialControllerBase
    {

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Rules()
        {
            return View();
        }

    }
}