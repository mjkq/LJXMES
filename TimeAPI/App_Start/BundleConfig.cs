using System.Web;
using System.Web.Optimization;

namespace TimeAPI
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/amcharts.js",
                      "~/Scripts/serial.js",
                      "~/Scripts/light.js",
                      "~/Scripts/jquery-1.10.2.min.js",
                      "~/Scripts/pie-chart.js",
                      "~/Scripts/owl.carousel.js",
                      "~/Scripts/moment.js",
                       "~/Scripts/Chart.js",
                       "~/Scripts/dataTables/jquery.dataTables.js",
                       "~/Scripts/dataTables/dataTables.bootstrap.js",
                      "~/Scripts/jquery.nicescroll.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.fn.gantt.js",
                      "~/Scripts/menu_jquery.js",
                       "~/Scripts/scripts.js",
                       "~/Scripts/jquery.flot.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/style.css",
                      "~/Content/font-awesome.css",
                      "~/Content/Site.css",
                      "~/Content/icon-font.min.css",
                      "~/Scripts/dataTables/dataTables.bootstrap.css"
                      ));
        }
    }
}
