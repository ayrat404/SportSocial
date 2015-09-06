﻿using System.Web.Http;
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
                LoginPath = new PathString("/"),
                //CookieDomain = ".fortress.club",
            };
            #if !DEBUG
            cookieOptions.CookieDomain = ".fortress.club";
            #endif
            app.UseCookieAuthentication(cookieOptions);
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