namespace ScreenShot
{
    partial class NetSpeedWatcher
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUpload = new System.Windows.Forms.Label();
            this.lblDownload = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.refreshData = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(86, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "↑";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NetSpeedWatcher_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NetSpeedWatcher_MouseMove);
            // 
            // lblUpload
            // 
            this.lblUpload.AutoSize = true;
            this.lblUpload.Location = new System.Drawing.Point(99, 3);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Size = new System.Drawing.Size(48, 13);
            this.lblUpload.TabIndex = 1;
            this.lblUpload.Text = "0.00 K/s";
            this.lblUpload.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NetSpeedWatcher_MouseDown);
            this.lblUpload.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NetSpeedWatcher_MouseMove);
            // 
            // lblDownload
            // 
            this.lblDownload.AutoSize = true;
            this.lblDownload.Location = new System.Drawing.Point(21, 3);
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(48, 13);
            this.lblDownload.TabIndex = 3;
            this.lblDownload.Text = "0.00 K/s";
            this.lblDownload.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NetSpeedWatcher_MouseDown);
            this.lblDownload.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NetSpeedWatcher_MouseMove);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(8, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "↓";
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NetSpeedWatcher_MouseDown);
            this.label4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NetSpeedWatcher_MouseMove);
            // 
            // refreshData
            // 
            this.refreshData.Enabled = true;
            this.refreshData.Interval = 1000;
            this.refreshData.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NetSpeedWatcher
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(164, 19);
            this.ControlBox = false;
            this.Controls.Add(this.lblDownload);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblUpload);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NetSpeedWatcher";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "NetSpeedWatcher";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NetSpeedWatcher_FormClosed);
            this.Load += new System.EventHandler(this.NetSpeedWatcher_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NetSpeedWatcher_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NetSpeedWatcher_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUpload;
        private System.Windows.Forms.Label lblDownload;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer refreshData;
    }
}