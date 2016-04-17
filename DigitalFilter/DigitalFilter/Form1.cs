using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AForge.Math;
using DigitalFilter.Exceptions;

namespace DigitalFilter
{
    public partial class Form1 : Form
    {
        FilterForm filterForm;
        const int sampleRate = 48000;
        const int timeMS = 10;
        const int AMPLITUDE = 128;
        const int DEFAULT_FREQUENCY = 1000;
        static double[] currentSignal;
        double[] filteredSignal;
        public void fillChartByValues<T, M>(Chart target,T[] x_axis,M[] y_axis)
        {
            target.Series.Clear();
            if (x_axis.Length != y_axis.Length)
                throw new Exception("count of elements in x_axis must be equal to y_axis");
            Series series = target.Series.Add("signal");
            series.BorderWidth = 4;
            series.ChartType = SeriesChartType.FastLine;
            for (int i=0;i<x_axis.Length;i++)
            series.Points.AddXY(x_axis[i],y_axis[i]);
        }
        public T[] getIntervalArray<T> (int from,int to) where T: IConvertible
        {
            int elementsCount = to - from;
            if (elementsCount <=0) throw new Exception("to <= from , error");
            T[] result = new T[elementsCount];
            for (int i = 0; i < elementsCount; i++)
            {
                result[i] = (T)Convert.ChangeType(i, typeof(T));
            }
            return result;
        }
        public Complex[] shortArrToComplex<T>(T[] arr) where T : IConvertible
        {
            return arr.Select(sample => new Complex((double)Convert.ChangeType(sample,typeof(double)), 0.0)).ToArray();
        }
        public T[] complexArrToNumber<T>(Complex[] arr) where T : IConvertible
        {
            double [] doubleArr = arr.Select(sample => sample.Re).ToArray();
             T[] result = new T[doubleArr.Length];
            for (int i = 0; i < doubleArr.Length; i++)
            {
                result[i] = (T)Convert.ChangeType(doubleArr[i], typeof(T));
            }
            return result;
        }
        public Complex[] normalizeComplexArr(Complex[] arr)
        {
            int POW_OF_TWO_COUNT = 15;
            int[] powOfTwoArr = new int[POW_OF_TWO_COUNT];
            int currentNum = 1;
            for (int i = 0; i < POW_OF_TWO_COUNT; i++)
            {
                powOfTwoArr[i] = currentNum;
                currentNum *= 2;
            }
            int lengthOfComplexArr = arr.Length;
            int newSize = 0;
            for (int i = 0; i < powOfTwoArr.Length;i++)
            {
                
                if (lengthOfComplexArr<= powOfTwoArr[i])
                { 
                    break;
                }
                newSize = powOfTwoArr[i];
            }
            if (lengthOfComplexArr == newSize) return arr;
            else
            {
                Complex[] newArr = new Complex[newSize];
                for (int i = 0; i < newSize; i++)
                {
                    newArr[i] = arr[i];
                }
                return newArr;
            }

        }
        public void showCurrentSignal()
        {
            double[] axisX = SignalManager.generateAxisX(sampleRate, timeMS);
            fillChartByValues<double, double>(signalChart, axisX, currentSignal);
            showSpecterOfSignal();
        }
        public void filterSignalAndShow(double[] signal,double[] filterKoffs, int filterOrder)
        {
            DigitalFilter myFilter = null;
            try {
                myFilter = new DigitalFilter(filterKoffs, filterOrder);
            }
            catch (FilterErrorStateException filterException)
            {
                MessageBox.Show("need more coefficients in terms of filter order");
                return;
            }
           
            filteredSignal = myFilter.filterSignal(currentSignal);
            double[] axisX = SignalManager.generateAxisX(sampleRate, timeMS);
            fillChartByValues<double, double>(signalChart, axisX, filteredSignal);
            showSpecterOfSignal();
        }
        public void filterSignalAndShow(double[] filterKoffs,int filterOrder)
        {
            filterSignalAndShow(currentSignal, filterKoffs, filterOrder);
        }
        public void assignFilteredSignalToCurrent()
        {
            currentSignal = filteredSignal;
        }
        public void showSpecterOfSignal()
        {
            Complex[] complexSinArr = shortArrToComplex(currentSignal);
            complexSinArr = normalizeComplexArr(complexSinArr);
            FourierTransform.FFT(complexSinArr, FourierTransform.Direction.Forward);
            short[] fftResult = complexArrToNumber<short>(complexSinArr);
            double[] axisX2 = getIntervalArray<double>(0, fftResult.Length);
            fillChartByValues<double, short>(processedSignalChart, axisX2, fftResult);
        }
        public Form1()
        {
            InitializeComponent();
            resetSignal();
            filterForm = new FilterForm(this);
            filterForm.Show(this);

            showCurrentSignal();

            //fillChartByValues<double, short>();
        }
        public void resetSignal()
        {
            currentSignal = SignalManager.generateSinWave(sampleRate, AMPLITUDE, DEFAULT_FREQUENCY, timeMS);
        }
    }
}
