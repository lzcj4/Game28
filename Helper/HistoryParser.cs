using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game28.Model;
using System.Windows.Forms;
using Game28.DB;

namespace Game28.Helper
{
    class HistoryParser
    {
        public IList<HistoryInfo> GetHistoryFromTable(HtmlElement table)
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


        public IList<HistoryInfo> GetRowsFromHtml(string htmlTable)
        {
            IList<HistoryInfo> result = new List<HistoryInfo>();
            string table = htmlTable;
            #region sample

            //<tr>
            //   <td class="a1" bgcolor="#ffffff">55830</td>
            //   <td class="a2" bgcolor="#ffffff">07-10<br>08:27</td>
            //   <td class="a3" bgcolor="#ffffff">
            //           <img src="http://image.juxiangyou.com/speed28/num2.gif">
            //           <img src="http://image.juxiangyou.com/speed28/numj.gif">
            //           <img src="http://image.juxiangyou.com/speed28/num6.gif">
            //           <img src="http://image.juxiangyou.com/speed28/numj.gif">
            //           <img src="http://image.juxiangyou.com/speed28/num9.gif">
            //           <img src="http://image.juxiangyou.com/speed28/numd.gif">
            //           <img src="http://image.juxiangyou.com/speed28/num17o.gif">
            //   </td>
            //   <td class="a4" bgcolor="#ffffff"><span class="udcl">23,344,882</span><img src="http://image.juxiangyou.com/images/xzgame/ud.gif"></td>
            //   <td class="a5" bgcolor="#ffffff"><a href="/speed28/list.php?a=55830">25</a></td>
            //   <td class="a6" bgcolor="#ffffff"><span class="udcl">收:0</span><br><span class="da3">竞:0</span></td>
            //   <td class="a7" bgcolor="#ffffff"><span class="da3">已开奖</span></td>
            //<td class="a7" bgcolor="#ffffff"><a class="jcbtn" href="/speed28/betting.php?a=55925">竞猜</a></td>
            // </tr>

            //<td bgcolor="#FFFFFF" class="a1">54971</td>
            //        <td bgcolor="#FFFFFF" class="a2">07-09<br />10:59</td>
            //        <td bgcolor="#FFFFFF" class="a3"><img src="http://image.juxiangyou.com/speed28/num2.gif" /><img src="http://image.juxiangyou.com/speed28/numj.gif" /><img src="http://image.juxiangyou.com/speed28/num9.gif" /><img src="http://image.juxiangyou.com/speed28/numj.gif" /><img src="http://image.juxiangyou.com/speed28/num4.gif" /><img src="http://image.juxiangyou.com/speed28/numd.gif" /><img src="http://image.juxiangyou.com/speed28/num15o.gif" /></td>
            //        <td bgcolor="#FFFFFF" class="a4"><span class="udcl">2,942,455,775</span><img src="http://image.juxiangyou.com/images/xzgame/ud.gif" /></td>
            //        <td bgcolor="#FFFFFF" class="a5"><a href="/speed28/list.php?a=54971">369</a></td>
            //        <td bgcolor="#FFFFFF" class="a6"><span class="udcl">收:0</span><br /><span class="da3">竞:0</span></td>
            //        <td bgcolor="#FFFFFF" class="a7"><span class="da3">已开奖</span></td>

            #endregion

            string rowStart = "<tr>", rowEnd = "</tr>";

            while (true)
            {
                string row = TextHelper.GetSubstring(table, rowStart, rowEnd);
                if (string.IsNullOrEmpty(row))
                {
                    break;
                }
                table = table.Replace(string.Format("{0}{1}{2}", rowStart, row, rowEnd), "");
                string state = TextHelper.GetSubstring(row, "a7", "/td>");
                if (!state.Contains("已开奖"))
                {
                    continue;
                }

                string colEnd = "/td>", valueStart = ">", valueEnd = "<";
                string roundId = TextHelper.GetSubstring(row, "a1", colEnd);
                roundId = TextHelper.GetSubstring(roundId, valueStart, valueEnd);
                if (DBHelper.Instance.IsContainRoundId(roundId))
                {
                    return result;
                }
                string date = TextHelper.GetSubstring(row, "a2", colEnd).Replace("<br />", "  ").Replace("<br>", "  "); ;
                date = TextHelper.GetSubstring(date, valueStart, valueEnd);

                string num = TextHelper.GetSubstringByEnd(row, "<img src=\"http://image.juxiangyou.com/speed28/num", "o.gif");

                string totalAmount = TextHelper.GetSubstring(row, "a4", colEnd);
                totalAmount = TextHelper.GetSubstring(totalAmount, "<span", "/span>");
                totalAmount = TextHelper.GetSubstring(totalAmount, valueStart, valueEnd);

                string winner = TextHelper.GetSubstring(row, "a5", colEnd);
                winner = TextHelper.GetSubstring(winner, "<a", "/a>");
                winner = TextHelper.GetSubstring(winner, valueStart, valueEnd);

                string stakeAndWin = TextHelper.GetSubstring(row, "a6", "</td>");
                // <span class="udcl">收:0</span><br /><span class="da3">竞:0</span></td>
                string shou = TextHelper.GetSubstring(stakeAndWin, "<span", "/span>");
                stakeAndWin = stakeAndWin.Replace(string.Format("{0}{1}{2}", "<span", shou, "/span>"), "");
                shou = TextHelper.GetSubstring(shou, valueStart, valueEnd).Replace("收:", "");

                string jin = TextHelper.GetSubstring(stakeAndWin, "<span", "/span>");
                jin = TextHelper.GetSubstring(jin, valueStart, valueEnd).Replace("竞:", ""); ;

                long amount = long.Parse(shou);
                long stake = long.Parse(jin);

                state = TextHelper.GetSubstring(row, "a7", "/td>");
                state = TextHelper.GetSubstring(state, "<span", "/span>");
                state = TextHelper.GetSubstring(state, valueStart, valueEnd);

                HistoryInfo item = new HistoryInfo();
                item.RoundId = roundId;
                item.Result = int.Parse(num);
                item.Amount = amount;
                item.Date = DateTime.Now.Year.ToString() + "-" + date;
                item.Stake = stake;
                item.WinnerNum = int.Parse(winner);
                item.TotalAmount = long.Parse(totalAmount.Replace(",", ""));

                result.Add(item);
            }
            return result;
        }

    }
}
