using System.Web;
using System.Web.Mvc;
using BLL.Blog;
using BLL.Blog.ViewModels;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class AdminController :SportSocialControllerBase
    {
        private readonly IBlogService _blogService;

        public AdminController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
	}
}