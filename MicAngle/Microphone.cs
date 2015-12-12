using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicAngle
{
    class Microphone
    {
        public Microphone(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
       public double x{get; set;}
       public double y{ get; set;}
    }
 
}
