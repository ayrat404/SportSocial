using System.Web.Optimization;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;

namespace Social
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            // assets
            // ---------------
            bundles.Add(new ScriptBundle("~/bundles/social/assets")
                .Include(
                    "~/Scripts/libs/jquery/jquery-2.0.3.js",
                    "~/Scripts/libs/nprogress/nprogress.js",
                    "~/Scripts/libs/angular/angular.js",
                    "~/Scripts/libs/angular/i18n/angular-locale_ru-ru.js",
                    //"~/Scripts/libs/angular/angular-mocks.js",
                    //"~/Scripts/libs/angular/angular-cookies.js",
                    "~/Scripts/libs/angular-ui-router/angular-ui-router.js",
                    //"~/Scripts/libs/bootstrap/modal.js",
                    //"~/Scripts/libs/bootstrap/collapse.js",
                    //"~/Scripts/libs/bootstrap/transition.js",
                    //"~/Scripts/libs/bootstrap/tab.js",
                    //"~/Scripts/libs/bootstrap/tooltip.js",
                    "~/Scripts/libs/angular-ui-bootstrap/ui-bootstrap-tpls-0.13.3.js",
                    "~/Scripts/libs/fotorama/fotorama.js",
                    "~/Scripts/libs/ng-flow/ng-flow-standalone.js",
                    "~/Scripts/libs/angular-storage/angular-storage.js"));

            // social with coffee
            // ---------------
            bundles.Add(new ScriptBundle("~/bundles/social/scripts")
                // include shared
                // ---------------
                .IncludeDirectory("~/Scripts/app/javascript/shared", "*.js", true)
                // include appServices
                // ---------------
                .IncludeDirectory("~/Scripts/app/javascript/appServices", "*.js", true)
                // include socialApp
                // ---------------
                .IncludeDirectory("~/Scripts/app/javascript/socialApp", "*.js", true)
                // include main app
                // ---------------
                .Include("~/Scripts/app/javascript/app.js")
                );

            // social
            // ---------------
            //bundles.Add(new ScriptBundle("~/bundles/social/scripts")
            //    // include shared
            //    // ---------------
            //    .IncludeDirectory("~/Scripts/app/simple/shared", "*.js", true)
            //    // include appServices
            //    // ---------------
            //    .IncludeDirectory("~/Scripts/app/simple/appServices", "*.js", true)
            //    // include socialApp
            //    // ---------------
            //    .IncludeDirectory("~/Scripts/app/simple/socialApp", "*.js", true)
            //    // include main app
            //    // ---------------
            //    .Include("~/Scripts/app/simple/app.js")
            //    );

            // optimization
            // ---------------
#if !DEBUG
                BundleTable.EnableOptimizations = true;
#endif
        }
    }
}