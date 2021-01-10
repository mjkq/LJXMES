using Business;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeAPI.Models;

namespace TimeAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          QHTEntities db = new QHTEntities();
            List<BusinessMdel> lbusinessMdel = new List<BusinessMdel>();
        List<Models.Business> buslist = db.Business.ToList();
            buslist.ForEach(a =>
            {
                Logdetail log = db.Logdetail.Where(l => l.busId == a.ID).OrderByDescending(l => l.date).FirstOrDefault();
                Count count = db.Count.Where(c => c.BusId == a.ID).FirstOrDefault();
                if (log == null) {
                    log = new Logdetail()
                    {
                        id=0,
                        busId= a.ID,
                        code=string.Empty,
                        date=string.Empty,
                        description=string.Empty,
                        mark=string.Empty,
                        postData=string.Empty,
                        postType=string.Empty,
                        result=string.Empty,
                        returnData=string.Empty,
                        url=string.Empty
                    };
                }
                if (count == null) {
                    count=new Count() {
                        id=0,
                        BusId=a.ID,
                        failcount=0,
                        failsum=0,
                        lattime=null,
                        succount=0,
                        sucsum=0

                    };

                }
                BusinessMdel businessMdel = new BusinessMdel()
                {
                    business = a,
                    count = count,
                    log = log
                };
                lbusinessMdel.Add(businessMdel);
                
            });
            TimeAPI.Models.BusinessMdellist businessMdellist = new BusinessMdellist();
            businessMdellist.busiMoList = lbusinessMdel;
            return View(businessMdellist);
        }

        /// <summary>
        /// 点击同步
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult Run(string code)
        {

            string msg = string.Empty;
            string business = string.Empty;
            try
            {
                if (code == null)
                {
                    msg = "请求参数为空";
                }
                else
                {
                    //寻找业务对应的任务
                    switch (code)
                    {
                        case "Test":
                            {
                                business = "测试";
                                new Test().Execute();
                            }
                            break;
                        case "Inventory":
                            {
                                business = "物料";
                                new Inventory().Execute();
                            }
                            break;
                        case "InventoryClass":
                            {
                                business = "物料类别";
                                new InventoryClass().Execute();
                            }
                            break;
                        case "SaleOrder":
                            {
                                //调试未完成
                                business = "销售订单";
                                new SaleOrder().Execute();
                            }
                            break;
                    
                        default:
                            break;
                    }
                }
                msg = "执行（" + business + ")的同步成功！！";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
  
            string script = string.Format("<script>alert('{0}');window.location.href='../home/index';</script>", msg);
            return Content(script);
        }

        /// <summary>
        /// 停止同步
        /// </summary>
        /// <returns></returns>
        public ActionResult Stop()
        {
            string msg = string.Empty;
            try
            {
                //停止所有的job
             //   JobManager.StopAndBlock();
                JobManager.Stop();
                msg = "停止所有同步任务成功！";
            }
            catch (Exception ex) {
                msg = ex.Message;
            }
            string script = string.Format("<script>alert('{0}');window.location.href='../home/index';</script>", msg);
            return Content(script);

        }


        public ActionResult Start()
        {
            string msg = string.Empty;
            try
            {
                //开始所有任务
                JobManager.Initialize(new RegBusiness());
                msg = "启动同步任务成功！";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            string script = string.Format("<script>alert('{0}');window.location.href='../home/index';</script>", msg);
            return Content(script);


        }

        /// <summary>
        /// 根据业务ID获取日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetLog(int? id)
        {
             QHTEntities db = new QHTEntities();
            return View("Buslog", db.Logdetail.Where(s => s.busId == id).OrderByDescending(a=>a.date).ToList());

        }
    }
}