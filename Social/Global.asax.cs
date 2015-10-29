using System.Data.Entity.Migrations;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BLL.Infrastructure.Localization;
using BLL.Infrastructure.Map;
using DAL.Migrations;
using Knoema.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Ninject.Web.WebApi;
using Social.App_Start;
using Social.Models;

namespace Social
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(NinjectWebCommon.bootstrapper.Kernel);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var services = GlobalConfiguration.Configuration.Services;
            //GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var bodyValidator = services.GetBodyModelValidator();
            //services.Replace(typeof(IBodyModelValidator), new PrefixlessBodyModelValidator(GlobalConfiguration.Configuration.Services.GetBodyModelValidator())); 
            services.Replace(typeof(IBodyModelValidator), new PrefixlessBodyModelValidator());
            LocalizationManager.Repository = new LocalizationRepository();

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter {CamelCaseText = true});

            CreateMaps.Register();

            var conf = new Configuration();
            DbMigrator migrator = new DbMigrator(conf);
            migrator.Update();
        }
    }
}
