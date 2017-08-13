using Newtonsoft.Json;
using SimpleAngle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAngle
{
    public class SignalManager
    {
        static short MAX_SHORT = 32767;

        public int SamplingRate { get; set; } = 41000;
        public static int V { get; set; } = 340;

        public List<SoundEmiter> Sn { get; set; }
        public List<Microphone> Mn { get; set; }

        public SignalManager()
        {
            Sn = new List<SoundEmiter>();
            Mn = new List<Microphone>();
        }

        public SignalManager(int samplingRate)
        {
            this.SamplingRate = samplingRate;
        }

        public static int generateSignal(int t, double A, int F, int samplingRate)
        {
            int signal = (int)(A * Math.Sin(2.0 * Math.PI * t * F / samplingRate) * (double)MAX_SHORT + A / 2);
            return signal;
        }

        public int processSignalElementsCount(int timeRangeMs)
        {
            return SamplingRate * timeRangeMs / 1000;
        }

        public int[] processEmiterArr(int timeRangeMs, int F, int delayShift)
        {
            int elementsCount = processSignalElementsCount(timeRangeMs);
            int[] signal = new int[elementsCount];
            for (int l = 0; l < signal.Length; l++)
            {
                signal[l] = generateSignal(delayShift + l, 1, F, SamplingRate);
            }

            return signal;
        }


        public static SignalManager fromConfig(String config)
        {
            SignalManager manager = new SignalManager();

            int? samplingRateCf = ConfigParser.parseSamplingRate(config);
            if (samplingRateCf.HasValue)
            manager.SamplingRate = samplingRateCf.Value;

            manager.Mn = ConfigParser.parseMicrophones(config);
            manager.Sn = ConfigParser.parseSoundEmiters(config);
            return manager;       
        }

        public int processMaxShiftsCount(double micDistanceDelta)
        {
            return (int)Math.Floor(micDistanceDelta * SamplingRate / V);
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

        public static double getAngleFromDelay(SoundConfig config,double distance,int delay)
        {

            double cosA = config.V * delay / (distance * config.SamplingRate);
            double arcCosA = Math.Acos(cosA);
            double angle = arcCosA * 180 / Math.PI;
            // if (delay < 0)angle = 360-angle;
            return angle;
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

        public static double getDistance(double x1, double y1, double x2, double y2)
        {
            double result = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            return result;
        }

        public double getDistanceBetweenMicrophones(int mic1, int mic2)
        {
            return getDistance(Mn[mic1].Position.X, Mn[mic1].Position.Y,
                Mn[mic2].Position.X, Mn[mic2].Position.Y);
        }

        public double getDistanceFromSoundEmitterToMicrophone(int soundEmitterIndex, int microphoneIndex)
        {
            return getDistance(Mn[microphoneIndex].Position.X, Mn[microphoneIndex].Position.Y,
                Sn[soundEmitterIndex].Position.X, Sn[soundEmitterIndex].Position.Y);
        }

        /* public static int[,] alignFourMicrophonesByChannels(int [,] arr,int indexAlignBy)
         {

         }*/




    }

}
