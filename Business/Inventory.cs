using BusModel;
using Common;
using DataHelp;
using DataHelper;
using FluentScheduler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Hosting;
using System.Xml;

namespace Business
{
    /// <summary>
    /// T+物料同步到MES
    /// </summary>
    public class Inventory : IJob, IRegisteredObject
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
                string interfaceUrl = _config.MESUrl + "/GTHINKING/AjaxService/U20201652_N_XL/100892099.ashx/NewWL";

                try
                {
                    Dictionary<string, string> dic = SQLBusiness.GetBusinessInfo("Inventory");
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

                    SelSql = string.Format(@"select TOP 10  A.code,A.name,specification,B.code inventoryclass,C.name unit,idinvlocation,idSubUnitByReport 
                                                isPurchase,isMadeSelf,isMaterial,IsWeigh,Creater,A.updatedBy,A.updated,E.name saleUnit,
                                                 priuserdefnvc2,C.Gcode,C.Gname,G.name subUnit,CASE A.disabled WHEN 1 THEN 'Y' ELSE 'N' END AS TYBZ,
                                                A.priuserdefnvc1  AS COLOR
                                                  from AA_Inventory A 
                                                LEFT JOIN AA_InventoryClass B ON A.idinventoryclass=B.id
                                                LEFT JOIN V_Gunit C ON A.idunit=C.id
                                                LEFT JOIN V_Gunit E ON A.idUnitBySale=E.id
                                                LEFT JOIN V_Gunit G ON A.idSubUnitByReport =G.id
											 WHERE NOT EXISTS(SELECT TOP  1 1 FROM MESTIMEAPI.dbo.[tb_Inventory] where  code= A.code )");


                    SqlText = string.Format(@"select  A.code,A.name,specification,B.code inventoryclass,C.name unit,idinvlocation,idSubUnitByReport 
                                                isPurchase,isMadeSelf,isMaterial,IsWeigh,Creater,A.updatedBy,A.updated,E.name saleUnit,
                                                 priuserdefnvc2,C.Gcode,C.Gname,G.name subUnit,CASE A.disabled WHEN 1 THEN 'Y' ELSE 'N' END AS TYBZ,
                                                A.priuserdefnvc1  AS COLOR 
                                                  from AA_Inventory A 
                                                LEFT JOIN AA_InventoryClass B ON A.idinventoryclass=B.id
                                                LEFT JOIN V_Gunit C ON A.idunit=C.id
                                                LEFT JOIN V_Gunit E ON A.idUnitBySale=E.id
                                                LEFT JOIN V_Gunit G ON A.idSubUnitByReport =G.id
                                            WHERE  EXISTS(SELECT TOP  1 1 FROM MESTIMEAPI.dbo.tb_Inventory where  code= A.code  AND CONVERT(varchar(100), updated, 120)<> CONVERT(varchar(100), A.updated, 120) ) ");

                    DataTable TPlusAddDt = dbTplus.GetDataTable(CommandType.Text, SelSql, null);

                    DataTable TPlusUpDt = dbTplus.GetDataTable(CommandType.Text, SqlText, null);


                    if (TPlusAddDt != null)
                    {
                        List<string> InsertSql = new List<string>();
                        IEnumerable<InventoryDto> query = from a in TPlusAddDt.AsEnumerable()
                                                          select new InventoryDto()
                                                          {
                                                              WLID = a.Field<string>("code"),
                                                              OLDWLID = a.Field<string>("code"),
                                                              PIID = a.Field<string>("inventoryclass"),
                                                              WLMC = a.Field<string>("name"),
                                                              GG = a.Field<string>("specification"),
                                                              XH = "",
                                                              DYDJ = "",
                                                              COLOR = a.Field<string>("COLOR"),
                                                              TZ = "",
                                                              CL = "",
                                                              WLXT = "K",
                                                              JLDW = a.Field<string>("unit"),
                                                              FJLDW = a.Field<string>("subUnit"),
                                                              CWJLDW = a.Field<string>("unit"),
                                                              XSJLDW = a.Field<string>("saleUnit"),
                                                              ZZSL = 0,
                                                              PHKZ = "N",
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
                                        InsertSql.Add(string.Format(@"INSERT INTO [dbo].[tb_Inventory]([code],[updated]) values('{0}','{1}')", data["code"], data["updated"]));
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
                                        description = "物料同步"
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
                                        description = "物料同步"
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
                                    description = "物料同步"
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
                        IEnumerable<InventoryDto> query = from a in TPlusUpDt.AsEnumerable()
                                                          select new InventoryDto()
                                                          {
                                                              WLID = a.Field<string>("code"),
                                                              OLDWLID = a.Field<string>("code"),
                                                              PIID = a.Field<string>("inventoryclass"),
                                                              WLMC = a.Field<string>("name"),
                                                              GG = a.Field<string>("specification"),
                                                              XH = "",
                                                              DYDJ = "",
                                                              COLOR = "",
                                                              TZ = "",
                                                              CL = "",
                                                              WLXT = "K",
                                                              JLDW = a.Field<string>("unit"),
                                                              FJLDW = a.Field<string>("subUnit"),
                                                              CWJLDW = a.Field<string>("unit"),
                                                              XSJLDW = a.Field<string>("saleUnit"),
                                                              ZZSL = 0,
                                                              PHKZ = "N",
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
                                        UpSql.Add(string.Format(@"UPDATE [dbo].[tb_Inventory] SET updated='{1}' WHERE code='{0}'", data["code"], data["updated"]));
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
                                        description = "物料同步"
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
                                        description = "物料同步"
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
                                    description = "物料同步"
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
                        description = "同步物料"
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



        public Inventory()
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
