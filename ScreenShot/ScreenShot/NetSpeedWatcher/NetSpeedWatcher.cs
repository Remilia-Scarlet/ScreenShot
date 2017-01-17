using Echevil;
using ScreenShot.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenShot
{
    public partial class NetSpeedWatcher : Form
    {
        private Point _mouseDownPoint;
        private NetworkMonitor _monitor;
        public NetSpeedWatcher()
        {
            InitializeComponent();
        }

        private void NetSpeedWatcher_Load(object sender, EventArgs e)
        {
            this.Height = 19;
            if (Settings.Default.LastPos.X == 0 && Settings.Default.LastPos.Y == 0)
                this.Location = new Point(Screen.PrimaryScreen.Bounds.Size.Width / 2, Screen.PrimaryScreen.Bounds.Size.Height / 2);
            else
                this.Location = Settings.Default.LastPos;
            this.Location = FixLocation(Location);
            _monitor = new NetworkMonitor();
            _monitor.StartMonitoring();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            long down = _monitor.TotolDown();
            long up = _monitor.TotolUp();
            if (down > 1024*1024)
                lblDownload.ForeColor = Color.Red;
            else
                lblDownload.ForeColor = Color.Black;
            if (up > 1024 * 1024)
                lblUpload.ForeColor = Color.Red;
            else
                lblUpload.ForeColor = Color.Black;
            this.lblDownload.Text = formatingDataSize(down);
            this.lblUpload.Text = formatingDataSize(up);
        }
        private string formatingDataSize(long size)
        {
            double n = size;
            n /= 1024;
            if(n < 1000)
                return n.ToString("F2") + " K/s";
            else
            {
                n /= 1024;
                return n.ToString("F2") + " M/s";
            }
        }
        private void NetSpeedWatcher_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDownPoint = new Point(e.X, e.Y);
        }
        private void NetSpeedWatcher_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point pos = new Point(this.Location.X + e.X - _mouseDownPoint.X, this.Location.Y + e.Y - _mouseDownPoint.Y);
                this.Location = FixLocation(pos);
                Settings.Default.LastPos = this.Location;
            }
        }
        private Point FixLocation(Point posToSet)
        {
            Rectangle rect = this.Bounds;
            rect.Location = posToSet;
            Rectangle screenRect = Screen.GetWorkingArea(rect);
            if (rect.Location.X + this.Width > screenRect.Left + screenRect.Width)
                rect.Location = new Point(screenRect.Left + screenRect.Width - this.Width, rect.Location.Y);
            if (rect.Location.X < screenRect.Left)
                rect.Location = new Point(screenRect.Left, rect.Location.Y);
            if (rect.Location.Y + this.Height > screenRect.Top + screenRect.Height)
                rect.Location = new Point(rect.Location.X, screenRect.Top + screenRect.Height - this.Height);
            if (rect.Location.Y < screenRect.Top)
                rect.Location = new Point(rect.Location.X, screenRect.Top);
            return rect.Location;
        }
        private void NetSpeedWatcher_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }


    }
}
