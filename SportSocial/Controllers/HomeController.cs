using System.Web.Mvc;
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

        public ActionResult Feedback()
        {
            return View();
        }

    }
}