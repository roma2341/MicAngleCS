using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
