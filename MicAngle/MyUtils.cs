using NAudio.Wave.Asio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace MicAngle
{
    class MyUtils
    {
      public static int[,] convertBytesToMicrophonesSignalArray(byte[] lineA, byte[] lineB,int chanels, int bytesInGroup)
        {
            if (lineA.Length != lineB.Length) return null;
            int[,] signalFromMics = new int[2*chanels, lineA.Length / (chanels* bytesInGroup)];
                int index = 0;
                for (int j = 0; j < signalFromMics.GetLength(1) / 2; j++)
                {
                for (int k = 0; k < chanels; k++)
                {
                    short sampleA = BitConverter.ToInt16(lineA, index);
                    short sampleB = BitConverter.ToInt16(lineB, index);
                    signalFromMics[k, j] = sampleA;
                    signalFromMics[k+ chanels, j] = sampleB;
                    index += 2;
                }
                }
            return signalFromMics;

        }
        public static Point rotate(Point point, Point center, double angle)
        {
            double angleRadians = angle * (Math.PI / 180);
            double s = Math.Sin(angleRadians);
            double c = Math.Cos(angleRadians);
            // translate point back to origin:
            //point.Lat -= center.Lat;
            // point.Lng -= center.Lng;

            // rotate point
            double xnew = (point.X - center.X) * c - (point.Y - center.Y) * s + center.X;
            double ynew = (point.X - center.X) * s + (point.Y - center.Y) * c + center.Y;
            //double xnew = decardPoint.Y * c - decardPoint.X * s;
            //double ynew = decardPoint.Y * s + decardPoint.X * c;

            // translate point back:
            // point.Lat = xnew + center.Lat;
            // point.Lng = ynew + center.Lng;
            Point newPt = new Point();
            newPt.X = xnew;
            newPt.Y = ynew;
            return newPt;
        }
        public static Point rotateAnticlockwise(Point point, Point center, double angle)
        {
            double angleRadians = angle * (Math.PI / 180);
            double s = Math.Sin(angleRadians);
            double c = Math.Cos(angleRadians);
            // translate point back to origin:
            //point.Lat -= center.Lat;
            // point.Lng -= center.Lng;

            // rotate point
            double xnew = (point.X - center.X) * c + (point.Y - center.Y) * s + center.X;
            double ynew = -(point.X - center.X) * s + (point.Y - center.Y) * c + center.Y;
            //double xnew = decardPoint.Y * c - decardPoint.X * s;
            //double ynew = decardPoint.Y * s + decardPoint.X * c;

            // translate point back:
            // point.Lat = xnew + center.Lat;
            // point.Lng = ynew + center.Lng;
            point.X = xnew;
            point.Y = ynew;
            return point;
        }

        public static double angleBetweenThreePoints(Point A, Point B, Point C)
        {
                var AB = Math.Sqrt(Math.Pow(B.X - A.X, 2) + Math.Pow(B.Y - A.Y, 2));
                var BC = Math.Sqrt(Math.Pow(B.X - C.X, 2) + Math.Pow(B.Y - C.Y, 2));
                var AC = Math.Sqrt(Math.Pow(C.X - A.X, 2) + Math.Pow(C.Y - A.Y, 2));
                return Math.Acos((BC * BC + AB * AB - AC * AC) / (2 * BC * AB)) * (180 / Math.PI);
        }
        public static int differenceInTimeToDelay(long difference)
        {
            return (int)(difference * 44100 / 1000000000);
        }
        public static T[] shiftRight<T>(T[] source, int n)
        {
            T[] result = new T[source.Length];
            for (int j = source.Length - 1; j >= 0; j--)
            {
                if (j - n < 0) result[j] = default(T);
                else
                    result[j] = source[j - n];
            }
            return result;
        }
        public static T[] shiftLeft<T>(T[] source, int n)
        {
            T[] result = new T[source.Length];
            for (int j = 0; j < source.Length; j++)
            {
                if (j + n >= source.Length) result[j] = default(T);
                else
                    result[j] = source[j + n];
            }
            return result;
        }
        public static T[] shift<T>(T[] source, int n)
        {
            if (n >= 0) return shiftRight<T>(source, n);
            else return shiftLeft<T>(source, -n);
        }
        public static int[] parseArray(String str)
        {
            String micPattern = @"[-]?[0-9]+";
            Regex newReg = new Regex(micPattern);
            MatchCollection matches = newReg.Matches(str);
            bool isCorrect = false;
            int[] result = new int[matches.Count];
            int index = 0;
            foreach (Match m in matches)
            {
                result[index++] = Int32.Parse(m.Groups[0].Value);
            }
            return result;
        }
        public static int[,] TrimArrayRow(int rowToRemove, int[,] originalArray)
        {
            int[,] result = new int[originalArray.GetLength(0) - 1, originalArray.GetLength(1)];

            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == rowToRemove)
                    continue;

                for (int k = 0; k < originalArray.GetLength(1); k++)
                {
                    result[j, k] = originalArray[i, k];
                }
                j++;
            }

            return result;
        }

        public static int[,] TrimArrayColumn(int columnToRemove, int[,] originalArray)
        {
            int[,] result = new int[originalArray.GetLength(0), originalArray.GetLength(1) - 1];

            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    if (k == columnToRemove)
                        continue;

                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j++;
            }

            return result;
        }

           /// <summary>
        /// Converts all the recorded audio into a buffer of 32 bit floating point samples, interleaved by channel
        /// </summary>
        /// <returns>The samples as 32 bit floating point, interleaved</returns>
        public static float[] GetAsInterleavedSamples(IntPtr[] InputBuffers, int SamplesPerBuffer, AsioSampleType asioSampleType)
        {
            int channels = InputBuffers.Length;
            float[] samples = new float[SamplesPerBuffer * channels];
            int index = 0;
            unsafe
            {
                if (asioSampleType == NAudio.Wave.Asio.AsioSampleType.Int32LSB)
                {
                    for (int n = 0; n < SamplesPerBuffer; n++)
                    {
                        for (int ch = 0; ch < channels; ch++)
                        {
                            samples[index++] = *((int*)InputBuffers[ch] + n) / (float)Int32.MaxValue;
                        }
                    }
                }
                else if (asioSampleType == NAudio.Wave.Asio.AsioSampleType.Int16LSB)
                {
                    for (int n = 0; n < SamplesPerBuffer; n++)
                    {
                        for (int ch = 0; ch < channels; ch++)
                        {
                            samples[index++] = *((short*)InputBuffers[ch] + n) / (float)Int16.MaxValue;
                        }
                    }
                }
                else if (asioSampleType == NAudio.Wave.Asio.AsioSampleType.Int24LSB)
                {
                    for (int n = 0; n < SamplesPerBuffer; n++)
                    {
                        for (int ch = 0; ch < channels; ch++)
                        {
                            byte *pSample = ((byte*)InputBuffers[ch] + n * 3);

                            //int sample = *pSample + *(pSample+1) << 8 + (sbyte)*(pSample+2) << 16;
                            int sample = pSample[0] | (pSample[1] << 8) | ((sbyte)pSample[2] << 16);
                            samples[index++] = sample / 8388608.0f;
                        }
                    }
                }
                else if (asioSampleType == NAudio.Wave.Asio.AsioSampleType.Float32LSB)
                {
                    for (int n = 0; n < SamplesPerBuffer; n++)
                    {
                        for (int ch = 0; ch < channels; ch++)
                        {
                            samples[index++] = *((float*)InputBuffers[ch] + n);
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException(String.Format("ASIO Sample Type {0} not supported", asioSampleType));
                }
            }
            return samples;
        }


        public static int[,] TrimArray(int rowToRemove, int columnToRemove, int[,] originalArray)
        {
            int[,] result = new int[originalArray.GetLength(0) - 1, originalArray.GetLength(1) - 1];

            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == rowToRemove)
                    continue;

                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    if (k == columnToRemove)
                        continue;

                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j++;
            }

            return result;
        }
        public static String arrayToString(int[,] arr, int limitWidth)
        {
            StringBuilder resultStrBuilder = new StringBuilder("");
            //micsSignal = unitePartialMeasurement(signalFromMicrophonesA);
            int width = (limitWidth < arr.GetLength(1)) ? limitWidth : arr.GetLength(1);
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < width; j++)
                {
                    resultStrBuilder.Append(arr[i, j] + " ");
                }
                resultStrBuilder.Append("\n");
            }
            return resultStrBuilder.ToString();
        }
        public static String arrayToString(int[,] arr)
        {
            return arrayToString(arr, int.MaxValue);
        }
       public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }


    }
}
