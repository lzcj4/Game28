using Game28UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Game28UI.Http
{
    public class GameHttp : HttpBase
    {
        protected override void SetHttpHeader(HttpWebRequest request)
        {
            base.SetHttpHeader(request);
            request.Host = "www.isuwen.com:5000";
            request.ContentType = "application/json";
        }

        public IList<RoundModel> GetGameRounds(JObject jObj)
        {
            string data = jObj.ToString();
            string url = "http://www.isuwen.com:5000/rounds";
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
