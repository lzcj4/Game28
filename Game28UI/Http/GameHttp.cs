using Game28UI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace Game28UI.Http
{
    public class GameHttp : HttpBase
    {
        public const string WEB_HOST = "http://www.isuwen.com:5000";
        //public const string WEB_HOST = "http://127.0.0.1:5000";
        protected override void SetHttpHeader(HttpWebRequest request)
        {
            base.SetHttpHeader(request);
            request.Host = "www.isuwen.com:5000";
            request.ContentType = "application/json";
        }

        public IList<RoundModel> GetGameRounds(JObject jObj)
        {
            string data = jObj.ToString();
            string url = string.Format("{0}/{1}", WEB_HOST, "rounds");
            string html = this.GetHtml(this.PostRequest(url, data));
            JObject json = JObject.Parse(html);
            int count = int.Parse(json["count"].ToString());

            IList<RoundModel> result = new List<RoundModel>();
            if (count > 0)
            {
                JArray jArray = json["items"] as JArray;
                if (!jArray.IsNull())
                {
                    foreach (var item in jArray)
                    {
                        int r_id = int.Parse(item["id"].ToString());
                        int r_value = int.Parse(item["value"].ToString());
                        DateTime r_date = DateTime.Parse(item["date"].ToString());
                        result.Add(new RoundModel() { ID = r_id, Value = r_value, Date = r_date });
                    }
                }
            }
            return result;

        }
    }
}
