using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusModel
{
    /// <summary>
    /// MES销售订单明细
    /// </summary>
    public class SaleOrderDetailDto
    {
        /// <summary>
        /// 销售订单号
        /// </summary>
        public string SOID { get; set; }


        /// <summary>
        /// 序号
        /// </summary>
        public int SOXH { get; set; }


        /// <summary>
        /// 物料编码
        /// </summary>
        public string WLID { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string JLDW { get; set; }


        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime? XQRQ { get; set; }

        /// <summary>
        /// 计划发货日期
        /// </summary>
        public DateTime? JHFHRQ { get; set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        public decimal XQSL { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal JG { get; set; }


        /// <summary>
        /// 金额
        /// </summary>
        public decimal JE { get; set; }


        /// <summary>
        /// 无税价格
        /// </summary>
        public decimal WSJG { get; set; }

        /// <summary>
        /// 无税金额
        /// </summary>
        public decimal WSJE { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
    }
}
