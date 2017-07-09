using MicAngle;
using SimpleAngle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAngle
{
    class DataCorrelation
    {
        const int BYTE_IN_SAMPLE = 2;

        public static int[] generateCorrelationArray(int[,] twoDimensional, CorrelationConfig config)
        {
            int[] result = new int[config.ShiftsCount];
            for (int shiftIndex = 0; shiftIndex < config.ShiftsCount; shiftIndex++)
            {
                int summa = 0;
                for (var i = config.ClampedElements; i < twoDimensional.GetLength(1) - config.ClampedElements; i++)
                {
                    int secondMultiplierIndex = config.Inverse ? shiftIndex - shiftIndex : shiftIndex + shiftIndex;
                    summa += twoDimensional[0, shiftIndex] * twoDimensional[1, secondMultiplierIndex];
                }
                result[shiftIndex] = summa;
            }
            return result;
        }


        public static int[,] convertByteArrayToChanneled(byte[] buffer, int channels)
        {
            int[,] result = new int[channels, buffer.Length / channels];
            int i = 0;
            for (int sample = 0; sample < buffer.Length / (channels * BYTE_IN_SAMPLE); sample++)
            {
                result[0, sample] = BitConverter.ToInt16(buffer, i);
                i += BYTE_IN_SAMPLE;
                result[1, sample] = BitConverter.ToInt16(buffer, i);
                i += BYTE_IN_SAMPLE;
            }
            return result;
        }

        public static  long getCrossCorrelation(int[,] source, int indexOfFirstArray, int indexOfSecondArray, int firstShift, int secondShift, int maxShiftValue)
        {
            long result = 0;
            int emptyElement = 1; //Take this if out of range after shift
            int totalArraysCount = source.GetLength(0);
            if (indexOfFirstArray >= totalArraysCount || indexOfSecondArray >= totalArraysCount) throw new IndexOutOfRangeException("uncorrect index");
            int startIndex = maxShiftValue ;
            int endIndex = source.GetLength(1) - maxShiftValue - 1;
            if (startIndex > endIndex) MyUtils.Swap<int>(ref startIndex, ref endIndex);
            for (int i = startIndex; i <= endIndex; i++)
            {
                int elementOfFirstArrayIndex = i - firstShift; //minus sign because of shift right if positive
                int elementOfFirstArray = 0;
                if (elementOfFirstArrayIndex < 0 || elementOfFirstArrayIndex > source.GetLength(1) - 1) elementOfFirstArray = emptyElement;
                else
                    elementOfFirstArray = source[indexOfFirstArray, elementOfFirstArrayIndex];

                int elementOfSecondArrayIndex = i - secondShift;
                int elementOfSecondArray = 0;
                if (elementOfSecondArrayIndex < 0 || elementOfSecondArrayIndex > source.GetLength(1) - 1) elementOfSecondArray = emptyElement;
                else
                    elementOfSecondArray = source[indexOfSecondArray, elementOfSecondArrayIndex];

                result += elementOfFirstArray * elementOfSecondArray;
            }
            return result;
        }

        public static int[,] alignAndCombineSignalData(int[,] source, int spareMicIndex1, int spareMicIndex2, int maxShiftCount)
        {
            int minSpareMicIndex = Math.Min(spareMicIndex1, spareMicIndex2);
            int maxSpareMicIndex = Math.Max(spareMicIndex1, spareMicIndex2);
            int[,] resultArray = new int[source.GetLength(0), source.GetLength(1)];
            int micIndex = 0;

            long maxCorrelation = 0;
            int maxIndex = 0;
            if (maxShiftCount > source.GetLength(1)) throw new IndexOutOfRangeException("max shift can't be greatest than array");
            for (int i = -maxShiftCount; i < maxShiftCount; i++)
            {
                long correlation = getCrossCorrelation(source, minSpareMicIndex, maxSpareMicIndex, 0, i, maxShiftCount);
                if (correlation > maxCorrelation)
                {
                    maxCorrelation = correlation;
                    maxIndex = i;
                }
            }

            Console.WriteLine("Max index:" + maxIndex);

            for (int i = 0; i < source.GetLength(0); i++)
            {
                //if (i == spareMicDataIndex) continue;
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    resultArray[micIndex, j] = source[i, j];
                }
                micIndex++;
            }
            Console.WriteLine("Test");
            resultArray = MyUtils.TrimArrayRow(maxSpareMicIndex, resultArray);
            SignalsOperations.shiftMultidimensional(resultArray, maxSpareMicIndex, maxIndex);
            return resultArray;
        }




    }
}
