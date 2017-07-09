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
        const int MICROPHONES_COUNT = 4;
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
            return this.waveInCapturedA && this.waveInCapturedB;
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
                waveInB.StopRecording();
        }
    
        void waveIn_DataAvailableA(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailableA), sender, e);
                return;
            }
            Console.WriteLine("DataAvailableA");

            waveProvider1.AddSamples(e.Buffer, 0, e.BytesRecorded);
            setWaveInCapturedA();
            if (isWaveInCaptured())
            {
                StopRecording();
            }

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
        void waveIn_DataAvailableB(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailableB), sender, e);
                return;
            }
            Console.WriteLine("DataAvailableB");

            waveProvider2.AddSamples(e.Buffer, 0, e.BytesRecorded);
            setWaveInCapturedB();
            if (isWaveInCaptured())
            {
                StopRecording();
            }
        }



        private  void waveIn_RecordingStoppedA(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStoppedA), sender, e);
                return;
            }

            setWaveInCapturedA();
            if(isWaveInCaptured())
            {
                Console.WriteLine("waveIn_RecordingStoppedA processAllWavesInRecorder");
                processAllWavesInRecorder();
            }
            //waveInCapturedA = false;
        }

        private void waveIn_RecordingStoppedB(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStoppedB), sender, e);
                return;
            }

            setWaveInCapturedB();
            if (isWaveInCaptured())
            {
                Console.WriteLine("waveIn_RecordingStoppedB processAllWavesInRecorder");
                processAllWavesInRecorder();
            }
            //waveInCapturedA = false;
        }

        public void processAllWavesInRecorder()
        {
            clearWaveInCaptured();
            byte[] bufferValue1 = new byte[waveProvider1.BufferedBytes];
            waveProvider1.Read(bufferValue1, 0, waveProvider1.BufferedBytes);
            int[,] signalFromMics1 = DataCorrelation.convertByteArrayToChanneled(bufferValue1, 2);

            byte[] bufferValue2 = new byte[waveProvider2.BufferedBytes];
            waveProvider2.Read(bufferValue2, 0, waveProvider2.BufferedBytes);
            int[,] signalFromMics2 = DataCorrelation.convertByteArrayToChanneled(bufferValue2, 2);


            int minSignalsCount = signalFromMics1.GetLength(1) < signalFromMics2.GetLength(1) ? signalFromMics1.GetLength(1) : signalFromMics2.GetLength(1);

            int[,] signalFromMics = new int[MICROPHONES_COUNT, minSignalsCount];

            for (int i = 0; i < MICROPHONES_COUNT; i++)
                for (int j = 0; j < minSignalsCount; j++)
            {
                    if( i < MICROPHONES_COUNT / 2 )
                    {
                        signalFromMics[i, j] = signalFromMics1[i, j];
                    }
                    else
                    {
                        signalFromMics[i, j] = signalFromMics2[i - (MICROPHONES_COUNT / 2 ), j];
                    }             
            }

            signalFromMics =  DataCorrelation.alignAndCombineSignalData(signalFromMics, 1, 2, 200);
        




            //clear chatrts part
            for (int i = 0; i < signalFromMics.GetLength(0); i++)
            {
                signalChart.Series[i].Points.Clear();
            }

                correlationChart.Series[0].Points.Clear();

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
                if (waveProvider2 == null)
                    waveProvider2 = new BufferedWaveProvider(new WaveFormat(SAMPLING_RATE, CHANNELS));
                else
                {
                    waveProvider2.ClearBuffer();
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
                        waveInA.BufferMilliseconds = 500;

                    }
                    if (waveInB == null)
                    {
                        waveInB = new WaveInEvent();
                        waveInB.DeviceNumber = deviceIdB;
                        waveInB.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailableB);
                        waveInB.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStoppedB);
                        waveInB.WaveFormat = new WaveFormat(SAMPLING_RATE, CHANNELS);
                        waveInB.BufferMilliseconds = 500;

                    }

                    //Инициализируем объект WaveFileWriter
                    // writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                    //Начало записи
                    stopwatch = new Stopwatch();
                        stopwatch.Start();
                        waveInA.StartRecording();
                        waveInB.StartRecording();

                    
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
