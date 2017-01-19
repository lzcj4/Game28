using System.Net;
using System.IO;

namespace Game28.Game
{
    public class Crazy28 : GameBase
    {
        private const string host = "http://www.juxiangyou.com/";
        private const string crazy28_url = host + "fun/play/crazy28/index";
        private const string verify_url = host + "verify";
        private const string login_url = host + "login/auth";

        public Stream GetVerifyCode()
        {
            HttpWebRequest request = this.GetRequest(crazy28_url);
            request = this.GetRequest(verify_url);
            Stream stream = this.GetStream(request);
            return stream;
        }

        public bool Login(string user, string pwd, string verifyCode)
        {
            string txt = @"jxy_parameter=%7B%22c%22%3A%22index%22%2C%22fun%22%3A%22login%22%2C%22account%22%3A%22{0}%22%2C%22password%22%3A%22{1}%22%2C%22verificat_code%22%3A%22{2}%22%2C%22is_auto%22%3Atrue%7D";

            string postTxt = string.Format(txt, user, pwd, verifyCode);
            HttpWebRequest request = this.PostRequest(login_url, postTxt);
            request.Referer = "http://www.juxiangyou.com/login/index?redirectUrl=/fun/play/crazy28/index";
            string html = this.GetHtml(request);
            bool result = html.Contains("10000") || html.Contains("/fun/play/crazy28/index");
            return result;

        }
    }
}
