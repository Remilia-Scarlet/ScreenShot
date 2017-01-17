using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShot
{
    /* 使用int作为储存数据的空间
    左侧是高位，右侧是低位
    */
    class BinaryStream
    {
        private int _data;
        public int Data
        {
            get { return _data; }
            set { _data = value; }
        }
        public BinaryStream() : this(0)
        {
        }
        public BinaryStream(int data)
        {
            _data = data;
        }
        public void PushBack(bool value)
        {
            _data <<= 1;
            _data |= (value ? 1 : 0);
        }
        public void PushFront(bool value)
        {
            _data >>= 1;
            _data |= (value ? 1 << 31 : 0);
        }
        public void Set(int index, bool value)
        {
            if(index >= 0 && index < 32)
            {
                if (value)
                    _data |= (1 << index);
                else
                    _data &= ~(1 << index);
            }
        }
        public bool Get(int index)
        {
            return (_data & (1 << index)) > 0;
        }
    }
}
