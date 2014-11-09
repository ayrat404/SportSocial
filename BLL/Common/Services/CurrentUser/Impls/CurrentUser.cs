using System.Web;
using BLL.Infrastructure.IdentityConfig;
using DAL.DomainModel;
using Microsoft.AspNet.Identity;

namespace BLL.Common.Services.CurrentUser.Impls
{
    public class CurrentUser: ICurrentUser
    {
        private readonly AppUserManager _appUserManager;

        public CurrentUser(AppUserManager appUserManager)
        {
            _appUserManager = appUserManager;
        }

        private AppUser _user;

        public string UserName
        {
            get { return HttpContext.Current.User.Identity.GetUserName(); }
        }

        public bool IsAnonimous { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool IsInRole(string role)
        {
            return true;
        }

        public string UserId
        {
            get
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }

        public AppUser User
        {
            get
            {
                if (_user != null)
                    return _user;
                _user = _appUserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
                return _user;
            }
        }
    }
}