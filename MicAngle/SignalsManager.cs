using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MicAngle
{
    class SignalsManager
    {
    static short MAX_SHORT = 32767;//32767
	public static double V =300;
   public List<SoundEmiter> Sn { get; set; }
    public List<Microphone> Mn { get; set; }

	public SignalsManager(){
		Sn = new List<SoundEmiter>();
		Mn = new List<Microphone>();
	}
	public static int generateSignal(int t,double A,int F,int samplingRate){
		int signal = (int) (A * Math.Sin(2.0*Math.PI*t*F/samplingRate)*(double)MAX_SHORT+A/2);
		return signal;
	}
    public static void shiftRight(int[,] source,int i,int n)
    {
            // int []result = new int[source.Length];
          
            for (int j = source.GetLength(1)-1; j >= 0; j--)
        {
            if (j - n < 0) source[i,j] = 0;
            else
            source[i,j] = source[i,j - n];
        }
            Console.WriteLine("shift right");
            //  return result;
        }
        public static void shiftLeft(int[,] source, int i, int n)
        {
            // int []result = new int[source.Length];
           
            for (int j = 0; j < source.GetLength(1); j++)
            {
                if (j - n >= 0) 
                     source[i, j - n] = source[i, j];
            }
            Console.WriteLine("shift left");
            //  return result;
        }
        public static void shift(int[,] source, int i, int n)
        {
            if (n == 0) return;
            if (n >= 0) shiftRight(source, i, n);
            else shiftLeft(source, i, -n);
        }
        public static int[] shiftRight(int[] source, int n)
        {
            Console.WriteLine("shift right");
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
	 public double  interCorelationFunc(int[,] buf,long[] maxes=null,int leftBorder=0,int rightBorder=0){
       //  const int MIC_COUNT = 2;
		 const int SHIFT_COUNT = 48;
         double result = 0;
			int[] delays = new int[]{0,1,2,4};
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
                    
                    for (int k = -SHIFT_COUNT; k < SHIFT_COUNT; k++)
                    {
               // Console.WriteLine("k:"+k);
                    long summ = 0;
                if (k==0) Array.Copy(bufSaved, 0, buf, 0, bufSaved.Length);

                // buf[0]=shiftRight(buf[0], delays[l]);
                //  buf[1] = shiftRight(buf[1], delays[l]);
                for (int i = 0; i < buf.GetLength(0); i++)
                        {
                              if (k<0)
                            shift(buf,i, -delays[i]);
                            else
                     shift(buf, i, delays[i]);
                    }

                //  long[] SM = new long[buf.GetLength(1)];
                
                       // for (int i = 0; i < SM.Length; i++)
                         //  SM[i] = 1;
                        for (int j = 0+leftBorder; j < buf.GetLength(1) - rightBorder; j++)//for (int j = 27000; j < SM.Length; j++)
                    {
                    long summOfDifferentSignalValues = 1;
                    for (int i = 0; i < Mn.Count; i++)
                        {
                        summOfDifferentSignalValues *= buf[i,j];
                           // Console.Write(buf[i][j]+" ");
                        }
                                summ += summOfDifferentSignalValues;
                              //  Console.Out.WriteLine("sum:" + summ);
                            }
                    //  Console.Out.WriteLine("MaxValue:" + maxValue);
                    if (maxes != null) maxes[k+SHIFT_COUNT] = summ;
               // Console.Out.WriteLine("before comparsion of  summ:" + summ + " and maxValue:" + maxValue);
                if (Math.Abs(summ) > Math.Abs(maxValue))//Брати абсолютне значенння
                //   if (summ > maxValue)
                        {
                   // Console.Out.WriteLine("index " + k+">"+maxIndex);
                    maxValue = summ;
                            maxIndex = (k );
                        }
                   
                    Console.Out.WriteLine("SUM:" + summ);
                    Console.Out.WriteLine("MAX_SUM:" + maxValue);
                    Console.Out.WriteLine("MAX_INDEX:" + maxIndex);
                    //System.out.println("summ:"+summ);
                }
              
                double L = SignalsManager.getDistance(Mn[0].x, Mn[0].y, Mn[1].x, Mn[1].y);
                    double cosA = V * (double)maxIndex / (L * (double)Sn[0].samplingRate);
                    double arcCosA = Math.Acos(cosA);
                result = arcCosA * 180 / Math.PI;

                    Console.Out.WriteLine("maxIndex:" + maxIndex + "cos:" + cosA + " Alpha:" + result);

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

