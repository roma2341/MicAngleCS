using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public static byte[] shiftRight(byte[] source, int n)
        {
            byte[] result = new byte[source.Length];
            for (int j = source.Length - 1; j >= 0; j--)
            {
                if (j - n < 0) result[j] = 0;
                else
                    result[j] = source[j - n];
            }
            return result;
        }
        public static byte[] shiftLeft(byte[] source, int n)
        {
            byte[] result = new byte[source.Length];
            for (int j = 0; j < source.Length; j++)
            {
                if (j + n >= source.Length) result[j] = 0;
                else
                    result[j] = source[j + n];
            }
            return result;
        }

    }
}
