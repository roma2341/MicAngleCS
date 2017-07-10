using MicAngle;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using SimpleAngle.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleAngle
{
    public partial class MainForm : Form
    {
        const int SAMPLING_RATE = 44100;
        const int CHANNELS = 2;
        const int MICROPHONES_COUNT = 4;
        const double MICROPHONE_DISTANCE = 0.5; //Meters
        const int CONJUNCTED_CHANNELS_1 = 1;
        const int CONJUNCTED_CHANNELS_2 = 2;
        const bool CONJUNTION_ENABLED = true;
        const int ALIGN_MIC_DATA_SHIFTS_MAX = 130;
        const int V = 340;

        DataCorrelation correlationer;
        SoundConfig soundConfig;

        Timer asioTimer = new Timer();


        bool isRecording = false;
        List<float[]> soundData = null;

        NAudio.Wave.AsioOut recAsio;

        List<CheckBox> signalChartCheckBoxes;
        List<CheckBox> correlationChartCheckBoxes;
        bool asioRunnerActive = false;
        public void HandleAsioRunner(Object myObject,
                                            EventArgs myEventArgs)
        {

            asioRunnerActive = true;
            BeginRecording();
        }

        public MainForm()
        {
            InitializeComponent();
            initDynamicCheckBoxes();

            asioTimer.Tick += HandleAsioRunner;
            asioTimer.Interval = 3000;

            soundConfig = new Models.SoundConfig();
            soundConfig.SamplingRate = SAMPLING_RATE;
            soundConfig.V = V;
            soundConfig.DistanceBetweenMicrophones = MICROPHONE_DISTANCE;
            correlationer = new DataCorrelation(soundConfig);

            string[] asioDevices = AsioOut.GetDriverNames();
            foreach (string devName in asioDevices)
            {
                comboAsioDevice.Items.Add(devName);
            }

            comboAsioDevice.SelectedIndex = 0;
        }


        /*CONTROL VISIBILITY OF SERIES*/

        private void updateSignalSeriesVisibility()
        {
            for (var i = 0; i < MICROPHONES_COUNT; i++)
            {
                signalChart.Series[i].Enabled = signalChartCheckBoxes[i].Checked;
            }
        }

        private void updateCorrelationSeriesVisibility()
        {
            for (var i = 0; i < MICROPHONES_COUNT; i++)
            {
                correlationChart.Series[i].Enabled = correlationChartCheckBoxes[i].Checked;
            }
        }


        private void signalCheckBoxClickEventHandler(object sender, EventArgs e)
        {
            updateSignalSeriesVisibility();
        }

        private void correlationCheckBoxClickEventHandler(object sender, EventArgs e)
        {
            updateCorrelationSeriesVisibility();
        }

        private void initDynamicCheckBoxes()
        {
            signalChartCheckBoxes = new List<CheckBox>();
            correlationChartCheckBoxes = new List<CheckBox>();

            for (var i = 0; i < MICROPHONES_COUNT; i++)
            {

                CheckBox cb = prepareCheckbox(i);
                cb.CheckedChanged += signalCheckBoxClickEventHandler;

                signalChartCheckBoxes.Add(cb);
                signalChartGroupBox.Controls.Add(cb);
            }
            for (var i = 0; i < MICROPHONES_COUNT; i++)
            {
                CheckBox cb = prepareCheckbox(i);
                cb.CheckedChanged += correlationCheckBoxClickEventHandler;

                correlationChartCheckBoxes.Add(cb);
                correlationChartGroupBox.Controls.Add(cb);
            }
        }

        private CheckBox prepareCheckbox(int index)
        {
            int micNumber = index + 1;
            String name = "Mic" + micNumber;
            CheckBox cb = new CheckBox();
            cb.Checked = true;
            cb.Text = name;
            cb.Name = name;
            cb.Location = new System.Drawing.Point(10, index * cb.Height);
            return cb;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (asioRunnerActive)
            {
                asioTimer.Stop();
                asioRunnerActive = false;
            }
            else {
                asioTimer.Start();
                asioRunnerActive = true;
            }
          //  BeginRecording();
        }



        public void toggleRecordButton()
        {
            isRecording = !isRecording;
        }

        void StopRecording()
        {
            //MessageBox.Show("StopRecording");
            recAsio.Stop();

        }
        
        public void drawAngles(double angle1, double  angle2)
        {

            int contextRotation = int.Parse(tbAngle.Text);
            float scaling = float.Parse(textBoxScaling.Text);


     Bitmap MyImage = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = MyImage;

            Graphics g;
            g = Graphics.FromImage(MyImage);
            g.TranslateTransform(pictureBox1.Size.Width / 2, pictureBox1.Size.Height / 2);
            g.ScaleTransform(scaling, scaling);
            g.RotateTransform(contextRotation);
           

            Pen mypen = new Pen(Brushes.Black);
            g.Clear(Color.White);
            MicAngle.Point center = new MicAngle.Point(0, 0);
            MicAngle.Point point1 = new MicAngle.Point(-100, 0);
            MicAngle.Point point2 = new MicAngle.Point(100, 0);
            MicAngle.Point rotated1 = MyUtils.rotate(center, point1, angle1);
            MicAngle.Point rotated2 = MyUtils.rotate(center, point2, 360 - angle2);



            Pen pen1 = new Pen(Color.Red);

            g.DrawLine(mypen, (float)center.X, (float)center.Y, (float)point1.X, (float)point1.Y);
            g.DrawLine(mypen, (float)center.X, (float)center.Y, (float)point2.X, (float)point2.Y);

            MicAngle.Point prolongedRotated1 = MyUtils.CalculatePointWithDistance(point1, rotated1, 1000);
            MicAngle.Point prolongedRotated2 = MyUtils.CalculatePointWithDistance(point2, rotated2, 1000);

            float rot1X = (float)prolongedRotated1.X;
            float rot1Y = (float)prolongedRotated1.Y;
            float rot2X = (float)prolongedRotated2.X;
            float rot2Y = (float)prolongedRotated2.Y;

            g.DrawLine(pen1, (float)point1.X, (float)point1.Y, rot1X, rot1Y);
            g.DrawLine(pen1, (float)point2.X, (float)point2.Y, rot2X, rot2Y);



            g.Dispose();
        }

        public void processSoundData(int[,] data)
        {
            data =  DataCorrelation.alignAndCombineSignalData(data, CONJUNCTED_CHANNELS_1, CONJUNCTED_CHANNELS_2, ALIGN_MIC_DATA_SHIFTS_MAX);
            int maxShiftCount = SignalsOperations.processMaxShiftsCount(MICROPHONE_DISTANCE, SAMPLING_RATE, V);
           int shift01 = correlationer.getShiftBetweenMicrophones(data, 0, 1, maxShiftCount);
            int shift12 = correlationer.getShiftBetweenMicrophones(data, 0, 1, maxShiftCount);

            double angle01 = SignalsOperations.getAngleFromDelay(soundConfig, shift01);
            double angle12 = SignalsOperations.getAngleFromDelay(soundConfig, shift12);

            drawAngles(angle01, angle12);

            Console.WriteLine("angle01:" + angle01);
            Console.WriteLine("angle12:" + angle12);

            //clear chatrts part
            for (int i = 0; i < data.GetLength(0); i++)
            {
                signalChart.Series[i].Points.Clear();
            }

                correlationChart.Series[0].Points.Clear();

            //fill data part
            for (int i = 0; i < data.GetLength(0); i++)
            {
                // Console.WriteLine("i:" + i);
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    // Console.WriteLine("j:" + j);
                    signalChart.Series[i].Points.AddXY(j, data[i, j]);
                }
            }

            int maxShift = SignalsOperations.processMaxShiftsCount(MICROPHONE_DISTANCE, SAMPLING_RATE,V);
            /*int[] shiftsCorrelation = DataCorrelation.generateCorrelationArray(data, new CorrelationConfig(maxShift, maxShift, false));
            int[] backwardShiftCorrelation = DataCorrelation.generateCorrelationArray(data, new CorrelationConfig(maxShift, maxShift, true));

            for (int i = 0; i < backwardShiftCorrelation.Length; i++)
            {
                // Console.WriteLine("j:" + j);
                int index = backwardShiftCorrelation.Length - i - 1;
                correlationChart.Series[0].Points.AddXY(-index, backwardShiftCorrelation[i]);
            }

            for (int i = 0; i < shiftsCorrelation.Length; i++)
            {
                // Console.WriteLine("j:" + j);
                correlationChart.Series[0].Points.AddXY(i, shiftsCorrelation[i]);
            }*/

        }
        int recordedIterations = 0;
        void waveIn_DataAvailableAsioTestA(object sender, AsioAudioAvailableEventArgs e)
        {
           
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<AsioAudioAvailableEventArgs>(waveIn_DataAvailableAsioTestA), sender, e);
                return;
            }
            Console.WriteLine("waveIn_DataAvailableAsioTestA begin");
            int samplesCount = e.SamplesPerBuffer * MICROPHONES_COUNT;
            float[] interlivedAsioSamples = new float[samplesCount];
            e.GetAsInterleavedSamples(interlivedAsioSamples);
            if(recordedIterations>0)
            soundData.Add(interlivedAsioSamples);

           
           // e.WrittenToOutputBuffers = true;
           if(recordedIterations > 2)
            StopRecording();
            /*StringBuilder builder = new StringBuilder("");
            for (var i = 0; i < interlivedAsioSamples.Length; i++)
            {
                builder.Append(" " + interlivedAsioSamples[i]);
            }
            Console.WriteLine(builder.ToString());*/
            recordedIterations++;
            Console.WriteLine("waveIn_DataAvailableAsioTestA end");

        }


        public void BeginRecording()
        {
            if (!isRecording)
            {
                int deviceIdAsio = comboAsioDevice.SelectedIndex; 

                soundData = new List<float[]>();
                if (recAsio==null) { 
                recAsio = new NAudio.Wave.AsioOut(deviceIdAsio);
                // buffer = new NAudio.Wave.BufferedWaveProvider(formato);
                // buffer.DiscardOnBufferOverflow = true;
                recAsio.AudioAvailable += new EventHandler<NAudio.Wave.AsioAudioAvailableEventArgs>(waveIn_DataAvailableAsioTestA);
                recAsio.InitRecordAndPlayback(null, MICROPHONES_COUNT, SAMPLING_RATE); //rec channel = 1                                                                                       // recAsio2.InitRecordAndPlayback(null, angleForm.getSignalManager().Mn.Count, SAMPLING_RATE); //rec channel = 1
                recAsio.PlaybackStopped += new EventHandler<StoppedEventArgs>(asio_RecordingStoppedA);
                }

                recAsio.Play();
            }
            else
            {
                toggleRecordButton();
                StopRecording();
            }
        }

        /*public void aggregatedDataTomicSignal()
        {

            int[,] result = aggregatedArraysToSeparated(aggregatedAsio, CHANNELS*MICROPHONES_COUNT);
            if (CONJUNTION_ENABLED)
                result = DataCorrelation.alignAndCombineSignalData(result, CONJUNCTED_CHANNELS_1, CONJUNCTED_CHANNELS_1, MAX_SHIFT_COUNT);
        }*/

        private int[,] aggregatedArraysToSeparated(List<float[]> arrs, int channels)
        {
            int totalArrayElmCount = 0;
            foreach (float[] a in arrs) totalArrayElmCount += a.Length;

            float[] source = new float[totalArrayElmCount];
            int[,] result = new int[channels, totalArrayElmCount / channels];
            int index = 0;
            foreach (float[] singleArr in arrs)
            {
                foreach (float val in singleArr)
                {
                    source[index++] = val;
                }
            }
            index = 0;
            for (int i = 0; i < source.Length; i += channels)
            {
                for (int j = 0; j < channels; j++)
                {
                    result[j, index] = (int)(source[i + j] * short.MaxValue);
                }
                // result[0, index] = ((int)(source[i] * Int32.MaxValue));
                // result[1, index] = ((int)(source[i] * Int32.MaxValue));
                index++;
            }
            return result;

        }

        private void asio_RecordingStoppedA(object sender, EventArgs e)
        {
            recordedIterations = 0;
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(asio_RecordingStoppedA), sender, e);
                return;
            }
            else
            {
                int[,] arr = aggregatedArraysToSeparated(soundData, MICROPHONES_COUNT);
               

                processSoundData(arr);
                //recAsio.Dispose();
                //recAsio = null;
               // recAsio.Dispose();
               // recAsio = null;
                //micsSignal = unitePartialMeasurement(signalFromMicrophonesA);
                // int[,] result = processMultipleChannels(AsioData,BYTES_PER_SAMPLE,true);
                // int[,] result= angleForm.getSignalManager().generateTestMicSignal(2900,4,2,15);
               // aggregatedDataTomicSignal();
                // Console.WriteLine("Arrays aggregated( height:" + result.GetLength(0) + "width: " + result.GetLength(1));
                //aggregatedAsio
               // int LIMIT = 100000;
               // rtbSignal.Text = MyUtils.arrayToString(MicsSignal, 100000);
                //rtbSignal.Text = arrayToString(micsSignal, LIMIT);
                //angleForm.processAngle(micsSignal);
                //  bufferedWaveProvider.ClearBuffer();
            }
        }

        private void btnAsioControlPanel_Click(object sender, EventArgs e)
        {
            if (recAsio!=null)
            recAsio.ShowControlPanel();
        }
    }
}
