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
        const double SOUND_EMITER_LISTEN_TIME = 5;
        MapForm mapForm;
        bool mapShowed = false;
        bool recordFormShowed = false;
        public double resultAngle { get; set; }
        SignalsManager sm;
        long[,] correlationDetailsPositive;
        long[,] correlationDetailsNegative;
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
            initSignalManager();

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
            bool success = true;
            CorrelationStatistic[] microphoneProcessingStatistic = sm.getMaxCrossCorrelationFromMicSignals(signalsArr,out success, sm.MicrophonesDelay,out correlationDetailsNegative, out correlationDetailsPositive);
            Console.WriteLine("Processing statistic:");
            double[] angles = correlationStatisticToAngles(microphoneProcessingStatistic);
            double[] realAngles = sm.getRealAngles();
            for (int i = 0; i < microphoneProcessingStatistic.Length; i++)
            {
                CorrelationStatistic statistic = microphoneProcessingStatistic[i];
                Console.WriteLine(statistic.ToString());
                Console.WriteLine("angle:" + angles[i] + " real angle:"+ realAngles[i]);
            }
            displayCorrelationStatistic();

            //sm.getAngleFromDelay()

            /*
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
                        for (int i = lastShiftIndex; i >= 0; i--)
                        {
                            long value = maxesLeft[i];
                            if (Math.Abs(value) >= Math.Abs(yMaxAbs))
                                seriesOfMax.Points.AddXY(-i, value);
                            else
                                series.Points.AddXY( -i, value);
                            // Console.WriteLine("series max[ " + i + "]=" + maxes[i+ SHIFT_COUNT]);
                        }


                        this.chartMaximum.ChartAreas[0].AxisY.Minimum = yMin;
                        this.chartMaximum.ChartAreas[0].AxisY.Maximum = yMax;
                        lblResult.Text = "" + resultAngle;
                        */
                        mapForm.processMap(angles);
        }
        public double[] correlationStatisticToAngles(CorrelationStatistic[] statisticArr)
        {
            double[] angles = new double[statisticArr.Length];
            for (int i = 0; i < statisticArr.Length; i++)
            {
                angles[i] = sm.getAngleFromDelay(statisticArr[i].maxShift, statisticArr[i].micsDistance);
            }
            return angles;
        }

        private void btnProcessAngle_Click(object sender, EventArgs e)
        {
            processAngle(recordForm.MicsSignal);
        }
        private void displayCorrelationStatistic()
        {
            string serieNameTemplate = "Мікрофони0-{0}";
            chartMaximum.Series.Clear();
            long[] maximumsValue = new long[correlationDetailsPositive.GetLength(0)];
            long[] maximumsIndex = new long[correlationDetailsPositive.GetLength(0)];
            for (int i = 1; i < correlationDetailsPositive.GetLength(0); i++)
            {
                Series currentSerie = (new Series(String.Format(serieNameTemplate, i)));
                chartMaximum.Series.Add(currentSerie);
                currentSerie.ChartType = SeriesChartType.Line;
                for (int j = correlationDetailsNegative.GetLength(1)-1; j >= 0 ; j--)
                {                   
                    currentSerie.Points.AddXY(-j, correlationDetailsNegative[i, j]);
                    if (correlationDetailsNegative[i, j]> maximumsValue[i])
                    {
                        maximumsValue[i] = correlationDetailsNegative[i,j];
                        maximumsIndex[i] = -j;
                    }
                }
                for (int j = 1; j < correlationDetailsPositive.GetLength(1); j++)
                {
                    currentSerie.Points.AddXY(j, correlationDetailsPositive[i, j]);
                    if (correlationDetailsPositive[i, j] > maximumsValue[i])
                    {
                        maximumsValue[i] = correlationDetailsPositive[i, j];
                        maximumsIndex[i] = j;
                    }
                }
            }
            Series seriesOfMaximum = new Series("Максимуми");
            seriesOfMaximum.ChartType = SeriesChartType.Point;
            for (int i = 0; i < maximumsValue.Length; i++)
            {
                DataPoint dataPoint = new DataPoint(maximumsIndex[i], maximumsValue[i]);
                dataPoint.Label = ""+maximumsValue[i];
            seriesOfMaximum.Points.Add(dataPoint);

            }
            chartMaximum.Series.Add(seriesOfMaximum);

        }
        private void initSignalManager()
        {
            if (rtbSettings.Text.Length < 1) return;
            sm.clear();
            sm.addSamplingRateFromString(rtbSettings.Text);
            sm.addSoundEmiterFromString(rtbSettings.Text);
            sm.addMicrophoneFromString(rtbSettings.Text);
            sm.getChannelsFromString(rtbSettings.Text);
            sm.getChannelsOffset(rtbSettings.Text);
            sm.getMicrophonesShift(rtbSettings.Text);
            sm.getMicrophonesDelays(rtbSettings.Text);
            dataInputCounter++;
        }

        private void btnInputData_Click(object sender, EventArgs e)
        {
            initSignalManager();
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
