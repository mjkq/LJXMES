using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TimeAPI.App_Start;
using TimeAPI.Models;

namespace TimeAPI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //log4
            log4net.Config.XmlConfigurator.Configure();
            //开始所有任务
            JobManager.Initialize(new RegBusiness());
        }

        /// <summary>
        /// 回收
        /// </summary>
        protected void Application_End()
        {
            /*IIS中，最好推薦將IIS改下配置，設置如下：
               应用程序池-高级设置-启动模式：AlwaysRunning 
                 应用程序-高级设置-常规-预加载已启用：True
             */
            log4net.ILog log = log4net.LogManager.GetLogger("Application_End:");//获取一个日志记录器
            log.Info("进程即将被IIS回收");
            //地址最好是全路徑，帶有http://開頭的，參考：http://localhost:8087/
            string strUrl = System.Configuration.ConfigurationManager.AppSettings["SelfAddress"];//本程序部署地址  
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                System.IO.Stream stream = wc.OpenRead(strUrl);
                System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                string html = reader.ReadToEnd();
                 log.Info("訪問的頁面" + html);
                if (!string.IsNullOrWhiteSpace(html))
                {
                    log.Info("喚醒程序成功");
                }
                reader.Close();
                reader.Dispose();
                stream.Close();
                stream.Dispose();
                wc.Dispose();
            }
            catch (Exception ex)
            {
                log.Info("喚醒異常" + ex.Message);
            }
        }
    }
}
