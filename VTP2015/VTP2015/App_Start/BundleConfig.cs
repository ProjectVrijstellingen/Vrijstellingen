using System.Web.Optimization;

namespace VTP2015
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/Stylesheet.css"));

            //Modules

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Scripts/App/Admin/adminPanel.js"));

            bundles.Add(new ScriptBundle("~/bundles/student").Include(
                "~/Scripts/App/Student/Dossier.js",
                "~/Scripts/App/Student/StudentIndex.js",
                "~/Scripts/App/Student/toDictionary.js"));

            bundles.Add(new ScriptBundle("~/bundles/counselor").Include(
                "~/Scripts/App/Counselor/DocentBewijsViewScript.js",
                "~/Scripts/App/Counselor/DossierScherm.js",
                "~/Scripts/App/Counselor/CounselorView.js",
                "~/Scripts/App/Counselor/CustomCheckBox.js",
                "~/Scripts/App/Counselor/CounselorSelect.js",
                "~/Scripts/App/Counselor/script.js"));

            bundles.Add(new ScriptBundle("~/bundles/lecturer").Include(
                "~/Scripts/App/Lecturer/DocentBewijsViewScript.js"));


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
