using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ScreenShot
{
    public class PictureReader
    {
        public const int BYTE_PIXEL_LEN = 3;
        public const int SHORT_PIXEL_LEN = 6;
        public const int INT_PIXEL_LEN = 11;
        public const int HEAD_SIZE = 8;

        public enum PicPos
        {
            WIDTH_POS = HEAD_SIZE,
            HEIGHT_POS = WIDTH_POS + SHORT_PIXEL_LEN,
            BACKSPACE_POS = HEIGHT_POS + SHORT_PIXEL_LEN,
            TYPE_BIT_POS = BACKSPACE_POS + 1,
            INDEX_POS = TYPE_BIT_POS + 1,
            LAST_DATA_POS0 = INDEX_POS + INT_PIXEL_LEN,
            LAST_DATA_POS1 = LAST_DATA_POS0 + BYTE_PIXEL_LEN,
            DATA_START = LAST_DATA_POS1 + BYTE_PIXEL_LEN,
            SEND_FINISHED = DATA_START + 0xffff + 1
        }

        //直接截屏模式的屏幕坐标
        private Point _startPos;
        public System.Drawing.Point StartPos
        {
            get { return _startPos; }
            set { _startPos = value; }
        }

        //整个数据区
        private Bitmap _bitmap;
        public System.Drawing.Bitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public static PictureReader CreatePictureReader(Bitmap bmp,Point startPos,int w,int h)
        {
            if (!FindStartPos(bmp, startPos, w, h, out startPos))
                return null;
            PictureReader reader = new PictureReader(startPos);
            return reader;
        }
        
        public bool ResetStartPos(Bitmap bmp, Point startPos, int w, int h)
        {
            if (!FindStartPos(bmp, startPos, w, h, out startPos))
                return false;
            _startPos = startPos;
            return Refresh();
        }
        
        //使用屏幕坐标作为开始坐标
        public PictureReader(Point startPos)
        {
            _startPos = startPos;
            Width = HEAD_SIZE + 2 * SHORT_PIXEL_LEN;
            Height = 1;
            Refresh();
            Width = ReadShort((int)PicPos.WIDTH_POS);
            Height = ReadShort((int)PicPos.HEIGHT_POS);
            _bitmap = null;
            Refresh();
        }
        public bool Refresh()
        {
            if(_bitmap == null)
                _bitmap = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(_bitmap);
            try {
                g.CopyFromScreen(new Point(_startPos.X, _startPos.Y), new Point(0, 0), new Size(Width, Height));
            }catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            if(!EnsureUsable())
            {
                _bitmap = null;
                return false;
            }
            return true;
        }
        public int ReadInt(int index)
        {
            return Read(index,INT_PIXEL_LEN);
        }
        public short ReadShort(int index)
        {
            return (short)Read(index, SHORT_PIXEL_LEN);
        }
        public byte ReadByte(int index)
        {
            return (byte)Read(index, BYTE_PIXEL_LEN);
        }
        public int Read(int index, int size)
        {
            if (_bitmap == null && !Refresh())
                return -1;
            BinaryStream bs = new BinaryStream();
            Color co = new Color(); ;
            for (int i = index; i < index + size; i++)
            {
                co = _bitmap.GetPixel(i % _bitmap.Width, i / _bitmap.Width);
                bs.PushBack(co.R == 255);
                bs.PushBack(co.G == 255);
                bs.PushBack(co.B == 255);
            }
            return bs.Data;
        }
        public static int SearchStartPos(Bitmap bmp, Point clickPos)
        {
            if (bmp == null)
                return -1;
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            IntPtr scan0 = data.Scan0;
            byte[] rgbVal = new byte[data.Stride * data.Height];
            System.Runtime.InteropServices.Marshal.Copy(scan0, rgbVal, 0, rgbVal.Length);
            bmp.UnlockBits(data);

            byte[] pattern = new byte[] {
                    0,0,0,
                    255,0,0,
                    0,255,0,
                    255,255,0,
                    0,0,255,
                    255,0,255,
                    0,255,255,
                    255,255,255
                };
            return KMP.Find(pattern, rgbVal);
        }
        private static bool FindStartPos(Bitmap bmp, Point clickPos, int w, int h,out Point startPos)
        {
            startPos = clickPos;
            int result = SearchStartPos(bmp, clickPos);
            if (result == -1)
                return false;
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int y = result / data.Stride;
            int x = result % data.Stride / 3;
            startPos.X += x;
            startPos.Y += y;
            return true;
        }
        private bool EnsureUsable()
        {
            return _bitmap != null && _bitmap.Width >= 8
                && _bitmap.GetPixel(0, 0) == Color.FromArgb(0, 0, 0)
                && _bitmap.GetPixel(1, 0) == Color.FromArgb(0, 0, 255)
                && _bitmap.GetPixel(2, 0) == Color.FromArgb(0, 255, 0)
                && _bitmap.GetPixel(3, 0) == Color.FromArgb(0, 255, 255)
                && _bitmap.GetPixel(4, 0) == Color.FromArgb(255, 0, 0)
                && _bitmap.GetPixel(5, 0) == Color.FromArgb(255, 0, 255)
                && _bitmap.GetPixel(6, 0) == Color.FromArgb(255, 255, 0)
                && _bitmap.GetPixel(7, 0) == Color.FromArgb(255, 255, 255);
        }
    }
}