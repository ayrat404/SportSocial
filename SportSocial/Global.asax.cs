using System.Web.Mvc;
using System.Web.Routing;

namespace SportSocial
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);

            LocalizationConfig.RegisterBinding();
        }
    }
}
