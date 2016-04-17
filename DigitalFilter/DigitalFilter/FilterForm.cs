using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalFilter
{
    public partial class FilterForm : Form
    {
        const int DEFAULT_FILTER_ITERATIONS_COUNT = 1;
        Form1 parentForm;
        public FilterForm(Form1 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
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
        private double[] parseDoubles(string inputStr)
        {
            // double[] doubles = inputStr.Split(',').Select(Double.Parse).ToArray();
            inputStr = inputStr.Replace("−", "-");
            Regex regex = new Regex(@"[-]{0,1}\d+.\d+");
            var fmt = new NumberFormatInfo();
            //fmt.NegativeSign = "-";
            fmt.NumberDecimalSeparator = ".";
            fmt.NumberGroupSeparator = ",";
            Match match = regex.Match(inputStr);
            List<double> doubleValues = new List<double>();
            while (match.Success)
            {
                Console.WriteLine(match.Value);
                double doubleValue = double.Parse(match.Value, fmt);
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
    }
}
