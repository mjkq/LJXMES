using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataHelper
{
    public class PostData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="method"></param>
        /// <param name="jsonParas"></param>
        /// <param name="nk"></param>
        /// <returns></returns>
        public static Tuple<bool, string> CallSH(string Url, string method, string jsonParas, NetworkCredential nk)
        {
            string strURL = Url;
            bool a = true;
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式  
            request.Method = method;
            if (nk != null)
            {
                request.Credentials = GetCredentialCache(Url, nk.UserName, nk.Password);
                request.Headers.Add("Authorization", GetAuthorization(nk.UserName, nk.Password));
            }
            if (!method.ToUpper().Equals("GET"))
            {
                request.ContentType = "application/json;";
                request.Timeout = 1000 * 180;
                request.ReadWriteTimeout = 1000 * 180;
                byte[] payload;
                //将Json字符串转化为字节  
                payload = Encoding.UTF8.GetBytes(jsonParas);
                //设置请求的ContentLength   
                request.ContentLength = payload.Length;
                //发送请求，获得请求流  
                Stream writer;
                try
                {
                    writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象                      
                    writer.Write(payload, 0, payload.Length);  //将请求参数写入流
                    writer.Close();//关闭请求流
                }
                catch (Exception ex)
                {
                    writer = null;
                    a = false;
                    return new Tuple<bool, string>(a, ex.Message);
                }

            }
            else
            {
                request.ContentType = "text/html;charset=UTF-8";
            }

            String strValue = "";//strValue为http响应所返回的字符流

            HttpWebResponse response = null;
            Stream s = null;
            StreamReader oSReader = null;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
                s = response.GetResponseStream();
                oSReader = new StreamReader(s, Encoding.UTF8);
                strValue = oSReader.ReadToEnd();

            }
            catch (WebException e)
            {
                a = false;
                strValue = e.Message;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (response != null)
                {
                    oSReader.Close();
                    oSReader = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
                request = null;
            }
            return new Tuple<bool, string>(a, strValue);
        }

        #region # 生成 Http Basic 访问凭证 #
        private static CredentialCache GetCredentialCache(string uri, string username, string password)
        {
            string authorization = string.Format("{0}:{1}", username, password);
            CredentialCache credCache = new CredentialCache();
            credCache.Add(new Uri(uri), "Basic", new NetworkCredential(username, password));
            return credCache;
        }
        private static string GetAuthorization(string username, string password)
        {
            string authorization = string.Format("{0}:{1}", username, password);
            return "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(authorization));
        }
        #endregion

        /// <summary>
        /// 使用HttpClient抛数据
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="method"></param>
        /// <param name="jsonParas"></param>
        /// <returns></returns>
        public static Tuple<bool, string> PotDataByClient(string Url, string method, string jsonParas)
        {
            try
            {

                if (Url.StartsWith("https"))
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                HttpContent httpContent = new StringContent(jsonParas, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient httpClient = new HttpClient();


                HttpResponseMessage response = httpClient.PostAsync(Url, httpContent).Result;

                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result.Replace("\\\"", "\"").Trim('\"');
                return new Tuple<bool, string>(true, s);


            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }

        }



        /// <summary>
        /// 登录请求获取Cookie
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="requestUrlString"></param>
        /// <returns></returns>
        public static Tuple<bool, CookieContainer, string> PostLogin(string postData, string requestUrlString)
        {
            CookieContainer cookies = new CookieContainer();
            try
            {

                //ASCIIEncoding encoding = new ASCIIEncoding();
                //byte[] data = encoding.GetBytes(postData);
                byte[] data = Encoding.UTF8.GetBytes(postData);
                //向服务端请求
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(requestUrlString);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                myRequest.ContentLength = data.Length;
                myRequest.CookieContainer = new CookieContainer();
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                //将请求的结果发送给客户端(界面、应用)
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                cookies.Add(myResponse.Cookies);
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string returnData = reader.ReadToEnd();
                return new Tuple<bool, CookieContainer, string>(true, cookies, returnData);

            }
            catch (Exception ex)
            {
                return new Tuple<bool, CookieContainer, string>(false, cookies, ex.Message);
            }
        }

        /// <summary>
        /// 带COOKIE发送请求
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="requestUrlString"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static Tuple<bool, string> PostDataWithCokie(string postData, string requestUrlString, CookieContainer cookie)
        {
            try
            {

                byte[] data = Encoding.UTF8.GetBytes(postData);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(requestUrlString);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                myRequest.ContentLength = data.Length;
                myRequest.CookieContainer = cookie;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string returnData = reader.ReadToEnd();
                return new Tuple<bool, string>(true, returnData);

            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }

        }
    }
}
