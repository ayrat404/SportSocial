using System.Web.Mvc;
using BLL.Blog;
using BLL.Blog.ViewModels;
using PagedList;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class BlogController :SportSocialControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(int page = 1, PostSortType sort = PostSortType.Last, int rubric = 0)
        {
            ViewBag.HidePromo = Request.Params.Count > 0;
            int pageSize = 2;
            var posts = _blogService.GetPosts(pageSize, sort, rubric, page);
            var pageList = new StaticPagedList<PostPreviewViewModel>(posts.PostPreview, page, pageSize,
                posts.PageInfo.Count);
            return View(pageList);
        }

        [HttpGet]
        public ActionResult UserArticles(long id)
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
        //[AllowAnonymous]
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_blogService.GetEditModel(id));
        }

        [HttpPost]
        public ActionResult Edit(CreatePostModel model)
        {
            if (ModelState.IsValid)
            {
                _blogService.EditPost(model);
                //TODO редирект на страницу своих постов
            }
            return View(model);
        }


        //[HttpPost]
        //public ActionResult Rating(BlogRatingViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //_blogService.RaitBlog(model);
        //        return Json(new {Success = true});
        //    }
        //    return Json(new {Success = false});
        //}

        //[HttpPost]
        //public JsonResult Comment(CreateCommentViewModel createCommentViewModelModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var comment = _blogService.AddComment(createCommentViewModelModel);
        //        return Json(new {Success = true, Comment = comment});
        //    }
        //    return Json(new {Success = false});
        //}

        //[HttpGet]
        //public JsonResult LoadComments(int id)
        //{
        //    return Json(_blogService.LoadComments(id), JsonRequestBehavior.AllowGet);
        //}
	}
}