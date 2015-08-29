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
                return DependencyResolver.Current.GetService<ICurrentUser>();
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