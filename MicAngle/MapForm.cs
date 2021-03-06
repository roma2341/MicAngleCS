﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MicAngle
{
    public partial class MapForm : Form
    {
        Form1 parent;
        public int testAngle = 0;
        public SignalsManager signalsManger { get; set; }
        enum CoordTypeMode {COORD_MODE_DECART,COORD_MODE_GEO};
        CoordTypeMode coordTypeMode = CoordTypeMode.COORD_MODE_GEO;
        public MapForm(Form1 parent, SignalsManager sm)
        {
            InitializeComponent();
            this.parent = parent;
            signalsManger = sm;
        }

        private void MapForm_Load(object sender, EventArgs e)
        {
            mapControl.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            mapControl.Bearing = 150;
            // mapControl.SetCurrentPositionByKeywords("Maputo, Mozambique");
        }

        private bool readGeoCoord(ref System.Windows.Point pt)
        {
            string longtitudeStr = tbLongtitude.Text;
            string latitudeStr = tbLatitude.Text;
            if (longtitudeStr.Length == 0 || latitudeStr.Length == 0) return false;
            double longtitude, latitude;
            bool resultOfParsing;
            resultOfParsing = Double.TryParse(longtitudeStr, out longtitude);
            if (resultOfParsing)
            {
                resultOfParsing = Double.TryParse(latitudeStr, out latitude);
                if (!resultOfParsing) return false;
            }
            else return false;
            pt.X = latitude;
            pt.Y = longtitude;
            return true;
        }

        private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void btnTestMap_Click(object sender, EventArgs e)
        {
            System.Windows.Point coord = new System.Windows.Point();
            if(!readGeoCoord(ref coord)) return;
           
            ////
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(-25.966688, 32.580528),
              GMarkerGoogleType.green);
            mapControl.Overlays.Clear();
           markersOverlay.Markers.Add(marker);
            mapControl.Overlays.Add(markersOverlay);
            mapControl.Position = new PointLatLng(coord.X, coord.Y);

            //mapBrowser.Url = imgTagSrc;
        }

        private void rbCoordType_Clicked(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

           if (rbDecart.Checked)
            {
                if (coordTypeMode == CoordTypeMode.COORD_MODE_DECART) return;
                Console.WriteLine("DECARD");
                System.Windows.Point geoCoord = new System.Windows.Point();
                if (!readGeoCoord(ref geoCoord))
                {
                    rbGeo.Checked = true;
                    coordTypeMode = CoordTypeMode.COORD_MODE_GEO;
                    return;
                }
                System.Windows.Point decardCoord = GlobalMercator.LatLonToMeters(geoCoord.X, geoCoord.Y);
                tbLatitude.Text = decardCoord.X.ToString();
                tbLongtitude.Text = decardCoord.Y.ToString();
               
                coordTypeMode = CoordTypeMode.COORD_MODE_DECART;
            }
           else 
            {
                if (coordTypeMode == CoordTypeMode.COORD_MODE_GEO) return;
                Console.WriteLine("GEO");
                System.Windows.Point decardCoord = new System.Windows.Point();
                if (!readGeoCoord(ref decardCoord))
                {
                    rbDecart.Checked = true;
                    coordTypeMode = CoordTypeMode.COORD_MODE_DECART;
                    return;
                }
                System.Windows.Point geoCoord = GlobalMercator.MetersToLatLon(decardCoord);

                tbLatitude.Text = geoCoord.X.ToString();
                tbLongtitude.Text = geoCoord.Y.ToString();
                
                coordTypeMode = CoordTypeMode.COORD_MODE_GEO;
            }
        }
        public void drawMicrophones(GMapOverlay markersOverlay)
        {
            int markersCount = 0;
            for (int i = 0; i < signalsManger.Mn.Count(); i++)
            {
                Microphone mic = signalsManger.Mn[i];
                PointLatLng micLocation = new PointLatLng(mic.GeoPosition.Lat, mic.GeoPosition.Lng);
                GMarkerGoogleType markerType;
                switch (markersCount)
                {
                    case 0:
                        markerType = GMarkerGoogleType.red;
                        break;
                    case 1:
                        markerType = GMarkerGoogleType.green;
                        break;
                    case 2:
                        markerType = GMarkerGoogleType.blue;
                        break;
                    default:
                        markerType = GMarkerGoogleType.yellow;
                        break;
                }

                GMarkerGoogle marker = new GMarkerGoogle(micLocation,
                 markerType);
                markersOverlay.Markers.Add(marker);
                markersCount++;
            }
        }
        public void drawSoundEmiter(GMapOverlay markersOverlay)
        {
            PointLatLng soundLocation = new PointLatLng(signalsManger.Sn[0].GeoPosition.X,
                signalsManger.Sn[0].GeoPosition.Y);
            GMarkerGoogle markerOfSoundEmiter = new GMarkerGoogle(soundLocation,
                GMarkerGoogleType.blue_pushpin);
            markersOverlay.Markers.Add(markerOfSoundEmiter);
        }
        public void drawAngles(GMapOverlay markersOverlay,double[] angles)
        {
            int colorPolygonCount = 0;
            for (int i = 0; i < angles.Length; i++)
            {
                double angle = angles[i];
                if (testAngle != 0) angle = testAngle;
                PointLatLng mainMicPos = signalsManger.Mn[0].GeoPosition; //signalsManger.Mn[0].GeoPosition
                PointLatLng secondMicPos = signalsManger.Mn[i+1].GeoPosition;//fiction zero
                PointLatLng directionPos, alternativeDirectionPos;


                    directionPos = rotate(mainMicPos, secondMicPos, angle);
                //else
                   // directionPos = rotate(mainMicPos, secondMicPos, angle);
                alternativeDirectionPos = rotate(mainMicPos, secondMicPos, 360-angle);
                /* PointLatLng vectorFormOfDirection = new PointLatLng(directionPos.Lat - secondMicPos.Lat,
                     directionPos.Lng - secondMicPos.Lng);*/

                List<PointLatLng> polygonPoints = new List<PointLatLng>();


                //Доводим вказівник на джерело звуку до меж екрану
                double arrowVectorSizeWidth = mapControl.FromLocalToLatLng(mapControl.Width, 0).Lat;
                double arrowVectorSizeHeight = mapControl.FromLocalToLatLng(0, mapControl.Height).Lng;
                //Довжина вектору
                double arrowVectorSize = Math.Abs((arrowVectorSizeWidth > arrowVectorSizeHeight) ? arrowVectorSizeWidth : arrowVectorSizeHeight);
                //Приводимо вектор до одиничної форми
                /* if (vectorFormOfDirection.Lat!=0)
                 vectorFormOfDirection.Lat /= Math.Abs(vectorFormOfDirection.Lat);
                 if (vectorFormOfDirection.Lng != 0)
                     vectorFormOfDirection.Lng /= Math.Abs(vectorFormOfDirection.Lng);*/
                //Саме виконуєм доводку вказівника до меж екрану.
                // vectorFormOfDirection.Lat *= 10000;
                // vectorFormOfDirection.Lng *= 10000;

                /*directionPos = new PointLatLng(vectorFormOfDirection.Lat + secondMicPos.Lat,
                    vectorFormOfDirection.Lng + secondMicPos.Lng);*/
                // secondMicPos = Geometry.multiplyVector(secondMicPos, mainMicPos, 1000000);
                directionPos = Geometry.multiplyVector(directionPos, secondMicPos, 100000);
                alternativeDirectionPos = Geometry.multiplyVector(alternativeDirectionPos, secondMicPos, 100000);
                //Будем повертати головний мікрофон відносно іншого, щоб отримати позицію звідки йде звук

               //  polygonPoints.Add(mainMicPos);
                polygonPoints.Add(secondMicPos);
                polygonPoints.Add(directionPos);
                polygonPoints.Add(alternativeDirectionPos);
                //direction poly
                GMapPolygon directionPolygon = new GMapPolygon(polygonPoints, "mypolygon");
                directionPolygon.Fill = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(10, System.Drawing.Color.Red));
                System.Drawing.Color color;
                switch (colorPolygonCount)
                {
                    case 0: color = System.Drawing.Color.Red;
                        break;
                    case 1: color = System.Drawing.Color.Green;
                        break;
                    case 2: color = System.Drawing.Color.Blue;
                        break;
                    default:
                     color = System.Drawing.Color.Yellow;
                        break;
                }
                directionPolygon.Stroke = new System.Drawing.Pen(color, 1);
                markersOverlay.Polygons.Add(directionPolygon);
                colorPolygonCount++;
            }
        }

        public void displayDataToGeoMap(double[] angles)
        {
            PointLatLng geoCoordOfMicrophone = signalsManger.Mn[0].GeoPosition;

            //map.Center = new Google.Maps.Location("1600 Amphitheatre Parkay Mountain View, CA 94043");

            mapControl.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            drawSoundEmiter(markersOverlay);
            drawMicrophones(markersOverlay);
            if (angles != null) drawAngles(markersOverlay, angles);



            mapControl.Overlays.Add(markersOverlay);

            //set position must be called last because otherwise map won't be redrawed
            mapControl.Position = geoCoordOfMicrophone;

            // mapControl.ReloadMap();


            ////
        }
        public void displayDataToDecardMap(double[] angles)
        {
            var bitmap = new System.Drawing.Bitmap(pictureBoxMap.Size.Width, pictureBoxMap.Size.Height);
            var mapImage = System.Drawing.Image.FromHbitmap(bitmap.GetHbitmap());
            var MicColors = new Color[] { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Violet };
            var SoundSourceColors = new Color[] { Color.Black,Color.DarkRed };
            System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Black, 1);
            System.Drawing.Pen dashedBluePen = new System.Drawing.Pen(System.Drawing.Color.Blue, 1);
            dashedBluePen.DashPattern = new float[]{ 5, 2, 15, 4 };
            System.Drawing.Pen greenPen = new System.Drawing.Pen(System.Drawing.Color.Green, 1);
            greenPen.DashPattern = new float[] { 5, 2, 15, 4 };
            using (var graphics = System.Drawing.Graphics.FromImage(mapImage))
          {
                GraphicsContainer containerState = graphics.BeginContainer();
                // Flip the Y-Axis
                graphics.ScaleTransform(1.0F, -1.0F);
                graphics.TranslateTransform((float)pictureBoxMap.Size.Width/2, -(float)pictureBoxMap.Size.Height/2);
                graphics.DrawLine(blackPen, 0, 0, pictureBoxMap.Size.Width, 0);
                graphics.DrawLine(blackPen, 0, 0,  0, pictureBoxMap.Size.Height);
                for (int i = 0; i < signalsManger.Mn.Count; i++)
                {
                    //System.Drawing.Pen micPen = new System.Drawing.Pen(MicColors[i], 1);
                    System.Drawing.Brush micBrush = new System.Drawing.SolidBrush(MicColors[i]);
                    graphics.FillCircle(micBrush, signalsManger.Mn[i].X, signalsManger.Mn[i].Y, 3);
                }
                for (int i = 0; i < signalsManger.Sn.Count; i++)
                {
                    //System.Drawing.Pen soundSourcePen = new System.Drawing.Pen(SoundSourceColors[i], 3);
                    System.Drawing.Brush soundSourceBrush = new System.Drawing.SolidBrush(SoundSourceColors[i]);
                    graphics.FillCircle(soundSourceBrush, signalsManger.Sn[i].Position.X, signalsManger.Sn[i].Position.Y, 3);
                }
                if (angles!=null)//TODO ZIGZAG -КОСТИЛЬ
                for (int i = 0; i < angles.Length; i++)
                {
                    var secondMicPos = signalsManger.Mn[i + 1].Position;
                    if (parent.rbMicRow.Checked)
                        secondMicPos = signalsManger.Mn[1].Position;
                    System.Windows.Point soundSourcePos = MyUtils.rotate(signalsManger.Mn[0].Position, secondMicPos, angles[i]);
                    System.Windows.Point alternativeSoundSourcePos = MyUtils.rotateAnticlockwise(signalsManger.Mn[0].Position, secondMicPos, angles[i]);
                    double realAngle = MyUtils.angleBetweenThreePoints(signalsManger.Mn[0].Position, secondMicPos, signalsManger.Sn[0].Position);
                    System.Console.WriteLine("angle:" + angles[i] + " real angle:" + realAngle);
                
                    soundSourcePos = Geometry.multiplyVector(soundSourcePos, secondMicPos, 1000);
                    alternativeSoundSourcePos = Geometry.multiplyVector(alternativeSoundSourcePos, secondMicPos, 1000);
                    graphics.DrawLine(greenPen, (float)secondMicPos.X, (float)secondMicPos.Y, (float)soundSourcePos.X, (float)soundSourcePos.Y);
                    graphics.DrawLine(dashedBluePen, (float)secondMicPos.X, (float)secondMicPos.Y, (float)soundSourcePos.X, (float)alternativeSoundSourcePos.Y);
                }

                graphics.EndContainer(containerState);
            }
            pictureBoxMap.Image = mapImage;
        }
        public void processMap(double[] angles=null)
        {
            if (tbTestAngle.TextLength > 0) testAngle = int.Parse(tbTestAngle.Text);
            if (signalsManger.Mn.Count == 0) return;
            if (rbGeo.Checked)
                displayDataToGeoMap(angles);
            else displayDataToDecardMap(angles);

            /*System.Drawing.Image mapImage = System.Drawing.Image.FromStream(wc.OpenRead(imgTagSrc));
            System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
            using (var graphics = System.Drawing.Graphics.FromImage(mapImage))
            {
                graphics.DrawLine(blackPen, 0, 0, 50, 50);
            }*/
        }
        private void btnProcessMap_Click(object sender, EventArgs e)
        {
            processMap();

        }

        private void MapForm_Resize(object sender, EventArgs e)
        {
       
        }
        public static PointLatLng rotate(PointLatLng point, PointLatLng center,double angle )
        {
            double angleRadians = angle * (Math.PI / 180);
            double s = Math.Sin(angleRadians);
            double c = Math.Cos(angleRadians);
            System.Windows.Point decardPoint = GlobalMercator.LatLonToMeters(point.Lat, point.Lng);
            System.Windows.Point decardCenter = GlobalMercator.LatLonToMeters(center.Lat, center.Lng);
            // translate point back to origin:
            //point.Lat -= center.Lat;
            // point.Lng -= center.Lng;

            // rotate point
            double xnew = (decardPoint.X - decardCenter.X) * c - (decardPoint.Y - decardCenter.Y) * s+ decardCenter.X;
            double ynew = (decardPoint.X - decardCenter.X) * s + (decardPoint.Y - decardCenter.Y) * c+ decardCenter.Y;
            //double xnew = decardPoint.Y * c - decardPoint.X * s;
            //double ynew = decardPoint.Y * s + decardPoint.X * c;

            // translate point back:
            // point.Lat = xnew + center.Lat;
            // point.Lng = ynew + center.Lng;
            decardPoint.X = xnew;
            decardPoint.Y = ynew;
            System.Windows.Point resultPoint = GlobalMercator.MetersToLatLon(decardPoint);
            PointLatLng resultLatLng = new PointLatLng();
            resultLatLng.Lat = resultPoint.X;
            resultLatLng.Lng = resultPoint.Y;
            return resultLatLng;
        }

        private void tbZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return; // process only ENTER key;
            int zoom = 0;
            int.TryParse(tbZoom.Text, out zoom);
            mapControl.Zoom = zoom;
        }
    }
}
