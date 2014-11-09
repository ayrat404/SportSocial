using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using BLL.Common.Helpers;
using BLL.Common.Services.CurrentUser;
using Knoema.Localization;

namespace SportSocial.Controllers.Base
{
    public abstract class SportSocialControllerBase: Controller
    {
        protected readonly ICurrentUser CurrentUser;

        public SportSocialControllerBase(): this(DependencyResolver.Current.GetService<ICurrentUser>()) { }

        public SportSocialControllerBase(ICurrentUser currentUser)
        {
            CurrentUser = currentUser;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentUserCulture = new CultureInfo(LanguageHelper.GetCurrentCulture());
            LocalizationManager.Instance.SetCulture(currentUserCulture);
            Thread.CurrentThread.CurrentCulture = currentUserCulture;
            Thread.CurrentThread.CurrentUICulture = currentUserCulture;
            base.OnActionExecuting(filterContext);
        }

        protected override JsonResult Json(object data, string contentType, 
            Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}