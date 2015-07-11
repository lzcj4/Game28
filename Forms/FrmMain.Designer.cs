namespace Game28
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.panelParams = new System.Windows.Forms.Panel();
            this.ucNum28 = new Game28.UC.UCNum28();
            this.panelNavigate = new System.Windows.Forms.Panel();
            this.lblLastDeal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBeans = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaxLimit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkOss = new System.Windows.Forms.CheckBox();
            this.lblState = new System.Windows.Forms.Label();
            this.btnOss = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.btnManual = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.btnAuto = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtRoundId = new System.Windows.Forms.TextBox();
            this.lable1 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.tabPreview = new System.Windows.Forms.TabPage();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.webView = new System.Windows.Forms.WebBrowser();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.lblRows = new System.Windows.Forms.Label();
            this.txtRows = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblHistoryLog = new System.Windows.Forms.Label();
            this.txtPages = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridHistory = new System.Windows.Forms.DataGridView();
            this.btnLoadHistory = new System.Windows.Forms.Button();
            this.btnGetHistory = new System.Windows.Forms.Button();
            this.colRoundId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStake = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWinAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWinner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOdd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEven = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMiddle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSmall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabSetting.SuspendLayout();
            this.panelParams.SuspendLayout();
            this.panelNavigate.SuspendLayout();
            this.tabPreview.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSetting);
            this.tabControl1.Controls.Add(this.tabPreview);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Controls.Add(this.tabHistory);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1145, 666);
            this.tabControl1.TabIndex = 0;
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.panelParams);
            this.tabSetting.Controls.Add(this.panelNavigate);
            this.tabSetting.Location = new System.Drawing.Point(4, 22);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetting.Size = new System.Drawing.Size(1137, 640);
            this.tabSetting.TabIndex = 0;
            this.tabSetting.Text = "参数配置";
            this.tabSetting.UseVisualStyleBackColor = true;
            // 
            // panelParams
            // 
            this.panelParams.Controls.Add(this.ucNum28);
            this.panelParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelParams.Location = new System.Drawing.Point(3, 111);
            this.panelParams.Name = "panelParams";
            this.panelParams.Size = new System.Drawing.Size(1131, 526);
            this.panelParams.TabIndex = 1;
            // 
            // ucNum28
            // 
            this.ucNum28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucNum28.Location = new System.Drawing.Point(0, 0);
            this.ucNum28.Name = "ucNum28";
            this.ucNum28.Size = new System.Drawing.Size(1131, 526);
            this.ucNum28.TabIndex = 0;
            // 
            // panelNavigate
            // 
            this.panelNavigate.Controls.Add(this.lblLastDeal);
            this.panelNavigate.Controls.Add(this.label5);
            this.panelNavigate.Controls.Add(this.lblBeans);
            this.panelNavigate.Controls.Add(this.label4);
            this.panelNavigate.Controls.Add(this.txtMaxLimit);
            this.panelNavigate.Controls.Add(this.label1);
            this.panelNavigate.Controls.Add(this.chkOss);
            this.panelNavigate.Controls.Add(this.lblState);
            this.panelNavigate.Controls.Add(this.btnOss);
            this.panelNavigate.Controls.Add(this.label34);
            this.panelNavigate.Controls.Add(this.btnManual);
            this.panelNavigate.Controls.Add(this.label32);
            this.panelNavigate.Controls.Add(this.btnStop);
            this.panelNavigate.Controls.Add(this.txtInterval);
            this.panelNavigate.Controls.Add(this.btnAuto);
            this.panelNavigate.Controls.Add(this.label31);
            this.panelNavigate.Controls.Add(this.txtUser);
            this.panelNavigate.Controls.Add(this.txtRoundId);
            this.panelNavigate.Controls.Add(this.lable1);
            this.panelNavigate.Controls.Add(this.label30);
            this.panelNavigate.Controls.Add(this.label2);
            this.panelNavigate.Controls.Add(this.txtPwd);
            this.panelNavigate.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavigate.Location = new System.Drawing.Point(3, 3);
            this.panelNavigate.Name = "panelNavigate";
            this.panelNavigate.Size = new System.Drawing.Size(1131, 108);
            this.panelNavigate.TabIndex = 0;
            // 
            // lblLastDeal
            // 
            this.lblLastDeal.AutoSize = true;
            this.lblLastDeal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLastDeal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblLastDeal.Location = new System.Drawing.Point(769, 74);
            this.lblLastDeal.Name = "lblLastDeal";
            this.lblLastDeal.Size = new System.Drawing.Size(38, 17);
            this.lblLastDeal.TabIndex = 124;
            this.lblLastDeal.Text = "空白";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(696, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 123;
            this.label5.Text = "上期盈利：";
            // 
            // lblBeans
            // 
            this.lblBeans.AutoSize = true;
            this.lblBeans.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBeans.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblBeans.Location = new System.Drawing.Point(769, 43);
            this.lblBeans.Name = "lblBeans";
            this.lblBeans.Size = new System.Drawing.Size(38, 17);
            this.lblBeans.TabIndex = 122;
            this.lblBeans.Text = "空白";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(696, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 121;
            this.label4.Text = "当前U豆：";
            // 
            // txtMaxLimit
            // 
            this.txtMaxLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMaxLimit.Location = new System.Drawing.Point(504, 41);
            this.txtMaxLimit.Name = "txtMaxLimit";
            this.txtMaxLimit.Size = new System.Drawing.Size(162, 23);
            this.txtMaxLimit.TabIndex = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 119;
            this.label1.Text = "最大限额：";
            // 
            // chkOss
            // 
            this.chkOss.AutoSize = true;
            this.chkOss.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkOss.Location = new System.Drawing.Point(434, 71);
            this.chkOss.Name = "chkOss";
            this.chkOss.Size = new System.Drawing.Size(96, 24);
            this.chkOss.TabIndex = 118;
            this.chkOss.Text = "上传规则";
            this.chkOss.UseVisualStyleBackColor = true;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblState.ForeColor = System.Drawing.Color.Red;
            this.lblState.Location = new System.Drawing.Point(769, 11);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(38, 17);
            this.lblState.TabIndex = 117;
            this.lblState.Text = "空白";
            // 
            // btnOss
            // 
            this.btnOss.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOss.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnOss.Location = new System.Drawing.Point(116, 17);
            this.btnOss.Name = "btnOss";
            this.btnOss.Size = new System.Drawing.Size(75, 28);
            this.btnOss.TabIndex = 7;
            this.btnOss.Text = "  云联动";
            this.btnOss.UseVisualStyleBackColor = true;
            this.btnOss.Click += new System.EventHandler(this.btnOss_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(696, 15);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(65, 12);
            this.label34.TabIndex = 116;
            this.label34.Text = "当前状态：";
            // 
            // btnManual
            // 
            this.btnManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnManual.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnManual.Location = new System.Drawing.Point(21, 17);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(75, 28);
            this.btnManual.TabIndex = 6;
            this.btnManual.Text = "手动";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(405, 76);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(17, 12);
            this.label32.TabIndex = 108;
            this.label32.Text = "秒";
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(116, 59);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 28);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtInterval
            // 
            this.txtInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInterval.Location = new System.Drawing.Point(287, 71);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(112, 23);
            this.txtInterval.TabIndex = 107;
            this.txtInterval.Text = "10";
            // 
            // btnAuto
            // 
            this.btnAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAuto.ForeColor = System.Drawing.Color.YellowGreen;
            this.btnAuto.Location = new System.Drawing.Point(22, 57);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(75, 28);
            this.btnAuto.TabIndex = 0;
            this.btnAuto.Text = "自动";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(212, 75);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(65, 12);
            this.label31.TabIndex = 106;
            this.label31.Text = "刷新间隔：";
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUser.Location = new System.Drawing.Point(257, 15);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(162, 23);
            this.txtUser.TabIndex = 60;
            // 
            // txtRoundId
            // 
            this.txtRoundId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRoundId.Location = new System.Drawing.Point(504, 13);
            this.txtRoundId.Name = "txtRoundId";
            this.txtRoundId.Size = new System.Drawing.Size(162, 23);
            this.txtRoundId.TabIndex = 63;
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Location = new System.Drawing.Point(212, 18);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(41, 12);
            this.lable1.TabIndex = 0;
            this.lable1.Text = "帐号：";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(431, 17);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(65, 12);
            this.label30.TabIndex = 62;
            this.label30.Text = "当前期号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码：";
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPwd.Location = new System.Drawing.Point(257, 42);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(162, 23);
            this.txtPwd.TabIndex = 61;
            // 
            // tabPreview
            // 
            this.tabPreview.Controls.Add(this.btnBack);
            this.tabPreview.Controls.Add(this.btnRefresh);
            this.tabPreview.Controls.Add(this.webView);
            this.tabPreview.Location = new System.Drawing.Point(4, 22);
            this.tabPreview.Name = "tabPreview";
            this.tabPreview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPreview.Size = new System.Drawing.Size(1137, 640);
            this.tabPreview.TabIndex = 1;
            this.tabPreview.Text = "网页预览";
            this.tabPreview.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(105, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 21);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "后退";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(11, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 21);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // webView
            // 
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(3, 3);
            this.webView.MinimumSize = new System.Drawing.Size(20, 18);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(1131, 634);
            this.webView.TabIndex = 0;
            this.webView.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(1137, 640);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "日志";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(1131, 634);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.lblRows);
            this.tabHistory.Controls.Add(this.txtRows);
            this.tabHistory.Controls.Add(this.label6);
            this.tabHistory.Controls.Add(this.lblHistoryLog);
            this.tabHistory.Controls.Add(this.txtPages);
            this.tabHistory.Controls.Add(this.label3);
            this.tabHistory.Controls.Add(this.dataGridHistory);
            this.tabHistory.Controls.Add(this.btnLoadHistory);
            this.tabHistory.Controls.Add(this.btnGetHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Size = new System.Drawing.Size(1137, 640);
            this.tabHistory.TabIndex = 3;
            this.tabHistory.Text = "历史数据";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(247, 33);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(29, 12);
            this.lblRows.TabIndex = 126;
            this.lblRows.Text = "空白";
            // 
            // txtRows
            // 
            this.txtRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRows.Location = new System.Drawing.Point(127, 27);
            this.txtRows.Name = "txtRows";
            this.txtRows.Size = new System.Drawing.Size(57, 23);
            this.txtRows.TabIndex = 125;
            this.txtRows.Text = "200";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 124;
            this.label6.Text = "条";
            // 
            // lblHistoryLog
            // 
            this.lblHistoryLog.AutoSize = true;
            this.lblHistoryLog.Location = new System.Drawing.Point(247, 81);
            this.lblHistoryLog.Name = "lblHistoryLog";
            this.lblHistoryLog.Size = new System.Drawing.Size(29, 12);
            this.lblHistoryLog.TabIndex = 123;
            this.lblHistoryLog.Text = "空白";
            // 
            // txtPages
            // 
            this.txtPages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPages.Location = new System.Drawing.Point(127, 75);
            this.txtPages.Name = "txtPages";
            this.txtPages.Size = new System.Drawing.Size(57, 23);
            this.txtPages.TabIndex = 122;
            this.txtPages.Text = "50";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(195, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 121;
            this.label3.Text = "页";
            // 
            // dataGridHistory
            // 
            this.dataGridHistory.AllowUserToAddRows = false;
            this.dataGridHistory.AllowUserToDeleteRows = false;
            this.dataGridHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRoundId,
            this.colColumn,
            this.colStake,
            this.colAmount,
            this.colWinAmount,
            this.colTime,
            this.colTotalAmount,
            this.colWinner,
            this.colOdd,
            this.colEven,
            this.colMiddle,
            this.colEdge,
            this.colBig,
            this.colSmall});
            this.dataGridHistory.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridHistory.Location = new System.Drawing.Point(0, 119);
            this.dataGridHistory.Name = "dataGridHistory";
            this.dataGridHistory.RowTemplate.Height = 23;
            this.dataGridHistory.Size = new System.Drawing.Size(1137, 521);
            this.dataGridHistory.TabIndex = 3;
            // 
            // btnLoadHistory
            // 
            this.btnLoadHistory.Location = new System.Drawing.Point(31, 27);
            this.btnLoadHistory.Name = "btnLoadHistory";
            this.btnLoadHistory.Size = new System.Drawing.Size(75, 23);
            this.btnLoadHistory.TabIndex = 2;
            this.btnLoadHistory.Text = "显示";
            this.btnLoadHistory.UseVisualStyleBackColor = true;
            this.btnLoadHistory.Click += new System.EventHandler(this.btnLoadHistory_Click);
            // 
            // btnGetHistory
            // 
            this.btnGetHistory.Location = new System.Drawing.Point(31, 75);
            this.btnGetHistory.Name = "btnGetHistory";
            this.btnGetHistory.Size = new System.Drawing.Size(75, 23);
            this.btnGetHistory.TabIndex = 0;
            this.btnGetHistory.Text = "提取";
            this.btnGetHistory.UseVisualStyleBackColor = true;
            this.btnGetHistory.Click += new System.EventHandler(this.btnGetHistory_Click);
            // 
            // colRoundId
            // 
            this.colRoundId.DataPropertyName = "RoundId";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colRoundId.DefaultCellStyle = dataGridViewCellStyle1;
            this.colRoundId.HeaderText = "期号";
            this.colRoundId.Name = "colRoundId";
            this.colRoundId.Width = 54;
            // 
            // colColumn
            // 
            this.colColumn.DataPropertyName = "Result";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.colColumn.HeaderText = "竞猜结果";
            this.colColumn.Name = "colColumn";
            this.colColumn.Width = 78;
            // 
            // colStake
            // 
            this.colStake.DataPropertyName = "Stake";
            this.colStake.HeaderText = "投注U豆";
            this.colStake.Name = "colStake";
            this.colStake.Width = 72;
            // 
            // colAmount
            // 
            this.colAmount.DataPropertyName = "Amount";
            this.colAmount.HeaderText = "竞得U豆";
            this.colAmount.Name = "colAmount";
            this.colAmount.Width = 72;
            // 
            // colWinAmount
            // 
            this.colWinAmount.DataPropertyName = "WinAmountStr";
            this.colWinAmount.HeaderText = "净赚U豆";
            this.colWinAmount.Name = "colWinAmount";
            this.colWinAmount.Width = 72;
            // 
            // colTime
            // 
            this.colTime.DataPropertyName = "Time";
            this.colTime.HeaderText = "开奖时间";
            this.colTime.Name = "colTime";
            this.colTime.Width = 78;
            // 
            // colTotalAmount
            // 
            this.colTotalAmount.DataPropertyName = "TotalAmountStr";
            this.colTotalAmount.HeaderText = "总投注U豆";
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.Width = 84;
            // 
            // colWinner
            // 
            this.colWinner.DataPropertyName = "WinnerNum";
            this.colWinner.HeaderText = "中奖人数";
            this.colWinner.Name = "colWinner";
            this.colWinner.Width = 78;
            // 
            // colOdd
            // 
            this.colOdd.DataPropertyName = "OddStr";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.colOdd.DefaultCellStyle = dataGridViewCellStyle3;
            this.colOdd.HeaderText = "单";
            this.colOdd.Name = "colOdd";
            this.colOdd.Width = 42;
            // 
            // colEven
            // 
            this.colEven.DataPropertyName = "EvenStr";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
            this.colEven.DefaultCellStyle = dataGridViewCellStyle4;
            this.colEven.HeaderText = "双";
            this.colEven.Name = "colEven";
            this.colEven.Width = 42;
            // 
            // colMiddle
            // 
            this.colMiddle.DataPropertyName = "MiddleStr";
            dataGridViewCellStyle5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Red;
            this.colMiddle.DefaultCellStyle = dataGridViewCellStyle5;
            this.colMiddle.HeaderText = "中";
            this.colMiddle.Name = "colMiddle";
            this.colMiddle.Width = 42;
            // 
            // colEdge
            // 
            this.colEdge.DataPropertyName = "EdgeStr";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Blue;
            this.colEdge.DefaultCellStyle = dataGridViewCellStyle6;
            this.colEdge.HeaderText = "边";
            this.colEdge.Name = "colEdge";
            this.colEdge.Width = 42;
            // 
            // colBig
            // 
            this.colBig.DataPropertyName = "BigStr";
            dataGridViewCellStyle7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.colBig.DefaultCellStyle = dataGridViewCellStyle7;
            this.colBig.HeaderText = "大";
            this.colBig.Name = "colBig";
            this.colBig.Width = 42;
            // 
            // colSmall
            // 
            this.colSmall.DataPropertyName = "SmallStr";
            dataGridViewCellStyle8.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Green;
            this.colSmall.DefaultCellStyle = dataGridViewCellStyle8;
            this.colSmall.HeaderText = "小";
            this.colSmall.Name = "colSmall";
            this.colSmall.Width = 42;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 666);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmMain";
            this.Text = "Game28";
            this.tabControl1.ResumeLayout(false);
            this.tabSetting.ResumeLayout(false);
            this.panelParams.ResumeLayout(false);
            this.panelNavigate.ResumeLayout(false);
            this.panelNavigate.PerformLayout();
            this.tabPreview.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabHistory.ResumeLayout(false);
            this.tabHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPreview;
        private System.Windows.Forms.WebBrowser webView;
        private System.Windows.Forms.TabPage tabSetting;
        private System.Windows.Forms.Panel panelParams;
        private System.Windows.Forms.Panel panelNavigate;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtRoundId;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button btnOss;
        private System.Windows.Forms.CheckBox chkOss;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtMaxLimit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBeans;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLastDeal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.Button btnGetHistory;
        private System.Windows.Forms.Button btnLoadHistory;
        private System.Windows.Forms.DataGridView dataGridHistory;
        private System.Windows.Forms.TextBox txtPages;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHistoryLog;
        private System.Windows.Forms.TextBox txtRows;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRows;
        private UC.UCNum28 ucNum28;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoundId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStake;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWinAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWinner;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEven;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMiddle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBig;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSmall;


    }
}

