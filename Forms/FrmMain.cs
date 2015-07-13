using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Game28.DB;
using Game28.Helper;
using Game28.Model;
using Game28.UC;

namespace Game28
{
    public partial class FrmMain : Form
    {
        bool isLogined = false;
        string cookies = string.Empty;

        bool isStarted = false;
        HistoryParser parser = new HistoryParser();
        DBHelper dbHelper = DBHelper.Instance;

        public FrmMain()
        {
            InitializeComponent();
            this.Load += (sender, e) =>
            {
                dataGridHistory.AutoGenerateColumns = false;
                dataGridStatistic.AutoGenerateColumns = false;
                // dataGridHistory.RowPrePaint += dataGridHistory_RowPrePaint;
                LoadParams();
                this.webView.Navigate("http://www.juxiangyou.com/");
            };

            this.FormClosing += (sender, e) =>
            {
                SaveParams();
            };

            this.webView.DocumentCompleted += webView_DocumentCompleted;
        }
        void webView_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            CheckLogin();
            if (e.Url.OriginalString.StartsWith(speed28Url))
            {
                HtmlElementCollection tableCollection = this.webView.Document.GetElementsByTagName("table");

                if (tableCollection != null && tableCollection.Count > 0)
                {
                    string table = tableCollection[0].InnerHtml;
                    Action action = () =>
                    {
                        var rows = parser.GetRowsFromHtml(table);
                        dbHelper.InsertHistory(rows);
                    };
                    action.BeginInvoke((ar) => action.EndInvoke(ar), null);
                }
            }
        }

        private void CheckLogin()
        {
            if (this.webView.Document == null || this.webView.Document.Body == null ||
                string.IsNullOrEmpty(this.webView.Document.Body.InnerText))
            {
                return;
            }
            cookies = this.webView.Document.Cookie;
            if (!string.IsNullOrEmpty(cookies))
            {
                speed28 = new Speed28(cookies);
                speed28.StateChanged += speed28_StateChanged;
            }

            string htmlBody = this.webView.Document.Body.InnerText;
            if (!isLogined)
            {
                var userItem = this.webView.Document.GetElementById("loginUser");
                if (userItem != null)
                    userItem.SetAttribute("value", txtUser.Text);

                var pwdItem = this.webView.Document.GetElementById("loginPwd");
                if (pwdItem != null)
                    pwdItem.SetAttribute("value", txtPwd.Text);

                if (!isLogined &&
                    (htmlBody.Contains("ID:") || htmlBody.Contains("签到") || htmlBody.Contains("会员中心") ||
                     htmlBody.Contains("当前U币")))
                {
                    isLogined = true;
                    NavigateToSpeed28();
                }
            }

            if (string.IsNullOrEmpty(txtRoundId.Text) && htmlBody.Contains("离竞猜截止时间"))
            {
                string id = GetLatestRoundId();
                if (!string.IsNullOrEmpty((id)))
                {
                    txtRoundId.Text = id;
                }
                GetCurrentBeans();
            }
        }

        private string speed28Url = "http://game.juxiangyou.com/speed28/index.php";
        private void NavigateToSpeed28()
        {
            this.webView.Navigate(speed28Url);
        }

        private void LoadParams()
        {
            txtUser.Text = AppSetting.User;
            txtPwd.Text = AppSetting.Pwd;
            txtInterval.Text = AppSetting.Interval;
            txtMaxLimit.Text = AppSetting.MaxLimit.ToString("N0");
        }

        private void SaveParams()
        {
            AppSetting.User = txtUser.Text;
            AppSetting.Pwd = txtPwd.Text;
            AppSetting.Interval = txtInterval.Text;
            AppSetting.MaxLimit = int.Parse(txtMaxLimit.Text.Replace(",", ""));
            AppSetting.Save();
        }

        #region Button event handlers

        Speed28 speed28;

        bool isAuto = false;
        private void btnAuto_Click(object sender, EventArgs e)
        {
            CheckLogin();
            if (!isLogined)
            {
                MessageBox.Show("请先登录");
                return;
            }
            SaveParams();
            isAuto = true;

            txtInterval.Enabled = false;
            txtRoundId.Enabled = false;
            btnAuto.Enabled = false;
            btnManual.Enabled = false;
            this.chkOss.Enabled = false;

            StartByClick();
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            CheckLogin();
            if (!isLogined)
            {
                MessageBox.Show("请先登录");
                return;
            }
            SaveParams();
            StartByClick();
        }

