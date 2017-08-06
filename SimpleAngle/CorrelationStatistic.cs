using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAngle
{
    public struct CorrelationStatistic
    {
        public int maxShift;
        public long maxValue;
        public double micsDistance;
        public CorrelationStatistic(int shift,long value, double distance)
        {
            this.maxShift = shift;
            this.maxValue = value;
            this.micsDistance = distance;
        }
        public override string ToString()
        {
            return (String.Format("(SHIFT:{0},VALUE:{1},DISTANCE:{2})", maxShift, maxValue, micsDistance));
        }
    }
}
