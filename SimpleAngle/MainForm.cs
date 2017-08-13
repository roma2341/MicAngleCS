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
        const int POINT_SIZE = 12;

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
        public Graphics createGraphicContext()
        {

            Bitmap MyImage = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = MyImage;

            Graphics g;
            g = Graphics.FromImage(MyImage);
            return g;
        }
        
        public void beginDrawing(Graphics g)
        {
            int contextRotation = int.Parse(tbAngle.Text);
            float scaling = float.Parse(textBoxScaling.Text);
            g.TranslateTransform(pictureBox1.Size.Width / 2, pictureBox1.Size.Height / 2);
            g.ScaleTransform(scaling, scaling);
            g.RotateTransform(contextRotation);
            g.Clear(Color.White);
        }

        public void endDrawing(Graphics g)
        {
           // g.Dispose();
        }

        public void drawSoundSource(Graphics g)
        {
            Pen penSound = new Pen(Color.Blue);

            System.Windows.Point soundPoint = signalManager.Sn[0].Position;

            /*Drawing sound emitter */
            g.DrawEllipse(penSound, (float)soundPoint.X - POINT_SIZE / 2,
                (float)soundPoint.Y - POINT_SIZE / 2, POINT_SIZE, POINT_SIZE);
        }

        public void drawMicrophones(Graphics g)
        {
            List<Color> microphoneColors = new List<Color>();
            microphoneColors.Add(Color.Red);
            microphoneColors.Add(Color.Green);
            microphoneColors.Add(Color.Blue);

            for (var i = 0; i < signalManager.Mn.Count; i++)
            {
                g.DrawEllipse(new Pen(microphoneColors[i]), (float)signalManager.Mn[i].X - POINT_SIZE / 2,
                    (float)signalManager.Mn[i].Y - POINT_SIZE / 2, POINT_SIZE, POINT_SIZE);
            }
        }

        public void drawAngleToSound(Graphics g,Microphone m1, Microphone m2, double angle,Color color)
        {
            Pen microphonePencil = new Pen(color);
            /*Drawing angles */        
            System.Windows.Point centralPoint = GlobalMercator.getMiddlePoint(m1.Position, m2.Position);
            System.Windows.Point rotatedM2Point = MyUtils.rotate(m2.Position, centralPoint, angle);
            System.Windows.Point prolongedM1Point = MyUtils.CalculatePointWithDistance(rotatedM2Point, centralPoint, 500);
            System.Windows.Point prolongedM2Point = MyUtils.CalculatePointWithDistance(centralPoint, rotatedM2Point, 500);

            g.DrawLine(microphonePencil, (float)prolongedM1Point.X, (float)prolongedM1Point.Y, (float)prolongedM2Point.X, (float)prolongedM2Point.Y);

            /* List<System.Windows.Point> mnPoints = new List<System.Windows.Point>();
             List<System.Windows.Point> rotatedPoints = new List<System.Windows.Point>();

             System.Windows.Point centerMicrophonePoint = signalManager.Mn[mainMicrophoneIndex].Position;

             int currentAngle = 0;
             for (int i = 0; i < signalManager.Mn.Count; i++)
             {
                 if (i == mainMicrophoneIndex) continue;
                 System.Windows.Point pointToDraw = signalManager.Mn[i].Position;
                 System.Windows.Point rotatedPoint =new System.Windows.Point();
                 if (relativityToCenter[currentAngle])
                     rotatedPoint = MyUtils.rotate(centerMicrophonePoint, pointToDraw, angles[currentAngle]);
                 else
                     rotatedPoint = MyUtils.rotate(pointToDraw,centerMicrophonePoint, angles[currentAngle]);

                 mnPoints.Add(pointToDraw);
                 rotatedPoints.Add(rotatedPoint);

                 g.DrawLine(mypen, (float)centerMicrophonePoint.X, (float)centerMicrophonePoint.Y,
                     (float)pointToDraw.X, (float)pointToDraw.Y);
                 System.Windows.Point prolongedRotated1 = MyUtils.CalculatePointWithDistance(
                     signalManager.Mn[i].Position, rotatedPoint, 500);

                 float rotatedX = (float)prolongedRotated1.X;
                 float rotatedY = (float)prolongedRotated1.Y;

                 g.DrawLine(microphonePencils[i], (float)pointToDraw.X, (float)pointToDraw.Y, rotatedX, rotatedY);
                 currentAngle++;
             }
             */

            /*
            System.Windows.Point intersectionPoint = GlobalMercator.Intersection(mnPoints[0], 
                rotatedPoints[0], mnPoints[1], rotatedPoints[1]);
            if(!Double.IsNaN(intersectionPoint.X) && !Double.IsNaN(intersectionPoint.Y )) { 
            g.DrawEllipse(intersectionPencil, (float)intersectionPoint.X - POINT_SIZE / 2,
                   (float)intersectionPoint.Y - POINT_SIZE / 2, POINT_SIZE, POINT_SIZE);
            }*/

            // System.Windows.Point rotated2 = MyUtils.rotate(center, point2, 360 - angle2);

        }

        public void processSoundData(int[,] data,bool combine = false)
        {
            Dictionary<KeyValuePair<int, int>, int> microphonePairAngleToSoundEmiter = new Dictionary<KeyValuePair<int,int>,int>();
            List<KeyValuePair<int, int>> pairs = new List<KeyValuePair<int, int>>();
            pairs.Add(new KeyValuePair<int, int>(0, 1));
            pairs.Add(new KeyValuePair<int, int>(0, 2));
            pairs.Add(new KeyValuePair<int, int>(1, 2));

            if (combine) {
            data =  DataCorrelation.alignAndCombineSignalData(data, CONJUNCTED_CHANNELS_1, CONJUNCTED_CHANNELS_2, ALIGN_MIC_DATA_SHIFTS_MAX);
            }

            //clear chatrts part
            for (int i = 0; i < data.GetLength(0); i++)
            {
                signalChart.Series[i].Points.Clear();
            }

            correlationChart.Series[0].Points.Clear();

            Graphics g = createGraphicContext();
            beginDrawing(g);
            drawSoundSource(g);
            drawMicrophones(g);
            

            foreach (KeyValuePair<int,int> micPair in pairs)
            {
                double distanceBetweenMicrophones = signalManager.getDistanceBetweenMicrophones(micPair.Key, micPair.Value);
                int maxShiftCount = signalManager.processMaxShiftsCount(distanceBetweenMicrophones);
                ShiftWithValue optimalShift= correlationer.getShiftBetweenMicrophones(data, micPair.Key, micPair.Value, maxShiftCount);
                double optimalAngle = SignalManager.getAngleFromDelay(soundConfig, optimalShift.Shift);
                if (!optimalShift.Positive) optimalAngle = 180-optimalAngle;
                Console.WriteLine("optimal angle:" + optimalAngle);
                drawAngleToSound(g,signalManager.Mn[micPair.Key],signalManager.Mn[micPair.Value],optimalAngle,Color.Black);
            }
            endDrawing(g);

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
