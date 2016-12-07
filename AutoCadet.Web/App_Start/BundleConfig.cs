using System.Web.Optimization;

namespace AutoCadet
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/agency.js",
                "~/Scripts/site-home-jquery.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/content/css")
                .Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include(
                "~/Content/agency.css",
                "~/Content/stars-rating-display.css",
                "~/Content/stars-rating-edit.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/content/fonts")
                .Include("~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/content/priority")
                .Include("~/Content/priority.css", new CssRewriteUrlTransform()));
        }
    }
}
