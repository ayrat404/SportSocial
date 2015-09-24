using System.Data.Entity;
using System.Linq;
using System.Web;
using BLL.Common.Helpers;
using BLL.Common.Services.Cookies;
using DAL.DomainModel;
using DAL.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Common.Services.CurrentUser.Impls
{
    public class CurrentUser: ICurrentUser
    {
        private readonly IRepository _repository;
        private readonly ICookiesService _cookiesService;

        public CurrentUser(IRepository repository, ICookiesService cookiesService)
        {
            _repository = repository;
            _cookiesService = cookiesService;
            int userId = HttpContext.Current.User.Identity.GetUserId<int>();
            User = repository
                .Queryable<AppUser>()
                .Include(u => u.Profile)
                .SingleOrDefault(u => u.Id == userId);
        }

        public string UserName
        {
            get { return User.Name; }
        }

        public string Phone
        {
            get { return HttpContext.Current.User.Identity.GetUserName(); }
        }

        public bool IsAnonimous
        {
            get { return !HttpContext.Current.User.Identity.IsAuthenticated; }
        }

        public bool IsPaid
        {
            get
            {
                if (User != null)
                    return User.Profile.IsPaid;
                else
                    return false;
            }
        }

        public bool IsAdmin
        {
            get { return IsInRole("Admin"); }
        }

        public bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        public int UserId
        {
            get
            {
                return HttpContext.Current.User.Identity.GetUserId<int>();
            }
        }

        public AppUser User { get; private set; }
        //{
        //    get
        //    {
        //        if (_user != null)
        //            return _user;
        //        _user = _repository.Find<AppUser>(HttpContext.Current.User.Identity.GetUserId());
        //        return _user;
        //    }
        //}

        public int UnreadedNews
        {
            get
            {
                if (!IsAnonimous)
                    return ApplicationStateHelper.NewsCount - User.Profile.ReadedNews;
                if (_cookiesService.ExistReadedNews())
                    return ApplicationStateHelper.NewsCount - _cookiesService.GetReadedNews();
                _cookiesService.SetReadedNews(ApplicationStateHelper.NewsCount - 1);
                return ApplicationStateHelper.NewsCount - 1;
            }
        }

        public void ReadAllNews()
        {
            if (!IsAnonimous)
                User.Profile.ReadedNews = ApplicationStateHelper.NewsCount;
            else
            {
                _cookiesService.SetReadedNews(ApplicationStateHelper.NewsCount);
            }
            _repository.SaveChanges();
        }
    }
}