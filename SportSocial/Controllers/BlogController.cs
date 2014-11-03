using System.Web.Mvc;
using BLL.Blog;
using BLL.Blog.ViewModels;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    //[Authorize]
    public class BlogController :SportSocialControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            var blogModel = new CreatePostModel()
            {
                Rubrics = _blogService.GetRubrics(),
            };
            return View(blogModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult New(CreatePostModel createPostModel)
        {
            if (ModelState.IsValid)
                return Json(_blogService.CreatePost(createPostModel));
            return Json(new {success = false});
        }

        public ActionResult Item()
        {
            return View();
        }
	}
}