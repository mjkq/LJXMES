using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusModel
{
    /// <summary>
    /// 工序进度信息Dto
    /// </summary>
    public class WorkProgressDto
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 物存货编码
        /// </summary>
        public string idinventory { get; set; }

        /// <summary>
        ///数量
        /// </summary>
        public decimal quantity { get; set; }


        /// <summary>
        /// 工序进度
        /// </summary>
        public string priuserdefnvc2 { get; set; }
    }

    
}
