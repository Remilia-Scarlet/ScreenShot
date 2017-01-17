using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenShot.Properties;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Win32;

namespace ScreenShot
{
    public partial class MainForm : Form
    {
        private SendManager _sendManager;
        private Data2Word _data2Word;
        private static HotkeyReceiver s_hotkeyReceiver = new HotkeyReceiver();
        private NetSpeedWatcher _speedWatcher;
        public MainForm(bool minimize)
        {
            s_hotkeyReceiver.MainForm = this;
            _sendManager = new SendManager();
            InitializeComponent();
            if(minimize)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Visible = true;
                this.ShowInTaskbar = true;
            }
        }
        private class HotkeyReceiver : Form
        {
            public MainForm MainForm { get; set; }
            protected override void WndProc(ref Message m)
            {
                const int WM_HOTKEY = 0x0312;//如果m.Msg的值为0x0312那么表示用户按下了热键
                switch (m.Msg)
                {
                    case WM_HOTKEY:
                        MainForm.ProcessHotkey(ref m);//按下热键时调用ProcessHotkey()函数
                        break;
                }
                base.WndProc(ref m);
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshUI();
            if (!RegisterHotKey())
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                this.Visible = true;
                string str = "";
                if (Settings.Default.CtrlHotKey)
                    str += "Ctrl + ";
                if (Settings.Default.AltHotKey)
                    str += "Alt + ";
                str += Settings.Default.HotKey;
                MessageBox.Show("Register hotkey <" + str + "> failed! Please check if it has already been registered by other program. Or you can reset another hotkey.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            setSpeedWatcherShow(Settings.Default.ShowNetSpeedBar);

        }
        private bool RegisterHotKey()
        {
            uint fsModifiers = 0;
            if (Settings.Default.CtrlHotKey == true)
                fsModifiers = fsModifiers | (uint)HotKey.KeyModifiers.Ctrl;
            if (Settings.Default.AltHotKey == true)
                fsModifiers = fsModifiers | (uint)HotKey.KeyModifiers.Alt;

            bool result = HotKey.RegisterHotKey(s_hotkeyReceiver.Handle, HotKey.HOT_KEY_ID, fsModifiers, Settings.Default.HotKey);//这时热键为Alt+CTRL+A
            return result;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                HotKey.UnregisterHotKey(s_hotkeyReceiver.Handle, HotKey.HOT_KEY_ID);//卸载第1个快捷键
                if(_speedWatcher != null)
                    _speedWatcher.Close();
            }
            else
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                minilizeHideTimer.Enabled = true;
            }
        }
        private void RefreshUI()
        {
            checkBoxHotkeyCtrl.Checked = Settings.Default.CtrlHotKey;
            checkBoxHotKeyAlt.Checked = Settings.Default.AltHotKey;
            comboBoxHotKey.Text = Settings.Default.HotKey.ToString();
            RegistryHandle.AutoStartStat stat = RegistryHandle.GetAutoStart();
            if (stat == RegistryHandle.AutoStartStat.NO_PERMISSION || stat == RegistryHandle.AutoStartStat.NO)
                autoStart.Checked = false;
            else
                autoStart.Checked = true;
        }

