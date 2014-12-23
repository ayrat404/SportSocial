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
    public class BlogController :SportSocialControllerBase
    {
        private readonly IBlogService _blogService;
        
        private const int PageSize = 10;

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
            var pageList = new StaticPagedList<PostPreviewModel>(posts.PostPreview, page, PageSize,
                posts.PageInfo.Count);
            return View(pageList);
        }

        [HttpGet]
        public ActionResult New()
        {
            var blogModel = new PostCreateModel()
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
        [CustomAntiForgeryValidator]
        public ActionResult New(PostCreateModel createPostModel)
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
            var postVm = _blogService.GetEditModel(id);
            if (postVm == null)
                return RedirectToAction("Index");
            return View(postVm);
        }

        [HttpPost]
        [CustomAntiForgeryValidator]
        public ActionResult Edit(PostEditModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(_blogService.EditPost(model));
                //TODO редирект на страницу своих постов
            }
            return Json(new ServiceResult {Success = false, ErrorMessage = GetModelStateErrors()});
        }

        [HttpGet]
        public ActionResult My(int page = 1)
        {
            var myPosts = _blogService.MyPosts(PageSize, page);
            var pageList = new StaticPagedList<PostPreviewModel>(myPosts.PostPreview, page, PageSize,
                myPosts.PageInfo.Count);
            return View("UserArticles", pageList);
        }

        [AllowAnonymous]
        [ChildActionOnly]
        [HttpGet]
        public ActionResult FortressPosts()
        {
            var posts = _blogService.OnMainPosts();
            return View(posts);
        }
	}
}