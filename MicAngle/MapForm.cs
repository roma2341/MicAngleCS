using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Google.Maps.StaticMaps;

namespace MicAngle
{
    public partial class MapForm : Form
    {
        Form1 parent;
        enum CoordTypeMode {COORD_MODE_DECART,COORD_MODE_GEO};
        CoordTypeMode coordTypeMode = CoordTypeMode.COORD_MODE_GEO;
        public MapForm(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void MapForm_Load(object sender, EventArgs e)
        {

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

            var map = new StaticMapRequest();
            //map.Center = new Google.Maps.Location("1600 Amphitheatre Parkay Mountain View, CA 94043");
            map.Size = new System.Drawing.Size(mapBrowser.Width, mapBrowser.Height);
            map.Zoom = 18;
            map.Sensor = false;
            map.Center = new Google.Maps.LatLng(coord.X, coord.Y);

            var marker1 = new Google.Maps.LatLng(coord.X, coord.Y);
            var marker2 = new Google.Maps.LatLng(coord.X + 0.0002, coord.Y + 0.0002);
            var marker3 = new Google.Maps.LatLng(coord.X + 0.0003, coord.Y + 0.0003);
            var marker4 = new Google.Maps.LatLng(coord.X + 0.0004, coord.Y + 0.0004);
            var markersArr = new Google.Maps.Location[4] { marker1, marker2, marker3, marker4 };
            Google.Maps.MapMarkersCollection markersCollection = new Google.Maps.MapMarkersCollection();
            markersCollection.Add(marker1);
            markersCollection.Add(marker2);
            markersCollection.Add(marker3);
            markersCollection.Add(marker4);
            //fake commit
            map.Markers = markersCollection;
            var imgTagSrc = map.ToUri();
            mapBrowser.Url = imgTagSrc;
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
    }
}
