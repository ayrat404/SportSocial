using System.Web.Mvc;
using BLL.Blog;
using BLL.Blog.ViewModels;
using BLL.Comments.Objects;
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
        public ActionResult Index(int? page)
        {
            //PostListViewModel model = new PostListViewModel
            //{
                
            //}
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

        public ActionResult Item(int id)
        {
            return View(_blogService.GetPost(id));
        }

        [HttpPost]
        public ActionResult Rating(BlogRatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                //_blogService.RaitBlog(model);
                return Json(new {Success = true});
            }
            return Json(new {Success = false});
        }

        [HttpPost]
        public JsonResult Comment(CreateCommentViewModel createCommentViewModelModel)
        {
            if (ModelState.IsValid)
            {
                var comment = _blogService.AddComment(createCommentViewModelModel);
                return Json(new {Success = true, Comment = comment});
            }
            return Json(new {Success = false});
        }

        [HttpGet]
        public JsonResult LoadComments(int id)
        {
            return Json(_blogService.LoadComments(id), JsonRequestBehavior.AllowGet);
        }
	}
}