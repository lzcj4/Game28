using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;

namespace Game28.Game
{
    class Vote
    {
        //        GET http://wzt.gwd.eyh.cn/index.php/Tp/ny?wxid=ow2WqjjuedHt-PLoYmSeaCpNNE-c&yid=524&callback=Ext.data.JsonP.callback4&_dc=1438657888398 HTTP/1.1
        //Host: wzt.gwd.eyh.cn
        //Accept: */*
        //Connection: keep-alive
        //Connection: keep-alive
        //Cookie: PHPSESSID=lsus45h18o0gevq7usbec11ge1
        //User-Agent: Mozilla/5.0 (iPhone; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12F70 MicroMessenger/6.2.3 NetType/WIFI Language/zh_CN
        //Accept-Language: zh-cn
        //Referer: http://wx.eyh.cn/cytp/?wxid=ow2WqjjuedHt-PLoYmSeaCpNNE-c
        //Accept-Encoding: gzip, deflate

        //        GET http://wx.eyh.cn/cytp?wxid=ow2WqjjuedHt-PLoYmSeaCpNNE-c HTTP/1.1
        //Host: wx.eyh.cn
        //Connection: keep-alive
        //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
        //User-Agent: Mozilla/5.0 (iPhone; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12F70 MicroMessenger/6.2.3 NetType/WIFI Language/zh_CN
        //Accept-Language: zh-cn
        //Accept-Encoding: gzip, deflate
        //Connection: keep-alive


        private void SetHttpHeader(HttpWebRequest request)
        {
            if (request == null)
            {
                return;
            }

            request.KeepAlive = true;
            request.Host = "wzt.gwd.eyh.cn";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12F70 MicroMessenger/6.2.3 NetType/WIFI Language/zh_CN";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Accept-Language", "zh-cn");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Cookie", "PHPSESSID=lsus45h18o0gevq7usbec11ge1");
            request.Referer = "http://wx.eyh.cn/cytp/?wxid=ow2WqjjuedHt-PLoYmSeaCpNNE-D";
        }

