using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MicAngle
{
   public static class MyGraphics
    {
        public static void DrawCircle(this Graphics g, Pen pen,
                                 float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
                                      float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public static void DrawCircle(this Graphics g, Pen pen,
                                double centerX, double centerY, double radius)
        {
            g.DrawEllipse(pen, (float)(centerX - radius), (float)(centerY - radius),
                          (float)(radius + radius), (float)(radius + radius));
        }

        public static void FillCircle(this Graphics g, Brush brush,
                                      double centerX, double centerY, double radius)
        {
            g.FillEllipse(brush, (float)(centerX - radius), (float)(centerY - radius),
                          (float)(radius + radius), (float)(radius + radius));
        }

    }
}
