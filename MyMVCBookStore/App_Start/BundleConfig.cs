using System.Web;
using System.Web.Optimization;

namespace MyMVCBookStore
{
    public class BundleConfig
    {        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-hover-dropdown.js"));
            bundles.Add(new ScriptBundle("~/bundles/modalform").Include(
                        "~/Scripts/modalform.js"));
            bundles.Add(new ScriptBundle("~/bundles/grid").Include(
                       "~/Scripts/Scripts/gridmvc.min.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/httpnotfoundstyle").Include(
                      "~/Content/httpnotfoundstyle.css"));
            bundles.Add(new StyleBundle("~/Content/GridMvcCss").Include(
                     "~/Content/Gridmvc.css"));
        }
    }
}

