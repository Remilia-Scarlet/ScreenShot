namespace ScreenShot
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.iconMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNetSpeedBar = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minilizeHideTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxHotkeyCtrl = new System.Windows.Forms.CheckBox();
            this.checkBoxHotKeyAlt = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxHotKey = new System.Windows.Forms.ComboBox();
            this.OKBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.resumeSendTimer = new System.Windows.Forms.Timer(this.components);
            this.autoStart = new System.Windows.Forms.CheckBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ckShowNetSpeedBar = new System.Windows.Forms.CheckBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.label5 = new System.Windows.Forms.Label();
            this.iconMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "ScreenShot tool is running now";
            this.notifyIcon.ContextMenuStrip = this.iconMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "ScreenShot";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // iconMenuStrip
            // 
            this.iconMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.toolStripSeparator2,
            this.showNetSpeedBar,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.iconMenuStrip.Name = "iconMenuStrip";
            this.iconMenuStrip.Size = new System.Drawing.Size(194, 82);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.OpenToolStripMenuItem.Text = "Open setting";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // showNetSpeedBar
            // 
            this.showNetSpeedBar.Checked = true;
            this.showNetSpeedBar.CheckOnClick = true;
            this.showNetSpeedBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showNetSpeedBar.Name = "showNetSpeedBar";
            this.showNetSpeedBar.Size = new System.Drawing.Size(193, 22);
            this.showNetSpeedBar.Text = "Show net speed bar";
            this.showNetSpeedBar.Click += new System.EventHandler(this.showNetSpeedBar_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // minilizeHideTimer
            // 
            this.minilizeHideTimer.Tick += new System.EventHandler(this.minilizeHideTimer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBoxHotkeyCtrl);
            this.groupBox1.Controls.Add(this.checkBoxHotKeyAlt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxHotKey);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 86);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HotKey";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(146, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "+";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Default HotKey: Ctrl+Alt+A";
            // 
            // checkBoxHotkeyCtrl
            // 
            this.checkBoxHotkeyCtrl.AutoSize = true;
            this.checkBoxHotkeyCtrl.Checked = true;
            this.checkBoxHotkeyCtrl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHotkeyCtrl.Location = new System.Drawing.Point(13, 20);
            this.checkBoxHotkeyCtrl.Name = "checkBoxHotkeyCtrl";
            this.checkBoxHotkeyCtrl.Size = new System.Drawing.Size(48, 16);
            this.checkBoxHotkeyCtrl.TabIndex = 0;
            this.checkBoxHotkeyCtrl.Text = "Ctrl";
            this.checkBoxHotkeyCtrl.UseVisualStyleBackColor = true;
            // 
            // checkBoxHotKeyAlt
            // 
            this.checkBoxHotKeyAlt.AutoSize = true;
            this.checkBoxHotKeyAlt.Checked = true;
            this.checkBoxHotKeyAlt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHotKeyAlt.Location = new System.Drawing.Point(92, 20);
            this.checkBoxHotKeyAlt.Name = "checkBoxHotKeyAlt";
            this.checkBoxHotKeyAlt.Size = new System.Drawing.Size(42, 16);
            this.checkBoxHotKeyAlt.TabIndex = 1;
            this.checkBoxHotKeyAlt.Text = "Alt";
            this.checkBoxHotKeyAlt.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "+";
            // 
            // comboBoxHotKey
            // 
            this.comboBoxHotKey.AutoCompleteCustomSource.AddRange(new string[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"});
            this.comboBoxHotKey.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxHotKey.FormattingEnabled = true;
            this.comboBoxHotKey.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "PrtScn"});
            this.comboBoxHotKey.Location = new System.Drawing.Point(187, 18);
            this.comboBoxHotKey.Name = "comboBoxHotKey";
            this.comboBoxHotKey.Size = new System.Drawing.Size(52, 20);
            this.comboBoxHotKey.TabIndex = 2;
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(207, 146);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(73, 23);
            this.OKBtn.TabIndex = 5;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "  ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // resumeSendTimer
            // 
            this.resumeSendTimer.Interval = 500;
            this.resumeSendTimer.Tick += new System.EventHandler(this.resumeSendTimer_Tick);
            // 
            // autoStart
            // 
            this.autoStart.AutoSize = true;
            this.autoStart.Location = new System.Drawing.Point(12, 124);
            this.autoStart.Name = "autoStart";
            this.autoStart.Size = new System.Drawing.Size(228, 16);
            this.autoStart.TabIndex = 11;
            this.autoStart.Text = "Start program with windows startup";
            this.autoStart.UseVisualStyleBackColor = true;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // ckShowNetSpeedBar
            // 
            this.ckShowNetSpeedBar.AutoSize = true;
            this.ckShowNetSpeedBar.Location = new System.Drawing.Point(12, 103);
            this.ckShowNetSpeedBar.Name = "ckShowNetSpeedBar";
            this.ckShowNetSpeedBar.Size = new System.Drawing.Size(132, 16);
            this.ckShowNetSpeedBar.TabIndex = 12;
            this.ckShowNetSpeedBar.Text = "Show net speed bar";
            this.ckShowNetSpeedBar.UseVisualStyleBackColor = true;
            this.ckShowNetSpeedBar.Click += new System.EventHandler(this.ckShowNetSpeedBar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "  ";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 180);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ckShowNetSpeedBar);
            this.Controls.Add(this.autoStart);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScreenShot";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.iconMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip iconMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Timer minilizeHideTimer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxHotkeyCtrl;
        private System.Windows.Forms.CheckBox checkBoxHotKeyAlt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxHotKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer resumeSendTimer;
        private System.Windows.Forms.CheckBox autoStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem showNetSpeedBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox ckShowNetSpeedBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label5;
    }
}