        private void btnOss_Click(object sender, EventArgs e)
        {
            CheckLogin();
            if (!isLogined)
            {
                MessageBox.Show("请先登录");
                return;
            }
            this.btnAuto.Enabled = false;
            this.btnManual.Enabled = false;
            this.txtRoundId.Enabled = false;
            this.txtInterval.Enabled = false;
            this.chkOss.Enabled = false;
            SaveParams();
            StartOssTimer();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isAuto = false;

            isStarted = false;
            txtInterval.Enabled = true;
            txtRoundId.Enabled = true;
            btnAuto.Enabled = true;
            this.btnManual.Enabled = true;
            this.chkOss.Enabled = true;
            txtRoundId.ForeColor = Color.Black;

            StopTimer();
            StopOssTimer();
        }

        private bool isSupportOssRule = false;
        private void StartByClick(bool isByOss = false)
        {
            if (string.IsNullOrEmpty(cookies))
                cookies = this.webView.Document.Cookie;
            if (speed28 == null)
            {
                speed28 = new Speed28(cookies);
                speed28.StateChanged += speed28_StateChanged;
            }
            isStarted = true;
            NavigateToSpeed28();
            this.isSupportOssRule = chkOss.Checked;
            StartRound(isByOss);
        }

        private void StartRound(bool isByOss = false)
        {
            if (!isStarted)
            {
                return;
            }

            int roundId = GetRoundId();

            string newRoundID = GetLatestRoundId();
            if (!string.IsNullOrEmpty(newRoundID) && roundId < int.Parse(newRoundID))
            {
                lblState.Text = string.Format("当前:{0}已经过期，新一轮为:{1}", roundId, newRoundID);
                txtRoundId.Text = newRoundID;
                txtRoundId.ForeColor = Color.Red;
                return;
            }

            int[] values = ucNum28.GetValues();
            if (roundId <= 0 || values == null)
            {
                return;
            }

            int currentToal = values.Sum();
            GetCurrentBeans();
            string total = lblBeans.Text.Replace(",", "");
            int totalSum = 0;
            int.TryParse(total, out totalSum);
            if (!string.IsNullOrEmpty(total) && currentToal >= totalSum)
            {
                lblState.Text = "当前下注U豆大于所拥有U豆，本期下注被取消";
                return;
            }
            if (currentToal >= AppSetting.MaxLimit)
            {
                lblState.Text = "当前下注大于最大限止，本期下注被取消";
                return;
            }

            lblState.Text = "开始下注";
            bool isUpload = !isByOss && this.isSupportOssRule;
            if (isUpload)
            {
                RuleFileHelper.SaveSpeed28Rule(roundId, values);
            }

            ResultCode code = speed28.StartNewRound(roundId, values);
            if (isUpload && code == ResultCode.Succeed)
                OSSHelper.UploadRuleToOSS();
        }

