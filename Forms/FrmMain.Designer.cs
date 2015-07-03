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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.panelParams = new System.Windows.Forms.Panel();
            this.panelNavigate = new System.Windows.Forms.Panel();
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
            this.ucNum28 = new Game28.UC.UCNum28();
            this.txtMaxLimit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabSetting.SuspendLayout();
            this.panelParams.SuspendLayout();
            this.panelNavigate.SuspendLayout();
            this.tabPreview.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSetting);
            this.tabControl1.Controls.Add(this.tabPreview);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1164, 675);
            this.tabControl1.TabIndex = 0;
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.panelParams);
            this.tabSetting.Controls.Add(this.panelNavigate);
            this.tabSetting.Location = new System.Drawing.Point(4, 22);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetting.Size = new System.Drawing.Size(1156, 649);
            this.tabSetting.TabIndex = 0;
            this.tabSetting.Text = "参数配置";
            this.tabSetting.UseVisualStyleBackColor = true;
            // 
            // panelParams
            // 
            this.panelParams.Controls.Add(this.ucNum28);
            this.panelParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelParams.Location = new System.Drawing.Point(3, 120);
            this.panelParams.Name = "panelParams";
            this.panelParams.Size = new System.Drawing.Size(1150, 526);
            this.panelParams.TabIndex = 1;
            // 
            // panelNavigate
            // 
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
            this.panelNavigate.Size = new System.Drawing.Size(1150, 117);
            this.panelNavigate.TabIndex = 0;
            // 
            // chkOss
            // 
            this.chkOss.AutoSize = true;
            this.chkOss.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkOss.Location = new System.Drawing.Point(699, 52);
            this.chkOss.Name = "chkOss";
            this.chkOss.Size = new System.Drawing.Size(79, 24);
            this.chkOss.TabIndex = 118;
            this.chkOss.Text = "云规则";
            this.chkOss.UseVisualStyleBackColor = true;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblState.ForeColor = System.Drawing.Color.Red;
            this.lblState.Location = new System.Drawing.Point(769, 15);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(38, 17);
            this.lblState.TabIndex = 117;
            this.lblState.Text = "空白";
            // 
            // btnOss
            // 
            this.btnOss.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOss.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnOss.Location = new System.Drawing.Point(116, 9);
            this.btnOss.Name = "btnOss";
            this.btnOss.Size = new System.Drawing.Size(75, 30);
            this.btnOss.TabIndex = 7;
            this.btnOss.Text = "  云联动";
            this.btnOss.UseVisualStyleBackColor = true;
            this.btnOss.Click += new System.EventHandler(this.btnOss_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(696, 16);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(67, 13);
            this.label34.TabIndex = 116;
            this.label34.Text = "当前状态：";
            // 
            // btnManual
            // 
            this.btnManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnManual.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnManual.Location = new System.Drawing.Point(21, 9);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(75, 30);
            this.btnManual.TabIndex = 6;
            this.btnManual.Text = "手动";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(405, 82);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(19, 13);
            this.label32.TabIndex = 108;
            this.label32.Text = "秒";
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(116, 54);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 30);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtInterval
            // 
            this.txtInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInterval.Location = new System.Drawing.Point(287, 77);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(112, 23);
            this.txtInterval.TabIndex = 107;
            this.txtInterval.Text = "10";
            // 
            // btnAuto
            // 
            this.btnAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAuto.ForeColor = System.Drawing.Color.Lime;
            this.btnAuto.Location = new System.Drawing.Point(22, 52);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(75, 30);
            this.btnAuto.TabIndex = 0;
            this.btnAuto.Text = "自动";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(212, 81);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(67, 13);
            this.label31.TabIndex = 106;
            this.label31.Text = "刷新间隔：";
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUser.Location = new System.Drawing.Point(257, 16);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(162, 23);
            this.txtUser.TabIndex = 60;
            // 
            // txtRoundId
            // 
            this.txtRoundId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRoundId.Location = new System.Drawing.Point(504, 14);
            this.txtRoundId.Name = "txtRoundId";
            this.txtRoundId.Size = new System.Drawing.Size(162, 23);
            this.txtRoundId.TabIndex = 63;
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Location = new System.Drawing.Point(212, 19);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(43, 13);
            this.lable1.TabIndex = 0;
            this.lable1.Text = "帐号：";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(431, 18);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(67, 13);
            this.label30.TabIndex = 62;
            this.label30.Text = "当前期号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码：";
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPwd.Location = new System.Drawing.Point(257, 46);
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
            this.tabPreview.Size = new System.Drawing.Size(1156, 649);
            this.tabPreview.TabIndex = 1;
            this.tabPreview.Text = "网页预览";
            this.tabPreview.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(105, 11);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "后退";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(11, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // webView
            // 
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(3, 3);
            this.webView.MinimumSize = new System.Drawing.Size(20, 20);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(1150, 643);
            this.webView.TabIndex = 0;
            this.webView.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(1156, 649);
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
            this.txtLog.Size = new System.Drawing.Size(1150, 643);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // ucNum28
            // 
            this.ucNum28.Location = new System.Drawing.Point(21, 23);
            this.ucNum28.Name = "ucNum28";
            this.ucNum28.Size = new System.Drawing.Size(1101, 505);
            this.ucNum28.TabIndex = 0;
            // 
            // txtMaxLimit
            // 
            this.txtMaxLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMaxLimit.Location = new System.Drawing.Point(504, 53);
            this.txtMaxLimit.Name = "txtMaxLimit";
            this.txtMaxLimit.Size = new System.Drawing.Size(162, 23);
            this.txtMaxLimit.TabIndex = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "最大限额：";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 675);
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
        private UC.UCNum28 ucNum28;
        private System.Windows.Forms.CheckBox chkOss;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtMaxLimit;
        private System.Windows.Forms.Label label1;


    }
}

