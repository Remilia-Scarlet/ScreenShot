using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShot.general.BigNumber
{
    class BigNumber
    {
        public const ulong MAX_UINT_BASE = 4294967296;
        public BigNumber()
        {
            _data = new List<uint>();
        }
        public BigNumber(uint number, ulong targetBase)
            :this()
        {
            fromInt(number, targetBase);
        }
        public BigNumber fromInt(uint number, ulong targetBase)
        {
            _data.Clear();
            while(number > 0)
            {
                _data.Add((uint)(number % targetBase));
                number = (uint)(number / targetBase);
            }
            _numBase = targetBase;
            return this;
        }
        public BigNumber fromArray(uint[] data, ulong dataBase, ulong targetBase)
        {
            
            return this;
        }
        public BigNumber divide(uint divisor)
        {
            
            return this;
        }
        public uint toInt()
        {
            uint num = 0;
            for (int i = 0; i < _data.Count; ++i) 
            {
                num += _data[i] * (uint)pow(_numBase, (uint)i);
            }
            return num;
        }

        private ulong pow(ulong x, ulong y)
        {
            ulong ret = 1;
            for (uint i = 0; i < y; ++i) 
            {
                ret *= x;
            }
            return ret;
        }
        private List<uint> _data;
        private ulong _numBase;
    }
}
