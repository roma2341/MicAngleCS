using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MicAngle
{
    public partial class Form1 : Form
    {
        SignalsManager sm;
        int dataInputCounter=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcessAngle_Click(object sender, EventArgs e)
        {
            if (dataInputCounter < 1) return;
          // SignalsManager sm = new SignalsManager();
           // sm.Mn.Add(new Microphone(10, -10));
           // sm.Mn.Add(new Microphone(10.47,-10));
           // sm.Sn.Add(new SoundEmiter(-100,100,1,44100));
            int signalValuesCount = sm.Sn[0].processEmiterArr(1, sm.Sn[0].samplingRate, (int)SignalsManager.V);
           int[,] signalsArr = new int[sm.Mn.Count, signalValuesCount];
            
           //for (int i = 0; i < signalsArr.Length;i++ )
              // signalsArr[i]=new int[signalValuesCount];
               for (int i = 0; i < sm.Mn.Count; i++)
               {
                   int[] arr = sm.Sn[0].generateSignal(sm.Mn[i]);
                   for (int j = 0; j < arr.Length; j++)
                       signalsArr[i,j] = arr[j];
               }
            int SHIFT_COUNT = 48;
            long[] maxes = new long[SHIFT_COUNT*2];
            double result =  sm.interCorelationFunc(signalsArr, maxes);
            this.chartMaximum.Series.Clear();
            Series series = this.chartMaximum.Series.Add("Intercorelation function\n value");
            this.chartMaximum.ChartAreas[0].AxisX.Maximum = SHIFT_COUNT;
            this.chartMaximum.ChartAreas[0].AxisX.Minimum = -SHIFT_COUNT;
            for (int i = -SHIFT_COUNT; i < SHIFT_COUNT; i++)
            {          
                series.Points.AddXY(i,maxes[i+ SHIFT_COUNT]);
                Console.WriteLine("series max[ " + i + "]=" + maxes[i+ SHIFT_COUNT]);
            }
            lblResult.Text = ""+result;
           
        }

        private void btnInputData_Click(object sender, EventArgs e)
        {
            if (rtbSettings.Text.Length < 1) return;
            sm = new SignalsManager();
            sm.addSoundEmiterFromString(rtbSettings.Text);
            sm.addMicrophoneFromString(rtbSettings.Text);
            dataInputCounter++;
        }
        

   
    }
}