        //重写WndProc()方法，通过监视系统消息，来调用过程
        protected override void WndProc(ref Message m)//监视Windows消息
        {
            const int WM_SYSCOMMAND = 0x112;
            const int SC_MINIMIZE = 0xF020;
            switch(m.Msg)
            { 
                case WM_SYSCOMMAND:
                    if (m.WParam.ToInt32() == SC_MINIMIZE)
                    {
                        minilizeHideTimer.Enabled = true;
                    }
                    break;
            }
            base.WndProc(ref m); //将系统消息传递自父类的WndProc
        }
        private void ProcessHotkey(ref Message m) //按下设定的键时调用该函数
        {
            IntPtr id = m.WParam;
            string sid = id.ToString();
            switch (id.ToInt32())
            {
                case HotKey.HOT_KEY_ID:
                    CutScreen();
                    break;
                default:
                    break;
            }
        }
        private void CutScreen()
        {
            if(!(_sendManager.IsSending && !_sendManager.IsPausing))
                ShotMaskManager.Instance().showMask(_maskWindow__shotFinishHandle);
        }
        private void _maskWindow__shotFinishHandle(Bitmap bmp,Point startPos)
        {
            if(bmp != null)
            {
                List<object> data = new List<object>();
                data.Add(bmp);
                data.Add(startPos);
                resumeSendTimer.Tag = data;
                resumeSendTimer.Start();
                if(PictureReader.SearchStartPos(bmp,startPos) == -1)
                    Clipboard.SetImage(bmp);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                OpenWindow();
            }
        }
        private void OpenWindow()
        {
            this.Show();
            this.Activate();
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minilizeHideTimer_Tick(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
                minilizeHideTimer.Enabled = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (_sendManager.IsSending)
                _sendManager.Stop();
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter = "*.*|*.*";
            DialogResult result = dia.ShowDialog();
            if(result == DialogResult.OK && dia.FileName != "")
            {
                _sendManager.Start(dia.FileName);
                this.Close();
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {
            if (_data2Word == null || _data2Word.IsDisposed)
                _data2Word = new Data2Word();
            _data2Word.Show();
        }
        private void resumeSendTimer_Tick(object sender, EventArgs e)
        {
            resumeSendTimer.Stop();
            List<object> data = (List<object>)(sender as Timer).Tag;
            Bitmap bmp = data[0] as Bitmap;
            Point startPos = (Point)data[1];
            if (_sendManager.IsSending)
                _sendManager.Resume(bmp, startPos, bmp.Width, bmp.Height);
            else if (Clipboard.ContainsText())
            {
                _sendManager.StartText(Clipboard.GetText());
                _sendManager.Resume(bmp, startPos, bmp.Width, bmp.Height);
            }

        }
        private void OKBtn_Click(object sender, EventArgs e)
        {
            bool shouldClose = true;
            shouldClose &= UpdateHotKeySetting();
            shouldClose &= UpdateAutoStartSetting();
            if (shouldClose)
                this.Close();
        }

        private bool UpdateHotKeySetting()
        {
            string hotKey = comboBoxHotKey.Text.ToUpper().Trim();
            if (hotKey == "PRINTSCREEN")
                hotKey = "PRTSCN";
            if ((hotKey != "PRTSCN") 
                && (hotKey.Length > 1 || hotKey.Length == 0 || hotKey[0] < 'A' || hotKey[0] > 'Z'))
            {
                MessageBox.Show("Hotkey must be A-Z or PrtScn", "input error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            HotKey.UnregisterHotKey(s_hotkeyReceiver.Handle, HotKey.HOT_KEY_ID);
            Settings.Default.CtrlHotKey = checkBoxHotkeyCtrl.Checked;
            Settings.Default.AltHotKey = checkBoxHotKeyAlt.Checked;
            Settings.Default.HotKey = (hotKey.Length == 1 ? (Keys)hotKey[0] : Keys.PrintScreen);
            Settings.Default.Save();
            if (!RegisterHotKey())
            {
                string str = "";
                if (Settings.Default.CtrlHotKey)
                    str += "Ctrl + ";
                if (Settings.Default.AltHotKey)
                    str += "Alt + ";
                str += Settings.Default.HotKey;
                MessageBox.Show("Register hotkey <" + str + "> failed! Please check if it has already been registered by other program. Or you can reset another hotkey.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool UpdateAutoStartSetting()
        {
            if ((autoStart.Checked && RegistryHandle.GetAutoStart() != RegistryHandle.AutoStartStat.YES)
                    || (!autoStart.Checked && RegistryHandle.GetAutoStart() != RegistryHandle.AutoStartStat.NO))
            {
                if (!RegistryHandle.SetAutoStart(autoStart.Checked))
                {
                    MessageBox.Show("Need permision to set auto start", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }
        private void MainForm_Activated(object sender, EventArgs e)
        {
            RefreshUI();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                OpenWindow();
            }
        }
        void setSpeedWatcherShow(bool show)
        {
            Settings.Default.ShowNetSpeedBar = show;
            this.showNetSpeedBar.Checked = show;
            this.ckShowNetSpeedBar.Checked = show;
            Settings.Default.Save();
            if (show == true)
            {
                if (_speedWatcher != null)
                    _speedWatcher.Show();
                else
                {
                    _speedWatcher = new NetSpeedWatcher();
                    _speedWatcher.Show();
                }
            }
            else
            {
                if(_speedWatcher != null)
                {
                    _speedWatcher.Close();
                    _speedWatcher = null;
                }
            }
        }
        private void showNetSpeedBar_Click(object sender, EventArgs e)
        {
            setSpeedWatcherShow(this.showNetSpeedBar.Checked);
        }

        private void ckShowNetSpeedBar_Click(object sender, EventArgs e)
        {
            setSpeedWatcherShow(this.ckShowNetSpeedBar.Checked);
        }

    }
}
