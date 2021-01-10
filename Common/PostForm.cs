using BusModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace Common
{
    public class PostForm
    {
        /// <summary>
        /// 返回参数
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static string GetPostParm(Dictionary<string, string> dic)
        {
            if (!dic.Any())
            {
                return "";
            }
            string strContent = string.Empty;
            foreach (var p in dic)
            {
                strContent += "&" + p.Key + "=" + p.Value;
            }
            return strContent.TrimStart('&');
        }


        /// <summary>
        /// T6/U8 Post XML数据
        /// </summary>
        /// <param name="v_strURL"></param>
        /// <param name="v_objXMLDoc"></param>
        /// <returns></returns>
        public static XmlDocument PostXMLTransaction(string v_strURL, XmlDocument v_objXMLDoc)
        {
            //Declare XMLResponse document
            XmlDocument XMLResponse = null;
            //Declare an HTTP-specific implementation of the WebRequest class.
            HttpWebRequest objHttpWebRequest;
            //Declare an HTTP-specific implementation of the WebResponse class
            HttpWebResponse objHttpWebResponse = null;
            //Declare a generic view of a sequence of bytes
            Stream objRequestStream = null;
            Stream objResponseStream = null;
            //Declare XMLReader
            XmlTextReader objXMLReader;
            //Creates an HttpWebRequest for the specified URL.
            objHttpWebRequest = (HttpWebRequest)WebRequest.Create(v_strURL);
            try
            {
                //---------- Start HttpRequest 

                //Set HttpWebRequest properties
                byte[] bytes;
                //Encoding gb2312 = Encoding.GetEncoding("gb2312");
                Encoding utf8 = Encoding.UTF8;
                bytes = utf8.GetBytes(v_objXMLDoc.InnerXml);
                objHttpWebRequest.Method = "POST";
                objHttpWebRequest.ContentLength = bytes.Length;
                objHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
                //Get Stream object 
                objRequestStream = objHttpWebRequest.GetRequestStream();
                //Writes a sequence of bytes to the current stream 
                objRequestStream.Write(bytes, 0, bytes.Length);
                //Close stream
                objRequestStream.Close();
                //---------- End HttpRequest

                //Sends the HttpWebRequest, and waits for a response.
                objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();
                //---------- Start HttpResponse
                if (objHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    //Get response stream 
                    objResponseStream = objHttpWebResponse.GetResponseStream();

                    //Load response stream into XMLReader
                    objXMLReader = new XmlTextReader(objResponseStream);

                    //Declare XMLDocument
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(objXMLReader);

                    //Set XMLResponse object returned from XMLReader
                    XMLResponse = xmldoc;

                    //Close XMLReader
                    objXMLReader.Close();
                }

                //Close HttpWebResponse
                objHttpWebResponse.Close();
            }
            catch (WebException we)
            {
                //TODO: Add custom exception handling
                throw new Exception(we.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //Close connections
                objRequestStream.Close();
                objResponseStream.Close();
                objHttpWebResponse.Close();

                //Release objects
                objXMLReader = null;
                objRequestStream = null;
                objResponseStream = null;
                objHttpWebResponse = null;
                objHttpWebRequest = null;
            }

            //Return
            return XMLResponse;
        }



        /// <summary>
        /// 推送档案信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static List<U8Result> PostXml(string url, XmlDocument dom)
        {

            XmlDocument xmlResult = PostXMLTransaction(url, dom);
            XmlNodeList itemList = xmlResult.SelectNodes("ufinterface/item");

            List<U8Result> resultList = new List<U8Result>();
            foreach (XmlNode item in itemList)
            {
                U8Result result = new U8Result();
                result.Key = item.Attributes["key"].Value;
                result.Succeed = item.Attributes["succeed"].Value;
                result.Dsc = item.Attributes["dsc"].Value;
                resultList.Add(result);
            }
            return resultList;
        }

        /// <summary>
        /// CRM系统推送XML数据
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public static string PostXMLToCrm(string Url, string postDataStr)
        {
            string ret = string.Empty;
            try
            {
                Encoding UTF8 = Encoding.GetEncoding("utf-8");
                byte[] byteArray = UTF8.GetBytes(postDataStr); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(Url));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

    }
}
