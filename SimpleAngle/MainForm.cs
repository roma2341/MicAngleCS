using NAudio.Wave;
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


        WaveInEvent waveInA, waveInB;

        BufferedWaveProvider waveProvider1, waveProvider2;

        bool isRecording = false;
        bool waveInCapturedA = false;
        bool waveInCapturedB = false;

        public void setWaveInCapturedA()
        {
            this.waveInCapturedA = true;
        }

        public void setWaveInCapturedB()
        {
            this.waveInCapturedB = true;
        }

        public bool isWaveInCaptured()
        {
            return this.waveInCapturedA;
        }

        public void clearWaveInCaptured()
        {
            this.waveInCapturedA = false;
            this.waveInCapturedB = false;
        }



        Stopwatch stopwatch;

        List<CheckBox> signalChartCheckBoxes;
        List<CheckBox> correlationChartCheckBoxes;

        public MainForm()
        {
            InitializeComponent();
            initDynamicCheckBoxes();

            int waveInDevicesCount = WaveIn.DeviceCount;
            for (int uDeviceID = 0; uDeviceID < waveInDevicesCount; uDeviceID++)
            {
                WaveInCapabilities waveInCaps = WaveIn.GetCapabilities(uDeviceID);
                String productName = waveInCaps.ProductName;
                comboWaveInDeviceA.Items.Add(productName);
                comboWaveInDeviceB.Items.Add(productName);
            }


            comboWaveInDeviceA.SelectedIndex = 0;
            if (waveInDevicesCount > 1)
                comboWaveInDeviceB.SelectedIndex = 1;
            else
                comboWaveInDeviceB.SelectedIndex = 0;
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
                waveInA.StopRecording();
                //waveInB.StopRecording();
        }

        void waveIn_DataAvailableA(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailableA), sender, e);
                return;
            }

            waveProvider1.AddSamples(e.Buffer, 0, e.BytesRecorded);

            if (true)
            return;

            //if (waveInCapturedA) return;
            //signalFromMicrophonesA = e.Buffer;
            // waveInStartTimeA = (long)(stopwatch.Elapsed.TotalMilliseconds * 1000000);
            // waveInCapturedA = true;
            /* if (this.InvokeRequired)
             {
                 this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
                 return;
             }
             else*/
            //Записываем данные из буфера в файл
            // const int bytesInOnePortion = 4;
            // int bytesPerChannel = bytesInOnePortion / channels;
            /*   string ouputStr = "WaveIn data:\n";
               for (int i = 0; i < e.Buffer.Length; i++)
               {
                   ouputStr += e.Buffer[i]+" ";
               }
               System.IO.File.WriteAllText("WaveInSignal.txt", ouputStr);*/
           
            // signalFromMicrophones.Add(signalFromMics);
            //angleForm.processAngle(signalFromMics);               
            // Thread.Sleep(4000);
            // bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }


       

        private void waveIn_RecordingStoppedA(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStoppedA), sender, e);
            }
            else
            {
              //  waveInA.Dispose();
               // waveInA = null;
            }
            setWaveInCapturedA();
            if(isWaveInCaptured())
            {
                processAllWavesInRecorder();
            }
            //waveInCapturedA = false;
        }

        public void processAllWavesInRecorder()
        {
            byte[] bufferValue = new byte[waveProvider1.BufferedBytes];
            waveProvider1.Read(bufferValue, 0, waveProvider1.BufferedBytes);
            int[,] signalFromMics = DataCorrelation.convertByteArrayToChanneled(bufferValue, 2);

            //clear chatrts part
            for (int i = 0; i < signalFromMics.GetLength(0); i++)
            {
                signalChart.Series[i].Points.Clear();
            }
            for (int i = 0; i < signalFromMics.GetLength(0); i++)
            {
                correlationChart.Series[i].Points.Clear();
            }

            //fill data part
            for (int i = 0; i < signalFromMics.GetLength(0); i++)
            {
                // Console.WriteLine("i:" + i);
                for (int j = 0; j < signalFromMics.GetLength(1) / 10; j++)
                {
                    // Console.WriteLine("j:" + j);
                    signalChart.Series[i].Points.AddXY(j, signalFromMics[i, j]);
                }
            }

            int maxShift = SignalsOperations.processMaxShiftsCount(MICROPHONE_DISTANCE, SAMPLING_RATE);
            int[] shiftsCorrelation = DataCorrelation.generateCorrelationArray(signalFromMics, new CorrelationConfig(maxShift, maxShift, false));
            int[] backwardShiftCorrelation = DataCorrelation.generateCorrelationArray(signalFromMics, new CorrelationConfig(maxShift, maxShift, true));

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
            }

        }


        public void BeginRecording()
        {
            //  cntEvent.Wait();
            if (!isRecording)
            {
                toggleRecordButton();
                clearWaveInCaptured();
                int deviceIdA = comboWaveInDeviceA.SelectedIndex;
                int deviceIdB = comboWaveInDeviceB.SelectedIndex;
                if (waveProvider1 == null)
                waveProvider1 = new BufferedWaveProvider(new WaveFormat(SAMPLING_RATE, CHANNELS));
                else
                {
                    waveProvider1.ClearBuffer();
                }
                try
                {
                        //  MessageBox.Show("Start Recording");
                        Thread.CurrentThread.IsBackground = true;
                        if (waveInA == null)
                    {
                        waveInA = new WaveInEvent();
                        waveInA.DeviceNumber = deviceIdA;
                        waveInA.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailableA);
                        waveInA.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStoppedA);
                        waveInA.WaveFormat = new WaveFormat(SAMPLING_RATE, CHANNELS);
                        waveInA.BufferMilliseconds = 1000;

                    }
                   /* if (waveInB == null)
                    {
                        waveInA = new WaveInEvent();
                        waveInA.DeviceNumber = deviceIdA;
                        waveInA.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailableA);
                        waveInA.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStoppedA);
                        waveInA.WaveFormat = new WaveFormat(SAMPLING_RATE, CHANNELS);
                        waveInA.BufferMilliseconds = 1000;

                    }*/

                    //Инициализируем объект WaveFileWriter
                    // writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                    //Начало записи
                    stopwatch = new Stopwatch();
                        stopwatch.Start();
                        waveInA.StartRecording();
                        //waveInB.StartRecording();

                    
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
            else
            {
                toggleRecordButton();
                StopRecording();
            }
        }

    }
}
