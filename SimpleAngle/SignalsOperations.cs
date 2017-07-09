using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAngle
{
    public class SignalsOperations
    {
        public static int processMaxShiftsCount(double micDistanceDelta, int samplingRate)
        {
            const int SOUND_SPEED = 340;
            return (int)Math.Ceiling(micDistanceDelta * samplingRate / SOUND_SPEED);
        }

        public static void shiftMultidimensionalRight(int[,] source, int dimensionI, int n)
        {
            // int []result = new int[source.Length];

            for (int j = source.GetLength(1) - 1; j >= 0; j--)
            {
                if (j - n < 0) source[dimensionI, j] = 0;
                else
                    source[dimensionI, j] = source[dimensionI, j - n];
            }
            //  return result;
        }
        public static void shiftMultidimensionalLeft(int[,] source, int dimensionI, int n)
        {
            // int []result = new int[source.Length];
            // for (int i = 0; i < 10; i++)
            //    Console.Write(source[dimensionI, i]+" ");


            for (int j = 0; j < source.GetLength(1); j++)
            {
                if (j + n >= source.GetLength(1)) source[dimensionI, j] = 0;
                else
                    source[dimensionI, j] = source[dimensionI, j + n];

            }
            //for (int i = 0; i < 10; i++)
            // Console.Write(source[dimensionI, i] + " ");
            //  return result;
        }
        public static void shiftMultidimensional(int[,] source, int dimensionI, int n)
        {
            if (n == 0) return;
            if (n >= 0) shiftMultidimensionalRight(source, dimensionI, n);
            else shiftMultidimensionalLeft(source, dimensionI, -n);
        }
        public static int[] shiftRight(int[] source, int n)
        {
            int[] result = new int[source.Length];
            for (int j = source.Length - 1; j >= 0; j--)
            {
                if (j - n < 0) result[j] = 0;
                else
                    result[j] = source[j - n];
            }
            return result;
        }

        public static int[] getTestData(int count,int shift)
        {
            int[] arr = new int[count];
            for (var i = 0; i < count; i++)
            {
                int value = i - shift;
                arr[i] = value > -1 ? value : 0;
            }
            return arr;
        }

       /* public static int[,] alignFourMicrophonesByChannels(int [,] arr,int indexAlignBy)
        {
            
        }*/




    }

}
