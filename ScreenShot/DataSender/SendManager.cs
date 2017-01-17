using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ScreenShot
{
    class SendManager
    {
        private const int RESEND_INTERVAL = 800;
        private const int WAIT_MIN_MIN = 20;
        private const int WAIT_MIN_MAX = 50;
        public SendManager()
        {
            IsSending = false;
            IsPausing = true;
            _waitForResult = WaitForClickResult.NONE;
            _typeBit = TypeBit.SHORT;
        }
        private enum WaitForClickResult
        {
            NONE,
            BACKSPACE,
            CHANGE_TYPE_BIT,
            SEND,
            SEND_FINISHED
        }
        private enum TypeBit
        {
            BYTE,
            SHORT
        }
        public bool IsSending { set; get; }
        public bool IsPausing { set; get; }

        private MemoryStream _fileStream;
        private Timer _timer;
        private PictureReader _picReader;
        private Point _lastMousePos;
        private WaitForClickResult _waitForResult;
        private TypeBit _typeBit;
        private int _waitForResultTime;
        private int _waitMin = 0;
        public void Start(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            if (!fs.CanRead)
            {
                fs.Close();
                return;
            }
            System.Console.WriteLine("start");
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();
            init(new MemoryStream(data, 0, data.Length, false, true));
        }
        private void init(MemoryStream data)
        {
            _fileStream = data;
            IsSending = true;
            IsPausing = true;
            _waitMin = WAIT_MIN_MIN;
            _timer = new Timer(10);
            _timer.Enabled = false;
            _timer.Elapsed += _timer_Elapsed;
            _waitForResult = WaitForClickResult.NONE;
            _typeBit = TypeBit.SHORT;
        }
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;

            if (!CheckMousePos() || !_picReader.Refresh())
            {
                Pause();
                return; 
            }
            do
            {
                if (WaitForResult())
                    break;

                if (!IsSending)
                    break;

                if (!CheckTypeBit())
                    break;

                if (!CheckIndex())
                    break;

                if (!CheckLastData())
                    break;

                SendData();
            } while (false);

            if(IsSending)
                _timer.Enabled = true;
        }
        //返回true继续等待，返回false继续执行tick
        private bool WaitForResult()
        {
            if (_waitForResult == WaitForClickResult.NONE)
                return false;
            if (System.Environment.TickCount - _waitForResultTime < _waitMin)
            {
                System.Console.WriteLine("wait less than min time");
                return true;
            }
            else if (System.Environment.TickCount - _waitForResultTime > RESEND_INTERVAL)
            {
                System.Console.WriteLine("wait timeout");
                _waitForResult = WaitForClickResult.NONE;
                return false;//超时不再继续等待
            }
            System.Console.Write("wait for ");
            bool ret = false;
            switch (_waitForResult)
            {
                case WaitForClickResult.CHANGE_TYPE_BIT:
                    {
                        int typeBit = _picReader.Read((int)PictureReader.PicPos.TYPE_BIT_POS, 1);
                        System.Console.Write("typebit:" + _typeBit + " " + typeBit);
                        ret = typeBit != (int)_typeBit;
                    }
                    break;
                case WaitForClickResult.BACKSPACE:
                    {
                        int nowIndex = _picReader.ReadInt((int)PictureReader.PicPos.INDEX_POS);
                        System.Console.WriteLine("backspace:" + _fileStream.Position + " " + nowIndex);
                        ret = nowIndex != _fileStream.Position;
                    }
                    break;
                case WaitForClickResult.SEND:
                    {
                        int nowIndex = _picReader.ReadInt((int)PictureReader.PicPos.INDEX_POS);
                        byte lastData0 = _picReader.ReadByte((int)PictureReader.PicPos.LAST_DATA_POS0);
                        byte lastData1 = _picReader.ReadByte((int)PictureReader.PicPos.LAST_DATA_POS1);
                        byte[] buffer = _fileStream.GetBuffer();
                        System.Console.Write("send: ");
                        System.Console.Write("i:" + _fileStream.Position + " " + nowIndex);
                        System.Console.Write(" d1:" + buffer[_fileStream.Position - 1] + " " + lastData1);
                        if (_fileStream.Position > 1)
                            System.Console.Write(" d0:" + buffer[_fileStream.Position - 2] + " " + lastData0);
                        if (nowIndex != _fileStream.Position)
                            ret |= true;
                        else if (lastData1 != buffer[_fileStream.Position - 1])
                            ret |= true;
                        else if (_fileStream.Position > 1 && lastData0 != buffer[_fileStream.Position - 2])
                            ret |= true;
                    }
                    break;
                case WaitForClickResult.SEND_FINISHED:
                    {
                        int finished = _picReader.Read((int)PictureReader.PicPos.SEND_FINISHED, 1);
                        System.Console.Write("finish:" + finished);
                        if (finished == 1)
                        {
                            Clear();
                            ret = false;
                            System.Windows.Forms.MessageBox.Show("finish");
                        }
                        else
                            ret = true;
                    }
                    break;
            }
            System.Console.WriteLine("  " + ret);
            if (!ret)
                _waitForResult = WaitForClickResult.NONE;
            return ret;
        }
        public void Stop()
        {
            if (!IsSending)
                return;
            Clear();
        }
        public void Pause()
        {
            if (!IsSending)
                return;
            System.Console.WriteLine("pause");
            IsPausing = true;
            _timer.Enabled = false;
        }
        public void Resume(Bitmap bmp, Point startPos, int w, int h)
        {
            if (!IsSending || !IsPausing)
                return;
            if (!EnsurePicReader(bmp, startPos, w, h))
                return;
            System.Console.WriteLine("resume");
            IsPausing = false;

            _lastMousePos = System.Windows.Forms.Control.MousePosition;
            _timer.Enabled = true;
        }
        public void StartText(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            init(new MemoryStream(data,0,data.Length,false,true));
        }
        private void SendData()
        {
            if (!IsSending || IsPausing)
                return;
            //发送新数据
            ushort s = 0;
            if(_typeBit == TypeBit.SHORT)
            {
                if(_fileStream.Position + 2 > _fileStream.Length)
                {
                    _typeBit = TypeBit.BYTE;
                    CheckTypeBit();
                    return;
                }
                else
                {
                    s = (ushort)_fileStream.ReadByte();
                    s <<= 8;
                    s |= (ushort)_fileStream.ReadByte();
                }
            }
            else
            {
                if (_fileStream.Position >= _fileStream.Length)
                {
                    SendFinished();
                    return;
                }
                else
                    s = (ushort)_fileStream.ReadByte();
            }
            int pos = s + (int)PictureReader.PicPos.DATA_START;
            Click(pos);
            if (_waitMin > WAIT_MIN_MIN)
                _waitMin--;
            _waitForResult = WaitForClickResult.SEND;
            _waitForResultTime = System.Environment.TickCount;
            return;
        }
        private void SendBackspace()
        {
            if (!IsSending || IsPausing)
                return;
            Click((int)PictureReader.PicPos.BACKSPACE_POS);
            if (_waitMin < WAIT_MIN_MAX)
                _waitMin += 5;
            _waitForResult = WaitForClickResult.BACKSPACE;
            _waitForResultTime = System.Environment.TickCount;
        }
        private bool EnsurePicReader(Bitmap bmp, Point startPos, int w, int h)
        {
            if (_picReader == null)
            {
                _picReader = PictureReader.CreatePictureReader(bmp, startPos, w, h);
                if (_picReader == null)
                    return false;
            }
            else
            {
                if (!_picReader.ResetStartPos(bmp, startPos, w, h))
                    return false;
            }
            return true;
        }
        private void SendFinished()
        {
            if (!IsSending)
                return;
            Click((int)PictureReader.PicPos.SEND_FINISHED);
            _waitForResult = WaitForClickResult.SEND_FINISHED;
            _waitForResultTime = System.Environment.TickCount;
        }
        private void Clear()
        {
            IsSending = false;
            if (_fileStream != null)
                _fileStream.Close();
            _fileStream = null;
            _timer.Enabled = false;
            _picReader = null;
        }
        private bool CheckMousePos()
        {
            if (_lastMousePos != System.Windows.Forms.Control.MousePosition)
                return false;
            return true;
        }
        private bool CheckTypeBit()
        {
            int typeBit = _picReader.Read((int)PictureReader.PicPos.TYPE_BIT_POS, 1);
            if (typeBit != (int)_typeBit)
            {
                Click((int)PictureReader.PicPos.TYPE_BIT_POS);
                _waitForResult = WaitForClickResult.CHANGE_TYPE_BIT;
                _waitForResultTime = System.Environment.TickCount;
                return false;
            }
            else
                return true;
        }
        private bool CheckIndex()
        {
            int nowIndex = _picReader.ReadInt((int)PictureReader.PicPos.INDEX_POS);
            if (_fileStream.Position < nowIndex)
            {
                SendBackspace();
                return false;
            }
            else if (_fileStream.Position > nowIndex)
            {
                if (_fileStream.Position - nowIndex < 10)
                    _fileStream.Position = nowIndex;
                else
                {
                    Pause();
                    return false;
                }
            }
            return true;
        }
        private bool CheckLastData()
        {
            if (_fileStream.Position == 0)
                return true;
            byte lastData0 = _picReader.ReadByte((int)PictureReader.PicPos.LAST_DATA_POS0);
            byte lastData1 = _picReader.ReadByte((int)PictureReader.PicPos.LAST_DATA_POS1);
            byte[] buffer = _fileStream.GetBuffer();
            if (lastData1 != buffer[_fileStream.Position - 1] 
                || (_fileStream.Position > 1 && lastData0 != buffer[_fileStream.Position - 2]))
            {
                SendBackspace();
                return false;
            }
            return true;
        }
        private void Click(int pos)
        {
            Point p = _picReader.StartPos;
            p.X += pos % _picReader.Width;
            p.Y += pos / _picReader.Width;
            MouseControl.Click(p);
            _lastMousePos = System.Windows.Forms.Control.MousePosition;
        }
    }
}
