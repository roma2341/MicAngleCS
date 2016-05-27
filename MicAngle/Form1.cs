using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MicAngle
{
    public partial class Form1 : Form
    {
        RecordForm recordForm;
        const double SOUND_EMITER_LISTEN_TIME = 20;
        MapForm mapForm;
        bool mapShowed = false;
        bool recordFormShowed = false;
       public bool resultIsPositiveRotation;
        public double resultAngle { get; set; }
        SignalsManager sm;
        int dataInputCounter=0;
        public SignalsManager getSignalManager()
        {
            return sm;
        }
        public Form1()
        {
            InitializeComponent();
            sm = new SignalsManager();
            mapForm = new MapForm(this,sm);
            recordForm = new RecordForm(this);

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
        public void processAngle(int[,] signalFromRealMicrophones)
        {
            if (dataInputCounter < 1) return;
            // SignalsManager sm = new SignalsManager();
            // sm.Mn.Add(new Microphone(10, -10));
            // sm.Mn.Add(new Microphone(10.47,-10));
            // sm.Sn.Add(new SoundEmiter(-100,100,1,44100));



            int[,] signalsArr;

            //for (int i = 0; i < signalsArr.Length;i++ )
            // signalsArr[i]=new int[signalValuesCount];
            // const int SIGNAL_VALUES_TO_OUTPUT = 20;
            int smnSizeMin = int.MaxValue;
            // Console.WriteLine("PROCESSING SIGNALS ON MICS...");
            if (signalFromRealMicrophones == null)
            {
                int signalValuesCount =
              sm.Sn[0].processEmiterArr(SOUND_EMITER_LISTEN_TIME, sm.SamplingRate, (int)SignalsManager.V);
                signalsArr = new int[sm.Mn.Count, signalValuesCount];
                for (int i = 0; i < sm.Mn.Count; i++)
                {
                    bool generationSuccess;
                    int[] arr = sm.Sn[0].generateSignal(sm.Mn[i], out generationSuccess,sm.SamplingRate);
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
                ResizeArray<int>(signalsArr, sm.Mn.Count, smnSizeMin);
            }
            else
            {
                signalsArr = signalFromRealMicrophones;
            }
            //Підганяємо масиви по масиву з мінімальною кількістю ел.

            //   Console.WriteLine("PROCESSING FINISHED.");
            const int NO_SHIFT_DATA_COUNT = 1; // +1 to total shift count, because wee need

            long[] maxesLeft = new long[sm.ShiftCount];
            long[] maxesRight = new long[sm.ShiftCount];
            bool success;
            int[] delayForFirstMicrophonePair = { 0, 1 };
            long leftShiftMaximumValue = 0, rightShiftMaximumValue = 0;
            double  rightShiftAngle = sm.interCorelationFunc(signalsArr, out success, delayForFirstMicrophonePair, true, out rightShiftMaximumValue, maxesRight);
            double  leftShiftAngle = sm.interCorelationFunc(signalsArr, out success, delayForFirstMicrophonePair, false, out leftShiftMaximumValue, maxesLeft);
            rightShiftMaximumValue = Math.Abs(rightShiftMaximumValue);
            leftShiftMaximumValue = Math.Abs(leftShiftMaximumValue);
            
            resultAngle = (rightShiftMaximumValue > leftShiftMaximumValue) ? rightShiftAngle : 360-leftShiftAngle;
            if (!success)
            {
               Console.WriteLine("Схоже що відстань між мікрофонами і джерелом звуку рівна нулю", "Помилка моделювання", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.chartMaximum.Series[0].Points.Clear();
            this.chartMaximum.Series[1].Points.Clear();
            Series series = this.chartMaximum.Series[0];//.Add("Intercorelation function\n value");
            Series seriesOfMax = this.chartMaximum.Series[1];//.Add("Intercorelation function\n value");
            int firstShiftIndex = 0;
            int lastShiftIndex = sm.ShiftCount-1;
            this.chartMaximum.ChartAreas[0].AxisX.Maximum = lastShiftIndex;
            this.chartMaximum.ChartAreas[0].AxisX.Minimum = -lastShiftIndex;
            this.chartMaximum.ChartAreas[0].AxisX.Interval = 1;
            series.Color = Color.Blue;
            long yMin = (maxesRight.Min() < maxesLeft.Min()) ? maxesRight.Min() : maxesLeft.Min();
            long yMax = (maxesRight.Max() > maxesLeft.Max()) ? maxesRight.Max() : maxesLeft.Max();
            long yMaxAbs = (Math.Abs(yMax) > Math.Abs(yMin)) ? yMax : yMin;
            for (int i = 1; i <= lastShiftIndex; i++)
            {
                long value = maxesLeft[i];
                Console.WriteLine(String.Format("x:{0} y:{1}", -i, value));
                if (Math.Abs(value) >= Math.Abs(yMaxAbs))
                    seriesOfMax.Points.AddXY(-i, value);
                else
                    series.Points.AddXY(-i, value);
                // Console.WriteLine("series max[ " + i + "]=" + maxes[i+ SHIFT_COUNT]);
            }

            for (int i = 0; i <= lastShiftIndex; i++)
            {
                long value = maxesRight[i];
                Console.WriteLine(String.Format("x:{0} y:{1}", i, value));
                if (Math.Abs(value) >= Math.Abs(yMaxAbs))
                    seriesOfMax.Points.AddXY(i, value);
                else
                    series.Points.AddXY(i, value);
                // Console.WriteLine("series max[ " + i + "]=" + maxes[i+ SHIFT_COUNT]);
            }

            this.chartMaximum.ChartAreas[0].AxisY.Minimum = yMin;
            this.chartMaximum.ChartAreas[0].AxisY.Maximum = yMax;
            lblResult.Text = "" + resultAngle;
            mapForm.processMap();
        }

        private void btnProcessAngle_Click(object sender, EventArgs e)
        {
            processAngle(recordForm.MicsSignal);
        }

        private void btnInputData_Click(object sender, EventArgs e)
        {
            if (rtbSettings.Text.Length < 1) return;
            sm.clear();
            sm.addSamplingRateFromString(rtbSettings.Text);
            sm.addSoundEmiterFromString(rtbSettings.Text);
            sm.addMicrophoneFromString(rtbSettings.Text);
            sm.getChannelsFromString(rtbSettings.Text);
            sm.getChannelsOffset(rtbSettings.Text);
            sm.getMicrophonesShift(rtbSettings.Text);
            sm.initMaxMicDelay();
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

        private void btnToggleRecordForm_Click(object sender, EventArgs e)
        {
            if (recordFormShowed)
            {
                btnToggleMap.Text = "Показати карту";
                recordForm.Hide();

            }
            else
            {
                btnToggleMap.Text = "Сховати карту";
                recordForm.Show();
            }
            recordFormShowed = !recordFormShowed;
        }
    }
}
