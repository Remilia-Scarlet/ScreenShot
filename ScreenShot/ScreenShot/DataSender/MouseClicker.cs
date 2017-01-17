using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace ScreenShot
{
    class MouseControl
    {
        /// <summary>
        /// 鼠标控制参数
        /// </summary>
        const int MOUSEEVENTF_LEFTDOWN = 0x2;
        const int MOUSEEVENTF_LEFTUP = 0x4;
        const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        const int MOUSEEVENTF_MIDDLEUP = 0x40;
        const int MOUSEEVENTF_MOVE = 0x1;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        const int MOUSEEVENTF_RIGHTUP = 0x10;

        /// <summary>
        /// 鼠标的位置
        /// </summary>
        public struct PONITAPI
        {
            public int x, y;
        }

        [DllImport("user32.dll")]
        public static extern int GetCursorPos(ref PONITAPI p);

        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public static void Click(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        public static void Click(Point p)
        {
            Click(p.X, p.Y);
        }
                /*
        [STAThread]
        static void Main()
        {
            PONITAPI p = new PONITAPI();
            GetCursorPos(ref p);
            Console.WriteLine("鼠标现在的位置X:{0}, Y:{1}", p.x, p.y);
            Console.WriteLine("Sleep 1 sec...");
            Thread.Sleep(1000);

            p.x = (new Random()).Next(Screen.PrimaryScreen.Bounds.Width);
            p.y = (new Random()).Next(Screen.PrimaryScreen.Bounds.Height);
            Console.WriteLine("把鼠标移动到X:{0}, Y:{1}", p.x, p.y);
            SetCursorPos(p.x, p.y);
            GetCursorPos(ref p);
            Console.WriteLine("鼠标现在的位置X:{0}, Y:{1}", p.x, p.y);
            Console.WriteLine("Sleep 1 sec...");
            Thread.Sleep(1000);

            Console.WriteLine("在X:{0}, Y:{1} 按下鼠标左键", p.x, p.y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, p.x, p.y, 0, 0);
            Console.WriteLine("Sleep 1 sec...");
            Thread.Sleep(1000);

            Console.WriteLine("在X:{0}, Y:{1} 释放鼠标左键", p.x, p.y);
            mouse_event(MOUSEEVENTF_LEFTUP, p.x, p.y, 0, 0);
            Console.WriteLine("程序结束，按任意键退出....");
            Console.ReadKey();
        }
        */
    }
}
