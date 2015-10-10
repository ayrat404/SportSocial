using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Social
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Templates",
                url: "template/{*path}",
                defaults: new { controller = "Template", action = "Index" }
            );

            routes.MapRoute(
                name: "ExternalLogin",
                url: "login/external",
                defaults: new { controller = "Home", action = "ExternalLogin" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{*route}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
