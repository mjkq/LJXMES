using Business;
using DataHelp;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TimeAPI.Models
{
    public class RegBusiness : Registry
    {
        public RegBusiness()
        {
            SQLHelper sql = new SQLHelper("DefaultConnection");
            string seltStr = @"SELECT ID
                                                      ,CODE
                                                      ,NAME
                                                      ,TYPE
                                                      ,STARTDATE
                                                      ,ENDDATE
													  ,case FREQUENCY when  '天数' then CAST(VALUE as int)*24 * 60
													    when '小时'  then CAST(VALUE as int) * 60  
														else CAST(VALUE as int)  end as 'VALUE'
                                                  FROM dbo.Business
                                               WHERE ISAUTO = 1  AND GETDATE() BETWEEN STARTDATE and ENDDATE";
            DataTable BusDt = sql.GetDataTable(CommandType.Text, seltStr,null);
            if (BusDt != null)
            {
                foreach (DataRow dr in BusDt.Rows)
                {

                    string id = dr["ID"].ToString();
                    string business = dr["CODE"].ToString();
                    int frequencyValue = Convert.ToInt32(dr["VALUE"]);
                    //string  frequency = dr["FREQUENCY"].ToString();
                    if (frequencyValue < 0)
                    {
                        continue;
                    }

                    //寻找业务对应的任务
                    switch (business)
                    {
                        case "Test":
                            Schedule<Test>().ToRunNow().AndEvery(frequencyValue).Minutes();
                            break;
                        case "Inventory":
                            Schedule<Inventory>().ToRunNow().AndEvery(frequencyValue).Minutes();
                            break;
                        
                        case "SaleOrder":
                            Schedule<SaleOrder>().ToRunNow().AndEvery(frequencyValue).Minutes();
                            break;
                        default:
                            break;
                    }

                }
            }
           
            log4net.ILog log = log4net.LogManager.GetLogger("RegBusiness:");//获取一个日志记录器
            log.Info(DateTime.Now.ToString()+"------"+"开始执行同步!---------");
            // Schedule an IJob to run at an interval
            // 立即执行每两秒一次的计划任务。（指定一个时间间隔运行，根据自己需求，可以是秒、分、时、天、月、年等。）
           // Schedule<MyJob>().ToRunNow().AndEvery(2).Seconds();

            // Schedule an IJob to run once, delayed by a specific time interval
            // 延迟一个指定时间间隔执行一次计划任务。（当然，这个间隔依然可以是秒、分、时、天、月、年等。）
          //  Schedule<MyJob>().ToRunOnceIn(5).Seconds();

            // Schedule a simple job to run at a specific time
            // 在一个指定时间执行计划任务（最常用。这里是在每天的下午 1:10 分执行）
           // Schedule(() => log.Info("It's 1:10 PM now.")).ToRunEvery(1).Days().At(18, 20);

            //Schedule(() => {

            //    // 做你想做的事儿。
            //    log.Info("做你想做的事儿");

            //}).ToRunEvery(1).Days().At(18, 21);

            // Schedule a more complex action to run immediately and on an monthly interval
            // 立即执行一个在每月的星期一 3:00 的计划任务（可以看出来这个一个比较复杂点的时间，它意思是它也能做到！）
            //Schedule<MyComplexJob>().ToRunNow().AndEvery(1).Months().OnTheFirst(DayOfWeek.Monday).At(3, 0);

            // Schedule multiple jobs to be run in a single schedule
            // 在同一个计划中执行两个（多个）任务
          //  Schedule<MyJob>().AndThen<MyOtherJob>().ToRunNow().AndEvery(5).Minutes();

        }


    }
}