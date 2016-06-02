using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MicAngle
{
    public class SignalsManager
    {
        static short MAX_SHORT = 32767;//short.MaxValue;//32767
        public int SamplingRate { get; set; }
	public static double V = 300;
   public List<SoundEmiter> Sn { get; set; }
     public List<Microphone> Mn { get; set; }
        public int Channels { get; set; }
        public int ChannelOffset { get; set; }
        public int[] MicrophonesShift { get; set; }
        public int[] MicrophonesDelay { get; set; }

    public SignalsManager(){
		Sn = new List<SoundEmiter>();
		Mn = new List<Microphone>();
            ChannelOffset = 0;
            Channels = 2;
            MicrophonesShift = new int[]{0,0};
        }
        public double getMaxMicsDelayTime(double distance)
        {
            return distance / V ;
        }
        public int convertTimeToSamplesCount(double seconds)
        {
            return (int)Math.Ceiling(seconds * SamplingRate + 0.51);
        }
        public int getMaxMicsDelaySamples(double distance)
        {
            double timeDelay = getMaxMicsDelayTime(distance);
            int samplesDelay = convertTimeToSamplesCount(timeDelay);
            return samplesDelay;
        }
        public int getMaxMicDelay(int mic1Index,int mic2Index, out double distance)
        {
            int firstMicrophoneIndex = 0;
            int lastMicrophoneIndex = Mn.Count - 1;
            double L = SignalsManager.getDistance(Mn[mic1Index].X, Mn[mic1Index].Y, Mn[mic2Index].X, Mn[mic2Index].Y);
            distance = L;
            return getMaxMicsDelaySamples(L);
        }
       
        public void clear()
        {
            Sn.Clear();
            Mn.Clear();
        }
	public static int generateSignal(int t,double A,int F,int samplingRate){
		int signal = (int) (A * Math.Sin(2.0*Math.PI*t*F/samplingRate)*(double)MAX_SHORT+A/2);
		return signal;
	}
    public static void shiftMultidimensionalRight(int[,] source,int dimensionI,int n)
    {
            // int []result = new int[source.Length];

            for (int j = source.GetLength(1)-1; j >= 0; j--)
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
                    source[dimensionI, j] = source[dimensionI, j+n];
 
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


        // Виконує розрахунок затримок проходження сигналу (t=s/v) від мікрофонів до джерел звуку
        // @return Двувимірний масив з затримкою від Мікрофони[i] до Звуки[j]


        static public long[] toSM(int[] arr1,int[] arr2,bool isCorelation){
		long[] result = new long [arr1.Length];
		if (isCorelation)
		{
		for (int i=0;i<result.Length;i++)
			result[i]=1;
		for (int i=0;i<result.Length;i++)
				result[i]=arr1[i]*arr2[i];
		}
		else
			for (int i=0;i<result.Length;i++)
				result[i]=arr1[i]+arr2[i];
		
		return result;
				
	}

        public double ComputeCoeff(double[] values1, double[] values2)
{
    if(values1.Length != values2.Length)
        throw new ArgumentException("values must be the same length");

    var avg1 = values1.Average();
    var avg2 = values2.Average();

    var sum1 = values1.Zip(values2, (x1, y1) => (x1 - avg1) * (y1 - avg2)).Sum();

    var sumSqr1 = values1.Sum(x => Math.Pow((x - avg1), 2.0));
    var sumSqr2 = values2.Sum(y => Math.Pow((y - avg2), 2.0));

    var result = sum1 / Math.Sqrt(sumSqr1 * sumSqr2);

    return result;
}

       public long getCrossCorrelation(int [,]source, int indexOfFirstArray, int indexOfSecondArray,int firstShift,int secondShift,int maxShiftValue)
        {
            long result = 0;
            int emptyElement = 1; //Take this if out of range after shift
            int totalArraysCount = source.GetLength(0);
                if (indexOfFirstArray >= totalArraysCount || indexOfSecondArray >= totalArraysCount) throw new IndexOutOfRangeException("uncorrect index");
            int startIndex = maxShiftValue;
            int endindex = source.GetLength(1) - maxShiftValue -1;
            for (int i = startIndex; i <= endindex; i++)
                {
               int elementOfFirstArrayIndex = i - firstShift; //minus sign because of shift right if positive
                int elementOfFirstArray = 0;
                if (elementOfFirstArrayIndex < 0 || elementOfFirstArrayIndex > source.GetLength(1)-1) elementOfFirstArray = emptyElement;
                else
                elementOfFirstArray = source[indexOfFirstArray, elementOfFirstArrayIndex];

                int elementOfSecondArrayIndex = i - secondShift;
                int elementOfSecondArray = 0;
                if (elementOfSecondArrayIndex < 0 || elementOfSecondArrayIndex > source.GetLength(1)-1) elementOfSecondArray = emptyElement;
                else
                elementOfSecondArray = source[indexOfSecondArray, elementOfSecondArrayIndex];

                result += Math.Abs(elementOfFirstArray * elementOfSecondArray);
            }
            return Math.Abs(result);
        }


        public CorrelationStatistic[] getMaxCrossCorrelationFromMicSignals(int[,] buf, out bool success, int[] delays, out long[,] correlationDetailsNegative, out long[,] correlationDetailsPositive)
        {
            //TEST - replacing original signal by random values and moving it to another array with shift to check if 
            //my alhorithm working correctly
            /* int[] originalValues = new int[buf_.GetLength(1)];

            Random rnd = new Random();
            for (int i = 0; i < originalValues.Length; i++)
            {
                originalValues[i] = rnd.Next(1,short.MaxValue);
            }
           int[] shiftedValues = MyUtils.shiftLeft(originalValues,4);


            int [,]buf = new int[2,originalValues.Length];
            for (int i = 0; i < originalValues.Length; i++)
            {
                buf[0, i] = originalValues[i];
                buf[1, i] = shiftedValues[i];
            }*/
            //END Of test

            success = true;
            int correlationStatisticSize = (buf.GetLength(0) - 1) ;//TODO
            int MAXIMAL_SHIFT_TO_SAVE = 100;
            correlationDetailsPositive = new long[buf.GetLength(0), MAXIMAL_SHIFT_TO_SAVE];
            correlationDetailsNegative = new long[buf.GetLength(0), MAXIMAL_SHIFT_TO_SAVE];
            CorrelationStatistic[] maxCorrelationValues = new CorrelationStatistic[correlationStatisticSize];//N-1 where N is microphones count;
            for (int i = 1; i < buf.GetLength(0); i++)
            {
                double maxDistance = 0;
                int maxShiftsCount = getMaxMicDelay(0, i, out maxDistance);
                long currentMicsPareMaxCorrelationValue = 0;
                int currentMicsPareMaxCorrelationShift = 0;
                for (int j = 0; j < maxShiftsCount; j++)
                {
                    int shiftStep = MicrophonesDelay[i];
                    int actualShift = j * shiftStep;
                    long correlation1 = getCrossCorrelation(buf, 0, i,0,actualShift, shiftStep* maxShiftsCount);
                    long correlation2 = getCrossCorrelation(buf, 0, i, 0, -actualShift, shiftStep * maxShiftsCount);
                    if (j < MAXIMAL_SHIFT_TO_SAVE) { 
                    correlationDetailsPositive[i, j] = correlation1;
                    correlationDetailsNegative[i, j] = correlation2;
                    }

                    if (correlation1 >= correlation2)
                    {
                        if (correlation1 > currentMicsPareMaxCorrelationValue)
                        {
                            currentMicsPareMaxCorrelationValue = correlation1;
                            currentMicsPareMaxCorrelationShift = j;
                        }
                    }
                    else
                    {
                        if (correlation2 > currentMicsPareMaxCorrelationValue)
                        {
                            currentMicsPareMaxCorrelationValue = correlation2;
                            currentMicsPareMaxCorrelationShift = -j;
                        }
                    }
                    //Console.WriteLine("shift:" + currentMicsPareMaxCorrelationShift + " val:" + currentMicsPareMaxCorrelationValue);


                }
                maxCorrelationValues[i - 1] = new CorrelationStatistic(currentMicsPareMaxCorrelationShift, currentMicsPareMaxCorrelationValue, maxDistance);
              
            }
            return maxCorrelationValues;     

		}

        public double getAngleFromDelay(int delay, double lengthBetweenMics)
        {
            
            double cosA = V * delay / (lengthBetweenMics * SamplingRate);
            double arcCosA = Math.Acos(cosA);
            double angle =  arcCosA * 180 / Math.PI;
         //   if (delay < 0) angle = 360-angle;
            return angle;
        }
        public double[] getRealAngles()
        {
            double[] angles = new double[Mn.Count-1];
            for (int i=0; i < angles.Length; i++)
            {
                angles[i] = MyUtils.angleBetweenThreePoints(Mn[0].Position, Mn[i+1].Position, Sn[0].Position);
            }
            return angles;
        }
	 
	public static double getDistance(double x1, double y1,double x2,double y2)
	{
		double result = Math.Sqrt((x2-x1)*(x2-x1)+(y2-y1)*(y2-y1));
		return result;
	}

   public void addMicrophoneFromString(String testString)
		{
			//Звук(X:-1;y:1.0;D:1.0)
			
			//String micPattern = new String("^Звук[(]x:\\d+;y:\\d+;A:\\d+[)]$");
			//String testString = new String("Звук(x:0;y:10;А:10)");
			 // String micPattern = new String("М[(][x,X]:(-?([0-9]+.)?[0-9]+);[y,Y]:(-?([0-9]+.)?[0-9]+);[d,D]:(-?([0-9]+.)?[0-9]+)[)]");
		 String micPattern = "М[(][x,X]:(-?([0-9]+.)?[0-9]+);[y,Y]:(-?([0-9]+.)?[0-9]+)[)]";
			Regex newReg = new Regex(micPattern);
			MatchCollection  matches = newReg.Matches(testString);
			bool isCorrect=false;
			double x=0,y=0,d=0;
         
			foreach(Match m in matches)
			{
				isCorrect=true;
				x = Double.Parse(m.Groups[1].Value);
                y = Double.Parse(m.Groups[3].Value);
				 Mn.Add(new Microphone(x,y));
			}
			if (!isCorrect)Console.Out.WriteLine("Невірні данні");
			//Звук(x:0;y:10;А:10)
			
		}
       public void getChannelsFromString(String str)
        {
            String micPattern = @"[cC][hH][aA][nN]{1,2}[eE][lL][sS]\(([123456789]{1})\)";
            Regex newReg = new Regex(micPattern);
            MatchCollection matches = newReg.Matches(str);
            bool isCorrect = false;

            foreach (Match m in matches)
            {
                Channels =  Int32.Parse(m.Groups[1].Value);
                isCorrect = true;
            }
            if (!isCorrect) Console.Out.WriteLine("Невірні данні");
        }
        public void getChannelsOffset(String str)
        {
            String micPattern = @"[cC][hH][aA][nN]{1,2}[eE][lL][oO][fF]{1,2}[Ss][Ee][Tt]\(([123456789]{1})\)";
            Regex newReg = new Regex(micPattern);
            MatchCollection matches = newReg.Matches(str);
            bool isCorrect = false;

            foreach (Match m in matches)
            {
                ChannelOffset = Int32.Parse(m.Groups[1].Value);
                isCorrect = true;
            }
            if (!isCorrect) Console.Out.WriteLine("Невірні данні");
        }
        public void getMicrophonesShift(String str)
        {
            String micPattern = @"[sS][hH][iI][fF][tT]\((-?\d+(?:[,]-?\d+)*)\)";
            //shift(num,num,num)
            Regex newReg = new Regex(micPattern);
            MatchCollection matches = newReg.Matches(str);
            if (matches.Count < 1) return;
            Match m = matches[0];
            String shiftsString = m.Groups[1].Value;
            MicrophonesShift =  MyUtils.parseArray(shiftsString);

        }
        public int[,] alignAndCombineSignalData(int[,] source,int micDataIndex, int spareMicDataIndex )
        {
            int[,] resultArray = new int[source.GetLength(0) - 1, source.GetLength(1)];
            int micIndex = 0;

            long maxCorrelation = 0;
            int maxIndex = 0;
            for (int i = 0; i < 45; i++)
            {
               long correlation =  getCrossCorrelation(source, spareMicDataIndex, micDataIndex, 0, i, 45);
                if (correlation > maxCorrelation)
                {
                    maxCorrelation = correlation;
                    maxIndex = i;
                }
            }

            for (int i=0; i < source.GetLength(0); i++)
            {
                if (i == spareMicDataIndex) continue;
                for (int j = 0; j < source.GetLength(0); j++)
                {
                    resultArray[micIndex,j] = source[i,j];
                }
                micIndex++;


            }
            shiftMultidimensional(resultArray, micDataIndex, maxIndex);
            return resultArray;
        }
        public void getMicrophonesDelays(String str)
        {
            String micPattern = @"[dD][eE][lL][aA][yY][sS]\((-?\d+(?:[,]-?\d+)*)\)";
            //shift(num,num,num)
            Regex newReg = new Regex(micPattern);
            MatchCollection matches = newReg.Matches(str);
            if (matches.Count < 1) return;
            Match m = matches[0];
            String shiftsString = m.Groups[1].Value;
            MicrophonesDelay= MyUtils.parseArray(shiftsString);

        }

        public void addSoundEmiterFromString(String testString)
	{
            //String micPattern = new String("^Звук[(]x:\\d+;y:\\d+;A:\\d+[)]$");
            //String testString = new String("Звук(x:0;y:10;А:10)");
            // String micPattern = new String("З[(][x,X]:(-?([0-9]+.)?[0-9]+);[y,Y]:(-?([0-9]+.)?[0-9]+);[a,A]:(-?([0-9]+.)?[0-9]+)[)]");
            String micPattern = "З[(][x,X]:(-?([0-9]+.)?[0-9]+);[y,Y]:(-?([0-9]+.)?[0-9]+);[a,A]:(-?([0-9]+.)?[0-9]+)[)]";                                                                          
            Regex newReg = new Regex(micPattern);
			MatchCollection  matches = newReg.Matches(testString);
		bool isCorrect=false;
		double x=0,y,a;
		foreach(Match m in matches)
		{
			isCorrect=true;
			x = Double.Parse(m.Groups[1].Value);
			y = Double.Parse(m.Groups[3].Value);
			 a = double.Parse(m.Groups[5].Value);
			 Sn.Add(new SoundEmiter(x,y,a));
		}
		if (!isCorrect)Console.Out.WriteLine("Невірні данні");
		//Звук(x:0;y:10;А:10)
		
	}
        public void addSamplingRateFromString(String testString)
        {
            String micPattern = "[sS][aA][mM][pP][lL][iI][nN][gG][rR][aA][tT][eE][(]([0-9]+)[)]";
            Regex newReg = new Regex(micPattern);
            MatchCollection matches = newReg.Matches(testString);
            bool isCorrect = false;
            foreach (Match m in matches)
            {
                isCorrect = true;
                SamplingRate = int.Parse(m.Groups[1].Value);
            }
            if (!isCorrect) Console.Out.WriteLine("Невірні данні");
        }




	/**
	 * Знаходження енергії сигналу
	 * @param buffer
	 * @return
	 */
	public double soundPressureLevel(float[] buffer) {
		double power = 0.0D;
		foreach (float element in buffer) {
		power += element * element;
		}
		double value = Math.Pow(power, 0.5)/ buffer.Length;;
		return 20.0 * Math.Log10(value);
		}
        class CorelationAngle{
		public Double angle{get;set;}
	
	}
	
}

    }