        public void StartVote()
        {
            //TestWLL();
            // Test();
            string url = "http://wzt.gwd.eyh.cn/index.php/Tp/ny?wxid=ow2WqjjuedHt-PLoYmSeaCpNNE-d&yid={0}&callback=Ext.data.JsonP.callback4&_dc=1438657888398";
            HttpWebRequest request = WebRequest.Create(string.Format(url, 544)) as HttpWebRequest;
            request.Method = "get";
            SetHttpHeader(request);
            using (WebResponse response = request.GetResponse())
            {
                //using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                //{
                //    string line = sr.ReadToEnd();
                //    Console.WriteLine(line);
                //}
                using (GZipStream gs = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gs))
                    {
                        string line = sr.ReadToEnd();
                    }
                }
            }
        }

        CookieContainer cookieContainer = new CookieContainer();
        private void SetHeaders(HttpWebRequest request, string wxid)
        {
            request.Method = "get";
            request.KeepAlive = true;
            request.Host = "wzt.gwd.eyh.cn";
            request.Accept = "zh-cn";
            request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12F70 MicroMessenger/6.2.4 NetType/WIFI Language/zh_CN";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Referer = string.Format("http://wx.eyh.cn/hos/?wxid={0}", wxid);
            request.Headers.Add("Accept-Language", "zh-cn");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.CookieContainer = cookieContainer;
        }

        private void Test()
        {
            string wxid = "ogJN4s_kCzPv7h2y4uCxYTFdpMzt";
            string[] urls = { "http://wzt.gwd.eyh.cn/index.php/Tp/qstp",
                                "http://wx.eyh.cn/hos/?wxid={0}",
                               "http://wx.eyh.cn/hos/app/model/jcmy.js?_dc=1439257512510",
                               "http://wx.eyh.cn/hos/app/model/mzy.js?_dc=1439257512498",
                               "http://wx.eyh.cn/hos/app/model/my.js?_dc=1439257512494",
                               "http://wx.eyh.cn/hos/app/view/tppn.js?_dc=1439257512520",
                               "http://wx.eyh.cn/hos/app/store/my.js?_dc=1439257512529",
                               "http://wx.eyh.cn/hos/app/store/mzy.js?_dc=1439257512540",
                               "http://wx.eyh.cn/hos/app/store/jcmy.js?_dc=1439257512575",
                               "http://wzt.gwd.eyh.cn/index.php?m=Fl&id=6&_dc=1439257512891&page=1&start=0&limit=25&callback=Ext.data.JsonP.callback1 ",
                               "http://wzt.gwd.eyh.cn/index.php?m=Fl&id=8&_dc=1439257512897&page=1&start=0&limit=25&callback=Ext.data.JsonP.callback2 ",
                               "http://wzt.gwd.eyh.cn/index.php/Tp/qstp?wxid={0}&hid=12&yid=5&callback=Ext.data.JsonP.callback3&_dc=1439257549525",
                               };

            foreach (var item in urls)
            {
                string url = item;
                if (url.Contains("{0}"))
                {
                    url = string.Format(url, wxid);
                }
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                SetHeaders(request, wxid);
                using (WebResponse response = request.GetResponse())
                {
                    Stream stream = null;
                    bool isGzip = false;
                    try
                    {
                        isGzip = response.Headers[HttpResponseHeader.ContentEncoding] == "gzip";
                    }
                    catch (Exception)
                    {
                        isGzip = false;
                    }
                    if (isGzip)
                    {
                        stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    }
                    else
                    {
                        stream = response.GetResponseStream();
                    }

                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string line = sr.ReadToEnd();
                        Console.WriteLine(line);
                    }
                }
            }

        }

        private void Test1()
        {
            string wxid = "ogJN4s_kCzPv7h2y4uCxYTFdpMzs";

            string url = "http://wzt.gwd.eyh.cn/index.php/Tp/ny?wxid={0}&yid=577&callback=Ext.data.JsonP.callback4&_dc=1439183495114 ";
            //string url = "http://mp.weixin.qq.com/s?__biz=MzAxMTAyMzc0NA==&mid=214501929&idx=2&sn=b3af45a8fd9301f521f25cc8e235911e";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "get";
            request.KeepAlive = true;
            request.Host = "wzt.gwd.eyh.cn";
            request.Accept = "zh-cn";
            request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12F70 MicroMessenger/6.2.4 NetType/WIFI Language/zh_CN";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Referer = "http://wx.eyh.cn/hos/?wxid=ogJN4s_kCzPv7h2y4uCxYTFdpMzs";
            request.Headers.Add("Accept-Language", "zh-cn");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Cookie", "PHPSESSID=cpb7ptd1bf3hs2t8gu0kcbpor4");


            using (WebResponse response = request.GetResponse())
            {
                using (GZipStream gs = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gs))
                    {
                        string line = sr.ReadToEnd();
                        Console.WriteLine(line);
                    }
                }

                //using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                //{
                //    string line = sr.ReadToEnd();
                //    Console.WriteLine(line);
                //}
                //using (GZipStream gs = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                //{
                //    using (StreamReader sr = new StreamReader(gs))
                //    {
                //        string line = sr.ReadToEnd();
                //    }
                //}
            }
        }

        CookieContainer cookies = new CookieContainer();
        string openId = "4&openid=okB_st9htJlcH8ZgCOlP3rNcQz8Q";
        private void TestWLL()
        {
            int i = 0;
            while (i++ < 20)
            {
                isFirst = true;
                string html = null;
                string u = "http://bbs1.weilamp.com/plugin.php?id=hejin_toupiao&model=vote&vid=4&openid=" + openId;
                html = SendRequest(u);
                //string url = "http://bbs1.weilamp.com/plugin.php?id=hejin_toupiao&model=vote&vid=4";
                //html = SendRequest(url);
                //  $.get("http://bbs1.weilamp.com/plugin.php?id=hejin_toupiao&model=ticket", { zid: uid,formhash:'637afb54'},
                string hash = TextHelper.GetSubstring(html, "formhash:'", "'},");

                //string url2 = "http://bbs1.weilamp.com/plugin.php?id=hejin_toupiao&model=clicks&vid=4&formhash={0}";
                //html = SendRequest(string.Format(url2, hash), true);
                string url3 = "http://bbs1.weilamp.com/plugin.php?id=hejin_toupiao&model=ticket&zid=9&formhash={0}";
                html = SendRequest(string.Format(url3, hash), true);
            }
        }

        private bool isFirst = true;
        private string SendRequest(string url, bool isRefer = false)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "get";
            request.KeepAlive = true;
            request.Host = "bbs1.weilamp.com";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_4 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12H143 MicroMessenger/6.2.4 NetType/WIFI Language/zh_CN";
            request.ContentType = "application/x-www-form-urlencoded";
            if (isRefer)
            {
                request.Referer = "http://bbs1.weilamp.com/plugin.php?id=hejin_toupiao&model=vote&vid=4";
                request.Accept = "*/*";
            }
            else
            {
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            }
            request.Headers.Add("Cookie", "hjbox_openid=okB_st9htJlcH8ZgCOlP3rNcQz8R; JVrw_2132_lastact=1439348738%09plugin.php%09; JVrw_2132_lastvisit=1439292770; JVrw_2132_saltkey=Kd93x96I; JVrw_2132_sid=j55Zbb");
            request.Headers.Add("Accept-Language", "zh-cn");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");

            //  request.CookieContainer = cookieContainer;

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("GBK")))
                {
                    string line = sr.ReadToEnd();
                    Console.WriteLine(line);
                    return line;
                }
            }
        }

    }
}
