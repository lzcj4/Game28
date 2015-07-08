using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game28.Model;
using System.Windows.Forms;

namespace Game28.Helper
{
    class HistoryParser
    {
        public IList<HistoryInfo> GetHistory(HtmlElement table)
        {
            IList<HistoryInfo> result = new List<HistoryInfo>();
            if (table == null)
            {
                throw new ArgumentException();
            }

            HtmlElementCollection rows = table.GetElementsByTagName("tr");
            if (rows == null || rows.Count == 0)
            {
                return result;
            }

            ///The 1st row are columns' name
            for (int i = 1; i < rows.Count; i++)
            {
                HtmlElement currentRow = rows[i];
                HistoryInfo item = GetItemFromRow(currentRow);
                if (item != null)
                    result.Add(item);
            }

            return result;
        }

        public HistoryInfo GetItemFromRow(HtmlElement row)
        {
            HistoryInfo result = null;
            HtmlElementCollection cols = row.GetElementsByTagName("td");
            if (cols == null || cols.Count != 7 || cols[6].InnerText != "已开奖")
            {
                return result;
            }

            string roundId = cols[0].InnerText;
            string time = cols[1].InnerText.Replace("\r\n", " ");

            HtmlElementCollection imgs = cols[2].GetElementsByTagName("img");
            if (imgs == null || imgs.Count != 7)
            {
                return result;
            }

            //<img src="http://image.juxiangyou.com/speed28/num14o.gif">
            string src = imgs[6].GetAttribute("src");
            int num = -1;
            if (!string.IsNullOrEmpty(src))
            {
                string numStr = src.Replace("http://image.juxiangyou.com/speed28/num", "").Replace("o.gif", "");
                if (!string.IsNullOrEmpty(numStr))
                {
                    if (!int.TryParse(numStr, out num))
                    {
                        throw new ArgumentException();
                    }
                }
            }

            HtmlElementCollection totalBeans = cols[3].GetElementsByTagName("span");
            long totalAmount = 0;
            if (totalBeans != null && totalBeans.Count == 1)
            {
                string totalAmountStr = totalBeans[0].InnerText.Replace(",", "");
                if (!string.IsNullOrEmpty(totalAmountStr))
                {
                    if (!long.TryParse(totalAmountStr, out totalAmount))
                    {
                        throw new ArgumentException();
                    }
                }
            }

            int winner = 0;
            string winnerStr = cols[4].InnerText.Replace(",", "");
            if (!string.IsNullOrEmpty(winnerStr))
            {
                if (!int.TryParse(winnerStr, out winner))
                {
                    throw new ArgumentException();
                }
            }

            long amount = 0, stake = 0;
            //<td bgcolor="#FFFFFF" class="a6"><span class="udcl">收:0</span><br><span class="da3">竞:0</span></td>
            HtmlElementCollection beans = cols[5].GetElementsByTagName("span");
            if (beans != null && beans.Count == 2)
            {
                string winStr = beans[0].InnerText.Replace("收:", "").Replace(",", "");
                if (!string.IsNullOrEmpty(winStr))
                {
                    if (!long.TryParse(winStr, out amount))
                    {
                        throw new ArgumentException();
                    }
                }
                string staketr = beans[1].InnerText.Replace("竞:", "").Replace(",", "");
                if (!string.IsNullOrEmpty(staketr))
                {
                    if (!long.TryParse(staketr, out stake))
                    {
                        throw new ArgumentException();
                    }
                }
            }

            result = new HistoryInfo();
            result.RoundId = roundId;
            result.Result = num;
            result.Stake = stake;
            result.TotalAmount = totalAmount;
            result.WinnerNum = winner;
            result.Amount = amount;
            result.Date = string.Format("{0}-{1}", DateTime.Now.Date.Year, time);
            return result;
        }


    }
}
