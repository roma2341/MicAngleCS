using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MicAngle
{
   public class Microphone
    {
        public Microphone(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
       public double X{get; set;}
       public double Y{ get; set;}
        //Decart coord
        public Point Position {
            get { return new Point(X, Y); }
            set { X = value.X; Y = value.Y; }
        }
        public Point GeoPosition
        {
            get
            {
                return GlobalMercator.MetersToLatLon(Position);
            }
            set
            {
                Point decartPos = GlobalMercator.LatLonToMeters(value.X,value.Y);
               X = decartPos.X; Y = decartPos.Y;
            }
        }
    }
 
}
