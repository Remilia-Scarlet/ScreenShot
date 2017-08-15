using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShot.general.BigNumber
{
    class BigNumber
    {
        public const uint MAX_UINT_BASE = 0;//Means system is 4294967296, as we doesn't have base of 0, so I use 0 as base 4294967296
        public BigNumber()
        {
            _data = new List<uint>();
        }
        public BigNumber(BigNumber other)
        {
            _data = new List<uint>(other._data);
            _numBase = other._numBase;
            _negative = other._negative;
        }
        public BigNumber(uint number, bool negative, uint targetBase)
            :this()
        {
            if (checkValidBase(targetBase))
            {
                fromInt(number, negative, targetBase);
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
        public BigNumber fromInt(uint number,bool negative, uint targetBase)
        {
            if(!checkValidBase(targetBase))
                return this;

            _data.Clear();
            while(number > 0)
            {
                _data.Add((number % targetBase));
                number = number / targetBase;
            }
            _numBase = targetBase;
            _negative = negative;
            return this;
        }
        public BigNumber sysConvertion(uint targetBase)
        {
            if (!checkValidBase(targetBase))
                return this;
            BigNumber sys = new BigNumber(targetBase, false, _numBase);
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
        public BigNumber add(BigNumber other)
        {
            if (other._numBase != _numBase)
                other = new BigNumber(other).sysConvertion(_numBase);
            int thisLen = _data.Count;
            int otherLen = other._data.Count;
            int maxLen = Math.Max(thisLen, otherLen);
            _data.Capacity = maxLen;
            bool carry = false;
            for (int i = 0; i < maxLen; ++i) 
            {
                ulong sum = _data[i]
                    + (i < otherLen ? other._data[i] : 0u)
                    + (carry ? 1u : 0u) ;
                carry = false;
                if(sum >= _numBase)
                {
                    sum -= _numBase;
                    carry = true;
                }
            }
            if(carry)
            {
                _data.Add(1u);
            }
            return this;
        }
        public BigNumber minus(BigNumber other)
        {
            if (other._numBase != _numBase)
                other = new BigNumber(other).sysConvertion(_numBase);


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
            for (uint i = 0; i < _data.Count; ++i) 
            {
                num += _data[(int)i] * pow(_numBase, i);
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
        private bool _negative;
    }
}
