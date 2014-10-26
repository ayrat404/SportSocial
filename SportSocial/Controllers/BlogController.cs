using System.Web.Mvc;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    public class BlogController :SportSocialControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult New()
        {
            return View();
        }
	}
}