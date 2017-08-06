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
        const double MICROPHONE_DISTANCE = 1; //Meters
        const int CONJUNCTED_CHANNELS_1 = 1;
        const int CONJUNCTED_CHANNELS_2 = 2;
        const bool CONJUNTION_ENABLED = true;
        const int ALIGN_MIC_DATA_SHIFTS_MAX = 130;
        const int V = 340;
        const int SOUND_EMITER_LISTEN_TIME = 150;
        const int CENTRAL_MIRCOPHONE_INDEX = 1;

        SignalManager signalManager = new SignalManager(SAMPLING_RATE);

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
        
        public void drawAngles(List<double> angles,int mainMicrophoneIndex,bool[] relativityToCenter)
        {
            const int POINT_SIZE = 15;
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
          //  System.Windows.Point center = new System.Windows.Point(0, 0);
         //   System.Windows.Point point1 = new System.Windows.Point(-100, 0);
          //  System.Windows.Point point2 = new System.Windows.Point(100, 0);

           // Pen penMicro = new Pen(Color.Red);
            Pen penSound = new Pen(Color.Blue);
            Pen intersectionPencil = new Pen(Color.DarkViolet);
            Pen[] microphonePencils = new Pen[3];
            microphonePencils[0] = new Pen(Color.Red);
            microphonePencils[1] = new Pen(Color.Green);
            microphonePencils[2] = new Pen(Color.Blue);


            System.Windows.Point soundPoint = signalManager.Sn[0].Position;
            

            g.DrawEllipse(penSound, (float)soundPoint.X- POINT_SIZE/2, 
                (float)soundPoint.Y- POINT_SIZE/2, POINT_SIZE, POINT_SIZE);
        
            for (var i = 0; i < signalManager.Mn.Count; i++)
            {
                g.DrawEllipse(microphonePencils[i], (float)signalManager.Mn[i].X- POINT_SIZE/2, 
                    (float)signalManager.Mn[i].Y- POINT_SIZE/2, POINT_SIZE, POINT_SIZE);
            }
            List<System.Windows.Point> mnPoints = new List<System.Windows.Point>();
            List<System.Windows.Point> rotatedPoints = new List<System.Windows.Point>();

            System.Windows.Point centerPoint = signalManager.Mn[mainMicrophoneIndex].Position;
            int currentAngle = 0;
            for (int i = 0; i < signalManager.Mn.Count; i++)
            {
                if (i == mainMicrophoneIndex) continue;
                System.Windows.Point pointToDraw = signalManager.Mn[i].Position;
                System.Windows.Point rotatedPoint =new System.Windows.Point();
                if (relativityToCenter[currentAngle])
                    rotatedPoint = MyUtils.rotate(centerPoint, pointToDraw, angles[currentAngle]);
                else
                    rotatedPoint = MyUtils.rotate(pointToDraw,centerPoint, angles[currentAngle]);

                mnPoints.Add(pointToDraw);
                rotatedPoints.Add(rotatedPoint);

                g.DrawLine(mypen, (float)centerPoint.X, (float)centerPoint.Y,
                    (float)pointToDraw.X, (float)pointToDraw.Y);
                System.Windows.Point prolongedRotated1 = MyUtils.CalculatePointWithDistance(
                    signalManager.Mn[i].Position, rotatedPoint, 500);

                float rotatedX = (float)prolongedRotated1.X;
                float rotatedY = (float)prolongedRotated1.Y;

                g.DrawLine(microphonePencils[i], (float)pointToDraw.X, (float)pointToDraw.Y, rotatedX, rotatedY);
                currentAngle++;
            }

            System.Windows.Point intersectionPoint = GlobalMercator.Intersection(mnPoints[0], 
                rotatedPoints[0], mnPoints[1], rotatedPoints[1]);
            if(!Double.IsNaN(intersectionPoint.X) && !Double.IsNaN(intersectionPoint.Y )) { 
            g.DrawEllipse(intersectionPencil, (float)intersectionPoint.X - POINT_SIZE / 2,
                   (float)intersectionPoint.Y - POINT_SIZE / 2, POINT_SIZE, POINT_SIZE);
            }

            // System.Windows.Point rotated2 = MyUtils.rotate(center, point2, 360 - angle2);

            g.Dispose();
        }

        public void processSoundData(int[,] data,bool combine = false)
        {
            if (combine) { 
            data =  DataCorrelation.alignAndCombineSignalData(data, CONJUNCTED_CHANNELS_1, CONJUNCTED_CHANNELS_2, ALIGN_MIC_DATA_SHIFTS_MAX);
            }
            double distance10 = signalManager.getDistanceBetweenMicrophones(0, CENTRAL_MIRCOPHONE_INDEX);
            double distance12 = signalManager.getDistanceBetweenMicrophones(2, CENTRAL_MIRCOPHONE_INDEX);

            int maxShiftCount10 = signalManager.processMaxShiftsCount(distance10);
            int maxShiftCount12 = signalManager.processMaxShiftsCount(distance12);
            ShiftWithValue shift10 = correlationer.getShiftBetweenMicrophones(data, CENTRAL_MIRCOPHONE_INDEX, 0, maxShiftCount10);
            ShiftWithValue shift10Negative = correlationer.getShiftBetweenMicrophones(data, 0, CENTRAL_MIRCOPHONE_INDEX, maxShiftCount10);
            ShiftWithValue shift12 = correlationer.getShiftBetweenMicrophones(data, CENTRAL_MIRCOPHONE_INDEX, 2, maxShiftCount12);
            ShiftWithValue shift12Negative = correlationer.getShiftBetweenMicrophones(data, 2, CENTRAL_MIRCOPHONE_INDEX, maxShiftCount12);

            int shift10Value = 0;
            bool shift10Positive = false;
            if (shift10.Value > shift10Negative.Value)
            {
                shift10Value = shift10.Shift;
                shift10Positive = true;
            }
            else
            {
                shift10Value = shift10Negative.Shift;
                shift10Positive = false;
            }

            int shift12Value = 0;
            bool shift12Positive = false;
            if (shift12.Value > shift12Negative.Value)
            {
                shift12Value = shift12.Shift;
                shift12Positive = true;
            }
            else
            {
                shift12Value = shift12Negative.Shift;
                shift12Positive = false;
            }
            Console.WriteLine("shift10Positive:" + shift10Positive);
            Console.WriteLine("shift12Positive:" + shift12Positive);


            double angle10 = SignalManager.getAngleFromDelay(soundConfig, shift10Value);
            double angle12 = SignalManager.getAngleFromDelay(soundConfig, shift12Value);

            // if (shift10Positive) angle10 = -angle10;
            // if (shift12Positive) angle12 = -angle12;
            bool[] relativityToCenter = new bool[2];
            relativityToCenter[0] = shift10Positive;
            relativityToCenter[1] = shift12Positive;

            List<double> angles = new List<double>();
            angles.Add(angle10);
            angles.Add(angle12);

            drawAngles(angles,CENTRAL_MIRCOPHONE_INDEX, relativityToCenter);

            Console.WriteLine("angle01:" + angle10);
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

            int maxShift = signalManager.processMaxShiftsCount(MICROPHONE_DISTANCE);
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
               

                processSoundData(arr,true);
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

        private void btnProcessAngle_Click(object sender, EventArgs e)
        {
            if(cbTestMode.Checked)
            {
                //Prepearing signals array
                int smnSizeMin = int.MaxValue;
                int signalValuesCount = signalManager.processSignalElementsCount(SOUND_EMITER_LISTEN_TIME);
                  //signalManager.Sn[0].processEmiterArr(SOUND_EMITER_LISTEN_TIME, signalManager.SamplingRate, SignalManager.V);
                   int[,] signalsArr = new int[signalManager.Mn.Count, signalValuesCount];

                for (int i = 0; i < signalManager.Mn.Count; i++)
                    {
                    double distanceFromSoundEmiterToMic = signalManager.getDistanceFromSoundEmitterToMicrophone(0, i);
                   int kDelay = (int)(distanceFromSoundEmiterToMic * signalManager.SamplingRate / V);
                    int[] arr = signalManager.processEmiterArr(SOUND_EMITER_LISTEN_TIME, SignalManager.V, kDelay);
                       
                     if (arr.Length < smnSizeMin) smnSizeMin = arr.Length;

                        for (int j = 0; j < arr.Length; j++)
                        {
                            signalsArr[i, j] = arr[j];
                            //  if (j< SIGNAL_VALUES_TO_OUTPUT)
                            if (j<50 || j > arr.Length - 50)
                             Console.Write(arr[j]+" ");
                        }
                         Console.WriteLine();
                    }
                    ResizeArray<int>(signalsArr, signalManager.Mn.Count, smnSizeMin);
                //processing signals data
                processSoundData(signalsArr);

            }
        }

       private T[,] ResizeArray<T>(T[,] original, int rows, int cols)
        {
            var newArray = new T[rows, cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    newArray[i, j] = original[i, j];
            return newArray;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            signalManager = SignalManager.fromConfig(rtbConfig.Text);
        }
    }
}
