using BusModel;
using DataHelper;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;

namespace Common
{
    public class Config
    {
        public Config()
        {
            MESUrl = ConfigurationManager.AppSettings["MESUrl"].ToString();
            lastTime= ConfigurationManager.AppSettings["lastTime"].ToString();
            TPlusUrl = ConfigurationManager.AppSettings["TPlusUrl"].ToString();
            Appkey = ConfigurationManager.AppSettings["Appkey"].ToString();
            Appsecret = ConfigurationManager.AppSettings["Appsecret"].ToString();
            UserName = ConfigurationManager.AppSettings["UserName"].ToString();
            PassWord = ConfigurationManager.AppSettings["PassWord"].ToString();
            AccountNum = ConfigurationManager.AppSettings["AccountNum"].ToString();
            RecptTime = ConfigurationManager.AppSettings["RecptTime"].ToString();
            DelayDay = -(Convert.ToInt32(ConfigurationManager.AppSettings["DelayDay"]));
        }


        /// <summary>
        ///MES接口地址
        /// </summary>
        public string MESUrl { get; set; }

        /// <summary>
        /// T+需要同步起始时间
        /// </summary>
        public string lastTime { get; set; }

        /// <summary>
        /// 收款单同步开始日期
        /// </summary>
        public string RecptTime { get; set; }
        /// <summary>
        /// T+接口地址
        /// </summary>
        public  string TPlusUrl { get; set; }

        /// <summary>
        /// T+对接的Appkey
        /// </summary>
        public string Appkey { get; set; }

        /// <summary>
        /// T+对接的Appsecret
        /// </summary>
        public string Appsecret { get; set; }

        /// <summary>
        /// T+登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// T+登陆密码
        /// </summary>
        public string PassWord { get; set; }


        /// <summary>
        /// T+登录账套
        /// </summary>
        public string AccountNum { get; set; }

        /// <summary>
        /// 延迟天数
        /// </summary>
        public int DelayDay { get; set; }

        /// <summary>
        /// 登录MES系统
        /// </summary>
        /// <returns></returns>
        public MesLoginResult LoginMes()
        {
            MesLoginResult loginResult = new MesLoginResult();
            string loginUrl = MESUrl + "/GTHINKING/AjaxService/N_MISPRO/100208057.ashx/Login";
            MESLoginDto mESLogin = new MESLoginDto()
            {
                userId = UserName,
                password = PassWord,
                clientType = "s",
                epId = ""
            };
            string postData = JsonConvert.SerializeObject(mESLogin);
            Tuple<bool, CookieContainer, string> returnData = PostData.PostLogin(postData,loginUrl);
            if (returnData.Item1 == true)
            {
                loginResult = JsonConvert.DeserializeObject<MesLoginResult>(returnData.Item3);
                if (loginResult.Success)
                {
                    DateTime expireTime = DateTime.Now.AddMinutes(210);
                    CacheHelper.Add("MESUser", returnData.Item2, expireTime);
                }
                else {
                    CacheHelper.Remove("MESUser");
                }
            }
            else {
                loginResult = new MesLoginResult()
                {
                    ErrorMessage = returnData.Item3,
                    Success = returnData.Item1
                };
            }
            return loginResult;
        }



    }
}
