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
            var lessBundle = new Bundle("~/content/bundles/main.less").Include(
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

                        //"~/Scripts/libs/signalR/jquery.signalR-2.0.3.min.js",
                        //"~/Scripts/libs/signalR/hub.js",



                        "~/Scripts/libs/imperaviRedactor/redactor.min.js",
                        "~/Scripts/libs/imperaviRedactor/ru.js",
                        "~/Scripts/libs/imperaviRedactor/blockquote.js",
                        "~/Scripts/libs/moment/moment.min.js",
                        "~/Scripts/libs/moment/ru.js",
                        "~/Scripts/libs/datepicker/jquery.datetimepicker.js",
                       "~/Scripts/libs/maskedinput/jquery.mask.min.js"
                    )
                    .Include(
                        "~/Scripts/libs/angular/angular.min.js",
                        "~/Scripts/app/blog/app.js"
                    )
            );

            // блог
            bundles.Add(new ScriptBundle("~/bundles/blog/scripts")
                    .IncludeDirectory(
                        "~/Scripts/app/blog", "*.js", true
                    )
            );


            var nullOrderer = new NullOrderer();
            foreach (var b in bundles)
            {
                b.Orderer = nullOrderer;
            }

            BundleTable.EnableOptimizations = false;
        }
    }
}