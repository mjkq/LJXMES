using BusModel;
using Common;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using DataHelper;
using System.Data;
using DataHelp;
using Newtonsoft.Json;
using System.Net;

namespace Business
{
    /// <summary>
    /// T+物料类别同步到MES
    /// </summary>
    public class InventoryClass : IJob, IRegisteredObject
    {
        private readonly object _lockMin = new object();
        int sucsum = 0;
        int failsum = 0;
        string busId = string.Empty;
        string upTime = string.Empty;
        private bool _shuttingDown;

        public void Execute()
        {
            lock (_lockMin)
            {

                if (_shuttingDown)
                {
                    return;
                }

                List<LogDetail> logDetails = new List<LogDetail>();
                Config _config = new Config();
                string interfaceUrl = _config.MESUrl+ "/GTHINKING/AjaxService/U20201652_N_XL/100892099.ashx/NewPL";

                try
                {
                    Dictionary<string, string> dic = SQLBusiness.GetBusinessInfo("InventoryClass");
                    busId = dic["id"];
                    upTime = dic["value"];
                    string newid = Guid.NewGuid().ToString();


                    string SqlText = string.Empty;
                    DataTable dt = new DataTable();

                    string SelSql = string.Empty;

                    SQLHelper dbTplus = new SQLHelper("Tplusconn");

                    SQLHelper dbSysConn = new SQLHelper("DefaultConnection");

                    string url = string.Empty;
                    string StrSql = string.Empty;

                    object mesCookie = CacheHelper.Get("MESUser");
                    if (mesCookie == null)
                    {
                        MesLoginResult mesLogin = _config.LoginMes();
                        if (!mesLogin.Success)
                        {
                            failsum = failsum + 1;
                            logDetails.Add(new LogDetail()
                            {
                                busId = busId,
                                code = "",
                                date = DateTime.Now,
                                url = interfaceUrl,
                                postData = $"Mes系统登录请求失败,请求信息(用户：{_config.UserName},密码：{_config.PassWord})",
                                postType = "Post",
                                returnData = mesLogin.ErrorMessage,
                                result = "失败",
                                description = "Mes系统登录请求"
                            });
                            SQLBusiness.WriteDetailLog(logDetails);
                            return;
                        }
                        else
                        {
                            mesCookie = CacheHelper.Get("MESUser");
                        }
                    }

                    CookieContainer cookie = mesCookie as CookieContainer;

                    SelSql = string.Format(@"SELECT code,name,CASE isEndNode WHEN 1 THEN 'Y' ELSE 'N' END AS MJBZ,
                                            CASE disabled WHEN 1 THEN 'Y' ELSE 'N' END AS TYBZ,A.updated FROM dbo.AA_InventoryClass  A
                                                WHERE NOT EXISTS(SELECT TOP  1 1 FROM MESTIMEAPI.dbo.tb_InventoryClass where  code= A.code ) ");


                    SqlText = string.Format(@"SELECT code,name,CASE isEndNode WHEN 1 THEN 'Y' ELSE 'N' END AS MJBZ,
                                            CASE disabled WHEN 1 THEN 'Y' ELSE 'N' END AS TYBZ,A.updated FROM dbo.AA_InventoryClass  A
                                            WHERE  EXISTS(SELECT TOP  1 1 FROM MESTIMEAPI.dbo.tb_InventoryClass where  code= A.code  AND CONVERT(varchar(100), updated, 120)<> CONVERT(varchar(100), A.updated, 120) ) ");

                    DataTable TPlusAddDt = dbTplus.GetDataTable(CommandType.Text, SelSql, null);

                    DataTable TPlusUpDt = dbTplus.GetDataTable(CommandType.Text, SqlText, null);


                    if (TPlusAddDt != null)
                    {
                        List<string> InsertSql = new List<string>();
                        IEnumerable<ClassDto> query = from a in TPlusAddDt.AsEnumerable()
                                                      select new ClassDto()
                                                      {
                                                          MJBZ = a.Field<string>("MJBZ"),
                                                          OLDPLID = a.Field<string>("code"),
                                                          PIID = a.Field<string>("code"),
                                                          PLMC = a.Field<string>("name"),
                                                          TYBZ = a.Field<string>("TYBZ")

                                                      };
                        if (query != null && query.Count() > 0)
                        {

                            var postObject = new { CZLX = "I", DATA = query };
                            string postData = JsonConvert.SerializeObject(postObject);

                            Tuple<bool, string> returnData = PostData.PostDataWithCokie(postData, interfaceUrl, cookie);
                            if (returnData.Item1)
                            {
                                Result result = JsonConvert.DeserializeObject<Result>(returnData.Item2);
                                if (result.Success)
                                {
                                    foreach (DataRow data in TPlusAddDt.Rows)
                                    {
                                        InsertSql.Add(string.Format(@"INSERT INTO [dbo].[tb_InventoryClass]([code],[updated]) values('{0}','{1}')", data["code"], data["updated"]));
                                        sucsum = sucsum + 1;
                                    }
                                    logDetails.Add(new LogDetail()
                                    {
                                        busId = busId,
                                        code = "",
                                        date = DateTime.Now,
                                        url = interfaceUrl,
                                        postData = postData,
                                        postType = "Post",
                                        returnData = returnData.Item2,
                                        result = "成功",
                                        description = "物料类别同步"
                                    });


                                }
                                else
                                {
                                    failsum = failsum + 1;
                                    logDetails.Add(new LogDetail()
                                    {
                                        busId = busId,
                                        code = "",
                                        date = DateTime.Now,
                                        url = interfaceUrl,
                                        postData = postData,
                                        postType = "Post",
                                        returnData = result.Message,
                                        result = "失败",
                                        description = "物料类别同步"
                                    });

                                }

                            }
                            else
                            {
                                failsum = failsum + 1;
                                logDetails.Add(new LogDetail()
                                {
                                    busId = busId,
                                    code = "",
                                    date = DateTime.Now,
                                    url = interfaceUrl,
                                    postData = postData,
                                    postType = "Post",
                                    returnData = returnData.Item2,
                                    result = "失败",
                                    description = "物料类别同步"
                                });
                            }
                        }

                        if (InsertSql.Count > 0)
                        {
                            dbSysConn.ExecuteSqlTran(InsertSql);
                        }
                    }


                    if (TPlusUpDt != null)
                    {
                        List<string> UpSql = new List<string>();
                        IEnumerable<ClassDto> query = from a in TPlusUpDt.AsEnumerable()
                                                      select new ClassDto()
                                                      {
                                                          MJBZ = a.Field<string>("MJBZ"),
                                                          OLDPLID = a.Field<string>("code"),
                                                          PIID = a.Field<string>("code"),
                                                          PLMC = a.Field<string>("name"),
                                                          TYBZ = a.Field<string>("TYBZ")

                                                      };
                        if (query != null && query.Count() > 0)
                        {

                            var postObject = new { CZLX = "U", DATA = query };
                            string postData = JsonConvert.SerializeObject(postObject);
                            Tuple<bool, string> returnData = PostData.PostDataWithCokie(postData, interfaceUrl, cookie);
                            if (returnData.Item1)
                            {
                                Result result = JsonConvert.DeserializeObject<Result>(returnData.Item2);
                                if (result.Success)
                                {
                                    foreach (DataRow data in TPlusUpDt.Rows)
                                    {
                                        UpSql.Add(string.Format(@"UPDATE [dbo].[tb_InventoryClass] SET updated='{1}' WHERE code='{0}'", data["code"], data["updated"]));
                                        sucsum = sucsum + 1;
                                    }
                                    logDetails.Add(new LogDetail()
                                    {
                                        busId = busId,
                                        code = "",
                                        date = DateTime.Now,
                                        url = interfaceUrl,
                                        postData = postData,
                                        postType = "Post",
                                        returnData = returnData.Item2,
                                        result = "成功",
                                        description = "物料类别同步"
                                    });


                                }
                                else
                                {
                                    failsum = failsum + 1;
                                    logDetails.Add(new LogDetail()
                                    {
                                        busId = busId,
                                        code = "",
                                        date = DateTime.Now,
                                        url = interfaceUrl,
                                        postData = postData,
                                        postType = "Post",
                                        returnData = result.Message,
                                        result = "失败",
                                        description = "物料类别同步"
                                    });

                                }

                            }
                            else
                            {
                                failsum = failsum + 1;
                                logDetails.Add(new LogDetail()
                                {
                                    busId = busId,
                                    code = "",
                                    date = DateTime.Now,
                                    url = interfaceUrl,
                                    postData = postData,
                                    postType = "Post",
                                    returnData = returnData.Item2,
                                    result = "失败",
                                    description = "物料类别同步"
                                });
                            }
                        }

                        if (UpSql.Count > 0)
                        {
                            dbSysConn.ExecuteSqlTran(UpSql);
                        }
                    }



                    SQLBusiness.WriteDetailLog(logDetails);
                }
                catch (Exception ex)
                {
                    failsum += 1;
                    logDetails.Add(new LogDetail()
                    {
                        busId = busId,
                        code = ex.HResult.ToString(),
                        date = DateTime.Now,
                        url = interfaceUrl,
                        postData = ex.Source.Replace("'", ""),
                        postType = "",
                        returnData = ex.Message.Replace("'", "''"),
                        result = "系统错误",
                        description = "同步物料类别"
                    });
                    SQLBusiness.WriteDetailLog(logDetails);
                }
                finally
                {
                    SQLBusiness.WriteCountLog(new CountDto()
                    {
                        BusId = busId,
                        sucsum = sucsum,
                        failsum = failsum,
                        latTime = DateTime.Now.AddMinutes(Convert.ToInt32(upTime)).ToString()
                    });
                }
            }
        }

        public InventoryClass()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }
        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lockMin)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }
}
