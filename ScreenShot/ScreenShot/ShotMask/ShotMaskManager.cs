using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShot
{
    public delegate void ShotFinishHandle(Bitmap bmp, Point startPos);
    class ShotMaskManager
    {
        public enum RectState//鼠标按下的状态
        {
            READY,
            WILL_DRAWING,
            DRAWING_RECT,
            RECT_READY
        }
        private ShotFinishHandle _shotFinishHandle;
        private static ShotMaskManager s_instance;
        private ShotMask[] _maskWindows;
        private Point _endPos;
        private Point _startPos;
        private RectState _rectState;
        private Point _willDrawClickPos;
        private RectState _lastRectState;
        private System.Timers.Timer _gcTimer;
        private Bitmap[] _screens;

        public Point StartPos
        {
            get { return _startPos; }
            set { _startPos = value; }
        }
        public System.Drawing.Point EndPos
        {
            get { return _endPos; }
            set { _endPos = value; }
        }
        public int Width
        {
            get
            {
                if (_rectState == RectState.DRAWING_RECT || _rectState == RectState.RECT_READY)
                    return _endPos.X - _startPos.X;
                return 0;
            }
        }
        public int Height
        {
            get
            {
                if (_rectState == RectState.DRAWING_RECT || _rectState == RectState.RECT_READY)
                    return _endPos.Y - _startPos.Y;
                return 0;
            }
        }
        public RectState RectStat { get { return _rectState; } }
        public RectState LastRectStat { get { return _lastRectState; } }

        private ShotMaskManager()
        {
            _rectState = RectState.READY;
            _lastRectState = _rectState;
            _gcTimer = new System.Timers.Timer(3000);
            _gcTimer.AutoReset = false;
            _gcTimer.Elapsed += _gcTimer_Elapsed;
        }

        private void _gcTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.GC.Collect();
        }

        public static ShotMaskManager Instance()
        {
            if (s_instance == null)
                s_instance = new ShotMaskManager();
            return s_instance;
        }
        public void showMask(ShotFinishHandle shotFinishHandle)
        {
            if (_maskWindows == null)
            {
                _screens = new Bitmap[Screen.AllScreens.Length];
                _maskWindows = new ShotMask[Screen.AllScreens.Length];
                for (int i = 0; i < _maskWindows.Length; i++)
                {
                    _screens[i] = new Bitmap(Screen.AllScreens[i].Bounds.Width, Screen.AllScreens[i].Bounds.Height);
                    Graphics g = Graphics.FromImage(_screens[i]);
                    g.CopyFromScreen(Screen.AllScreens[i].Bounds.Location, new Point(0, 0), Screen.AllScreens[i].Bounds.Size);
                    _maskWindows[i] = new ShotMask(i,_screens[i]);
                    _maskWindows[i].Show();
                    _maskWindows[i].Activate();
                }
                _shotFinishHandle = shotFinishHandle;
                _rectState = RectState.READY;
                _startPos.X = _startPos.Y = 0;
                _endPos.X = _endPos.Y = 0;
            }
        }
        private void SetRectState(RectState state)
        {
            switch (_rectState)
            {
                case RectState.READY:
                    {
                        switch (state)
                        {
                            case RectState.READY:
                                break;
                            case RectState.WILL_DRAWING:
                                _lastRectState = _rectState;
                                _rectState = RectState.WILL_DRAWING;
                                _willDrawClickPos = Control.MousePosition;
                                break;
                            case RectState.DRAWING_RECT:
                                throw new Exception("impossible");
                            case RectState.RECT_READY:
                                throw new Exception("impossible");
                        }
                    }
                    break;
                case RectState.WILL_DRAWING:
                    switch (state)
                    {
                        case RectState.READY:
                            _lastRectState = _rectState;
                            _rectState = RectState.READY;
                            break;
                        case RectState.WILL_DRAWING:
                            break;
                        case RectState.DRAWING_RECT:
                            _startPos = _willDrawClickPos;
                            _endPos = _startPos;
                            _lastRectState = _rectState;
                            _rectState = RectState.DRAWING_RECT;
                            foreach (ShotMask m in _maskWindows)
                                m.HideTool();
                            break;
                        case RectState.RECT_READY:
                            _lastRectState = _rectState;
                            _rectState = RectState.RECT_READY;
                            break;
                    }
                    break;
                case RectState.DRAWING_RECT:
                    {
                        switch (state)
                        {
                            case RectState.READY:
                                throw new Exception("impossible");
                            case RectState.WILL_DRAWING:
                                throw new Exception("impossible");
                            case RectState.DRAWING_RECT:
                                break;
                            case RectState.RECT_READY:
                                _lastRectState = _rectState;
                                _rectState = RectState.RECT_READY;
                                _endPos = getMousePos();
                                foreach (ShotMask m in _maskWindows)
                                    m.ShowTool(_endPos);
                                break;
                        }
                    }
                    break;
                case RectState.RECT_READY:
                    {
                        switch (state)
                        {
                            case RectState.READY:
                                _lastRectState = _rectState;
                                _rectState = RectState.READY;
                                foreach (ShotMask m in _maskWindows)
                                    m.HideTool();
                                break;
                            case RectState.WILL_DRAWING:
                                _lastRectState = _rectState;
                                _rectState = RectState.WILL_DRAWING;
                                _willDrawClickPos = getMousePos();
                                break;
                            case RectState.DRAWING_RECT:
                                throw new Exception("impossible");
                            case RectState.RECT_READY:
                                break;
                        }
                    }
                    break;

            }
            foreach (ShotMask m in _maskWindows)
                m.Refresh();
            foreach (ShotMask m in _maskWindows)
                m.RefreshRectDraw();
            Console.WriteLine("State:" + " " + _rectState);
        }

        public void _cbKeyDown(Keys key)
        {
            if (key == Keys.Escape)
                EscPress();
            else if (key == Keys.Enter)
                _cbEndShot(false);
        }
        public void _cbMouseDown(MouseButtons btn)
        {
            if (btn == MouseButtons.Left)
            {
                SetRectState(RectState.WILL_DRAWING);
            }
            else if (btn == MouseButtons.Right)
            {
                EscPress();
            }
        }
        public void _cbMouseMove(int x, int y)
        {
            if (_rectState == RectState.WILL_DRAWING && !getMousePos().Equals(_willDrawClickPos))
            {
                SetRectState(RectState.DRAWING_RECT);
            }
            if (_rectState == RectState.DRAWING_RECT)
            {
                _endPos = Control.MousePosition;
                foreach (ShotMask form in _maskWindows)
                {
                    form.RefreshRectDraw();
                }
            }
            if (_maskWindows != null)
                foreach (ShotMask form in _maskWindows)
                    form._cbRefreshMouseMove(Control.MousePosition.X, Control.MousePosition.Y);
        }
        public void _cbMouseUp(MouseButtons btn)
        {
            if (btn == MouseButtons.Left)
            {
                if (_rectState == RectState.DRAWING_RECT)
                {
                    SetRectState(RectState.RECT_READY);
                }
                else if (_rectState == RectState.WILL_DRAWING)
                {
                    SetRectState(_lastRectState);
                }

            }
        }
        public void _cbClosing()
        {
            _shotFinishHandle(null, new Point(0, 0));
            CloseAll();
        }
        private Point getMousePos()
        {
            Point po = Control.MousePosition;
            return po;
        }
        private void EscPress()
        {
            if (_rectState == RectState.RECT_READY)
                SetRectState(RectState.READY);
            else if (_rectState == RectState.READY)
            {
                _shotFinishHandle(null, new Point(0, 0));
                CloseAll();
            }
        }
        private void CloseAll()
        {
            if(_maskWindows != null)
            {
                ShotMask[] arr = _maskWindows;
                _maskWindows = null;
                foreach (Form f in arr)
                    f.Close();
            }
            _gcTimer.Enabled = true;
            _screens = null;
        }
        private Bitmap GetScreenShot()
        {
            Point startPos = _startPos;
            Point endPos = _endPos;
            int w = endPos.X - startPos.X;
            int h = endPos.Y - startPos.Y;
            if (h < 0)
            {
                int tmp = startPos.Y;
                startPos.Y = endPos.Y;
                endPos.Y = tmp;
                h = -h;
            }
            if (w < 0)
            {
                int tmp = startPos.X;
                startPos.X = endPos.X;
                endPos.X = tmp;
                w = -w;
            }
            if (h == 0)
                h = 1;
            if (w == 0)
                w = 1;
            Bitmap bmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(bmp);
            Rectangle destRect = new Rectangle(startPos, new Size(w, h));

            for(int i = 0;i < Screen.AllScreens.Length; ++i)
            {
                Screen sc = Screen.AllScreens[i];
                if (!sc.Bounds.IntersectsWith(destRect))
                    continue;
                Rectangle drawRect = destRect;
                drawRect.Intersect(sc.Bounds);
                Point poSrc = drawRect.Location;
                drawRect.X -= startPos.X; drawRect.Y -= startPos.Y;
                poSrc.X -= sc.Bounds.Location.X; poSrc.Y -= sc.Bounds.Location.Y;
                g.DrawImage(_screens[i], drawRect, new Rectangle(poSrc,drawRect.Size),GraphicsUnit.Pixel);
            }
            return bmp;
        }
        public void _cbEndShot(bool save)
        {
            if (save)
            {
                SaveFileDialog saveBmp = new SaveFileDialog();
                saveBmp.Filter = "png格式|*.png|jpg格式|*.jpg|bmp格式|*.bmp";//过滤器
                saveBmp.FileName = String.Format("ScreenShot_{0}_{1}_{2}_{3}_{4}_{5}",
                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);//默认名字
                DialogResult result = saveBmp.ShowDialog();//弹出对话框
                if (result == DialogResult.OK && saveBmp.FileName != "")
                {
                    Bitmap bmp = GetScreenShot();
                    switch (saveBmp.FilterIndex)
                    {
                        case 1:
                            bmp.Save(saveBmp.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                        case 2:
                            bmp.Save(saveBmp.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case 3:
                            bmp.Save(saveBmp.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                    }
                    if (_shotFinishHandle != null)
                        _shotFinishHandle(bmp, _startPos);
                    CloseAll();
                }
            }
            else
            {
                if (_shotFinishHandle != null)
                    _shotFinishHandle(GetScreenShot(), _startPos);
                CloseAll();
            }
        }
    }
}
