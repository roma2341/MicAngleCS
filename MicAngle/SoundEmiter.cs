using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicAngle
{
    class SoundEmiter
    {
        public SoundEmiter(double x, double y, double A, int samplingRate)
        {
            this.x = x;
            this.y = y;
            this.A = A;
            this.samplingRate = samplingRate;
        }
       public double x {get;set;}
         public double y {get;set;}
         public double A {get;set;}
       public int samplingRate {get;set;}
      public  int[] signal {get; set;}
  
       public int[] generateSignal(Microphone Mn)
	{
		
		int k;
			k=(int) (SignalsManager.getDistance(x, y, Mn.x, Mn.y)*samplingRate/SignalsManager.V);
            int[] SMn = new int[signal.Length - k];
            int smLength = SMn.Length;
            Array.Copy(signal, k, SMn,0, smLength);
           //SMn=SignalsManager.shiftRight(signal, k);
	return SMn;
	}
        public int processEmiterArr(int timeRange, int samplingRate, int F)
        {
           signal = new int[(int)(samplingRate * timeRange)];
            //	double step = 1.0/samplingRate; 
            //	double t=0;
            for (int l = 0; l < signal.Length; l++)
            {
                signal[l] = SignalsManager.generateSignal(l, A, F, samplingRate);
                //Console.WriteLine(signal[l]);
                //Console.Out.WriteLine("FFFFF:" + signal[l]);
            }
            
            return samplingRate * timeRange;
        }

    }
}
