using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShot
{
    class Serializer
    {
        byte[] _buffer;
        int _pos;
        public Serializer()
        {
            _buffer = new byte[16];
        }
        public void writeInt8(sbyte data)
        {
            ensureBufferSize(1);

        }
        public void writeInt16(short data)
        {

        }

        public void writeInt32(int data)
        {

        }
        public void writeInt64(long data)
        {

        }
        public void writeFloat(float data)
        {

        }
        public void writeDouble(double data)
        {

        }
        public void writeString(string data)
        {
            
        }
        public void writeBinary(byte[] data)
        {

        }
        private void ensureBufferSize(int needSize)
        {
            if (_pos + needSize < _buffer.Length)
                return;
            int newSize = (int)(_buffer.Length * 1.5);
            while (_pos + newSize >= newSize)
                newSize = (int)(newSize * 1.5);
            Array.Resize(ref _buffer, newSize);
        }
    }
}
