using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalFilter.utils;
using static DigitalFilter.DigitalFilter;

namespace DigitalFilter
{
    public partial class FilterForm : Form
    {
        const int DEFAULT_FILTER_ITERATIONS_COUNT = 1;
        const string FILTERS_FILE_DIR = "";
        const string FILTERS_FILE_NAME = "filters.txt";
        const string PATH_TO_FILTERS_FILE = FILTERS_FILE_DIR + FILTERS_FILE_NAME;
        const char COEFICIENTS_GROUP_SEPARATOR = ';';
      
       public FilterMode currentFilterMode = FilterMode.Forward;

        Form1 parentForm;
        List<DigitalFilter> digitalFilters;
        public FilterForm(Form1 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            PostConstructor();
        }
        public void PostConstructor()
        {
            digitalFilters = new List<DigitalFilter>();
            loadFilters();
          
        }
      
        public void loadFilters()
        {
            string s = "";
            using (StreamReader sr = File.OpenText(PATH_TO_FILTERS_FILE))
            {
                s = sr.ReadToEnd();
            }
            string[] coeficientStrings = s.Split(COEFICIENTS_GROUP_SEPARATOR);
           double[][] cofGroups = parseDoublesGroups(coeficientStrings);
            for (int i = 0; i < cofGroups.GetLength(0); i++)
            {
                DigitalFilter dFilter = new DigitalFilter(parentForm.CurrentNumberFormat,cofGroups[i]);
                digitalFilters.Add(dFilter);
                lbFilters.Items.Add(dFilter);
            }
           
            Console.WriteLine(s);
        }
        private void enableFilter(DigitalFilter filter)
        {
            tbFilterOrder.Text = filter.getFilterOrder().ToString();
            rtbFilterKoff.Text = filter.getCofsStr();
        }

        private void btnDoFilter_Click(object sender, EventArgs e)
        {
            int filterIterations = 1;
            int filterOrder = -1;
            try
            {
                filterIterations = int.Parse(tbFilterIterationsCount.Text);
             }
            catch (FormatException formatException) {
                MessageBox.Show("filterOrder format error, deffault value applied !");
                tbFilterIterationsCount.Text = DEFAULT_FILTER_ITERATIONS_COUNT.ToString();
            }          
            double[] filterKoffs = null;
            try
            {
                filterOrder  = int.Parse(tbFilterOrder.Text);
                filterKoffs = parseDoubles(rtbFilterKoff.Text);
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Error filter order or koffs format !");
                return;
            }
                for (int i = 0; i < filterIterations; i++)
                {
                parentForm.filterSignal(filterKoffs, filterOrder);
                if(filterIterations>1) parentForm.assignFilteredSignalToCurrent();
            }
            if (cbAutoApply.Checked) parentForm.assignFilteredSignalToCurrent();
            parentForm.showFilteredSignal();
        }

        private void btnAssignFilteredSignal_Click(object sender, EventArgs e)
        {
            parentForm.assignFilteredSignalToCurrent();
        }
        private double[][] parseDoublesGroups(string[] inputStr)
        {
            List<double[]> doubleGroups = new List<double[]>();
            for (int i = 0; i < inputStr.Length; i++) {
                    double[] arr = parseDoubles(inputStr[i]);
                    if (arr.Length>0)
                    doubleGroups.Add(arr);
                    }
            return MyArrayConverter.CreateRectangularArray<double>(doubleGroups);
        }

        private double[] parseDoubles(string inputStr)
        {
            // double[] doubles = inputStr.Split(',').Select(Double.Parse).ToArray();
            inputStr = inputStr.Replace("−", "-");
            Regex regex = new Regex(@"[-]{0,1}\d+(.\d+){0,1}");
           
            Match match = regex.Match(inputStr);
            List<double> doubleValues = new List<double>();
            while (match.Success)
            {
                Console.WriteLine(match.Value);
                double doubleValue = double.Parse(match.Value, parentForm.CurrentNumberFormat);
                doubleValues.Add(doubleValue);
                match = match.NextMatch();
            }
            return doubleValues.ToArray();
        }

        private void rtbFilterKoff_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnResetSignal_Click(object sender, EventArgs e)
        {
            parentForm.resetSignal();
            parentForm.showCurrentSignal();
        }

        private void lbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            DigitalFilter dFilter = (DigitalFilter)lbFilters.SelectedItem;
            enableFilter(dFilter);
            Console.WriteLine(dFilter);
        }

        private void rbFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                // This is the correct control.
                RadioButton rb = (RadioButton)sender;
                switch (rb.Text)
                {
                    case "Forward":
                        currentFilterMode = DigitalFilter.FilterMode.Forward;
                        break;
                    case "Backward":
                        currentFilterMode = DigitalFilter.FilterMode.Backward;
                        break;
                    case "AllPass":
                        currentFilterMode = DigitalFilter.FilterMode.AllPass;
                        break;
                }
            }
        }
    }
}
