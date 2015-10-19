using System.Web.Mvc;
using BLL.Infrastructure.IdentityConfig;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace SportSocial
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<AppUserManager>());
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<AppRoleManager>());

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/"),
                #if !DEBUG
                CookieDomain = ".fortress.club"
                #endif
                //CookieDomain = ".fortress.club"
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseGoogleAuthentication(
                clientId: "862601964196-1u3hup1l410d0ua9p4uj42a1m8n2eqvv.apps.googleusercontent.com",
                clientSecret: "M1_DIN4nDytKfKyjPezZc96n"
           );
        }
    }
}