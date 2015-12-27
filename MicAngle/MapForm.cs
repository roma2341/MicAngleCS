using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Google.Maps.StaticMaps;
using System.Net;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;

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
           // mapControl.SetCurrentPositionByKeywords("Maputo, Mozambique");
        }

        private bool readGeoCoord(ref Point pt)
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
            Point coord = new Point();
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
                Point geoCoord = new Point();
                if (!readGeoCoord(ref geoCoord))
                {
                    rbGeo.Checked = true;
                    coordTypeMode = CoordTypeMode.COORD_MODE_GEO;
                    return;
                }
                Point decardCoord = GlobalMercator.LatLonToMeters(geoCoord.X, geoCoord.Y);
                tbLatitude.Text = decardCoord.X.ToString();
                tbLongtitude.Text = decardCoord.Y.ToString();
               
                coordTypeMode = CoordTypeMode.COORD_MODE_DECART;
            }
           else 
            {
                if (coordTypeMode == CoordTypeMode.COORD_MODE_GEO) return;
                Console.WriteLine("GEO");
                Point decardCoord = new Point();
                if (!readGeoCoord(ref decardCoord))
                {
                    rbDecart.Checked = true;
                    coordTypeMode = CoordTypeMode.COORD_MODE_DECART;
                    return;
                }
                Point geoCoord = GlobalMercator.MetersToLatLon(decardCoord);

                tbLatitude.Text = geoCoord.X.ToString();
                tbLongtitude.Text = geoCoord.Y.ToString();
                
                coordTypeMode = CoordTypeMode.COORD_MODE_GEO;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (signalsManger.Mn.Count == 0) return;
            int zoom = 0;
            int.TryParse(tbZoom.Text, out zoom);
            Point geoCoordOfMicrophone = signalsManger.Mn[0].GeoPosition;

            //map.Center = new Google.Maps.Location("1600 Amphitheatre Parkay Mountain View, CA 94043");

           
            mapControl.Zoom = zoom;
            mapControl.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            
            foreach (Microphone mic in signalsManger.Mn)
            {
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(mic.GeoPosition.X, mic.GeoPosition.Y),
                 GMarkerGoogleType.green);
                markersOverlay.Markers.Add(marker);
            }
           
            mapControl.Overlays.Add(markersOverlay);
            mapControl.Position = new PointLatLng(geoCoordOfMicrophone.X, geoCoordOfMicrophone.Y);



            ////


            /*System.Drawing.Image mapImage = System.Drawing.Image.FromStream(wc.OpenRead(imgTagSrc));
            System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
            using (var graphics = System.Drawing.Graphics.FromImage(mapImage))
            {
                graphics.DrawLine(blackPen, 0, 0, 50, 50);
            }*/

        }

        private void MapForm_Resize(object sender, EventArgs e)
        {

        }
    }
}
