using System.Web.Mvc;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    public class BlogController : SportSocialControllerBase
    {
        //
        // GET: /Blog/
        public ActionResult Index()
        {
            return View();
        }
	}
}