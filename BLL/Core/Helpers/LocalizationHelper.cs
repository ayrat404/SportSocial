using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Common.Helpers
{
    public class LanguageHelper
    {
        public static string GetCurrentCulture()
        {
            var cultureFromCookie = CookieHelper.GetValue("current-lang");
            if (!string.IsNullOrEmpty(cultureFromCookie))
                return cultureFromCookie;

            string lang;

            var user = HttpContext.Current.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                var accountRepo = DependencyResolver.Current.GetService<IAccountRepository>();
                lang = accountRepo.GetUserLanguage(user.Identity.GetUserId());
                if (!string.IsNullOrEmpty(lang))
                    return lang;
            }
            if (HttpContext.Current.Request.UserLanguages != null)
                lang = HttpContext.Current.Request.UserLanguages.First();
            else
                lang = "en-US";
            return lang;
        }
    }
}