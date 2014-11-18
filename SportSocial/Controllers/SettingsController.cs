using System.Web.Mvc;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    public class SettingsController : SportSocialControllerBase
    {
        
        
        public ActionResult Index()
        {
            return View();
        }

    }
}