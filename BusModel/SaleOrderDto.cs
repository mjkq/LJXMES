using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusModel
{
    /// <summary>
    /// MES销售订单主表
    /// </summary>
    public class SaleOrderDto
    {

        /// <summary>
        /// 销售订单号
        /// </summary>
        public string SOID { get; set; }


        /// <summary>
        /// 客户编码（固定值）
        /// </summary>
        public string KHID { get; set; }


        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime DDRQ { get; set; }


        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime XQRQ { get; set; }


        /// <summary>
        /// 销售部门编码
        /// </summary>
        public string BMID { get; set; }


        /// <summary>
        /// 销售员[必填
        /// </summary>
        public string TXY { get; set; }


        /// <summary>
        /// 联系人
        /// </summary>
        public string LXR { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }

        /// <summary>
        /// 销售订单明细
        /// </summary>
        public List<SaleOrderDetailDto> DETAILS { get; set; }

      
    }
}
