using System.Web;
using System.Web.Optimization;

namespace BadMom
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/fancyapps").Include(
                       "~/Scripts/fancyapps/jquery.fancybox.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       //"~/Scripts/bootstrap.bundle.js",
                       "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/locales/bootstrap-datepicker.ru.min.js",
                      "~/Scripts/respond.js"
                       /*"~/Scripts/popper.js"*/));

            bundles.Add(new ScriptBundle("~/bundles/new").Include(
                      "~/Scripts/registration/new.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                     "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/blog").Include(
         "~/Scripts/blog/Blog.js"));

            bundles.Add(new ScriptBundle("~/bundles/wallet").Include(
        "~/Scripts/wallet/wallet.js"));

            bundles.Add(new ScriptBundle("~/bundles/user").Include(
       "~/Scripts/user.js"));

            bundles.Add(new ScriptBundle("~/bundles/advert").Include(
       "~/Scripts/advert.js"));

            bundles.Add(new ScriptBundle("~/bundles/planer").Include(
       "~/Scripts/planer.js"));

            bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
                       "~/Scripts/moment.min.js",
                        "~/Scripts/fullcalendar.min.js",
                        "~/Scripts/locale/ru.js",
                        "~/Scripts/my_calendar.js"));

            bundles.Add(new ScriptBundle("~/bundles/mycalendar").Include(
                       "~/Scripts/my_calendar.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datetimepicker").Include(
                      "~/Scripts/bootstrap-datetimepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
                      "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/locales/bootstrap-datepicker.ru.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/contextmenu").Include(
                      "~/Scripts/jquery.contextMenu.min.js",
                      "~/Scripts/jquery.ui.position.min.js"));

            //-------------STYLES--------------------------------------------------------


            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                     "~/Content/font-awesome.min.css"));


            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/bootstrap-datepicker3.min.css"));

            bundles.Add(new StyleBundle("~/Content/badmom").Include(
                     "~/Content/badmom/fontastic.css",
                     "~/Content/badmom/fancyapps/jquery.fancybox.min.css",
                     "~/Content/badmom/style.default.css"));

            bundles.Add(new StyleBundle("~/Content/layout").Include(
                                     "~/Content/layout.css"));

            bundles.Add(new StyleBundle("~/Content/user").Include(
                                     "~/Content/user.css"));

            bundles.Add(new StyleBundle("~/Content/advert").Include(
                                    "~/Content/advert.css"));

            bundles.Add(new StyleBundle("~/Content/planer").Include(
                                   "~/Content/planer.css"));

            bundles.Add(new StyleBundle("~/Content/wallet").Include(
                                   "~/Content/wallet.css"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                                   "~/Content/admin.css"));

            bundles.Add(new StyleBundle("~/Content/homepage").Include(
                                   "~/Content/homepage.css"));

            bundles.Add(new StyleBundle("~/Content/calendar").Include(
                                   "~/Content/fullcalendar.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-datetimepicker").Include(
                                  "~/Content/bootstrap-datetimepicker.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-datepicker").Include(
                                  "~/Content/bootstrap-datepicker3.min.css"));

            bundles.Add(new StyleBundle("~/Content/contextmenu").Include(
                                 "~/Content/jquery.contextMenu.min.css"));

            bundles.Add(new StyleBundle("~/Content/blog").Include(
                                 "~/Content/blog.css"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/badmom").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));









        }
    }
}
