using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusModel
{
    /// <summary>
    /// MES登录对象
    /// </summary>
    public class MESLoginDto
    {
        /// <summary>
        /// 账户编码
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 客户端类型（固定为 S）
        /// </summary>
        public string clientType { get; set; }
        /// <summary>
        /// 编码（固定为空白字符串）
        /// </summary>
        public string epId { get; set; }
    }
}
