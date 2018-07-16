using System.Web.Optimization;
using WebHelpers.Mvc5;

namespace CRM.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Bundles/css")
                .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/bootstrap-select.css")
                .Include("~/Content/css/bootstrap-datepicker3.min.css")
                .Include("~/Content/jsgrid/jsgrid.css")
                .Include("~/Content/DataTables/datatables.css")
                .Include("~/Content/jsgrid/jsgrid-theme.css")
                .Include("~/Content/css/font-awesome.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/icheck/blue1.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/AdminLTE1.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/skins/skin-blue1.css")
                 .Include("~/plugins/fullcalendar/fullcalendar.min.css")
                //.Include("~/plugins/fullcalendar/fullcalendar.print.css")

                );

            bundles.Add(new ScriptBundle("~/Bundles/js")
                .Include("~/Content/js/plugins/jquery/jquery-3.3.1.js")
                .Include("~/Content/js/plugins/bootstrap/bootstrap.js")
                .Include("~/Content/js/plugins/fastclick/fastclick.js")
                .Include("~/Content/jsgrid/jsgrid.js")
                 .Include("~/Content/DataTables/datatables.js")

                .Include("~/Content/js/plugins/slimscroll/jquery.slimscroll.js")
                .Include("~/Content/js/plugins/bootstrap-select/bootstrap-select.js")
                .Include("~/Content/js/plugins/moment/moment.js")
                .Include("~/Content/js/plugins/datepicker/bootstrap-datepicker.js")
                .Include("~/Content/js/plugins/icheck/icheck.js")
                .Include("~/Content/jsplugins/validator/validator.js")
                .Include("~/Content/js/plugins/inputmask/jquery.inputmask.bundle.js")
                .Include("~/Content/js/adminlte.js")
                //.Include("~/Content/js/d3-funnel.min.js")
                //.Include("~/Content/js/d3.min.js")
                .Include("~/Content/js/init.js")
                     .Include("~/plugins/fullcalendar/fullcalendar.min.js")
                );

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
