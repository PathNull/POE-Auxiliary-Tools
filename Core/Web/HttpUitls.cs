using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Web
{
    public class HttpUitls
    {
        public static string Get(string Url,string token)
        {
            //System.GC.Collect();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36";
            request.ContentType = "application/json";
            request.CookieContainer = new CookieContainer();
            Cookie cookie = new Cookie("POESESSID", token, "/", "poe.game.qq.com");
            request.CookieContainer.Add(cookie);
            request.AutomaticDecompression = DecompressionMethods.GZip;


            System.Net.HttpWebResponse response = null;
            Stream myResponseStream = null;
            StreamReader myStreamReader = null;
            try
            {
                response =  (HttpWebResponse)request.GetResponse();
                myResponseStream = response.GetResponseStream();
                myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

               
                return retString;
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
            finally
            {
                if (myStreamReader != null)
                {
                    myStreamReader.Close();
                }
                if (myResponseStream != null)
                {
                    myResponseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }

          

          
        }

        /// <summary> WebService：Post调用
        /// </summary>
        /// <param name="url">Webservice地址</param>
        /// <param name="paramsOfUrl">传入数据</param>
        /// <param name="priceType">价格通货类型</param>
        /// <returns>返回结果</returns>
        public static string DoPost(string url, string token, Hashtable paramsOfUrl,string priceType)
        {
            if (url == null)
            {
                throw new Exception("WebService地址为空");
            }
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            // 编辑并Encoding提交的数据 
            string json = "";
            if (paramsOfUrl["tjId"] != null)
            {
                 json = "{\"query\":{\"status\":{\"option\":\"any\"},\"stats\":[{\"type\":\"and\",\"filters\":[{\"id\":\"" + paramsOfUrl["tjId"] + "\"}]}]},\"sort\":{\"price\":\"asc\"}}";
            }
            else
            {
                 json = "{\"query\":{\"status\":{\"option\":\"any\"},\"type\":\"" + paramsOfUrl["name"] + "\",\"stats\":[{\"type\":\"and\",\"filters\":[]}],\"filters\":{\"trade_filters\":{\"filters\":{\"price\":{\"option\":\"" + priceType + "\"}}}}},\"sort\":{\"price\":\"asc\"}}";
            }
            
            
            byte[] data = Encoding.UTF8.GetBytes(json);

            

            // 获得回复 
            System.Net.HttpWebResponse response;
            StreamReader reader = null;
            try
            {
                // 发送请求 
                request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json; charset=UTF-8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36";
                request.ContentType = "application/json";
                request.CookieContainer = new CookieContainer();
                Cookie cookie = new Cookie("POESESSID", token, "/", "poe.game.qq.com");
                request.CookieContainer.Add(cookie);
                request.ContentLength = data.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                response = (System.Net.HttpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return result;
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary> 拼接参数串----Get
        /// </summary>
        /// <param name="paramsOfUrl">参数项</param>
        /// <returns>返回字符串</returns>
        private static String GetJointSOfParams(System.Collections.Hashtable paramsOfUrl)
        {
            if (paramsOfUrl == null || paramsOfUrl.Count == 0) return String.Empty;
            // 编辑并Encoding提交的数据 
            StringBuilder sbuilder = new StringBuilder();
            int i = 0;
            foreach (DictionaryEntry de in paramsOfUrl)
            {
                string value = ToHttpChar(de.Value.ToString());
                if (i == 0)
                {
                    sbuilder.Append(de.Key + "=" + value);
                }
                else
                {
                    sbuilder.Append("&" + de.Key + "=" + value);
                }
                i++;
            }
            return sbuilder.ToString();
        }

        /// <summary> 拼接参数串----Post
        /// </summary>
        /// <param name="paramsOfUrl">参数项</param>
        /// <returns>返回字节数组</returns>
        private static byte[] GetJointBOfParams(Hashtable paramsOfUrl)
        {
            // 编辑并Encoding提交的数据 
            String stringJointOfParams = GetJointSOfParams(paramsOfUrl);
            byte[] data = new ASCIIEncoding().GetBytes(stringJointOfParams);
            return data;
        }

        /// <summary> 转义特殊字符
        /// </summary>
        private static string ToHttpChar(string value)
        {
            value = value.ToString().Replace("+", "%2B");
            //value = value.ToString().Replace(" ", "%20");
            //value = value.ToString().Replace("/", "%2F");
            //value = value.ToString().Replace("?", "%3F");
            //value = value.ToString().Replace("%", "%25");
            //value = value.ToString().Replace("#", "%23");
            //value = value.ToString().Replace("&", "%26");
            //value = value.ToString().Replace("=", "%3D");
            //value = value.ToString().Replace(@"\", "%5C");
            //value = value.ToString().Replace(".", "%2E");
            //value = value.ToString().Replace(":", "%3A");
            return value;
        }

        /// <summary>  
        /// 调用api返回json  
        /// </summary>  
        /// <param name="url">api地址</param>  
        /// <param name="jsonstr">接收参数</param>  
        /// <param name="type">类型</param>  
        /// <returns></returns>  
        public static string HttpApi(string url, string jsonstr, string type)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//webrequest请求api地址  
            request.Accept = "text/html,application/xhtml+xml,*/*";
            request.ContentType = "application/json";
            request.Method = type.ToUpper().ToString();//get或者post  
            byte[] buffer = encoding.GetBytes(jsonstr);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

    }

}
