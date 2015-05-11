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
    public class ReviewsController :SportSocialControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;
        private readonly IBonusService _bonusService;

        public ReviewsController(IBlogService blogService, IRepository repository, IBonusService bonusService)
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
	}
}