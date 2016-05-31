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


    }
}
