using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAngle
{
    public class SignalsOperations
    {
        public static int processMaxShiftsCount(double micDistanceDelta, int samplingRate)
        {
            const int SOUND_SPEED = 340;
            return (int)Math.Ceiling(micDistanceDelta * samplingRate / SOUND_SPEED);
        }
    }
}
