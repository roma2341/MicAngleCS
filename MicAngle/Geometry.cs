using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MicAngle
{
    class Geometry
    {
        
        public static PointLatLng multiplyVector(PointLatLng point, PointLatLng center,double multiplyValue)
        {
            Point decardPoint = GlobalMercator.LatLonToMeters(point.Lat, point.Lng);
            Point decardCenter = GlobalMercator.LatLonToMeters(center.Lat, center.Lng);
            PointLatLng result = new PointLatLng();
            Point decardResult = new Point();
            double vectorX = decardPoint.X - decardCenter.X;
            double vectorY = decardPoint.Y - decardCenter.Y;
            vectorX *= multiplyValue;
            vectorY *= multiplyValue;
            decardResult.X = vectorX + decardCenter.X;
            decardResult.Y= vectorY + decardCenter.Y;
            Point resultLatLngPoint = GlobalMercator.MetersToLatLon(decardResult);
            result.Lat = resultLatLngPoint.X;
            result.Lng = resultLatLngPoint.Y;
            return result;

        }
    }
}
