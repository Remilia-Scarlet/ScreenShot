using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShot
{
    public partial class ShotMask : Form
    {
        private Image _screen;
        private Bitmap _solidScreen;

        private int _screenIndex;
        private ShotMaskManager _maskManager;
        private Pen _drawRectPen;
        private Bitmap[] _drawBuffer;
        private Graphics[] _drawBufferGraphic;
        private int _drawBufferIndex;
        public ShotMask(int screenIndex,Bitmap screen)
        {
            InitializeComponent();
            _screenIndex = screenIndex;
            this.Location = Screen.AllScreens[_screenIndex].Bounds.Location;
            _maskManager = ShotMaskManager.Instance();
            _drawRectPen = new Pen(Color.FromArgb(255, 24, 215, 255));
            _screen = screen;
        }
        
        private void ShotMask_Load(object sender, EventArgs e)
        {
            drawBox.Location = new Point(0, 0);
            drawBox.Size = new Size(this.Width, this.Height);
            
            _solidScreen = new Bitmap(_screen);
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(125, 0, 0, 0));
            Graphics graSolidScreen = Graphics.FromImage(_solidScreen);
            graSolidScreen.FillRectangle(solidBrush, 0, 0, _solidScreen.Width, _solidScreen.Height);

            _drawBuffer = new Bitmap[2];
            _drawBufferGraphic = new Graphics[2];
            _drawBuffer[0] = new Bitmap(_solidScreen);
            _drawBufferGraphic[0] = Graphics.FromImage(_drawBuffer[0]);
            _drawBuffer[1] = new Bitmap(_solidScreen);
            _drawBufferGraphic[1] = Graphics.FromImage(_drawBuffer[1]);
            _drawBufferIndex = 0;
            drawBox.Image = _drawBuffer[_drawBufferIndex];
            
            btnLayout.Visible = false;
        }
        public void HideTool()
        {
            btnLayout.Visible = false;
        }
        public void ShowTool(Point mousePos)
        {
            btnLayout.Location = convertMousePosToFormPos(mousePos);
            btnLayout.Show();
        }
        private void ShotMask_KeyDown(object sender, KeyEventArgs e)
        {
            _maskManager._cbKeyDown(e.KeyCode);
        }
        private void DrawBox_MouseDown(object sender, MouseEventArgs e)
        {
            _maskManager._cbMouseDown(e.Button);
        }
        private void DrawBox_MouseMove(object sender, MouseEventArgs e)
        {
            _maskManager._cbMouseMove(e.X, e.Y);
        }
        private void DrawBox_MouseUp(object sender, MouseEventArgs e)
        {
            _maskManager._cbMouseUp(e.Button);
        }
        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            if (panelInfo.Location.X == 0 && panelInfo.Location.Y == 0)
                panelInfo.Location = new Point(this.Width - panelInfo.Width, 0);
            else
                panelInfo.Location = new Point(0,0);
        }
        public void RefreshRectDraw()
        {
            panelInfo.Refresh();
            _drawBufferIndex = (_drawBufferIndex + 1) % _drawBuffer.Length;
            Graphics nowGragh = _drawBufferGraphic[_drawBufferIndex];
            nowGragh.DrawImage(_solidScreen, 0, 0, _solidScreen.Width, _solidScreen.Height);
            switch (ShotMaskManager.Instance().RectStat)
            {
                case ShotMaskManager.RectState.READY:
                    break;
                case ShotMaskManager.RectState.DRAWING_RECT:
                    {
                        Point downPos = convertMousePosToFormPos(_maskManager.StartPos);
                        Point mousePos = convertMousePosToFormPos(_maskManager.EndPos);
                        int w = mousePos.X - downPos.X;
                        int h = mousePos.Y - downPos.Y;
                        if (h < 0)
                        {
                            int tmp = downPos.Y;
                            downPos.Y = mousePos.Y;
                            mousePos.Y = tmp;
                            h = -h;
                        }
                        if (w < 0)
                        {
                            int tmp = downPos.X;
                            downPos.X = mousePos.X;
                            mousePos.X = tmp;
                            w = -w;
                        }
                        nowGragh.DrawRectangle(_drawRectPen, downPos.X, downPos.Y, w, h);
                    }
                    break;
                case ShotMaskManager.RectState.WILL_DRAWING:
                case ShotMaskManager.RectState.RECT_READY:
                    if (_maskManager.RectStat == ShotMaskManager.RectState.RECT_READY || (_maskManager.RectStat == ShotMaskManager.RectState.WILL_DRAWING && _maskManager.LastRectStat == ShotMaskManager.RectState.RECT_READY))
                    {
                        Point startPos = convertMousePosToFormPos(_maskManager.StartPos);
                        Point endPos = convertMousePosToFormPos(_maskManager.EndPos);
                        int w = endPos.X - startPos.X;
                        int h = endPos.Y - startPos.Y;
                        Rectangle destRect = new Rectangle(startPos.X, startPos.Y, w + 1, h + 1);
                        Rectangle srcRect = new Rectangle(startPos.X, startPos.Y, w + 1, h + 1);
                        nowGragh.DrawImage(_screen, destRect, srcRect, GraphicsUnit.Pixel);
                    }
                    break;
            }
            drawBox.Image = _drawBuffer[_drawBufferIndex];
            drawBox.Refresh();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            _maskManager._cbEndShot(false);
        }
       
        private void saveBtn_Click(object sender, EventArgs e)
        {
            _maskManager._cbEndShot(true);
        }
        private void drawBox_MouseEnter(object sender, EventArgs e)
        {
            Point po = MousePosition;
            po.X -= this.Location.X;
            po.Y -= this.Location.Y;
            if (po.X < this.Width / 2)
            {
                Rectangle rect = panelInfo.Bounds;
                rect.Location = new Point(0, 0);
                if (!rect.Contains(po))
                    panelInfo.Location = new Point(0, 0);
                else
                    panelInfo.Location = new Point(this.Width - panelInfo.Width, 0);
            }
            else
            {
                Rectangle rect = panelInfo.Bounds;
                rect.Location = new Point(this.Width - panelInfo.Width, 0);
                if(!rect.Contains(po))
                    panelInfo.Location = new Point(this.Width - panelInfo.Width, 0);
                else
                    panelInfo.Location = new Point(0, 0);
            }
            panelInfo.Show();
        }

        private void drawBox_MouseLeave(object sender, EventArgs e)
        {
            if(!this.Bounds.Contains(MousePosition))
                panelInfo.Hide();
        }

        private void ShotMask_FormClosing(object sender, FormClosingEventArgs e)
        {
            _maskManager._cbClosing();
        }
        private Point convertMousePosToFormPos(Point pos)
        {
            pos.X -= this.Location.X;
            pos.Y -= this.Location.Y;
            return pos;
        }
        public void _cbRefreshMouseMove(int x,int y)
        {
            labelLocation.Text = String.Format("X:{0} Y:{1}", x,y);
            if (_maskManager.RectStat == ShotMaskManager.RectState.DRAWING_RECT || _maskManager.RectStat == ShotMaskManager.RectState.RECT_READY)
            {
                labelWidth.Text = "" + Math.Abs(_maskManager.Width);
                labelHeight.Text = "" + Math.Abs(_maskManager.Height);
            }
            else
            {
                labelWidth.Text = "0";
                labelHeight.Text = "0";
            }
        }
    }
}
