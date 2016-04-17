using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFilter
{
    class SignalManager
    {
       public static double[] generateSinWave(int sampleRate,double amplitude, double frequency, int timeMS)
        {
            int samplesCountInTimeInterval = sampleRate * timeMS / 1000;
            double[] buffer = new double[samplesCountInTimeInterval];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (short)(amplitude * Math.Sin((2 * Math.PI * i * frequency) / sampleRate));
            }
            return buffer;

        }
        public static double[] generateAxisX(int sampleRate, int timeMS)
        {
            int samplesCount = sampleRate * timeMS / 1000;
            double[] x_axis = new double[samplesCount];          
            double currentX = 0;
            for (int i = 0; i < samplesCount; i++)
            {
                x_axis[i] = currentX;
                currentX += 1 / sampleRate;
            }
            return x_axis;
        }
    }
}
