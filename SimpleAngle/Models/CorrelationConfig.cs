using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAngle.Models
{
   public struct CorrelationConfig
    {
        int shiftsCount;
        int clampedElements;
        bool inverse;

    public int ShiftsCount
        {
            get
            {
                return shiftsCount;
            }

            set
            {
                shiftsCount = value;
            }
        }

        public int ClampedElements
        {
            get
            {
                return clampedElements;
            }

            set
            {
                clampedElements = value;
            }
        }

        public bool Inverse
        {
            get
            {
                return inverse;
            }

            set
            {
                inverse = value;
            }
        }

        public CorrelationConfig(int shiftsCount = 0, int clampedElements = 0, bool inverse = false)
        {
            this.shiftsCount = shiftsCount;
            this.clampedElements = clampedElements;
            this.inverse = inverse;
        }
    }
}
