using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MicAngle
{
    public partial class MapForm : Form
    {
        Form1 parent;
        public MapForm(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void MapForm_Load(object sender, EventArgs e)
        {

        }

        private void btnConvertDecartToGeo_Click(object sender, EventArgs e)
        {
            string xStr = tbLongtitude.Text;
            string yStr = tbLatitude.Text;
            if (xStr.Length == 0 || yStr.Length == 0) return;
            double x, y;
            bool resultOfParsing;
            resultOfParsing = Double.TryParse(xStr, out x);
            if (resultOfParsing)
            {
                resultOfParsing = Double.TryParse(yStr, out y);
                if (!resultOfParsing) return;
            }
            else return;
            Point geoCoord = GlobalMercator.MetersToLatLon(new Point(x, y));

            tbLongtitude.Text = geoCoord.X.ToString();
            tbLatitude.Text = geoCoord.Y.ToString();
        }

        private void btnConvertGeoToDecart_Click(object sender, EventArgs e)
        {
            string longtitudeStr = tbLongtitude.Text;
            string latitudeStr = tbLatitude.Text;
            if (longtitudeStr.Length == 0 || latitudeStr.Length == 0) return;
            double longtitude, latitude;
            bool resultOfParsing;
            resultOfParsing = Double.TryParse(longtitudeStr, out longtitude);
            if (resultOfParsing)
            {
                resultOfParsing = Double.TryParse(latitudeStr, out latitude);
                if (!resultOfParsing) return;
            }
            else return;
            Point geoCoord = GlobalMercator.LatLonToMeters(latitude, longtitude);

            tbLongtitude.Text = geoCoord.X.ToString();
            tbLatitude.Text = geoCoord.Y.ToString();

        }

        private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
