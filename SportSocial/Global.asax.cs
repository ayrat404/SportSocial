using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using BLL.Infrastructure.Map;
using DAL.Migrations;
using SportSocial.Knoema;

namespace SportSocial
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            BundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);
            GlobalFilters.Filters.Add(new ErrorHandlerAttr());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            CreateMaps.Register();
            LocalizationConfig.RegisterBinding();

            var conf = new Configuration();
            DbMigrator migrator = new DbMigrator(conf);
            migrator.Update();
        }
    }
}
