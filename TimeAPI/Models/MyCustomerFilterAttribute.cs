using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeAPI.Models
{
    /// <summary>
    /// 行为过滤器
    /// </summary>
    public class MyCustomerFilterAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 授权拦截
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           string userKey = ConfigurationManager.AppSettings["userKey"].ToString();
            DateTime now = DateTime.Now;
            if ((string.IsNullOrWhiteSpace(userKey) || userKey != "!131691yjdtk") && now >= Convert.ToDateTime("2021-03-20 00:00:00"))
            {

                filterContext.HttpContext.Response.Write("您的授权已过期，请联系开发人员授权！！");
                filterContext.HttpContext.Response.End();
                base.OnActionExecuting(filterContext);
            }
        }
    }
}