        private int currentRoundid = 0;
        private int logCount = 0;
        void speed28_StateChanged(object sender, Speed28ResultEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            Action act = () =>
            {
                if (e.ResultCode == ResultCode.Succeed)
                {
                    lblState.Text = string.Format("当前期:{0} 投注成功", e.RoundId);
                }

                txtRoundId.ForeColor = Color.Black;
                logCount++;
                //webView.Refresh();
                currentRoundid = e.RoundId;
                StartTimer();
                if (logCount % 10 == 0)
                {
                    txtLog.Text = string.Empty;
                }

                if (string.IsNullOrEmpty(txtLog.Text))
                {
                    txtLog.Text = "  " + e.ToString();
                }
                else
                {
                    txtLog.Text = "  " + e.ToString() + "\r\n" + txtLog.Text;
                }

                if (e.ResultCode != ResultCode.Succeed)
                {

                    lblState.Text = e.ResultDescription;
                }
            };
            this.BeginInvoke(act);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.webView.CanGoBack)
                this.webView.GoBack();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.webView.Refresh();
        }

        #endregion

        #region Timer

        private Timer timer;

        /// <summary>
        /// Timer for parsing round is completed
        /// </summary>
        private void StartTimer()
        {
            if (null == timer)
            {
                timer = new Timer();
                timer.Interval = GetInterval();
                timer.Tick += timer_Tick;
            }
            if (!timer.Enabled)
            {
                timer.Start();
            }
        }

        /// <summary>
        /// Timer for parsing round is completed
        /// </summary>
        void timer_Tick(object sender, EventArgs e)
        {
            HtmlElementCollection tableCollection = this.webView.Document.GetElementsByTagName("table");
            HtmlElement table = null;
            if (tableCollection.Count > 0)
            {
                table = tableCollection[0];
            }

            #region tr

            //<td bgcolor="#FFFFFF" class="a1">45605</td>
            //        <td bgcolor="#FFFFFF" class="a2">06-29<br />16:50</td>
            //        <td bgcolor="#FFFFFF" class="a3"><img src="http://image.juxiangyou.com/speed28/num0.gif" /><img src="http://image.juxiangyou.com/speed28/numj.gif" /><img src="http://image.juxiangyou.com/speed28/num4.gif" /><img src="http://image.juxiangyou.com/speed28/numj.gif" /><img src="http://image.juxiangyou.com/speed28/num9.gif" /><img src="http://image.juxiangyou.com/speed28/numd.gif" /><img src="http://image.juxiangyou.com/speed28/num13o.gif" /></td>
            //        <td bgcolor="#FFFFFF" class="a4"><span class="udcl">2,914,925,999</span><img src="http://image.juxiangyou.com/images/xzgame/ud.gif" /></td>
            //        <td bgcolor="#FFFFFF" class="a5"><a href="/speed28/list.php?a=45605">341</a></td>
            //        <td bgcolor="#FFFFFF" class="a6"><span class="udcl">收:0</span><br /><span class="da3">竞:0</span></td>
            //        <td bgcolor="#FFFFFF" class="a7"><span class="da3">已开奖</span></td>
            //      </tr>


            #endregion

            if (table == null)
            {
                return;
            }

            HtmlElementCollection rows = table.GetElementsByTagName("tr");
            if (rows == null || rows.Count == 0)
            {
                return;
            }

            Debug.WriteLine("/---- 开始当前期投注是否开奖结果解释  ---/");
            for (int i = 0; i < rows.Count; i++)
            {
                HtmlElement currentRow = rows[i];
                HtmlElementCollection cols = currentRow.GetElementsByTagName("td");

                if (cols == null || cols.Count != 7)
                {
                    continue;
                }

                if (cols[0].InnerText == currentRoundid.ToString() &&
                    (cols[6].InnerText == "已开奖"))// || cols[6].InnerText == "发奖中"))
                {
                    string idStr = GetLatestRoundId();
                    int id;
                    int.TryParse(idStr, out id);

                    currentRoundid++;
                    if (id >= currentRoundid)
                    {
                        txtRoundId.ForeColor = Color.Red;
                        currentRoundid = id;
                        txtRoundId.Text = currentRoundid.ToString();
                    }

                    lblState.Text = "自增为下一期号";
                    GetCurrentBeans();
                    string num = TextHelper.GetSubstringByEnd(cols[2].InnerHtml, "<img src=\"http://image.juxiangyou.com/speed28/num", "o.gif");
                    lblLastDeal.Text = string.Format("期号:{0} ; 结果:{1} \r\n{2}", cols[0].InnerText, num, cols[5].InnerText.Replace("\r\n", "  "));

                    if (isAuto)
                    {
                        lblState.Text = "开始自动投注下期";
                        StartRound();
                    }
                    else
                    {
                        StopTimer();
                        //lblState.Text = "准备下期投注";
                    }
                    return;
                }
            }
        }


        private string GetLatestRoundId()
        {
            HtmlElementCollection tableCollection = this.webView.Document.GetElementsByTagName("table");
            HtmlElement table = null;
            if (tableCollection.Count > 0)
            {
                table = tableCollection[0];
            }

            #region tr

            //<tr>
            //  <td bgcolor="#FFFFFF" class="a1">47328</td>
            //  <td bgcolor="#FFFFFF" class="a2">07-01<br />11:54</td>
            //  <td bgcolor="#FFFFFF" class="a3"><img src="http://image.juxiangyou.com/speed28/numq.gif" /><img src="http://image.juxiangyou.com/speed28/numj.gif" /><img src="http://image.juxiangyou.com/speed28/numq.gif" /><img src="http://image.juxiangyou.com/speed28/numj.gif" /><img src="http://image.juxiangyou.com/speed28/numq.gif" /><img src="http://image.juxiangyou.com/speed28/numd.gif" /><img src="http://image.juxiangyou.com/speed28/numq.gif" /></td>
            //  <td bgcolor="#FFFFFF" class="a4"><span class="udcl">29,314,408</span><img src="http://image.juxiangyou.com/images/xzgame/ud.gif" /></td>
            //  <td bgcolor="#FFFFFF" class="a5"><a href="/speed28/list.php?a=47328">---</a></td>
            //  <td bgcolor="#FFFFFF" class="a6"><span class="udcl">收:0</span><br /><span class="da3">竞:0</span></td>
            //  <td bgcolor="#FFFFFF" class="a7"><a href="/speed28/betting.php?a=47328" class="jcbtn">竞猜</a></td>
            //</tr>			  		


            #endregion

            if (table != null)
            {
                HtmlElementCollection rows = table.GetElementsByTagName("tr");
                if (rows.Count > 0)
                {
                    string roundid = string.Empty;
                    for (int i = 0; i < rows.Count; i++)
                    {
                        HtmlElement currentRow = rows[i];
                        HtmlElementCollection cols = currentRow.GetElementsByTagName("td");

                        if (cols.Count == 7)
                        {
                            // string aa = cols[1].InnerText;
                            if (cols[6].InnerText == "竞猜")
                            {
                                roundid = cols[0].InnerText;
                            }
                            else if (cols[6].InnerText == "已开奖") //|| cols[6].InnerText == "发奖中")
                            {
                                var item = parser.GetItemFromRow(currentRow);
                                if (item != null)
                                {
                                    SetSuggestLoadPages(roundid);
                                    dbHelper.InsertHistory(item);
                                }

                                string num = TextHelper.GetSubstringByEnd(cols[2].InnerHtml, "<img src=\"http://image.juxiangyou.com/speed28/num", "o.gif");
                                lblLastDeal.Text = string.Format("期号:{0} ; 结果:{1} \r\n{2}", cols[0].InnerText, num, cols[5].InnerText.Replace("\r\n", "  "));
                                GetCurrentBeans();
                                return roundid;
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        private void SetSuggestLoadPages(string roundid)
        {
            string maxIdStr = dbHelper.GetMaxRoundId();
            long nextId, maxId;

            long.TryParse(roundid, out nextId);
            long.TryParse(maxIdStr, out maxId);
            if (nextId > 0 && maxId > 0 && nextId > maxId)
            {
                int pages = (int)Math.Ceiling((1.0 * (nextId - maxId) / 20));
                if (pages > 0)
                {
                    pages = pages > 50 ? 50 : pages;
                    txtPages.Text = pages.ToString();
                }
            }
        }

        private void GetCurrentBeans()
        {
            if (this.webView.Document.Body != null)
            {
                string bodyHtml = this.webView.Document.Body.InnerHtml;
                string beans = TextHelper.GetSubstring(bodyHtml, "<strong class=\"udou-color\">", "</strong>");
                lblBeans.Text = beans;
            }
        }

        /// <summary>
        /// Timer for parsing round is completed
        /// </summary>
        private void StopTimer()
        {
            if (timer != null && timer.Enabled)
            {
                timer.Stop();
            }
        }

        #endregion

        #region cloud oss timer

        Timer ossTimer;
        private void StartOssTimer()
        {
            if (null == ossTimer)
            {
                ossTimer = new Timer();
                ossTimer.Interval = GetInterval();
                ossTimer.Tick += ossTimer_Tick;
            }
            if (!ossTimer.Enabled)
            {
                ossTimer.Start();
            }
        }

        string ossCurrentRoundId = string.Empty;
        void ossTimer_Tick(object sender, EventArgs e)
        {
            Rule rule = OSSHelper.GetRuleFromOss();
            if (rule == null || rule.Name == ossCurrentRoundId)
            {
                if (rule == null)
                    txtLog.Text = string.Format("---获取云配置失败---\r\n") + txtLog.Text;
                return;
            }
            txtLog.Text = string.Format("---获取云配置：{0}---\r\n", rule.ToString()) + txtLog.Text;
            int roundId;
            if (int.TryParse(rule.Name, out roundId))
            {
                ossCurrentRoundId = rule.Name;

                ucNum28.SetValues(rule.Values);
                txtRoundId.Text = rule.Name;
                Debug.WriteLine(string.Format("/**** 云联动执行：{0}  ***/", rule.Name));
                StartByClick(true);
            }
        }

        private void StopOssTimer()
        {
            if (ossTimer != null && ossTimer.Enabled)
            {
                ossTimer.Stop();
            }
        }

        #endregion

        private int GetRoundId()
        {
            int itemValue = 0;
            int.TryParse(txtRoundId.Text, out  itemValue);
            if (itemValue <= 0)
            {
                MessageBox.Show("开始期号必须为大于0的有效值");
            }
            return itemValue;
        }

        private int GetInterval()
        {
            float interval = 0;
            float.TryParse(txtInterval.Text, out  interval);
            if (interval <= 0)
            {
                interval = 10;
            }
            return (int)(interval * 1000);
        }

        #region History

        private void btnLoadHistory_Click(object sender, EventArgs e)
        {
            var list = GetRows();
            lblRows.Text = string.Format("共:{0} 条记录", list.Count);
            dataGridHistory.DataSource = new BindingList<HistoryInfo>(list);

            long sumAmount = list.Sum(item => item.Amount);
            long sumStake = list.Sum(item => item.Stake);
            lblSummary.Text = string.Format("总投入:{0},总竞得:{1},总利润:{2}"
                , sumStake.ToString(UCNum28.NumFormat), sumAmount.ToString(UCNum28.NumFormat)
                , (sumAmount - sumStake).ToString(UCNum28.NumFormat));
            dataGridHistory.Visible = true;
            dataGridStatistic.Visible = false;
            panelHistory.Controls.Remove(dataGridStatistic);
            panelHistory.Controls.Add(dataGridHistory);
            //for (int i = 0; i < list.Count; i++)
            //{
            //    int result = list[i].Result;
            //    Action<int, int> action = (num, index) =>
            //     {
            //         SetRowColor(index, num);
            //     };
            //    this.BeginInvoke(action, result, i);
            //};
        }

        private IList<HistoryInfo> GetRows()
        {
            int rows = 0;
            int.TryParse(txtRows.Text, out  rows);

            var list = dbHelper.GetByRows(rows);
            return list;
        }

        private void SetRowColor(int num, int index)
        {
            if (num % 2 == 1)
            {   //单
                DataGridViewCell cell = dataGridHistory.Rows[index].Cells[8];
                cell.Value = "单";
                cell.Style.BackColor = Color.Yellow;
            }
            else
            {
                //双
                DataGridViewCell cell = dataGridHistory.Rows[index].Cells[9];
                cell.Value = "双";
                cell.Style.BackColor = Color.LightSlateGray;
            }

            if (num >= 10 && num <= 17)
            {
                //中
                DataGridViewCell cell = dataGridHistory.Rows[index].Cells[10];
                cell.Value = "中";
                cell.Style.BackColor = Color.Red;
            }
            else
            {
                //边
                DataGridViewCell cell = dataGridHistory.Rows[index].Cells[11];
                cell.Value = "边";
                cell.Style.BackColor = Color.LightSkyBlue;
            }

            if (num > 13)
            {
                //大
                DataGridViewCell cell = dataGridHistory.Rows[index].Cells[12];
                cell.Value = "大";
                cell.Style.BackColor = Color.LightPink;
            }
            else
            {
                //小
                DataGridViewCell cell = dataGridHistory.Rows[index].Cells[13];
                cell.Value = "小";
                cell.Style.BackColor = Color.LightGreen;
            }
        }

        void dataGridHistory_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = dataGridHistory.Rows[index];
            if (row == null || row.Tag != null)
            {
                return;
            }
            //row.HeaderCell.Value = (index + 1).ToString();
            int num = (row.DataBoundItem as HistoryInfo).Result;
            row.Tag = num;
            SetRowColor(num, index);
            e.Handled = true;
        }

        private void btnGetHistory_Click(object sender, EventArgs e)
        {
            Action action = new Action(() =>
            {
                //http://game.juxiangyou.com/speed28/index.php?p=50
                int pages = 0;
                if (!int.TryParse(txtPages.Text, out pages))
                {
                    return;
                }
                if (pages > 50)
                {
                    pages = 50;
                }

                for (int i = 0; i <= pages; i++)
                {
                    string table = speed28.GetHistoryByPage(i);
                    if (!string.IsNullOrEmpty(table))
                    {
                        IList<HistoryInfo> list = parser.GetRowsFromHtml(table);
                        list = list.OrderByDescending(item => item.RoundId).ToList();
                        dbHelper.InsertHistory(list);

                        Debug.WriteLine(string.Format("/--- current history page:{0} queryed ---/", i));
                        string progress = string.Format("当前加载完:{0}页,共:{1}条记录", i, list.Count);

                        this.BeginInvoke(new Action(() =>
                        {
                            lblHistoryLog.Text = progress;
                        }));
                    }
                }

                this.BeginInvoke(new Action(() =>
                {
                    lblHistoryLog.Text = string.Format("完成");
                }));
            });
            action.BeginInvoke((ar) => action.EndInvoke(ar), action);
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            IList<HistoryInfo> list = GetRows();
            if (list == null || list.Count == 0)
            {
                return;
            }

            int listCount = list.Count;
            lblRows.Text = string.Format("共:{0} 条记录", listCount);

            IDictionary<string, StatisticItem> dic = new Dictionary<string, StatisticItem>()
            { 
                { "单", new StatisticItem("单") } ,
                { "双", new StatisticItem("双") },
                { "分隔线1", new StatisticItem(string.Empty) },

                { "中", new StatisticItem("中") }, 
                { "边", new StatisticItem("边") }, 
                { "分隔线2", new StatisticItem(string.Empty) },

                { "大", new StatisticItem("大")}, 
                { "小", new StatisticItem("小")}, 
                { "分隔线3", new StatisticItem(string.Empty) }, 

                { "梅", new StatisticItem("梅")}, 
                { "分隔线4", new StatisticItem(string.Empty) }, 
            };

            for (int i = 0; i < listCount; i++)
            {
                HistoryInfo item = list[i];
                if (item.Result % 2 == 1)
                {
                    dic["单"].Count++;
                }
                else
                {
                    dic["双"].Count++;
                }

                if (item.Result >= 10 && item.Result <= 17)
                {
                    dic["中"].Count++;
                }
                else
                {
                    dic["边"].Count++;
                }

                if (item.Result >= 14)
                {
                    dic["大"].Count++;
                }
                else
                {
                    dic["小"].Count++;
                }
                if (item.Result >= 13 && item.Result <= 15)
                {
                    dic["梅"].Count++;
                }
            }

            foreach (var item in dic.Values)
            {
                item.Percent = Math.Round(item.Count * 1.0 / listCount, 2);
            }

            int mostOdd = this.AddStatistic(list, dic, (num) => { return num % 2 == 1; }, "最长连 单");
            int mostEven = this.AddStatistic(list, dic, (num) => { return num % 2 == 0; }, "最长连 双");
            int mostMid = this.AddStatistic(list, dic, (num) => { return num >= 10 && num <= 17; }, "最长连 中");
            int mostEdeg = this.AddStatistic(list, dic, (num) => { return num < 10 || num > 17; }, "最长连 边");
            int mostBig = this.AddStatistic(list, dic, (num) => { return num >= 14; }, "最长连 大");
            int mostSmal = this.AddStatistic(list, dic, (num) => { return num < 14; }, "最长连 小");

            dic["长_多分隔"] = new StatisticItem(string.Empty, false);
            dic["连的总次数 单"] = new StatisticItem("连的总次数 单", false) { Count = mostOdd };
            dic["连的总次数 双"] = new StatisticItem("连的总次数 双", false) { Count = mostEven };
            dic["连的总次数 中"] = new StatisticItem("连的总次数 中", false) { Count = mostMid };
            dic["连的总次数 边"] = new StatisticItem("连的总次数 边", false) { Count = mostEdeg };
            dic["连的总次数 大"] = new StatisticItem("连的总次数 大", false) { Count = mostBig };
            dic["连的总次数 小"] = new StatisticItem("连的总次数 小", false) { Count = mostSmal };

            IList<StatisticItem> result = dic.Values.ToList();
            dataGridStatistic.DataSource = new BindingList<StatisticItem>(result);

            dataGridHistory.Visible = false;
            dataGridStatistic.Visible = true;
            dataGridStatistic.Dock = DockStyle.Fill;
            panelHistory.Controls.Remove(dataGridHistory);
            panelHistory.Controls.Add(dataGridStatistic);
        }

        private int AddStatistic(IList<HistoryInfo> list, IDictionary<string, StatisticItem> dic,
                                  Func<int, bool> func, string maxTypeName)
        {
            if (list == null || dic == null || func == null || string.IsNullOrEmpty(maxTypeName))
            {
                return 0;
            }

            int count = 0, maxCount = 0, mostCount = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (func(list[i].Result))
                {
                    count++;
                    if (count > 1)
                    {
                        if (i == list.Count - 1)
                        {
                            mostCount++;
                        }
                        if (count > maxCount)
                            maxCount = count;
                    }
                }
                else
                {
                    if (count > 1)
                    {
                        mostCount++;
                    }
                    count = 0;
                }
            }

            dic[maxTypeName] = new StatisticItem(maxTypeName, false) { Count = maxCount };
            return mostCount;
        }

        #endregion


    }
}
