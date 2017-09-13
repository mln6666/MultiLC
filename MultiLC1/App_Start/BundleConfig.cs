using System.Web;
using System.Web.Optimization;

namespace MultiLC1
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;
            ScriptBundle scriptBundle = new ScriptBundle("~/js");
            string[] scriptArray =
            {
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery-3.1.1.min.js",
                "~/Scripts/toastr.js",
                "~/Scripts/DataTables/jquery.datatables.min.js",
                "~/Scripts/DataTables/datatables.bootstrap.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"
            };
            scriptBundle.Include(scriptArray);
            scriptBundle.IncludeDirectory("~/Scripts/", "*.js");
            bundles.Add(scriptBundle);

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));



            bundles.Add(new StyleBundle("~/Styles/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/Site.css",
                "~/Content/toastr.css",
                "~/Content/DataTables/css/dataTables.bootstrap.min.css"

            ));
        }
    }
}
