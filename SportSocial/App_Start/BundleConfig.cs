using System.Web.Optimization;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;

namespace SportSocial
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //стили
            //=======================================================
            var cssTransformer = new CssTransformer();
            var cssMinify = new CssMinify();
            var cssBundle = new StyleBundle("~/content/stylesCss1").Include(
                "~/Scripts/libs/datepicker/jquery.datetimepicker.css",
                "~/Content/styles/other/font-awesome.css",
                "~/Content/styles/other/animate.css",
                "~/Scripts/libs/jquery-noui/jquery.nouislider.min.css",
                "~/Scripts/libs/jquery-noui/jquery.nouislider.pips.min.css"
                );
            //cssBundle.Transforms.Add(cssTransformer);
            //cssBundle.Transforms.Add(cssMinify);
            bundles.Add(cssBundle);
            var lessBundle = new Bundle("~/content/stylesLess").Include(
                "~/Content/styles/style.less"
                );
            lessBundle.Transforms.Add(cssTransformer);
            lessBundle.Transforms.Add(cssMinify);
            bundles.Add(lessBundle);

            // библиотеки
            bundles.Add(new ScriptBundle("~/bundles/assets/scripts")
                   .Include(
                        "~/Scripts/libs/jquery/jquery-2.0.3.js",

                        "~/Scripts/libs/jquery-fileapi/config.js",
                        "~/Scripts/libs/jquery-fileapi/FileAPI.min.js",
                        "~/Scripts/libs/jquery-fileapi/FileAPI.exif.js",
                        "~/Scripts/libs/jquery-fileapi/jquery.fileapi.min.js",
                        
                        "~/Scripts/libs/bootstrap/tooltip.js",
                        "~/Scripts/libs/bootstrap/modal.js",
                        //"~/Scripts/libs/signalR/jquery.signalR-2.0.3.min.js",
                        //"~/Scripts/libs/signalR/hub.js",

                        "~/Scripts/libs/moment/moment.min.js",
                        "~/Scripts/libs/moment/ru.js",
                        "~/Scripts/libs/datepicker/jquery.datetimepicker.js",
                        "~/Scripts/libs/maskedinput/jquery.mask.min.js",
                        "~/Scripts/libs/jquery-noui/jquery.nouislider.all.min.js"
                    )
                    .Include(
                        "~/Scripts/libs/angular/angular.min.js",
                        "~/Scripts/libs/angular/angular-touch.min.js",
                        "~/Scripts/libs/text-angular/textAngular-sanitize.min.js",
                        "~/Scripts/libs/text-angular/textAngular.min.js",

                        "~/Scripts/app/shared/app.js"
                    )
                    .IncludeDirectory(
                        "~/Scripts/app/shared", "*.js", true
                    )
            );

            // блог
            bundles.Add(new ScriptBundle("~/bundles/blog/scripts")
                    .Include(
                        "~/Scripts/app/blog/app.js"
                    )
                    .IncludeDirectory(
                        "~/Scripts/app/blog", "*.js", true
                    )
            );

            // панель управления
            bundles.Add(new ScriptBundle("~/bundles/admin/scripts")
                    .Include(
                        "~/Scripts/libs/angular/angular-route.min.js",
                        "~/Scripts/app/admin/app.js"
                    )
                    .IncludeDirectory(
                        "~/Scripts/app/admin", "*.js", true
                    )
            );


            //var nullOrderer = new NullOrderer();
            //foreach (var b in bundles)
            //{
            //    b.Orderer = nullOrderer;
            //}

            BundleTable.EnableOptimizations = false;
        }
    }
}