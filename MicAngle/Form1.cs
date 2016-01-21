using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MicAngle
{
    public partial class Form1 : Form
    {
        MapForm mapForm;
        bool mapShowed = false;
       public double resultAngle { get; set; }
        SignalsManager sm;
        int dataInputCounter=0;
        public Form1()
        {
            InitializeComponent();
            sm = new SignalsManager();
            mapForm = new MapForm(this,sm);

        }
        T[,] ResizeArray<T>(T[,] original, int rows, int cols)
        {
            var newArray = new T[rows, cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    newArray[i, j] = original[i, j];
            return newArray;
        }

        private void btnProcessAngle_Click(object sender, EventArgs e)
        {
            if (dataInputCounter < 1) return;
          // SignalsManager sm = new SignalsManager();
           // sm.Mn.Add(new Microphone(10, -10));
           // sm.Mn.Add(new Microphone(10.47,-10));
           // sm.Sn.Add(new SoundEmiter(-100,100,1,44100));
            int signalValuesCount =
                sm.Sn[0].processEmiterArr(1, sm.Sn[0].samplingRate, (int)SignalsManager.V);
           
        
           int[,] signalsArr = new int[sm.Mn.Count, signalValuesCount];

            //for (int i = 0; i < signalsArr.Length;i++ )
            // signalsArr[i]=new int[signalValuesCount];
           // const int SIGNAL_VALUES_TO_OUTPUT = 20;
            int smnSizeMin = int.MaxValue;
           // Console.WriteLine("PROCESSING SIGNALS ON MICS...");
            for (int i = 0; i < sm.Mn.Count; i++)
               {
                bool generationSuccess;
                   int[] arr = sm.Sn[0].generateSignal(sm.Mn[i], out generationSuccess);
                if (!generationSuccess)
                {
                    MessageBox.Show("Помилкові данні, не вдалось згенерувати масив звуку", "Помилка моделювання", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (arr.Length < smnSizeMin) smnSizeMin = arr.Length;
                
                for (int j = 0; j < arr.Length; j++)
                {
                    signalsArr[i, j] = arr[j];
                  //  if (j< SIGNAL_VALUES_TO_OUTPUT)
                  //  Console.Write(arr[j]+" ");
                }
               // Console.WriteLine();
            }
            //Підганяємо масиви по масиву з мінімальною кількістю ел.
            ResizeArray<int>(signalsArr, sm.Mn.Count, smnSizeMin);
         //   Console.WriteLine("PROCESSING FINISHED.");

            long[] maxes = new long[SignalsManager.SHIFT_COUNT*2];//*2 because need contains left and right shift
            bool success;
            resultAngle =  sm.interCorelationFunc(signalsArr, out success, maxes);
            if (!success)
            {
                MessageBox.Show("Схоже що відстань між мікрофонами і джерелом звуку рівна нулю","Помилка моделювання", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.chartMaximum.Series[0].Points.Clear();
            this.chartMaximum.Series[1].Points.Clear();
            Series series = this.chartMaximum.Series[0];//.Add("Intercorelation function\n value");
            Series seriesOfMax = this.chartMaximum.Series[1];//.Add("Intercorelation function\n value");
            this.chartMaximum.ChartAreas[0].AxisX.Maximum = SignalsManager.SHIFT_COUNT;
            this.chartMaximum.ChartAreas[0].AxisX.Minimum = -SignalsManager.SHIFT_COUNT;
            this.chartMaximum.ChartAreas[0].AxisX.Interval = 1;
            long yMin = maxes.Min();
            long yMax = maxes.Max();
            for (int i = -SignalsManager.SHIFT_COUNT; i < SignalsManager.SHIFT_COUNT; i++)
            {
                long value = maxes[i + SignalsManager.SHIFT_COUNT];
                if (value==yMax || value==yMin)
                    seriesOfMax.Points.AddXY(i, value);
                else
                series.Points.AddXY(i, value);
               // Console.WriteLine("series max[ " + i + "]=" + maxes[i+ SHIFT_COUNT]);
            }
            this.chartMaximum.ChartAreas[0].AxisY.Minimum = yMin;
            this.chartMaximum.ChartAreas[0].AxisY.Maximum = yMax;
            lblResult.Text = ""+ resultAngle;
            mapForm.processMap();
        }

        private void btnInputData_Click(object sender, EventArgs e)
        {
            if (rtbSettings.Text.Length < 1) return;
            sm.clear();
            sm.addSoundEmiterFromString(rtbSettings.Text);
            sm.addMicrophoneFromString(rtbSettings.Text);
            dataInputCounter++;
        }

        private void btnToggleMap_Click(object sender, EventArgs e)
        {
            if (mapShowed)
            {
                btnToggleMap.Text = "Показати карту";
                mapForm.Hide();
               
            }
            else
            {
                btnToggleMap.Text = "Сховати карту";
                mapForm.Show();
            }
            mapShowed = !mapShowed;
        }

        private void rtbSettings_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
