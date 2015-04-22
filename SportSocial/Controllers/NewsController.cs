using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using BLL.Blog;
using BLL.Bonus;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    [Authorize]
    public class NewsController :SportSocialControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;
        private readonly IBonusService _bonusService;

        public NewsController(IBlogService blogService, IRepository repository, IBonusService bonusService)
        {
            _blogService = blogService;
            _repository = repository;
            _bonusService = bonusService;
        }

        [HttpGet]
        public ActionResult Index()
        { 
            return View();
        }

        // надо модель нормальную засунуть
        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult Item(int id)
        //{
        //    return View("~/Views/Blog/Shared/Partials/Article/Info.cshtml", _blogService.GetPost(id));
        //}
	}
}