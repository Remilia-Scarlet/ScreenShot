using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShot.general.BigNumber
{
    class BigNumber
    {
        public const uint MAX_UINT_BASE = 0;//Means 4294967296, as we doesn't have base of 0, so I use 0 as base 4294967296
        public BigNumber()
        {
            _data = new List<uint>();
        }
        public BigNumber(uint number, uint targetBase)
            :this()
        {
            if (checkValidBase(targetBase))
            {
                fromInt(number, targetBase);
            }
        }
        public BigNumber(uint[] data, uint dataBase)
        {
            if (checkValidBase(dataBase))
            {
                _data = new List<uint>(data);
                _numBase = dataBase;
            }
        }
        public BigNumber fromInt(uint number, uint targetBase)
        {
            if(!checkValidBase(targetBase))
                return this;

            _data.Clear();
            while(number > 0)
            {
                _data.Add((uint)(number % targetBase));
                number = (uint)(number / targetBase);
            }
            _numBase = targetBase;
            return this;
        }
        public BigNumber sysConvertion(uint targetBase)
        {
            if (!checkValidBase(targetBase))
                return this;
            BigNumber sys = new BigNumber(targetBase, _numBase);
            uint remainder = 0;
            List<uint> newNum = new List<uint>();
            while (this.toInt() != 0)
            {
                this.divide(sys, out remainder);
                newNum.Add(remainder);
            }
            _data = newNum;
            return this;
        }
        public BigNumber divide(BigNumber divisor, out uint remainder)
        {
            //remainder = 0;
            //List<uint> tempDividend = new List<uint>();
            //List<uint> result = new List<uint>();
            //for (int i = _data.Count; i >= 0; --i)
            //{
            //    tempDividend.Insert(0, _data[i]);
            //    uint quotient = baseDivisor(tempDividend, divisor);
            //    result.Insert(0, quotient);
            //}
            return this;
        }
        public BigNumber divide(BigNumber divisor)
        {
            uint remainder = 0;
            return this.divide(divisor, out remainder);
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
        
        private uint pow(uint x, uint y)
        {
            uint ret = 1;
            for (uint i = 0; i < y; ++i) 
            {
                ret *= x;
            }
            return ret;
        }
        private bool checkValidBase(uint baseNum)
        {
            return baseNum >= 2 || baseNum == MAX_UINT_BASE;
        }
        private uint baseDivisor(List<uint> tempDividend, BigNumber divisor)
        {

        }
        private List<uint> _data;
        private uint _numBase;
    }
}
