using System.Linq;
using System.Web.Mvc;
using BLL.Blog;
using BLL.Blog.ViewModels;
using BLL.Common.Objects;
using PagedList;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class BonusController :SportSocialControllerBase
    {
        private readonly IBlogService _blogService;

        public BonusController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

	}
}