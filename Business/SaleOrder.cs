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
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Business
{
    /// <summary>
    /// T+销售订单同步到MES
    /// </summary>
    public class SaleOrder : IJob, IRegisteredObject
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
                string interfaceUrl = _config.MESUrl + "/GTHINKING/AjaxService/U20201652_N_XL/100892099.ashx/NewSO";

                try
                {
                    Dictionary<string, string> dic = SQLBusiness.GetBusinessInfo("SaleOrder");
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

                    SelSql = string.Format(@"SELECT TOP 10 A.id, A.voucherdate AS  DDRQ, A.code AS SOID,'ljxk' KHID,ISNULL(C.code,'') BMID,
                                            ISNULL(C.name,'') deptName,ISNULL(D.name,'') TXY,left(A.linkMan,4) AS LXR,A.acceptDate AS XQRQ,ISNULL(A.memo,'') BZ 
                                            FROM PU_PurchaseOrder A
                                            LEFT JOIN AA_Partner  B ON A.idpartner =B.id
                                             LEFT JOIN AA_Department C ON A.iddepartment =C.id
                                             LEFT JOIN AA_Person D ON A.idclerk=D.id
                                             WHERE  NOT EXISTS(SELECT TOP 1 1 FROM MESTIMEAPI.dbo.tb_PurchaseOrder WHERE A.code=code)");

                    DataTable TPlusAddDt = dbTplus.GetDataTable(CommandType.Text, SelSql, null);

                    if (TPlusAddDt != null && TPlusAddDt.Rows.Count > 0)
                    {
                        List<string> InsertSql = new List<string>();
                        foreach (DataRow orderHead in TPlusAddDt.Rows)
                        {
                            SqlText = string.Format($@"SELECT  A.id AS SOXH,'{orderHead["SOID"].ToString()}' AS SOID,B.code AS WLID,C.name AS JLDW,A.acceptDate  AS XQRQ,
                                                 A.quantity  AS XQSL,A.origTaxPrice AS JG,A.origTaxAmount AS  JE,A.origPrice AS WSJG,
                                                 (A.origTaxAmount-A.origTax) WSJE,A.origTax,A.pubuserdefnvc2 AS BZ,A.saleOrderCode
                                                 FROM PU_PurchaseOrder_b A
                                                 LEFT JOIN dbo.AA_Inventory B ON A.idinventory=B.id
                                                LEFT JOIN dbo.AA_Unit C ON A.idunit=C.id
                                                WHERE A.idPurchaseOrderDTO='{orderHead["id"].ToString()}'");

                            DataTable orderBodyDt = dbTplus.GetDataTable(CommandType.Text, SqlText, null);

                            IEnumerable<SaleOrderDetailDto> query = from a in orderBodyDt.AsEnumerable()
                                                                    select new SaleOrderDetailDto()
                                                                    {
                                                                        WLID = a.Field<string>("WLID"),
                                                                        BZ = a.Field<string>("BZ"),
                                                                        JE = a.Field<decimal>("JE"),
                                                                        JG = a.Field<decimal>("JG"),
                                                                        JHFHRQ = null,
                                                                        SOID = a.Field<string>("SOID"),
                                                                        SOXH = a.Field<int>("SOXH"),
                                                                        WSJE = a.Field<decimal>("WSJE"),
                                                                        WSJG = a.Field<decimal>("WSJG"),
                                                                        XQRQ = a.Field<DateTime?>("XQRQ"),
                                                                        XQSL = a.Field<decimal>("XQSL"),
                                                                        JLDW = a.Field<string>("JLDW")


                                                                    };
                            SaleOrderDto saleOrder = new SaleOrderDto()
                            {
                                BMID = orderHead["BMID"].ToString(),
                                BZ = orderHead["BZ"].ToString(),
                                DDRQ = orderHead["DDRQ"] != null ? Convert.ToDateTime(orderHead["DDRQ"]) : DateTime.Now,
                                KHID = orderHead["KHID"].ToString(),
                                LXR = orderHead["LXR"].ToString(),
                                SOID = orderHead["SOID"].ToString(),
                                TXY = orderHead["TXY"].ToString(),
                                XQRQ = orderHead["XQRQ"] != null ? Convert.ToDateTime(orderHead["XQRQ"]) : DateTime.Now

                            };
                            if (query != null && query.Count() > 0)
                            {
                                saleOrder.DETAILS = query.ToList();


                                var postObject = new { CZLX = "I", DATA = saleOrder };
                                string postData = JsonConvert.SerializeObject(postObject);
                                Tuple<bool, string> returnData = PostData.PostDataWithCokie(postData, interfaceUrl, cookie);
                                if (returnData.Item1)
                                {
                                    Result result = JsonConvert.DeserializeObject<Result>(returnData.Item2);
                                    if (result.Success)
                                    {
                                        InsertSql.Add(string.Format(@"INSERT INTO [dbo].[tb_PurchaseOrder]([code]) values('{0}')", orderHead["SOID"]));
                                        sucsum = sucsum + 1;

                                        logDetails.Add(new LogDetail()
                                        {
                                            busId = busId,
                                            code = orderHead["SOID"].ToString(),
                                            date = DateTime.Now,
                                            url = interfaceUrl,
                                            postData = postData,
                                            postType = "Post",
                                            returnData = returnData.Item2,
                                            result = "成功",
                                            description = "销售订单同步"
                                        });


                                    }
                                    else
                                    {
                                        failsum = failsum + 1;
                                        logDetails.Add(new LogDetail()
                                        {
                                            busId = busId,
                                            code = orderHead["SOID"].ToString(),
                                            date = DateTime.Now,
                                            url = interfaceUrl,
                                            postData = postData,
                                            postType = "Post",
                                            returnData = result.Message,
                                            result = "失败",
                                            description = "销售订单同步"
                                        });

                                    }

                                }
                                else
                                {
                                    failsum = failsum + 1;
                                    logDetails.Add(new LogDetail()
                                    {
                                        busId = busId,
                                        code = orderHead["SOID"].ToString(),
                                        date = DateTime.Now,
                                        url = interfaceUrl,
                                        postData = postData,
                                        postType = "Post",
                                        returnData = returnData.Item2,
                                        result = "失败",
                                        description = "销售订单同步"
                                    });
                                }
                            }
                        }



                        if (InsertSql.Count > 0)
                        {
                            dbSysConn.ExecuteSqlTran(InsertSql);
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
                        description = "同步销售订单"
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



        public SaleOrder()
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
