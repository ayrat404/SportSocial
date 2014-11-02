using System.Web.Mvc;
using BLL.Blog;
using DAL.DomainModel.EnumProperties;
using SportSocial.Controllers.Base;
using WebGrease.Css.Extensions;

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

        [HttpPost]
        public JsonResult GetArticles(BlogPostStatus? status, string query = null)
        {
            var posts = _blogService.GetPostsForAdmin(status, query);
            posts.ForEach(p => p.Url = Url.Action("Index", "Blog", new {id = p.Id}));
            return Json(posts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void ChangeArticleStatus(int id, int status)
        {
            _blogService.ChangeStatus(id, status);
        }
	}
}