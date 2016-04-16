using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MicAngle
{
    public class SignalsManager
    {
        public const int SHIFT_COUNT = 48;
        static short MAX_SHORT = 255;//short.MaxValue;//32767
	public static double V =300;
   public List<SoundEmiter> Sn { get; set; }
     public List<Microphone> Mn { get; set; }

	public SignalsManager(){
		Sn = new List<SoundEmiter>();
		Mn = new List<Microphone>();
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
    public static void shiftRight(int[,] source,int dimensionI,int n)
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
        public static void shiftLeft(int[,] source, int dimensionI, int n)
        {
            // int []result = new int[source.Length];
           // Console.WriteLine("SHIFT LEFT TEST...");
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
            //Console.WriteLine("\nSHIFT LEFT TEST END.");
            //  return result;
        }
        public static void shift(int[,] source, int dimensionI, int n)
        {
            if (n == 0) return;
            if (n >= 0) shiftRight(source, dimensionI, n);
            else shiftLeft(source, dimensionI, -n);
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
       //I THINK BUG HERE we need to sum all values not only midle
	 public double  interCorelationFunc(int[,] buf,out bool success,out bool positiveRotation,long[] maxes=null){
            //  const int MIC_COUNT = 2;
            Console.WriteLine("******INTERCORELATION_FUNCTION********");
            for (int i = 0; i < buf.GetLength(0); i++)
            {
                for (int j = 0; j < buf.GetLength(1); j++)
                {
                    Console.Write(buf[i, j] + " ");
                    if (j >= 20) break;
                }
                Console.WriteLine();
            }
            Console.WriteLine("***************************************");
            success = true;
            positiveRotation = true;
         double result = 0;
			int[] delays = new int[]{0,1,2,4};

            /*
            *Визначаєм затримку для повороту планки, але покищо працює лише статично заданий варіант
            for (int i = 1; i < Mn.Count; i++)
            {
                double distanceFromSoundEmiterToMic = getDistance(Mn[0].X, Mn[0].Y, Mn[i].X, Mn[i].Y);
                delays[i] = (int)(distanceFromSoundEmiterToMic * Sn[0].samplingRate / V);
                Console.WriteLine("delay["+i+"]="+delays[i]);
            }
            */
            //long[] SM = new long[buf1.Length];
            ///
            /* int[] buf1Saved =  new int[buf1.Length];
                buf1.CopyTo(buf1Saved,0);
             int[] buf2Saved =  new int[buf2.Length];
                buf2.CopyTo(buf2Saved,0);*/
            int[,] bufSaved = new int[buf.GetLength(0),buf.GetLength(1)];
            /* for (int i = 0; i < bufSaved.GetLength(0); i++)
             {
                 bufSaved[i] = new int[buf[0].Length];
             }*/
            /* for (int i = 0; i < buf.GetLength(0); i++)
             {
                 for (int j = 0; j < buf.GetLength(1); j++)
                     bufSaved[i,j] = buf[i,j];
             }*/
            Array.Copy(buf, 0, bufSaved, 0, buf.Length);
            ///
            //delays[0] = 0;
              //  for (int l = 1; l < MIC_COUNT; l++)
               // {
                   // delays = new int[]{0,1,2,4};
                    int maxIndex = 0;
                    long maxValue = 0;
            double cosA = 0, arcCosA = 0;
            for (int k = SHIFT_COUNT; k < SHIFT_COUNT*2; k++)
                    {
               // Console.WriteLine("k:"+k);
                    long korelKoff = 0;
                //Вертаємо масив в початковий стан без зсувів
                if (k==0 || k==SHIFT_COUNT) Array.Copy(bufSaved, 0, buf, 0, bufSaved.Length);

                // buf[0]=shiftRight(buf[0], delays[l]);
                //  buf[1] = shiftRight(buf[1], delays[l]);
                for (int i = 0; i < buf.GetLength(0); i++)
                        {
                              if (k<SHIFT_COUNT)
                            shift(buf,i, -delays[i]);
                            else
                     shift(buf, i, delays[i]);
                        }

                //  long[] SM = new long[buf.GetLength(1)];

                // for (int i = 0; i < SM.Length; i++)
                //  SM[i] = 1;
                //int middleOfSignalArray = buf.GetLength(1) / 2;
                //   int startIndex = delays.Max() * SHIFT_COUNT;
                // int startIndex = 0;
                //  int endIndex = buf.GetLength(1); //startIndex + elementsToSum;
                int startIndex = 0;//buf.GetLength(1)/2-2000;
                int endIndex = buf.GetLength(1)-1;//buf.GetLength(1) / 2 + 2000;
                // if (k < 0) startIndex = -k - 1;
                //if (k > 0)
                // endIndex -= startIndex;



                for (int j = startIndex; j < endIndex; j++)//for (int j = 27000; j < SM.Length; j++)
                    {
                    long summOfDifferentSignalValues = 1;
                    for (int i = 0; i < Mn.Count; i++)
                        {
                        summOfDifferentSignalValues *= buf[i,j];
                       // Console.WriteLine("buf[" + i + "," + j + "]="+ buf[i, j]);
                        }
                   // Console.WriteLine("summOfDifferentSignalValues:"+ summOfDifferentSignalValues);
                    //summ += (long)Math.Sqrt((double)Math.Abs(summOfDifferentSignalValues));
                    korelKoff += Math.Abs(summOfDifferentSignalValues);
                    }
               // korelKoff /= buf.GetLength(1);
                  //long absSumm = Math.Abs(summ);
                  //summ = (long)Math.Sqrt(absSumm);
                if (maxes != null) maxes[k] = korelKoff;
                 if (Math.Abs(korelKoff) > Math.Abs(maxValue))
                //if (summ > maxValue)
                {
                   Console.Out.WriteLine("max Long:"+long.MaxValue);
                            maxValue = korelKoff;
                            maxIndex = (k-SHIFT_COUNT );
                        }
                Console.Out.WriteLine();
                    Console.Out.WriteLine("K:"+k+ " SUM:" + korelKoff +" MAX_SUM:" + maxValue +
                " MAX_INDEX:" + maxIndex);
                int firstMicrophoneIndex = 0;
                int lastMicrophoneIndex = Mn.Count-1;
                double L = SignalsManager.getDistance(Mn[firstMicrophoneIndex].X, Mn[firstMicrophoneIndex].Y, Mn[lastMicrophoneIndex].X, Mn[lastMicrophoneIndex].Y);
                if (L==0)
                {
                    /*success = false;
                    Array.Copy(bufSaved, 0, buf, 0, bufSaved.Length);
                    return 0;*/
                    L=1;
                }
                // Console.WriteLine("L:" + L);
              
                if (maxIndex >= 0)
                {
                    cosA = V * (double)maxIndex / (L * (double)Sn[0].samplingRate);
                    arcCosA = Math.Acos(cosA);
                    result = arcCosA * 180 / Math.PI;
                    positiveRotation = true;
                }
                else
                {
                    cosA = V * (double)maxIndex / (L * (double)Sn[0].samplingRate);
                    arcCosA = Math.Acos(cosA);
                    result = -arcCosA * 180 / Math.PI;
                    positiveRotation = false;
                }
                
               

               // Console.Out.WriteLine("maxIndex:" + maxIndex + "cos:" + cosA + " Alpha:" + result);
                //System.out.println("summ:"+summ);
            }
            if (cosA > 1)
                success = false;
            



            //buffer=savedBuffer;


            /*for (int i = 0; i < buf.GetLength(0); i++)
            {
                for (int j = 0; j < buf.GetLength(1); j++)
                    buf[i,j] = bufSaved[i,j];
            }*/
            Array.Copy(bufSaved, 0, buf, 0, bufSaved.Length);

            
			return result;
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
        
        public void addSoundEmiterFromString(String testString)
	{	
		//String micPattern = new String("^Звук[(]x:\\d+;y:\\d+;A:\\d+[)]$");
		//String testString = new String("Звук(x:0;y:10;А:10)");
		 // String micPattern = new String("З[(][x,X]:(-?([0-9]+.)?[0-9]+);[y,Y]:(-?([0-9]+.)?[0-9]+);[a,A]:(-?([0-9]+.)?[0-9]+)[)]");
        String micPattern = "З[(][x,X]:(-?([0-9]+.)?[0-9]+);[y,Y]:(-?([0-9]+.)?[0-9]+);[a,A]:(-?([0-9]+.)?[0-9]+);"
                                                                                                   + "[f,F]:(-?([0-9]+.)?[0-9]+)[)]";
            Regex newReg = new Regex(micPattern);
			MatchCollection  matches = newReg.Matches(testString);
		bool isCorrect=false;
		double x=0,y,a;
		int f;
		foreach(Match m in matches)
		{
			isCorrect=true;
			x = Double.Parse(m.Groups[1].Value);
			y = Double.Parse(m.Groups[3].Value);
			 a = double.Parse(m.Groups[5].Value);
			 f = int.Parse(m.Groups[7].Value);
			 Sn.Add(new SoundEmiter(x,y,a,f));
		}
		if (!isCorrect)Console.Out.WriteLine("Невірні данні");
		//Звук(x:0;y:10;А:10)
		
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

