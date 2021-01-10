using BusModel;
using DataHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TimeAPI.Controllers
{
    /// <summary>
    /// T+操作接口
    /// </summary>
    [RoutePrefix("TPlus/api/v1")]
    public class OPTPlusController : ApiController
    {
        /// <summary>
        /// 刷新订单理论交期
        /// </summary>
        /// <param name="preDeliveries">理论交期信息</param>
        /// <returns>更新结果</returns>
        [HttpPost]
        [Route("UpDeliveryDate")]
        public Result UpDeliveryDate(List<PreDeliveryDto>  preDeliveries)
        {
            Result result = new Result();

  
            try
            {
                if (preDeliveries != null && preDeliveries.Any())
                {
                    SQLHelper dbTplus = new SQLHelper("Tplusconn");
                    List<string> upSqlText = new List<string>();
                    preDeliveries.ForEach(p=> {
                        upSqlText.Add($@"UPDATE A SET A.priuserdefnvc1='{p.priuserdefnvc1}'  FROM dbo.SA_SaleOrder_b A INNER JOIN dbo.AA_Inventory B ON A.idinventory = B.id 
                                 INNER JOIN dbo.SA_SaleOrder C ON A.idSaleOrderDTO = C.ID WHERE A.quantity = '{p.quantity}' AND B.code = '{p.idinventory}' AND c.code = '{p.code}'");
                    });
                    if (upSqlText != null && upSqlText.Any()) {
                        dbTplus.ExecuteSqlTran(upSqlText);
                        result.Success = true;
                        result.Message = "更新订单理论交期成功";

                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "理论交期信息为空";
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }



        /// <summary>
        /// 刷新订单工序进度
        /// </summary>
        /// <param name="workProgresses">工序进度信息</param>
        /// <returns>更新结果</returns>
        [HttpPost]
        [Route("UpWorkProgress")]
        public Result UpWorkProgress(List<WorkProgressDto>  workProgresses)
        {
            Result result = new Result();


            try
            {
                if (workProgresses != null && workProgresses.Any())
                {
                    SQLHelper dbTplus = new SQLHelper("Tplusconn");
                    List<string> upSqlText = new List<string>();
                    workProgresses.ForEach(p => {
                        upSqlText.Add($@"UPDATE A SET A.priuserdefnvc2='{p.priuserdefnvc2}'  FROM dbo.SA_SaleOrder_b A INNER JOIN dbo.AA_Inventory B ON A.idinventory = B.id 
                                 INNER JOIN dbo.SA_SaleOrder C ON A.idSaleOrderDTO = C.ID WHERE A.quantity = '{p.quantity}' AND B.code = '{p.idinventory}' AND c.code = '{p.code}'");
                    });
                    if (upSqlText != null && upSqlText.Any())
                    {
                        dbTplus.ExecuteSqlTran(upSqlText);
                        result.Success = true;
                        result.Message = "更新订单工序进度成功";

                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "工序进度信息为空";
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
