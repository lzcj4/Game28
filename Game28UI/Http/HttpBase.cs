using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Game28UI.Http
{
    public abstract class HttpBase
    {
        CookieContainer Cookie { get; set; }
        public HttpBase()
        {
            this.Cookie = new CookieContainer();
        }

        protected virtual void SetHttpHeader(HttpWebRequest request)
        {
            if (request == null)
            {
                return;
            }

            request.KeepAlive = true;
            request.Host = "www.juxiangyou.com";
            request.Referer = "http://www.juxiangyou.com/";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2914.3 Safari/537.36";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            request.CookieContainer = this.Cookie;
        }

        public HttpWebRequest GetRequest(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            SetHttpHeader(request);
            return request;
        }

        public HttpWebRequest PostRequest(string url, string content)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            SetHttpHeader(request);
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(content);
            }

            return request;
        }

        protected string GetHtml(HttpWebRequest request)
        {
            using (WebResponse response = request.GetResponse())
            {
                //using (GZipStream strem = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                using (Stream strem = response.GetResponseStream())
                {

                    Encoding gb2312Encoding = Encoding.GetEncoding("utf-8");
                    using (StreamReader sr = new StreamReader(strem, gb2312Encoding))
                    {
                        string result = sr.ReadToEnd();
                        return result;
                    }
                }
            }
        }

        protected Stream GetStream(HttpWebRequest request)
        {
            using (WebResponse response = request.GetResponse())
            {
                MemoryStream ms = new MemoryStream();
                using (Stream stream = response.GetResponseStream())
                {
                    int len = 1024;
                    int pos = 0;
                    int readLen = 1;
                    byte[] buffer = new byte[len];
                    while (readLen > 0)
                    {
                        readLen = stream.Read(buffer, pos, len);
                        if (readLen > 0)
                        {
                            ms.Write(buffer, 0, readLen);
                            Array.Clear(buffer, 0, len);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return ms;
            }
        }
    }
}
