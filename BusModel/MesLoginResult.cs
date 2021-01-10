using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusModel
{
    /// <summary>
    /// Mes登录结果
    /// </summary>
    public class MesLoginResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServerDateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserInfo UserInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Menu Menu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> UserOption { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> AuthorizedModules { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string XTID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int XTVer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DWFBH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UICSXT { get; set; }
    }


    /// <summary>
    /// 登录信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RYID { get; set; }
        /// <summary>
        /// 测试01
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeptName { get; set; }
    }

    /// <summary>
    /// 权限菜单
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 
        /// </summary>
        public int UmId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BaseMdId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Children { get; set; }
    }
}
