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

namespace MicAngle
{
    public partial class RecordForm : Form
    {
        #region struct WaveInCaps
        [StructLayout(LayoutKind.Sequential)]
        public struct WaveInCaps
        {
            public short wMid; 	// Код изготовителя драйвера для устройства
            public short wPid; 	// Код устройства, назначенный изготовителем
            public int vDriverVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szPname; 	// Описание устройства 
            public uint dwFormats;	// Флаги, соответствующие стандартным форматам 
            // звуковых данных, с которыми может работать 
            // устройство 
            public short wChannels;	// количество каналов (1 – моно, 2 – стерео) 
        }
        #endregion
        #region struct WAVEFORMAT
        [StructLayout(LayoutKind.Sequential)]
        public struct WAVEFORMAT
        {
            public ushort wFormatTag; //тип аудио-данных
            public ushort nChannels; //определяет количество каналов
            public uint nSamplesPerSec; //определяет норму отбора в секунду
            public uint nAvgBytesPerSec;
            public ushort nBlockAlign; //определяет выравнивание в байтах
            public ushort wBitsPerSample; //определяет количество бит для выборки
            public ushort cbSize; //определяет размер всей структуры
        }
        #endregion
        #region struct WAVEHDR
        [StructLayout(LayoutKind.Sequential)]
        public struct WAVEHDR
        {
            public IntPtr lpData;
            public uint dwBufferLength;
            public uint dwBytesRecorded;
            public IntPtr dwUser;
            public uint dwFlags;
            public uint dwLoops;
            public IntPtr lpNext;
            public IntPtr reserved;
        }
        #endregion
        enum WaveInOpenFlags : uint
        {
            CALLBACK_NULL = 0,
            CALLBACK_FUNCTION = 0x30000,
            CALLBACK_EVENT = 0x50000,
            CALLBACK_WINDOW = 0x10000,
            CALLBACK_THREAD = 0x20000,
            WAVE_FORMAT_QUERY = 1,
            WAVE_MAPPED = 4,
            WAVE_FORMAT_DIRECT = 8
        }
        enum MMRESULT : uint
        {
            MMSYSERR_NOERROR = 0, //успіх
            MMSYSERR_ERROR = 1,
            MMSYSERR_BADDEVICEID = 2,
            MMSYSERR_NOTENABLED = 3,
            MMSYSERR_ALLOCATED = 4,
            MMSYSERR_INVALHANDLE = 5,
            MMSYSERR_NODRIVER = 6,
            MMSYSERR_NOMEM = 7,
            MMSYSERR_NOTSUPPORTED = 8,
            MMSYSERR_BADERRNUM = 9,
            MMSYSERR_INVALFLAG = 10,
            MMSYSERR_INVALPARAM = 11, //базова адреса буфера вирівняний з обсягом вибірки
            MMSYSERR_HANDLEBUSY = 12,
            MMSYSERR_INVALIDALIAS = 13,
            MMSYSERR_BADDB = 14,
            MMSYSERR_KEYNOTFOUND = 15,
            MMSYSERR_READERROR = 16,
            MMSYSERR_WRITEERROR = 17,
            MMSYSERR_DELETEERROR = 18,
            MMSYSERR_VALNOTFOUND = 19,
            MMSYSERR_NODRIVERCB = 20,
            WAVERR_BADFORMAT = 32,
            WAVERR_STILLPLAYING = 33,
            WAVERR_UNPREPARED = 34
        }
        #region waveIn
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern int waveInGetNumDevs();
        [DllImport("winmm.dll", EntryPoint = "waveInGetDevCaps")]
        public static extern int waveInGetDevCaps(int uDeviceID, ref WaveInCaps lpCaps, int uSize);
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern MMRESULT waveInOpen(ref IntPtr hWaveIn, uint deviceId, ref WAVEFORMAT wfx, IntPtr dwCallBack, uint dwInstance, WaveInOpenFlags dwFlags);
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern MMRESULT waveInAddBuffer(IntPtr hWaveIn, ref WAVEHDR lpWaveHdr, uint cWaveHdrSize);
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern MMRESULT waveInPrepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveHdr, uint Size);
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern MMRESULT waveInUnprepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveHdr, uint Size);
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern MMRESULT waveInStart(IntPtr hWaveIn);
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern MMRESULT waveInClose(IntPtr hWaveIn);
        #endregion
        public RecordForm()
        {
            InitializeComponent();
            int waveInDevicesCount = waveInGetNumDevs();
            for (int uDeviceID = 0; uDeviceID < waveInDevicesCount; uDeviceID++)
            {
                WaveInCaps waveInCaps = new WaveInCaps();
                waveInGetDevCaps(uDeviceID, ref waveInCaps, Marshal.SizeOf(typeof(WaveInCaps)));
                String szPname = new string(waveInCaps.szPname).Remove(new string(waveInCaps.szPname).IndexOf('\0')).Trim() + ")";
                comboBox1.Items.Add(szPname);
            }
        }
        public class readSound
        {
            private const ushort WAVE_FORMAT_PCM = 0x0001;
            private IntPtr hwWaveIn = IntPtr.Zero;
            private IntPtr dwCallBack = IntPtr.Zero;
            private GCHandle bufferPin;
            public byte[] buffer;
            public double[] valueBufferL = new double[24000];
            public double[] valueBufferR = new double[24000];
            public void readMic(uint deviceId)
            {
                WAVEFORMAT waveFormat;
                waveFormat = new WAVEFORMAT();
                waveFormat.wFormatTag = WAVE_FORMAT_PCM;
                waveFormat.nChannels = 2;
                waveFormat.wBitsPerSample = 16;
                waveFormat.nBlockAlign = (ushort)((waveFormat.nChannels * waveFormat.wBitsPerSample) / 8);
                waveFormat.nSamplesPerSec = 44100;
                waveFormat.nAvgBytesPerSec = waveFormat.nSamplesPerSec * waveFormat.nBlockAlign;
                waveFormat.cbSize = 0;
                buffer = new byte[waveFormat.nAvgBytesPerSec];
                bufferPin = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                WAVEHDR waveInHdr;
                waveInHdr.lpData = bufferPin.AddrOfPinnedObject();
                waveInHdr.dwBufferLength = waveFormat.nAvgBytesPerSec;
                waveInHdr.dwFlags = 0;
                waveInHdr.dwBytesRecorded = 0;
                waveInHdr.dwLoops = 0;
                waveInHdr.dwUser = IntPtr.Zero;
                waveInHdr.lpNext = IntPtr.Zero;
                waveInHdr.reserved = IntPtr.Zero;
                MMRESULT res = waveInOpen(ref hwWaveIn, deviceId, ref waveFormat, dwCallBack, 0, WaveInOpenFlags.CALLBACK_NULL);
                res = waveInPrepareHeader(hwWaveIn, ref waveInHdr, Convert.ToUInt32(Marshal.SizeOf(waveInHdr)));
                res = waveInAddBuffer(hwWaveIn, ref waveInHdr, Convert.ToUInt32(Marshal.SizeOf(waveInHdr)));
                res = waveInStart(hwWaveIn);
                while (waveInUnprepareHeader(hwWaveIn, ref waveInHdr, Convert.ToUInt32(Marshal.SizeOf(waveInHdr))) == MMRESULT.WAVERR_STILLPLAYING) { }
                res = waveInClose(hwWaveIn);
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
        public readSound a = new readSound();


        private void button1_Click_1(object sender, EventArgs e)
        {

            CountdownEvent cntEvent = new CountdownEvent(1);
            uint aMicId = Convert.ToUInt32(comboBox1.SelectedIndex);
            Thread sound1 = new Thread(delegate () { a.readMic(aMicId); cntEvent.Signal(); });
            sound1.Start();
            cntEvent.Wait();
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            int n = 0;
            for (int i = 0; i < 4000; i++)
            {
                short l = (short)((a.buffer[n + 1] << 8) | a.buffer[n + 0]);
                short r = (short)((a.buffer[n + 3] << 8) | a.buffer[n + 2]);
                chart1.Series[0].Points.AddXY(i, l);
                chart1.Series[1].Points.AddXY(i, r);
                n += 4;
            }
            float[] sigL = new float[44100];
            float[] sigR = new float[44100];
            double[] sigLd = new double[44100];
            double[] sigRd = new double[44100];
            int j = 0;
            for (int m = 0; m < (44100 * 4); m += 4)
            {
                sigL[j] = (float)((short)((a.buffer[m + 1] << 8) | a.buffer[m + 0]));
                sigR[j] = (float)((short)((a.buffer[m + 3] << 8) | a.buffer[m + 2]));
                sigLd[j] = (double)((short)((a.buffer[m + 1] << 8) | a.buffer[m + 0]));
                sigRd[j] = (double)((short)((a.buffer[m + 3] << 8) | a.buffer[m + 2]));
                j++;
            }
            chart2.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart2.Series[0].Points.Clear();
            chart3.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart3.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart3.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart3.Series[0].Points.Clear();
            float[] sigLb = new float[44000];
            float[] sigRb = new float[44000];
            double[] sigLbd = new double[44000];
            double[] sigRbd = new double[44000];
            Array.Copy(sigL, 49, sigLb, 0, 44000);
            Array.Copy(sigLd, 49, sigLbd, 0, 44000);
            for (int i = -39; i < 40; i++)
            {
                Array.Copy(sigR, 49 + i, sigRb, 0, 44000);
                Array.Copy(sigRd, 49 + i, sigRbd, 0, 44000);
                chart2.Series[0].Points.AddXY(i, ComputeCoeff(sigLb, sigRb));
                chart3.Series[0].Points.AddXY(i, ComputeCoeffd(sigLbd, sigRbd));
            }
        }

        public T[,] getSignalFromMics<T>() where T : IConvertible
        {
            CountdownEvent cntEvent = new CountdownEvent(1);
            if (comboBox1.SelectedIndex == -1) return null;
            uint aMicId = Convert.ToUInt32(comboBox1.SelectedIndex);
            if (comboBox1.GetItemText(comboBox1.SelectedItem)=="")return null;
            Thread sound1 = new Thread(delegate () { a.readMic(aMicId); cntEvent.Signal(); });
            sound1.Start();
            cntEvent.Wait();
            int n = 0;
            T[,] signal = new T[2,44100];
            for (int i = 0; i < 4000; i++)
            {
                signal[0,n] = (T)(object)((a.buffer[n + 1] << 8) | a.buffer[n + 0]);
                signal[1,n] = (T)(object)((a.buffer[n + 3] << 8) | a.buffer[n + 2]);
                n += 4;
            }
            return signal;

        }

    }
}
