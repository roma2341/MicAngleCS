using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MicAngle
{
    public class SoundEmiter
    {
        public SoundEmiter(double x, double y, double A)
        {
            this.x = x;
            this.y = y;
            this.A = A;
        }
       public double x {get;set;}
         public double y {get;set;}
         public double A {get;set;}
      public  int[] signal {get; set;}

        public int[] generateSignal(Microphone Mn, out bool status,int samplingRate)
	{
		
		int k;
            double distanceFromSoundEmiterToMic = SignalsManager.getDistance(x, y, Mn.X, Mn.Y);
            
            k =(int) (distanceFromSoundEmiterToMic * (double)samplingRate/(double)SignalsManager.V);
           Console.WriteLine("generated signal shift(k):"+k +" and distance:"+ distanceFromSoundEmiterToMic);
            if (signal.Length - k <= 0)
            {
                status = false;
                return null;
            }
            int smLength = signal.Length - k;
            int[] SMn = new int[smLength];
            Array.Copy(signal, k, SMn,0, smLength);
            //SMn=SignalsManager.shiftRight(signal, k);
            status = true;
	return SMn;
	}
        public int processEmiterArr(double timeRange, int samplingRate, int F)
        {
           signal = new int[(int)(samplingRate * timeRange)];
            for (int l = 0; l < signal.Length; l++)
            {
                signal[l] = SignalsManager.generateSignal(l, A, F, samplingRate);
            }
            
            return (int)(samplingRate * timeRange);
        }
        public Point Position
        {
            get { return new Point(x, y); }
            set { x = value.X; y = value.Y; }
        }
        public Point GeoPosition
        {
            get
            {
                return GlobalMercator.MetersToLatLon(Position);
            }
            set
            {
                Point decartPos = GlobalMercator.LatLonToMeters(value.X, value.Y);
                x = decartPos.X; y = decartPos.Y;
            }
        }

    }
}
