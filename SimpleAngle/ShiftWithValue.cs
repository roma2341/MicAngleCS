using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAngle
{
    class ShiftWithValue
    {
        public ShiftWithValue(int shift, long value,bool positive = true)
        {
            this.Shift = shift;
            this.FunctionResult = value;
            this.Positive = positive;
        }
        public int Shift { get; set; }
        public long FunctionResult { get; set; }
        public bool Positive { get; set; }
    }
}
