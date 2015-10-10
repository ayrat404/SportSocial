using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using BLL.Infrastructure.IdentityConfig;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Social
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(GetUserManager);
            app.CreatePerOwinContext(GetRoleManager);
            var cookieOptions = new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //CookieDomain = ".fortress.club",
            };
            #if !DEBUG
            cookieOptions.CookieDomain = ".fortress.club";
            #endif
            app.UseCookieAuthentication(cookieOptions);
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseVkontakteAuthentication(
                appId: "5097824",
                appSecret: "hozcd1wthAk4ZlGGBMTI", 
                scope: "email"
            );
            app.UseGoogleAuthentication(
                clientId: "862601964196-1u3hup1l410d0ua9p4uj42a1m8n2eqvv.apps.googleusercontent.com",
                clientSecret: "M1_DIN4nDytKfKyjPezZc96n"
           );
        }

        private AppUserManager GetUserManager()
        {
            var r = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof (AppUserManager)) as
                    AppUserManager;
            return r;
        }

        private AppRoleManager GetRoleManager()
        {
            return GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof (AppRoleManager)) as
                    AppRoleManager;
        }
    }
}