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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAngle
{
    public partial class MainForm : Form
    {
        const int SAMPLING_RATE = 44100;
        const int CHANNELS = 2;
        const int MICROPHONES_COUNT = 2;
        const double MICROPHONE_DISTANCE = 0.5; //Meters
        const int CONJUNCTED_CHANNELS_1 = 1;
        const int CONJUNCTED_CHANNELS_2 = 1;
        const bool CONJUNTION_ENABLED = true;
        const int ALIGN_MIC_DATA_SHIFTS_MAX = 130;


        bool isRecording = false;
        List<float[]> soundData = null;

        NAudio.Wave.AsioOut recAsio;

        List<CheckBox> signalChartCheckBoxes;
        List<CheckBox> correlationChartCheckBoxes;

        public MainForm()
        {
            InitializeComponent();
            initDynamicCheckBoxes();

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
            cb.Location = new Point(10, index * cb.Height);
            return cb;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            BeginRecording();
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
    

        public void processSoundData(int[,] data)
        {
            data =  DataCorrelation.alignAndCombineSignalData(data, CONJUNCTED_CHANNELS_1, CONJUNCTED_CHANNELS_2, ALIGN_MIC_DATA_SHIFTS_MAX);

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

            int maxShift = SignalsOperations.processMaxShiftsCount(MICROPHONE_DISTANCE, SAMPLING_RATE);
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
            recordedIterations++;
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<AsioAudioAvailableEventArgs>(waveIn_DataAvailableAsioTestA), sender, e);
                return;
            }
            Console.WriteLine("waveIn_DataAvailableAsioTestA begin");
            int samplesCount = e.SamplesPerBuffer * MICROPHONES_COUNT;
            float[] interlivedAsioSamples = new float[samplesCount];
            e.GetAsInterleavedSamples(interlivedAsioSamples);
            soundData.Add(interlivedAsioSamples);

           
           // e.WrittenToOutputBuffers = true;
          // if(recordedIterations > 3)
            StopRecording();
            /*StringBuilder builder = new StringBuilder("");
            for (var i = 0; i < interlivedAsioSamples.Length; i++)
            {
                builder.Append(" " + interlivedAsioSamples[i]);
            }
            Console.WriteLine(builder.ToString());*/
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
