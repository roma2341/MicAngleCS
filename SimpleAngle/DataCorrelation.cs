﻿using SimpleAngle.Models;
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




    }
}