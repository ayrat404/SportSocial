using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DAL.Migrations;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using Social.App_Start;

namespace Social
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(NinjectWebCommon.bootstrapper.Kernel);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            var conf = new Configuration();
            DbMigrator migrator = new DbMigrator(conf);
            migrator.Update();
        }
    }
}
