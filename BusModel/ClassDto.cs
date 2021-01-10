using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusModel
{
    /// <summary>
    /// MES物料类别
    /// </summary>
    public class ClassDto
    {
        /// <summary>
        /// 类别编码
        /// </summary>
        public string PIID { get; set; }

        /// <summary>
        /// 原类别编码
        /// </summary>
        public string OLDPLID { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string PLMC { get; set; }

        /// <summary>
        /// 末级标志
        /// </summary>
        public string MJBZ { get; set; }

        /// <summary>
        /// 停用标志
        /// </summary>
        public string TYBZ { get; set; }
    }
}
