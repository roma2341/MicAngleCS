using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using System.Threading;
using NAudio.Wave;

namespace MicAngle
{



    public partial class RecordForm : Form
    {
       // CountdownEvent cntEvent = new CountdownEvent(1);
        WaveIn waveIn;
        Form1 angleForm;
        const int SAMPLING_RATE = 44000;
        //Класс для записи в файл
        //BufferedWaveProvider bufferedWaveProvider = null;


        void StopRecording()
        {
            MessageBox.Show("StopRecording");
            waveIn.StopRecording();
        }
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
            }
            else
            {
                //Записываем данные из буфера в файл
                const int bytesInOnePortion = 4;
                int channels = angleForm.getSignalManager().Mn.Count;
                int bytesPerChannel = bytesInOnePortion - channels;
                int[,] signalFromMics = getSignalFromMics(e.Buffer, channels, bytesInOnePortion / bytesPerChannel);
                angleForm.processAngle(signalFromMics);
               // Thread.Sleep(4000);
               // bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
            }
        }
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
            }
            else
            {
                waveIn.Dispose();
                waveIn = null;
              //  bufferedWaveProvider.ClearBuffer();
            }
        }
        public RecordForm(Form1 angleForm)
        {
            InitializeComponent();
            this.angleForm=angleForm;
            int waveInDevicesCount = WaveIn.DeviceCount;
            for (int uDeviceID = 0; uDeviceID < waveInDevicesCount; uDeviceID++)
            {
                WaveInCapabilities waveInCaps = WaveIn.GetCapabilities(uDeviceID);
                String productName = waveInCaps.ProductName;
                comboBox1.Items.Add(productName);
            }
             
        }
        
        double ComputeCoeffd(double[] values1, double[] values2)
        {
            if (values1.Length != values2.Length)
                throw new ArgumentException("values must be the same length");
            var avg1 = values1.Average();
            var avg2 = values2.Average();
            var sum1 = values1.Zip(values2, (x1, y1) => (x1 - avg1) * (y1 - avg2)).Sum();
            var sumSqr1 = values1.Sum(x => Math.Pow((x - avg1), 2.0));
            var sumSqr2 = values2.Sum(y => Math.Pow((y - avg2), 2.0));
            var result = sum1 / Math.Sqrt(sumSqr1 * sumSqr2);
            return result;
        }
        float ComputeCoeff(float[] values1, float[] values2)
        {
            float[] buf = new float[44000];
            for (int i = 0; i < values1.Length; i++)
            {
                buf[i] = (values1[i] + values2[i]) * (values1[i] + values2[i]);
            }
            float sum = 0;
            for (int i = 0; i < values1.Length; i++)
            {
                sum = sum + buf[i];
            }
            return (float)Math.Sqrt(sum / 44000);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            //  cntEvent.Wait();
            if (comboBox1.SelectedIndex == -1) return ;
            int deviceId = comboBox1.SelectedIndex;
            //bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44000, 1));
            try
            {
                MessageBox.Show("Start Recording");
                waveIn = new WaveIn();
                //Дефолтное устройство для записи (если оно имеется)
                waveIn.DeviceNumber = deviceId;
                //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                waveIn.DataAvailable += waveIn_DataAvailable;
                //Прикрепляем обработчик завершения записи
                waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStopped);
                //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                waveIn.WaveFormat = new WaveFormat(SAMPLING_RATE, 1);
                //Инициализируем объект WaveFileWriter
                // writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                //Начало записи
                waveIn.StartRecording();
                //soundDisplayTimer.Start();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        public int[,] getSignalFromMics(byte[] buffer,int channels,int bytesPerChanel) 
        {
           
            if (comboBox1.SelectedIndex == -1) return null;
            //cntEvent.Wait();
           // SharedRes.mtx.WaitOne();
           
            int n = 0;
            int[,] signal = new int[channels, buffer.Length];
            for (int i = 0; i < buffer.Length / 4; i++)
            {
                for (int j = 0; j < channels; j+=2) 
                    for (int k=0; k < bytesPerChanel;k++)
                signal[j, i] |= buffer[n + j+ k] << k*8;
                n += bytesPerChanel * channels;//bytes
            }
           // SharedRes.mtx.ReleaseMutex();
            return signal;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
    }
    class SharedRes
    {
        public static int Count;
       volatile public static Mutex mtx = new Mutex();
    }
}
