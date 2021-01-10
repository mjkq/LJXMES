using BusModel;
using Chanjet.TP.OpenAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ufida.T.EAP.Net;

namespace Common
{
    /// <summary>
    /// T+接口操作
    /// </summary>
   public class OperateTplus
    {
        /// <summary>
        /// 链接t+服务器
        /// </summary>
        /// <returns></returns>
        public static string ConnectServer(string TplusUrl,string AppKey,string AppSecret) {

            string result = string.Empty;
            if (TplusUrl.Contains("api/v2"))
            {
                TplusUrl = TplusUrl.Replace("api/v2", "api/v1");
            }

            OpenAPI api = new OpenAPI(TplusUrl, new Credentials()
            {
                AppKey = AppKey,
                AppSecret = AppSecret
            });

            try
            {
                dynamic r = api.ConnectTest();
                result = r.status + r.ToString();
            }
            catch (RestException ex)
            {
                if (ex == null || ex.Response == null)
                {

                    result = "请求地址错误！";
                }
                else
                {
                    result = "\r\n Call:Connection \r\n error:" + ex.Response.StatusCode + "  " + ex.Message + "\r\n" + ex.ResponseBody;
                }
            }
            return result;
        }

        /// <summary>
        /// t+接口登录获取Token
        /// </summary>
        /// <returns></returns>
        public static Result Login() {
            Result  result= new Result();
            // v2 获取token 用户名 密码
            Config config = new Config();
            string privateKeyPath = XmlPath.PemPath;
            if (string.IsNullOrEmpty(privateKeyPath) || !File.Exists(privateKeyPath))
            {
                result.Success = false;
                result.Message = "私钥文件丢失";
            }
            var header = new Dictionary<string, object>
            {
                {"appkey", config.Appkey},
                {"orgid",string.Empty},//90009444367
                {"appsecret", config.Appsecret}
            };
            RestSharp.Serializers.JsonSerializer jsonSerializer = new RestSharp.Serializers.JsonSerializer();
            string datas = jsonSerializer.Serialize(header);
            Ufida.T.EAP.Net.security.TokenManage tokenManage = new Ufida.T.EAP.Net.security.TokenManage();

            string signvalue = tokenManage.CreateSignedToken(datas, privateKeyPath);
            string authStr = @"{""appKey"":""" + config.Appkey + @""",""authInfo"":""" + signvalue + @""",""orgId"":""""}";
            string encode = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(authStr));
            string serverUrl = config.TPlusUrl;
            if (serverUrl.Contains("api/v1"))
            {
                serverUrl = serverUrl.Replace("api/v1", "api/v2");
            }
            string host = serverUrl.Substring(0, serverUrl.IndexOf('/', serverUrl.IndexOf("//") + 2) + 1);
            TRestClient restclient = new TRestClient(host);
            ITRestRequest restquest = new TRestRequest();
            restquest.Resource = serverUrl.Replace(host, "") + "collaborationapp/GetRealNameTPlusToken?IsFree=1";
            restquest.AddParameter("Authorization", encode, TParameterType.HttpHeader);
            string    pass = EncodeMD5(config.PassWord);
            string args = string.Format("{{userName:\"{0}\",password:\"{1}\",accNum:\"{2}\"}}", config.UserName, pass, config.AccountNum);
            restquest.AddParameter("_args", args);
            restquest.Method = TMethod.POST;
            string responsedata = restclient.Execute(restquest);
            Newtonsoft.Json.Linq.JObject token = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(responsedata);
            if (token["access_token"] != null)
            {
              string   token_v2 = token["access_token"].ToString();
                result.Success = true;
                result.Message = token_v2;
            }
            else
            {
                result.Success = false;
                result.Message = responsedata;
            }
            return result;
        }



        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeMD5(string str)
        {
            UTF8Encoding encode = new UTF8Encoding();
            Byte[] hashByte = new byte[] { };

            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            hashByte = md5.ComputeHash(encode.GetBytes(str));

            return Ufida.T.EAP.Aop.Util.SpereTool.ConvertBytesToHex(hashByte, false);
        }

        /// <summary>
        /// T+接口跑送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static Result PostData(string token,string PostData,string ApiName)
        {
            Result result = new Result();
            //v2
            try
            {
                Config config = new Config();
                //业务请求的Authorization
                var customParas = new Dictionary<string, object>
                {
                    {"access_token", token},

                };

                //默认规则是当前参数+appsecret，组成签名的原值
                var bizheader = new Dictionary<string, object>
                {
                    {"appkey", config.Appkey},
                    {"orgid",string.Empty},
                    {"appsecret", config.Appsecret}
                };
                string privateKeyPath = XmlPath.PemPath;
              

                RestSharp.Serializers.JsonSerializer jsonSerializer = new RestSharp.Serializers.JsonSerializer();
                string bizdatas = jsonSerializer.Serialize(bizheader);
                Ufida.T.EAP.Net.security.TokenManage tokenManage = new Ufida.T.EAP.Net.security.TokenManage();
                string bizAuthorization = tokenManage.CreateSignedToken(bizdatas, privateKeyPath, customParas);
                ITRestRequest restquest1 = new TRestRequest();
                restquest1.Method = TMethod.POST;
                string serverUrl = config.TPlusUrl;
                string host = serverUrl.Substring(0, serverUrl.IndexOf('/', serverUrl.IndexOf("//") + 2) + 1);
                restquest1.Resource = serverUrl.Replace(host, "") + ApiName;
                string authStr1 = @"{""appKey"":""" + config.Appkey + @""",""authInfo"":""" + bizAuthorization + @""",""orgId"":" + @"""""" + @"}";
                string encode1 = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(authStr1));
                restquest1.AddParameter("Authorization", encode1, TParameterType.HttpHeader);
                restquest1.AddParameter("_args", PostData);
                TRestClient restclient = new TRestClient(host);
                string responsedata = restclient.Execute(restquest1);
                try {
                    result.Success = true;
                    result.Message = responsedata;
                } catch {
                    result.Success = false;
                    result.Message = responsedata.Replace("'", "''");
                }
           
            }
            catch (RestException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            
            }
            return result;
        }
    }
}
