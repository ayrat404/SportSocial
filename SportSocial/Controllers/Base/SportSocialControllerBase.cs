using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using BLL.Common.Helpers;
using Knoema.Localization;

namespace SportSocial.Controllers.Base
{
    public class SportSocialControllerBase: Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentUserCulture = new CultureInfo(LocalizationHelper.GetCurrentCulture());
            LocalizationManager.Instance.SetCulture(currentUserCulture);
            Thread.CurrentThread.CurrentCulture = currentUserCulture;
            Thread.CurrentThread.CurrentUICulture = currentUserCulture;
            base.OnActionExecuting(filterContext);
        }
    }
}