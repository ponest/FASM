using System.Web.Optimization;

namespace FASM
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
           

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                     "~/Scripts/jQuery.js",
                     "~/Scripts/bootstrap.js",
                     "~/DataTables/datatables.js",
                     "~/Scripts/FASM.js",
                     "~/Scripts/main.js"
                    ));

            bundles.Add(new ScriptBundle("~/DataTables/js").Include(
                "~/DataTables/datatables.js"
                ));
           
            bundles.Add(new ScriptBundle("~/plugin/sweetAlert").Include(
                     "~/Scripts/sweetalert.min.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.css",
                      "~/Content/main.css",
                      "~/Content/Site.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/datePicker").Include(
                "~/Content/daterangepicker.css"
                ));

            bundles.Add(new StyleBundle("~/DataTables/css").Include(
                "~/DataTables/datatable.bootstrap.css",
                "~/DataTables/datatables.css"
                ));

            bundles.Add(new StyleBundle("~/plugin/sweetAlertStyles").Include(
                    "~/Content/sweetalert.css"
                    ));
        }
    }
}
