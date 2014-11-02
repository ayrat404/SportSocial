using System.Web.Mvc;
using BLL.Admin.Conference;
using BLL.Admin.Conference.ViewModels;
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
        private readonly IConferenceService _conferenceService;

        public AdminController(IBlogService blogService, IConferenceService conferenceService)
        {
            _blogService = blogService;
            _conferenceService = conferenceService;
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

        [HttpGet]
        public ActionResult GetConferences()
        {
            var model = _conferenceService.GetAll();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(CreateConfModel model)
        {
            if (!ModelState.IsValid)
                return Json(new {success = false});
            _conferenceService.Create(model);
            return Json(new {success = true});
        }

        [HttpPost]
        public ActionResult Edit(ConfModel model)
        {
            if (!ModelState.IsValid)
                return Json(new {success = false});
            _conferenceService.Edit(model);
            return Json(new {success = true});
        }
	}
}