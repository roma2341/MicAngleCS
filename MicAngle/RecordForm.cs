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
        //ASIO
        NAudio.Wave.AsioOut recAsio,recAsio2;
        NAudio.Wave.BufferedWaveProvider buffer;
        // CountdownEvent cntEvent = new CountdownEvent(1);
        WaveInEvent waveIn;
        Form1 angleForm;
        const int SAMPLING_RATE = 44100;
        bool isRecording = false;
        int asioInvocesCount1 = 0;
        int asioInvocesCount2 = 0;
        //Класс для записи в файл
        //BufferedWaveProvider bufferedWaveProvider = null;


        void StopRecording()
        {
            MessageBox.Show("StopRecording");
            if (rbWaveIn.Checked)
            {
                waveIn.StopRecording();
            }
            else
            {
                recAsio.Stop();
                recAsio2.Stop();
            }
        }
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            /* if (this.InvokeRequired)
             {
                 this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
                 return;
             }
             else*/
            {
                //Записываем данные из буфера в файл
                // const int bytesInOnePortion = 4;

                // int bytesPerChannel = bytesInOnePortion / channels;
                string ouputStr = "WaveIn data:\n";
                for (int i = 0; i < e.Buffer.Length; i++)
                {
                    ouputStr += e.Buffer[i]+" ";
                }
                System.IO.File.WriteAllText("WaveInSignal.txt", ouputStr);
                int[,] signalFromMics = convertByteBufferToIntWithChanels(e.Buffer);
                //angleForm.processAngle(signalFromMics);
                
                // Thread.Sleep(4000);
                // bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
                //StopRecording();
            }
     
        }
        int[,] convertByteBufferToIntWithChanels(byte[] buffer)
        {
            int channels = angleForm.getSignalManager().Channels;
            // int bytesPerChannel = bytesInOnePortion / channels;
            int[,] signalFromMics = getSignalFromMics(buffer, channels);
            return signalFromMics;
        }
         void waveIn_DataAvailableAsio(object sender, AsioAudioAvailableEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<AsioAudioAvailableEventArgs>(waveIn_DataAvailableAsio), sender, e);
                return;
            }
            asioInvocesCount1++;
            Console.WriteLine("asioInvocesCount:" + asioInvocesCount1);
            if (asioInvocesCount1 < 30)
            {
                return;
            }
            else
            {
                asioInvocesCount1=0;
            }
            int channels = angleForm.getSignalManager().Channels;
            int[,] signalFromMics = new int[e.InputBuffers.Length, e.SamplesPerBuffer/2];
            byte[] buf = new byte[e.SamplesPerBuffer];
            for (int i = 0; i < e.InputBuffers.Length; i++)
            {
                Marshal.Copy(e.InputBuffers[i], buf, 0, e.SamplesPerBuffer);
                int index = 0;
                for (int j = 0; j < e.SamplesPerBuffer/2; j ++)
                {
                    signalFromMics[i, j] = BitConverter.ToInt16(buf, index);
                    index+=2;
                }
            }
            buffer.AddSamples(buf, 0, buf.Length);
            angleForm.processAngle(signalFromMics);
            e.WrittenToOutputBuffers = true;

        }
        void waveIn_DataAvailableAsioTestA(object sender, AsioAudioAvailableEventArgs e)
        {
            /*if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<AsioAudioAvailableEventArgs>(waveIn_DataAvailableAsioTestA), sender, e);
                return;
            }*/
            // asioInvocesCount1++;
            //Console.WriteLine("asioInvocesCount:" + asioInvocesCount);
            /*if (asioInvocesCount1 < 100)
            {
                return;
            }
            else
            {
                asioInvocesCount1 = 0;
            }*/
            int channels = angleForm.getSignalManager().Channels;
            int micSamplesCount = e.SamplesPerBuffer / (2 * channels);
            int[,] signalFromMics = new int[e.InputBuffers.Length* channels, micSamplesCount];
            byte[] buf = new byte[e.SamplesPerBuffer];
            Console.WriteLine("e.InputBuffers.Length:" + e.InputBuffers.Length);
            int micIndex = 0;
            String fileContentStr = "";
            for (int i = 0; i < e.InputBuffers.Length; i++)
            {
                Marshal.Copy(e.InputBuffers[i], buf, 0, e.SamplesPerBuffer);
                fileContentStr +=("Raw data:\n");
                for (int j = 0; j < buf.Length; j++)
                {
                    Console.WriteLine(buf[j] + " ");
                    fileContentStr += buf[j] + " ";
                }
                fileContentStr +="\n";

                int index = 0;
                for (int j = 0; j < micSamplesCount; j++)
                {
                    for (int k = 0; k < channels;k++)
                    {
                        signalFromMics[micIndex + k, j] = BitConverter.ToInt16(buf, index);
                        index += 2;
                    }

                }
                micIndex += 2;
            }
            //buffer.AddSamples(buf, 0, buf.Length);
           // string[] seriesNames = { "firstLeft", "firstRight", "secondLeft", "secondRight" };
            //clear series
            //for (int i = 0; i < seriesNames.Length; i++)
              // chart1.Series[seriesNames[i]].Points.Clear();
            //
            
            for (int i = 0; i < signalFromMics.GetLength(0); i++)
            {
                for (int j = 0; j < signalFromMics.GetLength(1); j++)
                {
                    //if (chart1.Series[seriesNames[i]]!=null)
                   // chart1.Series[seriesNames[i]].Points.AddXY(j,signalFromMics[i, j]);
                    Console.Write(signalFromMics[i, j] + " ");
                    fileContentStr += signalFromMics[i, j] + " ";
                }
                fileContentStr += Environment.NewLine;
                Console.WriteLine();
            }
            System.IO.File.WriteAllText("inputSignal.txt", fileContentStr);

            e.WrittenToOutputBuffers = true;

        }
        void waveIn_DataAvailableAsioTestB(object sender, AsioAudioAvailableEventArgs e)
        {
           /* if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<AsioAudioAvailableEventArgs>(waveIn_DataAvailableAsioTestB), sender, e);
                return;
            }*/
            //Console.WriteLine("asioInvocesCount:" + asioInvocesCount);
            int channels = angleForm.getSignalManager().Channels;
            int[,] signalFromMics = new int[e.InputBuffers.Length, e.SamplesPerBuffer / 2];
            byte[] buf = new byte[e.SamplesPerBuffer];
            for (int i = 0; i < e.InputBuffers.Length; i++)
            {
                Marshal.Copy(e.InputBuffers[i], buf, 0, e.SamplesPerBuffer);
                int index = 0;
                for (int j = 0; j < e.SamplesPerBuffer / 2; j++)
                {
                    signalFromMics[i, j] = BitConverter.ToInt16(buf, index);
                    index += 2;
                }
            }
            //clear series
            String fileContentStr = "";
            for (int i = 0; i < signalFromMics.GetLength(0); i++)
            {
                for (int j = 0; j < signalFromMics.GetLength(1); j++)
                {
                    fileContentStr += signalFromMics[i, j] + " ";
                }
                fileContentStr += Environment.NewLine;
            }
            System.IO.File.WriteAllText("inputSignal2.txt", fileContentStr);

            e.WrittenToOutputBuffers = true;

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
        private void asio_RecordingStopped(object sender, EventArgs e)
        {
           /* if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
            }
            else*/
            {
                recAsio.Dispose();
                recAsio = null;
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
                 comboWaveInDevices.Items.Add(productName);
             }
            string[] asioDevices = AsioOut.GetDriverNames();
            foreach (string devName in asioDevices)
            {
                comboAsioDrivers.Items.Add(devName);
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
        private void toggleRecordButton()
        {
            if (isRecording)
            {
                button1.Text = "Зчитати";
            }
            else button1.Text = "Стоп";
            isRecording = !isRecording;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //  cntEvent.Wait();
            if (!isRecording)
            {
                if (comboAsioDrivers.SelectedIndex == -1) return;
                toggleRecordButton();
                int deviceId = comboWaveInDevices.SelectedIndex;
                int driverId = comboAsioDrivers.SelectedIndex;
                //bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44000, 1));
                try
                {
                    if (rbWaveIn.Checked)
                    {
                        MessageBox.Show("Start Recording");

                            Thread.CurrentThread.IsBackground = true;
                            waveIn = new WaveInEvent();
                            //Дефолтное устройство для записи (если оно имеется)
                            waveIn.DeviceNumber = deviceId;
                            //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
                            //Прикрепляем обработчик завершения записи
                            waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStopped);
                            //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                            waveIn.WaveFormat = new WaveFormat(SAMPLING_RATE, angleForm.getSignalManager().Channels);
                            waveIn.BufferMilliseconds = 60;
                            //Инициализируем объект WaveFileWriter
                            // writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                            //Начало записи
                            waveIn.StartRecording();
                      
                    }
                    else
                    {
                        ///ASIO
                        ///
                        recAsio = new NAudio.Wave.AsioOut(driverId);
                        recAsio2 = new NAudio.Wave.AsioOut(driverId);
                        recAsio2.ChannelOffset = 1;
                        NAudio.Wave.WaveFormat formato = new NAudio.Wave.WaveFormat();
                        buffer = new NAudio.Wave.BufferedWaveProvider(formato);
                        recAsio.AudioAvailable += new EventHandler<NAudio.Wave.AsioAudioAvailableEventArgs>(waveIn_DataAvailableAsioTestA);
                        //recAsio2.AudioAvailable += new EventHandler<NAudio.Wave.AsioAudioAvailableEventArgs>(waveIn_DataAvailableAsioTestB);
                        int channels = angleForm.getSignalManager().Channels;
                        recAsio.InitRecordAndPlayback(null, channels, SAMPLING_RATE); //rec channel = 1
                                                                                      // recAsio2.InitRecordAndPlayback(null, angleForm.getSignalManager().Mn.Count, SAMPLING_RATE); //rec channel = 1
                        recAsio.PlaybackStopped += new EventHandler<StoppedEventArgs>(asio_RecordingStopped);
                        recAsio.Play();
                        //recAsio2.Play();
                    }
                    




                    //soundDisplayTimer.Start();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
            else
            {
                StopRecording();
                toggleRecordButton();
            }
        }

        public int[,] getSignalFromMics(byte[] buffer,int channels) 
        {
           
            //if (comboAsioDrivers.SelectedIndex == -1) return null;
            //cntEvent.Wait();
            // SharedRes.mtx.WaitOne();

            /*int n = 0;
            int[,] signal = new int[channels, buffer.Length];
            for (int i = 0; i < buffer.Length / 4; i++)
            {
                for (int j = 0; j < channels; j+=2) 
                    for (int k=0; k < bytesPerChanel;k++)
                signal[j, i] |= buffer[n + j+ k] << k*8;
                n += bytesPerChanel * channels;//bytes
            }*/

            // SharedRes.mtx.ReleaseMutex();
            int[,] signal = new int[channels, buffer.Length/4];
            int index = 0;
            for (int sample = 0; sample < buffer.Length/4; sample++)
            {
                for (int j = 0; j < channels; j++)
                {
                    signal[j,sample] = BitConverter.ToInt16(buffer, index);
                    index += 2;
                }
                 
            }
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
