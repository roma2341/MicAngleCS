using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicAngle
{
    class Geometry
    {
        public static PointLatLng multiplyVector(PointLatLng point, PointLatLng center,double multiplyValue)
        {
            PointLatLng result = new PointLatLng();
            double vectorX = point.Lat - center.Lat;
            double vectorY = point.Lng - center.Lng;
            vectorX *= multiplyValue;
            vectorY *= multiplyValue;
            result.Lat = vectorX + center.Lat;
            result.Lng = vectorY + center.Lng;
            return result;

        }
    }
}
