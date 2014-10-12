using System.Web.Mvc;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace SportSocial
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            //app.CreatePerOwinContext<EntityDbContext>(EntityDbContext.Create);
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<AppUserManager>());
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<AppRoleManager>());

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login"),
            });
        }
    }
}