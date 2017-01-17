using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShot
{
    class BinaryReader
    {
        private byte[] _buffer;
        int _length;
        public int Length
        {
            get { return _length; }
        }
        int _pos;
        public int Position{
            get { return _pos; }
            set { _pos = value; }
        }
        public BinaryReader(byte[] buffer)
            :this(buffer,buffer.Length * 8)
        {
        }
        public BinaryReader(byte[] buffer,int len)
        {
            _buffer = buffer;
            _length = len;
        }
        public int read(int len)
        {
            int ret = 0;

            for (int i = len - 1; i >= 0; --i)
            {
                ret |= (int)(readBit() ? 1 : 0) << i;
            }

            return ret;
        }
        public bool readBit()
        {
            if (_pos >= _length)
                return false;
            int dataBytePos = _pos / 8;
            int bitPos = 7 - (_pos % 8);
            ++_pos;
            return ((_buffer[dataBytePos] >> bitPos) & 0x1) == 1;
        }
    }
}
