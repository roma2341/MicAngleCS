using NAudio.Wave;
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
    public partial class Form1 : Form
    {
        WaveInEvent waveInA, waveInB;

        const int SAMPLING_RATE = 44100;
        const int CHANNELS = 2;

        Stopwatch stopwatch;

        public Form1()
        {
            InitializeComponent();
            int waveInDevicesCount = WaveIn.DeviceCount;
            for (int uDeviceID = 0; uDeviceID < waveInDevicesCount; uDeviceID++)
            {
                WaveInCapabilities waveInCaps = WaveIn.GetCapabilities(uDeviceID);
                String productName = waveInCaps.ProductName;
                comboWaveInDeviceA.Items.Add(productName);
               // comboWaveInDeviceB.Items.Add(productName);
            }


            comboWaveInDeviceA.SelectedIndex = 0;
           /* if (waveInDevicesCount > 1)
                comboWaveInDeviceB.SelectedIndex = 1;
            else
                comboWaveInDeviceB.SelectedIndex = 0;*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BeginRecording();
        }

        bool isRecording = false;

        public void toggleRecordButton()
        {
            isRecording = !isRecording;
        }

        void StopRecording()
        {
            const int BYTE_IN_SAMPLE = 2;
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
            int[,] signalFromMics = convertByteArrayToChanneled(e.Buffer,2);
            // signalFromMicrophones.Add(signalFromMics);
            //angleForm.processAngle(signalFromMics);               
            // Thread.Sleep(4000);
            // bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        public int[,] convertByteArrayToChanneled(byte[] buffer,int channels)
        {
            int[,] result = new int[channels, buffer.Length / 2];
            int i = 0;
            for (int sample = 0; sample < buffer.Length / 4; sample++)
            {
                result[0,i] = BitConverter.ToInt16(buffer, i);
                i += 2;
                result[1,i] = BitConverter.ToInt16(buffer, i);
                i += 2;
            }
            return result;
        }

        private void waveIn_RecordingStoppedA(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStoppedA), sender, e);
            }
            else
            {
                waveInA.Dispose();
                waveInA = null;
            }
            //waveInCapturedA = false;
        }

        public void BeginRecording()
        {
            //  cntEvent.Wait();
            if (!isRecording)
            {
                toggleRecordButton();
                int deviceIdA = comboWaveInDeviceA.SelectedIndex;
                int deviceIdB = comboWaveInDeviceB.SelectedIndex;
                //bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44000, 1));
                try
                {
                        //  MessageBox.Show("Start Recording");

                        Thread.CurrentThread.IsBackground = true;
                        waveInA = new WaveInEvent();
                        waveInB = new WaveInEvent();
                        //Дефолтное устройство для записи (если оно имеется)
                        waveInA.DeviceNumber = deviceIdA;
                        waveInB.DeviceNumber = deviceIdB;
                        //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                        waveInA.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailableA);
                       // waveInB.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailableB);
                        //Прикрепляем обработчик завершения записи
                        waveInA.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStoppedA);
                        //waveInB.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStoppedB);
                        //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                        waveInA.WaveFormat = new WaveFormat(SAMPLING_RATE, CHANNELS);
                       // waveInB.WaveFormat = new WaveFormat(angleForm.getSignalManager().SamplingRate, angleForm.getSignalManager().Channels);
                        waveInA.BufferMilliseconds = 100;
                        waveInB.BufferMilliseconds = 100;
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
                StopRecording();
            }
        }

    }
}
