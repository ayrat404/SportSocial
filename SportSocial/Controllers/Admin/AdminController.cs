using System.Web.Mvc;
using BLL.Admin.Conference;
using BLL.Admin.Conference.ViewModels;
using BLL.Blog;
using DAL.DomainModel.EnumProperties;
using SportSocial.Controllers.Base;
using WebGrease.Css.Extensions;

namespace SportSocial.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController :SportSocialControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IConferenceService _conferenceService;

        public AdminController(IBlogService blogService, IConferenceService conferenceService)
        {
            _blogService = blogService;
            _conferenceService = conferenceService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetArticles(BlogPostStatus? status, string query = null)
        {
            var posts = _blogService.GetPostsForAdmin(status, query);
            posts.ForEach(p => p.Url = Url.Action("Item", "Blog", new {id = p.Id}));
            return Json(posts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeArticleStatus(int id, int status)
        {
            return Json(_blogService.ChangeStatus(id, status));
        }

        [HttpGet]
        public ActionResult GetConferences(int? id = null)
        {
            if (id.HasValue)
                return Json(_conferenceService.GetConf(id.Value), JsonRequestBehavior.AllowGet);
            else
                return Json(_conferenceService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateConference(CreateConfModel model)
        {
            if (!ModelState.IsValid)
                return Json(new {success = false});
            return Json(_conferenceService.Create(model));
        }

        [HttpPost]
        public JsonResult ChangeConferenceStatus(int id, ConfStatus status)
        {
            return Json(_conferenceService.ChangeStatus(id, status));
        }

        [HttpPost]
        public ActionResult EditConference(ConfModel model)
        {
            if (!ModelState.IsValid)
                return Json(new {success = false});
            return Json(_conferenceService.Edit(model));
        }
	}
}