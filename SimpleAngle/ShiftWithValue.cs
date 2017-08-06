using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAngle
{
    class ShiftWithValue
    {
        public ShiftWithValue(int shift, long value)
        {
            this.Shift = shift;
            this.Value = value;
        }
        public int Shift { get; set; }
        public long Value { get; set; }
    }
}
