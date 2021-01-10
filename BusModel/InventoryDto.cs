using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusModel
{
    /// <summary>
    /// MES物料
    /// </summary>
    public class InventoryDto
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string WLID { get; set; }


        /// <summary>
        /// 原物料编码
        /// </summary>
        public string OLDWLID { get; set; }


        /// <summary>
        /// 物料名称
        /// </summary>
        public string WLMC { get; set; }


        /// <summary>
        /// 规格
        /// </summary>
        public string GG { get; set; }


        /// <summary>
        /// 型号
        /// </summary>
        public string XH { get; set; }


        /// <summary>
        /// 电压等级
        /// </summary>
        public string DYDJ { get; set; }


        /// <summary>
        /// 颜色
        /// </summary>
        public string COLOR { get; set; }

        /// <summary>
        /// 图号
        /// </summary>
        public string TZ { get; set; }

        /// <summary>
        /// 材料
        /// </summary>
        public string CL { get; set; }

        /// <summary>
        /// 物料形态(K|库存、N|非库存，必填)
        /// </summary>
        public string WLXT { get; set; }

        /// <summary>
        /// 物料类别
        /// </summary>
        public string PIID { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string JLDW { get; set; }

        /// <summary>
        /// 辅计量单位
        /// </summary>
        public string FJLDW { get; set; }


        /// <summary>
        /// 财务计量单位
        /// </summary>
        public string CWJLDW { get; set; }


        /// <summary>
        /// 销售计量单位
        /// </summary>
        public string XSJLDW { get; set; }

        /// <summary>
        /// 增值税率
        /// </summary>
        public float ZZSL { get; set; }

        /// <summary>
        /// 批号控制（Y/N）
        /// </summary>
        public string PHKZ { get; set; }

        /// <summary>
        /// 停用标志（Y/N）
        /// </summary>
        public string TYBZ { get; set; }
    }
}
