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
using System.Diagnostics;

namespace MicAngle
{



    public partial class RecordForm : Form
    {
        Stopwatch stopwatch;
        const int ASIO_CHANELS = 8;
        int INPUTS_COUNT = 2;
        String fileContentStrAsioA, fileContentStrAsioB;
        long  waveInStartTimeA=0, waveInStartTimeB=0;
        bool waveInCapturedA = false, waveInCapturedB = false;
        //ASIO
        NAudio.Wave.AsioOut recAsio, recAsio2;
        NAudio.Wave.BufferedWaveProvider buffer;

        // CountdownEvent cntEvent = new CountdownEvent(1);
        WaveInEvent waveInA,waveInB;
        Form1 angleForm;
        const int BYTES_PER_SAMPLE = 4;
        bool isRecording = false;
        int asioInvocesCount1 = 0;
        int asioInvocesCount2 = 0;
        public int[,] MicsSignal;

        List<byte[]>[] AsioData;
        List<float[]> aggregatedAsio; //LRLRLRLR;
        int aggregatedAsioCount = 0;
        int[] asioTotalBytesCount;
        //List<int[,]> signalFromMicrophones;
        byte[] signalFromMicrophonesA;
        byte[] signalFromMicrophonesB;
       // int signalABytes = 0, signalBBytes = 0;

        //Класс для записи в файл
        //BufferedWaveProvider bufferedWaveProvider = null;
        public void stringDataToChart(String input)
        {
            string[] originRows = input.Split('\n');
            int nonemptyStrCount=0;
            foreach (string str in originRows)
            {
                if (str.Length > 0) nonemptyStrCount++;
            }
            string[] rows = new string[nonemptyStrCount];
            int curStr = 0;
             for (int i = 0; i < originRows.Length; i++)
            {
                if (originRows[i].Length > 0) rows[curStr++] = originRows[i];
            }
            int currentRow = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                chartSignal.Series[currentRow].Points.Clear();
                string[] strNums = rows[i].Split(' ');
                bool good = false;
                for (int j = 0; j < strNums.Length; j++)
                {
                    int number = 0;
                    try
                    {
                        number = int.Parse(strNums[j]);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    good = true;
                    chartSignal.Series[currentRow].Points.AddXY(j+1, number);
                }
                if (good) currentRow++;
            }
        }
        public void signalDataToChart(int [,] data)
        {
            const int CHART_STEP = 1;
            int LIMIT = 10000;
            ///
            int width = (LIMIT < data.GetLength(1)) ? LIMIT : data.GetLength(1);
            for (int i = 0; i < chartSignal.Series.Count; i++)
            {
                chartSignal.Series[i].Points.Clear();
            }
                for (int i = 0; i < data.GetLength(0); i++)
            {
               
                for (int j = 0; j < width; j+=CHART_STEP)
                {
                    if (i > data.GetLength(0)) return;
                    chartSignal.Series[i].Points.AddXY(j + 1, data[i, j]);
                }

            }

            
        }

