using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using BLL.Common.Services.CurrentUser;
using Knoema.Localization.Mvc;

namespace BLL.Infrastructure
{
    public abstract class SportSocialWebViewPage : LocalizedWebViewPage
    {
        public ICurrentUser CurrentUser
        {
            get
            {
                return DependencyResolver.Current.GetService<ICurrentUser>() ??
                       (ICurrentUser)
                           GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof (ICurrentUser));
            }
        }
    }

    public abstract class SportSocialWebViewPage<TModel> : LocalizedWebViewPage<TModel>
    {
        public ICurrentUser CurrentUser
        {
            get
            {
                return DependencyResolver.Current.GetService<ICurrentUser>();
            }
        }
    }
}