using System.Linq;
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
        
        private const int PageSize = 2;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int page = 1, PostSortType sort = PostSortType.Last, int rubric = 0)
        {
            ViewBag.HidePromo = Request.QueryString.Count > 0;
            ViewBag.Sort = sort;
            ViewBag.Rubric = rubric;
            var currentRubric = _blogService.GetRubrics().SingleOrDefault(r => r.Id == rubric);
            ViewBag.RubricName = currentRubric != null ? currentRubric.Name : "";
            var posts = _blogService.GetPosts(PageSize, sort, rubric, page);
            //posts.PostPreview.ForEach(p => p.Text = Regex.Replace(p.Text, @"<[^>]+>|&nbsp;", "").Trim());
            var pageList = new StaticPagedList<PostPreviewViewModel>(posts.PostPreview, page, PageSize,
                posts.PageInfo.Count);
            return View(pageList);
        }

        //[HttpGet]
        //public ActionResult UserArticles(long id)
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult New()
        {
            var blogModel = new CreatePostModel()
            {
                Rubrics = _blogService.GetRubrics(),
            };
            return View(blogModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetRubrics(long id = 0)
        {
            var rubrics = _blogService.GetRubrics();
            ViewBag.Rubric = id;
            return View("Shared/Partials/Blocks/Menu", rubrics);
        }

        [HttpPost]
        //[AllowAnonymous]
        public ActionResult New(CreatePostModel createPostModel)
        {
            if (ModelState.IsValid)
                return Json(_blogService.CreatePost(createPostModel));
            return Json(new {success = false});
        }

        [HttpGet]
        [AllowAnonymous]
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

        [HttpGet]
        public ActionResult My(int page = 1)
        {
            var myPosts = _blogService.MyPosts(PageSize, page);
            var pageList = new StaticPagedList<PostPreviewViewModel>(myPosts.PostPreview, page, PageSize,
                myPosts.PageInfo.Count);
            return View("UserArticles", pageList);
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult FortressPosts()
        {
            var posts = _blogService.TopFortressPosts();
            //return Json(posts.PostPreview, JsonRequestBehavior.AllowGet);
            return View(posts.PostPreview);
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