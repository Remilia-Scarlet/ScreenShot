namespace ScreenShot
{
    partial class Data2Word
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
            this.resultTextBox = new System.Windows.Forms.RichTextBox();
            this.CtrlVReceiver = new System.Windows.Forms.TextBox();
            this.infoLabel = new System.Windows.Forms.Label();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.radioBtnW2D = new System.Windows.Forms.RadioButton();
            this.radioBtnD2W = new System.Windows.Forms.RadioButton();
            this.panelSaveFile = new System.Windows.Forms.Panel();
            this.lbSaveFileName = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelInfo.SuspendLayout();
            this.panelSaveFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // resultTextBox
            // 
            this.resultTextBox.DetectUrls = false;
            this.resultTextBox.Location = new System.Drawing.Point(12, 35);
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.Size = new System.Drawing.Size(369, 502);
            this.resultTextBox.TabIndex = 1;
            this.resultTextBox.Text = "";
            this.resultTextBox.TextChanged += new System.EventHandler(this.resultTextBox_TextChanged);
            this.resultTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.resultTextBox_KeyPress);
            // 
            // CtrlVReceiver
            // 
            this.CtrlVReceiver.BackColor = System.Drawing.SystemColors.Control;
            this.CtrlVReceiver.Location = new System.Drawing.Point(0, 0);
            this.CtrlVReceiver.Multiline = true;
            this.CtrlVReceiver.Name = "CtrlVReceiver";
            this.CtrlVReceiver.ReadOnly = true;
            this.CtrlVReceiver.Size = new System.Drawing.Size(396, 502);
            this.CtrlVReceiver.TabIndex = 2;
            this.CtrlVReceiver.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CtrlVReceiver_KeyPress);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(151, 246);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(107, 12);
            this.infoLabel.TabIndex = 3;
            this.infoLabel.Text = "Press Ctrl+V here";
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.infoLabel);
            this.panelInfo.Controls.Add(this.CtrlVReceiver);
            this.panelInfo.Location = new System.Drawing.Point(406, 35);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(397, 502);
            this.panelInfo.TabIndex = 4;
            // 
            // radioBtnW2D
            // 
            this.radioBtnW2D.AutoSize = true;
            this.radioBtnW2D.Location = new System.Drawing.Point(120, 13);
            this.radioBtnW2D.Name = "radioBtnW2D";
            this.radioBtnW2D.Size = new System.Drawing.Size(155, 16);
            this.radioBtnW2D.TabIndex = 5;
            this.radioBtnW2D.TabStop = true;
            this.radioBtnW2D.Text = "Convert string to data";
            this.radioBtnW2D.UseVisualStyleBackColor = true;
            // 
            // radioBtnD2W
            // 
            this.radioBtnD2W.AutoSize = true;
            this.radioBtnD2W.Location = new System.Drawing.Point(513, 12);
            this.radioBtnD2W.Name = "radioBtnD2W";
            this.radioBtnD2W.Size = new System.Drawing.Size(155, 16);
            this.radioBtnD2W.TabIndex = 6;
            this.radioBtnD2W.TabStop = true;
            this.radioBtnD2W.Text = "Convert data to string";
            this.radioBtnD2W.UseVisualStyleBackColor = true;
            // 
            // panelSaveFile
            // 
            this.panelSaveFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSaveFile.Controls.Add(this.buttonSave);
            this.panelSaveFile.Controls.Add(this.lbSaveFileName);
            this.panelSaveFile.Location = new System.Drawing.Point(406, 35);
            this.panelSaveFile.Name = "panelSaveFile";
            this.panelSaveFile.Size = new System.Drawing.Size(396, 502);
            this.panelSaveFile.TabIndex = 7;
            this.panelSaveFile.Visible = false;
            // 
            // lbSaveFileName
            // 
            this.lbSaveFileName.Location = new System.Drawing.Point(3, 14);
            this.lbSaveFileName.Name = "lbSaveFileName";
            this.lbSaveFileName.Size = new System.Drawing.Size(388, 219);
            this.lbSaveFileName.TabIndex = 0;
            this.lbSaveFileName.Text = "filename";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(306, 461);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 25);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // Data2Word
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 551);
            this.Controls.Add(this.panelSaveFile);
            this.Controls.Add(this.radioBtnD2W);
            this.Controls.Add(this.radioBtnW2D);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.resultTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Data2Word";
            this.Text = "Data2Word";
            this.Load += new System.EventHandler(this.Data2Word_Load);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.panelSaveFile.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox resultTextBox;
        private System.Windows.Forms.TextBox CtrlVReceiver;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.RadioButton radioBtnW2D;
        private System.Windows.Forms.RadioButton radioBtnD2W;
        private System.Windows.Forms.Panel panelSaveFile;
        private System.Windows.Forms.Label lbSaveFileName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSave;
    }
}