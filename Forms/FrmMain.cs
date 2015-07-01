﻿using Aliyun.OpenServices.OpenStorageService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace Game28
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.Load += (sender, e) =>
            {
                LoadParams();
            };

            this.FormClosing += (sender, e) =>
            {
                SaveParams();
            };

            this.webView.DocumentCompleted += webView_DocumentCompleted;
        }


        bool isLogined = false;
        string cookies = string.Empty;

        bool isStarted = false;

        void webView_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            CheckLogin();
        }

        private void CheckLogin()
        {
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
                     htmlBody.Contains("当前U币") || htmlBody.Contains("提现")))
                {
                    isLogined = true;
                    webView.Url = new Uri(@"http://game.juxiangyou.com/speed28/index.php");
                }
            }

            if (string.IsNullOrEmpty(txtRoundId.Text) && htmlBody.Contains("离竞猜截止时间"))
            {
                string id = GetLatestRoundId();
                if (!string.IsNullOrEmpty((id)))
                {
                    txtRoundId.Text = id;
                }
            }
        }

        private void LoadParams()
        {
            txtUser.Text = AppSetting.User;
            txtPwd.Text = AppSetting.Pwd;
            txtInterval.Text = AppSetting.Interval;
        }

        private void SaveParams()
        {
            AppSetting.User = txtUser.Text;
            AppSetting.Pwd = txtPwd.Text;
            AppSetting.Interval = txtInterval.Text;
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
            txtRoundId.ForeColor = Color.Black;

            StopTimer();
            StopOssTimer();
        }

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
            webView.Url = new Uri(@"http://game.juxiangyou.com/speed28/index.php");
            StartRound(isByOss);
        }

        private void StartRound(bool isByOss = false)
        {
            if (!isStarted)
            {
                return;
            }

            int roundId = GetRoundId();
            int[] values = ucNum28.GetValues();
            if (values==null)
            {
                return;
            }
            lblState.Text = "开始下注";
            Action action = () =>
            {
                RuleFileHelper.SaveSpeed28Rule(roundId, values);
                ResultCode code = speed28.StartNewRound(roundId, values);

                if (!isByOss && code == ResultCode.Succeed)
                    OSSHelper.UploadRuleToOSS();
            };
            action.BeginInvoke((ar) => action.EndInvoke(ar), null);
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
                logCount++;
                webView.Refresh();
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
                    txtLog.Text = e.ToString() + "\r\n  " + txtLog.Text;
                }
            };
            this.BeginInvoke(act);
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

            if (table != null)
            {
                Debug.WriteLine("/---- 开始当前期投注是否开奖结果解释  ---/");
                HtmlElementCollection rows = table.GetElementsByTagName("tr");
                if (rows.Count > 0)
                {
                    for (int i = 0; i < rows.Count; i++)
                    {
                        HtmlElement currentRow = rows[i];
                        HtmlElementCollection cols = currentRow.GetElementsByTagName("td");

                        if (cols.Count == 7)
                        {
                            if (cols[0].InnerText == currentRoundid.ToString() &&
                                (cols[6].InnerText == "已开奖"))// || cols[6].InnerText == "发奖中"))
                            {
                                if (isAuto)
                                {
                                    lblState.Text = "自动下一期";
                                    currentRoundid++;
                                    Action action = () =>
                                    {
                                        txtRoundId.ForeColor = Color.Red;
                                        txtRoundId.Text = currentRoundid.ToString();
                                        StartRound();
                                    };
                                    this.BeginInvoke(action);
                                }
                                else
                                {
                                    StopTimer();
                                    lblState.Text = "执行完毕";
                                }
                                return;
                            }
                        }
                    }
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
                            else if (cols[6].InnerText == "已开奖" || cols[6].InnerText == "发奖中")
                            {
                                return roundid;
                            }
                        }
                    }
                }
            }
            return string.Empty;

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
                Debug.WriteLine("/**** 云联动执行：{0}  ***/", rule.Name);
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

        bool isInvalidParam = false;

        private int GetRoundId()
        {
            int itemValue = 0;
            int.TryParse(txtRoundId.Text, out  itemValue);
            if (itemValue <= 0)
            {
                MessageBox.Show("开始期号必须为大于0的有效值");
                isInvalidParam = true;
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

    }
}