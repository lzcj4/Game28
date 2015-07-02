using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Game28
{
    public enum ResultCode
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unkown = 0,
        /// <summary>
        /// 投注成功
        /// </summary>
        Succeed,
        /// <summary>
        /// 该期已经开奖
        /// </summary>
        OutOfTime,
        /// <summary>
        /// 格式错误
        /// </summary>
        FormatError,
        /// <summary>
        /// 正在开奖
        /// </summary>
        Openging
    }

    class Speed28ResultEventArgs : EventArgs
    {
        public int RoundId { get; private set; }
        public int[] Values { get; private set; }
        public string ResultDescription { get; set; }
        public ResultCode ResultCode { get; set; }

        public Speed28ResultEventArgs(ResultCode resultCode, string description, int roundId, int[] values)
        {
            ResultCode = resultCode;
            ResultDescription = description;
            RoundId = roundId;
            Values = values;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("当前期号：{0},投注结果:{1}", RoundId, ResultDescription));
            string valuesStr = string.Empty;
            for (int i = 0; i < Values.Length; i++)
            {
                valuesStr += string.Format("{0}={1},", i, Values[i]);
            }
            sb.AppendLine(valuesStr);
            return sb.ToString();
        }
    }

    class Speed28
    {
        public const int MaxLen = 28;

        private string Cookies { get; set; }
        const string urlFormat = "http://game.juxiangyou.com/speed28/betting.php?a={0}";

        public event EventHandler<Speed28ResultEventArgs> StateChanged;

        public Speed28(string cookies)
        {
            if (string.IsNullOrEmpty(cookies))
            {
                throw new ArgumentException();
            }

            this.Cookies = cookies;
        }

        #region test http request

        //POST http://game.juxiangyou.com/speed28/betting.php?a=45379 HTTP/1.1
        //Host: game.juxiangyou.com
        //Proxy-Connection: keep-alive
        //Content-Length: 695
        //Cache-Control: max-age=0

        //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
        //Origin: http://game.juxiangyou.com
        //User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36
        //Content-Type: application/x-www-form-urlencoded
        //Referer: http://game.juxiangyou.com/speed28/betting.php?a=45379
        //Accept-Encoding: gzip, deflate
        //Accept-Language: zh-CN,zh;q=0.8,en;q=0.6
        //Cookie: Hm_lvt_3c3148dd83879e6e382aaed4c28f8b51=1417587277,1417772040; isMiniLogin=1; PHPSESSID=frplibtafcuko7ijd8d8all0i7; CNZZDATA5330249=cnzz_eid%3D1365108283-1434673027-http%253A%252F%252Fwww.juxiangyou.com%252F%26ntime%3D1435542968; Hm_lvt_ba6a689cacbcb9ef7318f568c35bd6df=1434674786,1434675221,1435024494,1435540159; Hm_lpvt_ba6a689cacbcb9ef7318f568c35bd6df=1435547384

        //geetest_challenge=&geetest_validate=&geetest_seccode=&tbSpeed28Chk%5B0%5D=on&tbSpeed28Value%5B%5D=100&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=&tbSpeed28Value%5B%5D=

        #endregion

        public ResultCode StartNewRound(int roundId, int[] values)
        {
            if (roundId <= 0 || values.Length != Speed28.MaxLen)
            {
                throw new ArgumentException();
            }

            string url = string.Format(urlFormat, roundId);
            HttpWebRequest request = WebRequest.Create(string.Format(urlFormat, roundId)) as HttpWebRequest;

            request.Method = "Post";
            request.KeepAlive = true;
            request.Host = "game.juxiangyou.com";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Referer = url;
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            if (!string.IsNullOrEmpty(Cookies))
                request.Headers.Add("Cookie", Cookies);

            string repContent = "geetest_challenge=&geetest_validate=&geetest_seccode=";
            string flagFormat = "&tbSpeed28Chk%5B{0}%5D=on";
            string itemValueFormat = "&tbSpeed28Value%5B%5D={0}";
            for (int i = 0; i < values.Length; i++)
            {
                int item = values[i];
                if (item <= 0)
                {
                    repContent += string.Format(itemValueFormat, "");
                }
                else
                {
                    repContent += string.Format(flagFormat, i) + string.Format(itemValueFormat, item);
                }
            }

            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(repContent);
            }

            ResultCode result = ResultCode.Unkown;
            string desc = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                using (GZipStream gs = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                {
                    Encoding gb2312Encoding = Encoding.GetEncoding("gb2312");
                    using (StreamReader sr = new StreamReader(gs, gb2312Encoding))
                    {
                        string line = sr.ReadToEnd();
                        desc = TextHelper.GetSubstring(line, "alert('", "');");
                        if (line.Contains("投注成功"))
                            result = ResultCode.Succeed;
                        else if (line.Contains("该期已经开奖"))
                            result = ResultCode.OutOfTime;
                        else if (line.Contains("格式错误"))
                            result = ResultCode.FormatError;
                    }
                }
            }

            if (StateChanged != null)
            {
                StateChanged(this, new Speed28ResultEventArgs(result, desc, roundId, values));
            }
            return result;
        }
    }
}
