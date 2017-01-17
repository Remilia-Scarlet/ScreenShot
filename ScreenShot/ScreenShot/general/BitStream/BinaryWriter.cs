using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShot
{
    class BinaryWriter
    {
        private int _pos;
        private byte[] _buffer;
        public byte[] Buffer
        {
            get
            {
                byte[] ret = new byte[(_length + 7) / 8];
                Array.Copy(_buffer, ret, ret.Length);
                return ret;
            }
        }
        public int Position
        {
            get { return _pos; }
            set { _pos = value; }
        }
        private int _length;
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }
        public BinaryWriter()
        {
            _buffer = new byte[16];
        }
        public void write(int data,int len)
        {
            for (int i = len - 1; i >= 0;--i)
            {
                writeBit(((data >> i) & 1) == 1);
            }
        }
        public void writeBit(bool bit)
        {
            if (_pos == _length)
                ++_length;
            if (_pos / 8 >= _buffer.Length)
                Array.Resize(ref _buffer, (int)(_buffer.Length * 1.5));

            int bitPos = 7 - (_pos % 8);
            int bytePos = _pos / 8;
            ++_pos;

            _buffer[bytePos] |= (byte)((bit ? 1 : 0) << bitPos);
        }
    }
}
