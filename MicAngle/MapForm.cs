using System;
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

namespace MicAngle
{
    public partial class MapForm : Form
    {
        Form1 parent;
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
        public void processMap()
        {
            if (signalsManger.Mn.Count == 0) return;
            int zoom = 0;
            int.TryParse(tbZoom.Text, out zoom);
            System.Windows.Point geoCoordOfMicrophone = signalsManger.Mn[0].GeoPosition;

            //map.Center = new Google.Maps.Location("1600 Amphitheatre Parkay Mountain View, CA 94043");


            mapControl.Zoom = zoom;
            mapControl.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            List<PointLatLng> polygonPoints = new List<PointLatLng>();

            PointLatLng soundLocation = new PointLatLng(signalsManger.Sn[0].GeoPosition.X,
                signalsManger.Sn[0].GeoPosition.Y);
            GMarkerGoogle markerOfSoundEmiter = new GMarkerGoogle(soundLocation,
                GMarkerGoogleType.red_dot);
            markersOverlay.Markers.Add(markerOfSoundEmiter);



            int markersCount = 0;
            foreach (Microphone mic in signalsManger.Mn)
            {
                PointLatLng micLocation = new PointLatLng(mic.GeoPosition.X, mic.GeoPosition.Y);
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
                polygonPoints.Add(micLocation);
                markersCount++;
            }
            double angle = parent.resultAngle;
            PointLatLng mainMicPos = polygonPoints.First();//signalsManger.Mn[0].GeoPosition
            PointLatLng secondMicPos = polygonPoints.Last();//fiction zero
            PointLatLng directionPos = rotate(mainMicPos, secondMicPos, angle);
            PointLatLng vectorFormOfDirection = new PointLatLng(directionPos.Lat - secondMicPos.Lat,
                directionPos.Lng - secondMicPos.Lng);
            
            //Доводим вказівник на джерело звуку до меж екрану
            double arrowVectorSizeWidth = mapControl.FromLocalToLatLng(mapControl.Width, 0).Lat;
            double arrowVectorSizeHeight = mapControl.FromLocalToLatLng(0, mapControl.Height).Lng;
            //Довжина вектору
           // double arrowVectorSize = Math.Abs((arrowVectorSizeWidth > arrowVectorSizeHeight) ? arrowVectorSizeWidth : arrowVectorSizeHeight);
            //Приводимо вектор до одиничної форми
           // if (vectorFormOfDirection.Lat!=0)
           // vectorFormOfDirection.Lat /= Math.Abs(vectorFormOfDirection.Lat);
           // if (vectorFormOfDirection.Lng != 0)
           //     vectorFormOfDirection.Lng /= Math.Abs(vectorFormOfDirection.Lng);
            //Саме виконуєм доводку вказівника до меж екрану.
            vectorFormOfDirection.Lat *= 100;
            vectorFormOfDirection.Lng *= 100;

            directionPos = new PointLatLng(vectorFormOfDirection.Lat + secondMicPos.Lat,
                vectorFormOfDirection.Lng + secondMicPos.Lng);

          
            //Будем повертати головний мікрофон відносно іншого, щоб отримати позицію звідки йде звук


            polygonPoints.Add(directionPos);
            //direction poly
            GMapPolygon directionPolygon = new GMapPolygon(polygonPoints, "mypolygon");
            directionPolygon.Fill = new SolidBrush(System.Drawing.Color.FromArgb(50, System.Drawing.Color.Red));
            directionPolygon.Stroke = new Pen(System.Drawing.Color.Red, 1);
            markersOverlay.Polygons.Add(directionPolygon);


            mapControl.Overlays.Add(markersOverlay);        

            //set position must be called last because otherwise map won't be redrawed
            mapControl.Position = new PointLatLng(geoCoordOfMicrophone.X, geoCoordOfMicrophone.Y);
            // mapControl.ReloadMap();


            ////


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
        public PointLatLng rotate(PointLatLng point, PointLatLng center,double angle )
        {
           double angleRadians = angle*Math.PI / 180;
            double s = Math.Sin(angleRadians);
            double c = Math.Cos(angleRadians);

            // translate point back to origin:
            point.Lat -= center.Lat;
            point.Lng -= center.Lng;

            // rotate point
            double xnew = point.Lat * c - point.Lng * s;
            double ynew = point.Lat * s + point.Lng * c;

            // translate point back:
            point.Lat = xnew + center.Lat;
            point.Lng = ynew + center.Lng;
            return point;
        }
    }
}
