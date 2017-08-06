using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SimpleAngle
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
        public PointLatLng GeoPosition
        {
            get
            {
                Point latLon = GlobalMercator.MetersToLatLon(Position);
                return new PointLatLng(latLon.X, latLon.Y);
            }
            set
            {
                Point decartPos = GlobalMercator.LatLonToMeters(value.Lng, value.Lat);
               X = decartPos.X; Y = decartPos.Y;
            }
        }
    }
 
}
