using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebInterface.App_Start;

namespace SportSocial
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected override void RegisterApplicationBundles(BundleCollection bundles)
        {
            BundleConfig.RegisterBundles(bundles);
        }
    }
}
