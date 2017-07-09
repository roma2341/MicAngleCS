using MicAngle;
using Newtonsoft.Json;
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


        public static int[] getTestData(int count,int shift)
        {
            int[] arr = new int[count];
            for (var i = 0; i < count; i++)
            {
                arr[i] = i;
            }
            arr = MyUtils.shift<int>(arr, shift);
            return arr;
        }

        public static int[,] getTestDataMultidimensional(int count,int dimenison, int shift)
        {
            int[,] arr = new int[2,count];
            Random rand = new Random(DateTime.Now.Millisecond);
            for (var i = 0; i < count; i++)
            {
                int num = rand.Next(-10000, 10000);
                arr[0,i] = num;
                arr[1,i] = num;
            }
            String srcArrStr = JsonConvert.SerializeObject(arr);
            Console.WriteLine("arr:" + srcArrStr);
            shiftMultidimensional(arr, dimenison, shift);

            return DataCorrelation.alignAndCombineSignalData(arr, 0, 1,10);
           // return arr;
        }

        /* public static int[,] alignFourMicrophonesByChannels(int [,] arr,int indexAlignBy)
         {

         }*/




    }

}