        void StopRecording()
        {
            const int BYTE_IN_SAMPLE = 2;
            //MessageBox.Show("StopRecording");
            if (rbWaveIn.Checked)
            {
                waveInA.StopRecording();
                waveInB.StopRecording();

                int signalABytes = signalFromMicrophonesA.Length;
                int signalBBytes = signalFromMicrophonesB.Length;
                int channels = angleForm.getSignalManager().Channels;
                Console.WriteLine("waveInStartTimeA:" + waveInStartTimeA);
                Console.WriteLine("waveInStartTimeB:" + waveInStartTimeB);
                long delta = waveInStartTimeB - waveInStartTimeA;
                Console.WriteLine("Delta:" + delta);
                int delay = MyUtils.differenceInTimeToDelay(delta) * BYTE_IN_SAMPLE * channels;
                Console.WriteLine("Delay:" + delay);
                //Взагалі-то для позитивного значення має зсув вправо бути... але покищо так
                if (delay > 0) signalFromMicrophonesB = MyUtils.shiftRight(signalFromMicrophonesB, delay);
                else signalFromMicrophonesB = MyUtils.shiftLeft(signalFromMicrophonesB, -delay);
                MicsSignal = MyUtils.convertBytesToMicrophonesSignalArray(signalFromMicrophonesA, signalFromMicrophonesB, channels, BYTE_IN_SAMPLE);
                signalDataToChart(MicsSignal);
                rtbSignal.Text = MyUtils.arrayToString(MicsSignal, 100000);
            }
            else
            {
                recAsio.Stop();
                //recAsio2.Stop();
            }
            //stringDataToChart(fileContentStrAsioA + '\n' + fileContentStrAsioB);
          
        }
        void waveIn_DataAvailableA(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailableA), sender, e);
                return;
            }
            if (waveInCapturedA) return;
            //signalFromMicrophonesA = e.Buffer;
            waveInStartTimeA = (long)(stopwatch.Elapsed.TotalMilliseconds * 1000000);
            waveInCapturedA = true;
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
             int[,] signalFromMics = convertByteBufferToIntWithChanels(e.Buffer);
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
            if (waveInCapturedB) return;
            signalFromMicrophonesB = e.Buffer;
            waveInStartTimeB = (long)(stopwatch.Elapsed.TotalMilliseconds * 1000000);
            waveInCapturedB = true;
            //angleForm.processAngle(signalFromMics);               
            // Thread.Sleep(4000);
            // bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
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
        /// <summary>
        /// Обєднує данні, зчитані з мікрофонів в подіях
        /// </summary>
        /// <param name="signalA"></param>
        /// <param name="signalB"></param>
        /// <param name="inputsCount">Кількість використовуємих входів на інтерфейсі</param>
        /// <param name="samplesPerBuffer">Кількість семплів зчитаних з одного інтерфейсу</param>
        /// <returns></returns>

        public int [,] pasteTogetherSignals(List<byte[]> signalA, List<byte[]> signalB,int inputsCount,int bytesPerBuffer)
        {
            
            int channels = angleForm.getSignalManager().Channels;
            int samplesPerBuffer = bytesPerBuffer / (channels* inputsCount);
            int micSamplesCount = samplesPerBuffer;
            int micBufferLength = micSamplesCount * bytesPerBuffer * channels;
            //int[,] signalFromMics = new int[e.InputBuffers.Length* channels, micSamplesCount];
            int[,] signalFromMics = new int[inputsCount * channels, micSamplesCount];
            int sampleOffset = 0;
            for (int i = 0; i < signalA.Count; i++)
            {
                /*
                  for (int k = 0; k < channels;k++)
                    {
                        short sample = BitConverter.ToInt16(buf, index); // INDEX OUT OF BOUNDS EXCEPTION HERE, Good Night)
                        signalFromMics[micIndex + k, j] = sample;
                        index += SAMPLE_SIZE_BYTES;
                    }
                 */
                int index = 0;
             
                int localSamplesCount = signalA[i].Length / (bytesPerBuffer * channels);
                for (int j = 0; j < localSamplesCount; j++)
                {
                    for (int k = 0; k < channels; k++)
                    {
                        short sample = BitConverter.ToInt16(signalA[i], index); // INDEX OUT OF BOUNDS EXCEPTION HERE, Good Night)
                        signalFromMics[k, sampleOffset+j] = sample;
                        index += bytesPerBuffer;
                    }
                    
                }
                sampleOffset += localSamplesCount;
            }
            sampleOffset = 0; 
            for (int i = 0; i < signalB.Count; i++)
            {
                /*
                  for (int k = 0; k < channels;k++)
                    {
                        short sample = BitConverter.ToInt16(buf, index); // INDEX OUT OF BOUNDS EXCEPTION HERE, Good Night)
                        signalFromMics[micIndex + k, j] = sample;
                        index += SAMPLE_SIZE_BYTES;
                    }
                 */
                int index = 0;
                int MICROPHONE_OFFEST = 2;
                int localSamplesCount = signalB[i].Length / (bytesPerBuffer * channels);
                for (int j = 0; j < localSamplesCount; j++)
                {
                    for (int k = 0; k < channels; k++)
                    {
                        short sample = BitConverter.ToInt16(signalB[i], index); // INDEX OUT OF BOUNDS EXCEPTION HERE, Good Night)
                        signalFromMics[MICROPHONE_OFFEST+k, sampleOffset + j] = sample;
                        index += bytesPerBuffer;
                    }

                }
                sampleOffset += localSamplesCount;


            }
            /*int startIndex = signalA.Count;
            for (int i = 0; i < signalB.Count; i++)
            {
                int index = 0;
                for (int j = 0; j < signalB[i].Length/ SAMPLE_SIZE_BYTES; j++)
                {
                    short sample = BitConverter.ToInt16(signalB[i], index); // INDEX OUT OF BOUNDS EXCEPTION HERE, Good Night)
                    signalFromMics[startIndex+i, j] = sample;
                    index += SAMPLE_SIZE_BYTES;

                }
            }
            */

            return signalFromMics; 
        }
        public int [,] unitePartialMeasurement(List<int[,]> source)
        {
            bool flag = false;
            //int dimensions = 0;
            int widthElementCount = 0;
            int heightElementCount = 0;
            foreach (int[,] item in source)
            {
              /*  if (flag)
                {
                    if (item.Rank != dimensions) throw new Exception("Dimension count must be equal");
                }
                else
                {
                    dimensions = item.Rank;
                }*/
                widthElementCount += item.GetLength(1);
                flag = true;
            }
            heightElementCount = source[0].GetLength(0);
            int[,] result = new int[heightElementCount, widthElementCount];

            int itemsCount = 0;
            foreach (int[,] item in source)
            {
                itemsCount++;
                int i = 0;
                int j = 0;
                for (int l = 0; l < item.GetLength(0); l++) {
                    for (int m = 0; m < item.GetLength(1); m++)
                    {
                        //Console.WriteLine(String.Format("i={0} j={1} l={2} m={3} val={4} height={5} width={6} itms={7} slen={8}",i,j,l,m,item[l, m], item.GetLength(0), item.GetLength(1), itemsCount, source.Count));
                        result[i, j++] = item[l, m];
                      
                    }
                    i++;
                    j = 0;
                }
                
            }
            return result;


            }
        void waveIn_DataAvailableAsioTestA(object sender, AsioAudioAvailableEventArgs e)
        {
            Console.WriteLine("AsioSampleType:"+e.AsioSampleType.ToString());
            int samplesCount = e.SamplesPerBuffer*angleForm.getSignalManager().Channels;
            float[] interlivedAsioSamples = new float[samplesCount];
            e.GetAsInterleavedSamples(interlivedAsioSamples);
            aggregatedAsio.Add(interlivedAsioSamples);
            aggregatedAsioCount += samplesCount;
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
            const int SAMPLE_SIZE_BYTES = 4;//WARNING 3 MUST ME HERE I THINK
            int channels = angleForm.getSignalManager().Channels;
            int micSamplesCount = e.SamplesPerBuffer * SAMPLE_SIZE_BYTES;
            int micBufferLength = micSamplesCount ;
            //int[,] signalFromMics = new int[e.InputBuffers.Length* channels, micSamplesCount];
            byte[] buf = new byte[micBufferLength];
             Console.WriteLine("e.InputBuffers.Length:" + e.InputBuffers.Length);
            // int micIndex = 0;
            // fileContentStrAsioA = "";
            /*for (int i = 0; i < e.InputBuffers.Length; i++)
            {
                Marshal.Copy(e.InputBuffers[i], buf, 0, micBufferLength);
                fileContentStrAsioA +="\n";

                int index = 0;
                for (int j = 0; j < micSamplesCount; j++)
                {
                    for (int k = 0; k < channels;k++)
                    {
                        short sample = BitConverter.ToInt16(buf, index); // INDEX OUT OF BOUNDS EXCEPTION HERE, Good Night)
                        signalFromMics[micIndex + k, j] = sample;
                        index += SAMPLE_SIZE_BYTES;
                    }

                }
                micIndex += channels;
            }*/
            /* for (int i = 0; i < e.InputBuffers.Length; i++)
             {
                 Marshal.Copy(e.InputBuffers[i], buf, 0, micBufferLength);
                 fileContentStrAsioA += "\n";

                 int index = 0;
                 for (int j = 0; j < micSamplesCount; j++)
                 {
                         short sample = BitConverter.ToInt16(buf, index); // INDEX OUT OF BOUNDS EXCEPTION HERE, Good Night)
                         signalFromMics[i, j] = sample;
                         index += SAMPLE_SIZE_BYTES;

                 }
             }/

             signalFromMicrophones.Add(signalFromMics);
             //buffer.AddSamples(buf, 0, buf.Length);
             // string[] seriesNames = { "firstLeft", "firstRight", "secondLeft", "secondRight" };
             //clear series
             //for (int i = 0; i < seriesNames.Length; i++)
             // chart1.Series[seriesNames[i]].Points.Clear();
             //


             /* for (int i = 0; i < signalFromMics.GetLength(0); i++)
              {
                  for (int j = 0; j < signalFromMics.GetLength(1); j++)
                  {
                      //if (chart1.Series[seriesNames[i]]!=null)
                     // chart1.Series[seriesNames[i]].Points.AddXY(j,signalFromMics[i, j]);
                      Console.Write(signalFromMics[i, j] + " ");
                      fileContentStrAsioA += signalFromMics[i, j] + " ";
                  }
                  fileContentStrAsioA += Environment.NewLine;
                  Console.WriteLine();
              }
              System.IO.File.WriteAllText("inputSignal.txt", fileContentStrAsioA); */
           /* for (int i = 0; i < e.InputBuffers.Length; i++)
            {
                int firstMicIndex = angleForm.getSignalManager().ChannelOffset;//2
                int lastMicIndex = firstMicIndex + channels-1;//5
                Marshal.Copy(e.InputBuffers[i], buf, 0, micBufferLength);
                if (i >= firstMicIndex && i <= lastMicIndex) {
                    Marshal.Copy(e.InputBuffers[i], buf, 0, micBufferLength);
                    AsioData[i - firstMicIndex].Add(buf);
                    asioTotalBytesCount[i - firstMicIndex] += buf.Length;
                        }
            }*/

            //signalFromMicrophonesA.Add(buf);

            e.WrittenToOutputBuffers = true;

        }
        /// <summary>
        /// Для 8канального віртуального мікрофону
        /// </summary>
        int[,] processMultipleChannels(List<byte[]>[] source, int sampleBytes,bool isLSB)
        {
            //TEST ARRAY
          /*   List<byte[]>[] source = new List<byte[]>[2];
            for (int i=0; i < source.Length; i++)
            {
                source[i] = new List<byte[]>();
            }
            byte[] testByteArrayA= { 1, 0, 1, 0, 1, 0 };
            byte[] testByteArrayB = { 1, 1, 1, 1 };
            source[0].Add(testByteArrayA);
            source[0].Add(testByteArrayA);
            source[1].Add(testByteArrayB);
            source[1].Add(testByteArrayB); */


            int width = asioTotalBytesCount[0] / sampleBytes;
            int[,] result = new int[source.GetLength(0), width]; // 3 bytes because 24 bits/sample
            for (int micIndex = 0; micIndex < source.Length; micIndex++)
            {
                int index = 0;
                for (int windowIndex = 0; windowIndex < source[micIndex].Count; windowIndex++)
                {
                    for (int startSampleByteIndex = 0; startSampleByteIndex < source[micIndex][windowIndex].Length; startSampleByteIndex+=sampleBytes)
                    {
                        for (int byteOffset = 0; byteOffset < sampleBytes; byteOffset++)
                        {
                            if (isLSB)//Якщо старший байт зліва
                            {
                                int topOffset = sampleBytes - 1;
                                if (byteOffset == 0)
                                    result[micIndex, index / sampleBytes] = source[micIndex][windowIndex][startSampleByteIndex] << 8 * (topOffset-byteOffset);
                                else result[micIndex, index / sampleBytes] |= (source[micIndex][windowIndex][startSampleByteIndex + byteOffset] << 8 * (topOffset - byteOffset));
                            }
                            else
                            {
                                if (byteOffset == 0)
                                    result[micIndex, index / sampleBytes] = source[micIndex][windowIndex][startSampleByteIndex];
                                else result[micIndex, index / sampleBytes] |= (source[micIndex][windowIndex][startSampleByteIndex + byteOffset] << 8 * byteOffset); //| (source[micIndex][windowIndex][byteN+2] << 16);
                            }
                            }
                            index += sampleBytes;
                    }
                }

            }
            return result;
        }
        void waveIn_DataAvailableAsioTestB(object sender, AsioAudioAvailableEventArgs e)
        {
            const int SAMPLE_SIZE_BYTES = 3;
            int channels = angleForm.getSignalManager().Channels;
            int micSamplesCount = e.SamplesPerBuffer;
            int micBufferLength = micSamplesCount * SAMPLE_SIZE_BYTES * channels;
            byte[] buf = new byte[micBufferLength];
            /*for (int i = 0; i < e.InputBuffers.Length; i++)
            {
                Marshal.Copy(e.InputBuffers[i], buf, 0, micBufferLength);
                fileContentStrAsioA +="\n";

                int index = 0;
                for (int j = 0; j < micSamplesCount; j++)
                {
                    for (int k = 0; k < channels;k++)
                    {
                        short sample = BitConverter.ToInt16(buf, index); // INDEX OUT OF BOUNDS EXCEPTION HERE, Good Night)
                        signalFromMics[micIndex + k, j] = sample;
                        index += SAMPLE_SIZE_BYTES;
                    }

                }
                micIndex += channels;
            }*/
            for (int i = 0; i < e.InputBuffers.Length; i++)
            {
                Marshal.Copy(e.InputBuffers[i], buf, 0, micBufferLength);
            }

           // signalFromMicrophonesB.Add(buf);
            //buffer.AddSamples(buf, 0, buf.Length);
            // string[] seriesNames = { "firstLeft", "firstRight", "secondLeft", "secondRight" };
            //clear series
            //for (int i = 0; i < seriesNames.Length; i++)
            // chart1.Series[seriesNames[i]].Points.Clear();
            //


            /* for (int i = 0; i < signalFromMics.GetLength(0); i++)
             {
                 for (int j = 0; j < signalFromMics.GetLength(1); j++)
                 {
                     //if (chart1.Series[seriesNames[i]]!=null)
                    // chart1.Series[seriesNames[i]].Points.AddXY(j,signalFromMics[i, j]);
                     Console.Write(signalFromMics[i, j] + " ");
                     fileContentStrAsioA += signalFromMics[i, j] + " ";
                 }
                 fileContentStrAsioA += Environment.NewLine;
                 Console.WriteLine();
             }
             System.IO.File.WriteAllText("inputSignal.txt", fileContentStrAsioA); */

            e.WrittenToOutputBuffers = true;

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
            waveInCapturedA = false;
        }
        private void waveIn_RecordingStoppedB(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStoppedA), sender, e);
            }
            else
            {
                waveInB.Dispose();
                waveInB = null;
                //  bufferedWaveProvider.ClearBuffer();
            }
            waveInCapturedB = false;
        }
      
        private int[,] aggregatedArraysToSeparated(List<float[]> arrs,int totalArrayElmCount, int channels)
        {
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
                    result[j, index] = (int)(source[i + j]* short.MaxValue);
                }
               // result[0, index] = ((int)(source[i] * Int32.MaxValue));
               // result[1, index] = ((int)(source[i] * Int32.MaxValue));
                index++;
            }
            SignalsManager.shiftMultidimensional(result, 0, angleForm.getSignalManager().MicrophonesShift[0]);
            SignalsManager.shiftMultidimensional(result, 1, angleForm.getSignalManager().MicrophonesShift[0]);
            return result;
           
        }
        private void asio_RecordingStoppedA(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(asio_RecordingStoppedA), sender, e);
            }
            else
            {
                recAsio.Dispose();
                recAsio = null;
                //micsSignal = unitePartialMeasurement(signalFromMicrophonesA);
                // int[,] result = processMultipleChannels(AsioData,BYTES_PER_SAMPLE,true);




               // int[,] result= angleForm.getSignalManager().generateTestMicSignal(2900,4,2,15);


                int MAX_SHIFT_COUNT = 30;
               int[,] result = aggregatedArraysToSeparated(aggregatedAsio, aggregatedAsioCount, angleForm.getSignalManager().Channels);
                if (angleForm.getSignalManager().ConjuctedChannelsIndexes != null && angleForm.getSignalManager().ConjuctedChannelsIndexes.Length > 1)
                    result = angleForm.getSignalManager().alignAndCombineSignalData(result, angleForm.getSignalManager().ConjuctedChannelsIndexes[0], angleForm.getSignalManager().ConjuctedChannelsIndexes[1], MAX_SHIFT_COUNT);
                MicsSignal = result;
                Console.WriteLine("Arrays aggregated( height:" + result.GetLength(0) + "width: " + result.GetLength(1));
                //aggregatedAsio
                int LIMIT = 100000;
                rtbSignal.Text = MyUtils.arrayToString(result,100000);
                MicsSignal = result;
                //rtbSignal.Text = arrayToString(micsSignal, LIMIT);
                //angleForm.processAngle(micsSignal);
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
                 comboWaveInDeviceA.Items.Add(productName);
                comboWaveInDeviceB.Items.Add(productName);
            }
            string[] asioDevices = AsioOut.GetDriverNames();
            foreach (string devName in asioDevices)
            {
                comboAsioDrivers.Items.Add(devName);
            }
            
            //select default devices
            if (asioDevices.Length>0)
            comboAsioDrivers.SelectedIndex = 0;

            comboWaveInDeviceA.SelectedIndex = 0;
            if (waveInDevicesCount > 1)
            comboWaveInDeviceB.SelectedIndex = 1;
            else
            comboWaveInDeviceB.SelectedIndex = 0;




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

        private void btnTextInputToChart_Click(object sender, EventArgs e)
        {
            stringDataToChart(rtbSignal.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int channels = angleForm.getSignalManager().Channels;
            //  cntEvent.Wait();
            if (!isRecording)
            {
                if (comboAsioDrivers.SelectedIndex == -1) return;
                toggleRecordButton();
                int deviceIdA = comboWaveInDeviceA.SelectedIndex;
                int deviceIdB = comboWaveInDeviceB.SelectedIndex;
                int driverId = comboAsioDrivers.SelectedIndex;
                //bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44000, 1));
                try
                {
                    signalFromMicrophonesA = null;
                    signalFromMicrophonesB = null;

                    if (rbWaveIn.Checked)
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
                        waveInB.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailableB);
                        //Прикрепляем обработчик завершения записи
                        waveInA.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStoppedA);
                        waveInB.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStoppedB);
                        //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                        waveInA.WaveFormat = new WaveFormat(angleForm.getSignalManager().SamplingRate, angleForm.getSignalManager().Channels);
                        waveInB.WaveFormat = new WaveFormat(angleForm.getSignalManager().SamplingRate, angleForm.getSignalManager().Channels);
                        waveInA.BufferMilliseconds = 100;
                        waveInB.BufferMilliseconds = 100;
                        //Инициализируем объект WaveFileWriter
                        // writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                        //Начало записи
                        stopwatch = new Stopwatch();
                        stopwatch.Start();
                        waveInA.StartRecording();
                        waveInB.StartRecording();

                    }
                    else
                    {
                        ///ASIO
                        ///
                        AsioData = new List<byte[]>[channels];
                       aggregatedAsio = new List<float[]>(); //LRLRLRLR;
                        aggregatedAsioCount = 0;

                        asioTotalBytesCount = new int[channels];
                        for (int i = 0; i < AsioData.Length; i++)
                        {
                            AsioData[i] = new List<byte[]>();
                            asioTotalBytesCount[i] = 0;
                        }
                       

                        recAsio = new NAudio.Wave.AsioOut(driverId);
                       
                        Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||");
                        Console.WriteLine("Input channels:");
                       for (int i = 0; i < recAsio.DriverInputChannelCount; i++)
                        {

                                Console.WriteLine(recAsio.AsioInputChannelName(i));
                        }
                        Console.WriteLine("Output channels:");
                        for (int i = 0; i < recAsio.DriverOutputChannelCount; i++)
                        {
                                Console.WriteLine(recAsio.AsioOutputChannelName(i));
                        }
                        Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||");
                        //recAsio2 = new NAudio.Wave.AsioOut(driverId);
                        //recAsio2.ChannelOffset = 1;
                        NAudio.Wave.WaveFormat formato = new NAudio.Wave.WaveFormat(angleForm.getSignalManager().SamplingRate, channels);
                       buffer = new NAudio.Wave.BufferedWaveProvider(formato);
                        recAsio.AudioAvailable += new EventHandler<NAudio.Wave.AsioAudioAvailableEventArgs>(waveIn_DataAvailableAsioTestA);
                        //recAsio2.AudioAvailable += new EventHandler<NAudio.Wave.AsioAudioAvailableEventArgs>(waveIn_DataAvailableAsioTestB);
                      
                        recAsio.InitRecordAndPlayback(null, channels, angleForm.getSignalManager().SamplingRate); //rec channel = 1
                                                                                      // recAsio2.InitRecordAndPlayback(null, angleForm.getSignalManager().Mn.Count, SAMPLING_RATE); //rec channel = 1
                        recAsio.PlaybackStopped += new EventHandler<StoppedEventArgs>(asio_RecordingStoppedA);
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

        private void btnSignalToChart_Click(object sender, EventArgs e)
        {
            if (MicsSignal!=null)
            signalDataToChart(MicsSignal);
            else MessageBox.Show("Signal data is empty!");
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
