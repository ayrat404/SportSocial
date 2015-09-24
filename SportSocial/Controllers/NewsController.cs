using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using BLL.Blog;
using BLL.Blog.ViewModels;
using BLL.Bonus;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using PagedList;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    //[Authorize]
    public class NewsController :SportSocialControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IRepository _repository;
        private readonly IBonusService _bonusService;

        public NewsController(IBlogService blogService, IRepository repository, IBonusService bonusService)
        {
            _blogService = blogService;
            _repository = repository;
            _bonusService = bonusService;
        }

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var pageSize = 10;
            var posts = _blogService.GetNews(pageSize, page);
            var pageList = new StaticPagedList<PostPreviewModel>(posts.PostPreview, page, pageSize,
                posts.PageInfo.Count);
            return View(pageList);
        }

         //надо модель нормальную засунуть
        [HttpGet]
        //[AllowAnonymous]
        public ActionResult Item(int id)
        {
            return View("~/Views/Blog/Item.cshtml", _blogService.GetPost(id));
        }
	}
}