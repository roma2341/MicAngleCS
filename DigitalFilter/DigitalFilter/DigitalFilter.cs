using DigitalFilter.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFilter
{
    class DigitalFilter
    {
        int filterOrder = -1;
        int filterKoffCount = 0; // I don't know why +1, but i see it in Matlab.
        double[] filterKoff = {  };

        public DigitalFilter(double[] filterKoff,int filterOrder)
        {
            if (filterKoff.Length <= filterOrder  ) throw new FilterErrorStateException("need more coefficients");
            this.filterKoff = filterKoff;
            this.filterOrder = filterOrder;
            this.filterKoffCount = filterOrder + 1;

        }
        public double[] filterSignal(double[] inputSignal)
        {
            double[] result = new double[inputSignal.Length];
            for(int i = 0; i < inputSignal.Length; i++)
            {
                int startFilteringIndex = i;
                int endFilteringIndex = startFilteringIndex + filterKoffCount - 1;

                //skip summing signal values with filterKoff
                //if overflow size of signal array
                if (endFilteringIndex >= inputSignal.Length) endFilteringIndex = inputSignal.Length - 1;

                for (int j = startFilteringIndex; j <= endFilteringIndex; j++)
                {
                    int relativeFilterIndex = j - i;
                    result[i] += inputSignal[j] * filterKoff[relativeFilterIndex];
                }
            }
            return result;
        }
        static double[] processCoffs(int f0,int Fs, int Q, int coffsNumber)
        {
            double w0 = 2 * Math.PI * f0 / Fs;
            double alpha = Math.Sin(w0) / (2 * Q);
            double b0 = 1 - alpha;
            double b1 = -2 * Math.Cos(w0);
            double b2 = 1 + alpha;
            double a0 = 1 + alpha;
            double a1 = -2 * Math.Cos(w0);
            double a2 = 1 - alpha;
            double[] filterCoffs = new double[coffsNumber];
            for (int i = 0; i < filterCoffs.Length; i++)
            {
             //   filterCoffs[i]=
            }
            return filterCoffs;
        }

    }

}
