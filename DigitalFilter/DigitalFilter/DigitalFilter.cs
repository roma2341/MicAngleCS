using DigitalFilter.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFilter
{
   public class DigitalFilter
    {
        public enum FilterMode { Forward, Backward,AllPass };
        int filterOrder = -1;
        int filterKoffCount = 0; // I don't know why +1, but i see it in Matlab.
        double[] filterKoff = {  };
        public NumberFormatInfo CurrentNumberFormat { get; set; }

        public DigitalFilter(NumberFormatInfo numberFormat,double[] filterKoff,int filterOrder)
        {
            CurrentNumberFormat = numberFormat;
            if (filterKoff.Length <= filterOrder  ) throw new FilterErrorStateException("need more coefficients");
            this.filterKoff = filterKoff;
            this.filterOrder = filterOrder;
            this.filterKoffCount = filterOrder + 1;

        }
        public DigitalFilter(NumberFormatInfo numberFormat, double[] filterKoff) : this(numberFormat,filterKoff, filterKoff.Length-1)
        {
       

        }
        public DigitalFilter()
        {


        }

        public double[] filterSignalAllPass(double[] inputSignal)
        {
            double[] result = new double[inputSignal.Length];
            for (int i = 0; i < inputSignal.Length; i++)
            {
                //skip summing signal values with filterKoff
                //if overflow size of signal array
                for (int j = 0; j < filterKoffCount; j++)
                {
                    int positionInInput = i - j - 1;
                    if (positionInInput < 0) continue;
                    double C = filterKoff[j];
                    if (j == 0)
                        result[i] = inputSignal[i] * C;
                    result[i] += inputSignal[positionInInput];
                    result[i] -= result[positionInInput] * C;
                }
            }
            return result;
        }

        public double[] filterSignalForward(double[] inputSignal)
        {
            double[] result = new double[inputSignal.Length];
            for (int i = 0; i < inputSignal.Length; i++)
            {
                //skip summing signal values with filterKoff
                //if overflow size of signal array
                for (int j = 0; j < filterKoffCount; j++)
                {
                    int positionInInput = i + j + 1;
                    if (positionInInput >= inputSignal.Length) continue;
                    result[i] += inputSignal[positionInInput] * filterKoff[j];
                }
            }
            return result;
        }
        public double[] filterSignalBackward(double[] inputSignal)
        {
            double[] result = new double[inputSignal.Length];
            for (int i = 0; i < inputSignal.Length; i++)
            {
                //skip summing signal values with filterKoff
                //if overflow size of signal array
                for (int j = 0; j < filterKoffCount; j++)
                {
                    int positionInInput = i - j - 1;
                    if (positionInInput < 0) continue;
                    result[i] += inputSignal[positionInInput] * filterKoff[j];
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
        public string getCofsStr()
        {
            string result = "";
            for (int i = 0; i < filterKoff.Length; i++)
            {
                if (i > 0) result += " ";
                result += filterKoff[i].ToString(CurrentNumberFormat);
            }
            return result;
        }
        override public string ToString()
        {
            return string.Format("{0}", this.getCofsStr());
        }
        public int getFilterOrder()
        {
            return filterOrder;
        }
        public void setFilterOrder(int filterOrder)
        {
            this.filterOrder = filterOrder;
        }
        public double[] getFilterKoff()
        {
            return filterKoff;
        }
        public void setFilterKoff(double[] filterKoff)
        {
            this.filterKoff = filterKoff;
        }

    }

}
