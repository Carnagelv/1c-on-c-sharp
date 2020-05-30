using System.Web.Optimization;

namespace OneC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Bundles/libraries").Include(
                        "~/Scripts/jquery-3.3.1.min.js",
                        "~/Scripts/angular.min.js",
                        "~/Scripts/angular-aria.min.js",
                        "~/Scripts/angular-material.min.js",
                        "~/Scripts/angular-animate.min.js",
                        "~/Scripts/angular-messages.min.js",
                        "~/Scripts/angular-route.min.js",
                        "~/Scripts/angular-sanitize.min.js",
                        "~/AngularApp/Scripts/main/mainFactory.js",
                        "~/AngularApp/Scripts/main/mainController.js",
                        "~/AngularApp/Scripts/main/mainModalController.js",
                        "~/AngularApp/Scripts/main/main.js"));

            bundles.Add(new StyleBundle("~/Content/libraries").Include(
                      "~/Content/css/angular-material.min.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/bootstrap-grid.min.css"));
        }
    }
}
