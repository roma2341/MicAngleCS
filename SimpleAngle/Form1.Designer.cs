namespace SimpleAngle
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnShowDiagrams = new System.Windows.Forms.Button();
            this.signalChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tbIterations = new System.Windows.Forms.TextBox();
            this.comboWaveInDeviceA = new System.Windows.Forms.ComboBox();
            this.comboWaveInDeviceB = new System.Windows.Forms.ComboBox();
            this.correlationChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.signalChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.correlationChart)).BeginInit();
            this.SuspendLayout();
            // 
            // btnShowDiagrams
            // 
            this.btnShowDiagrams.Location = new System.Drawing.Point(13, 1);
            this.btnShowDiagrams.Name = "btnShowDiagrams";
            this.btnShowDiagrams.Size = new System.Drawing.Size(116, 23);
            this.btnShowDiagrams.TabIndex = 0;
            this.btnShowDiagrams.Text = "Show diagrams";
            this.btnShowDiagrams.UseVisualStyleBackColor = true;
            this.btnShowDiagrams.Click += new System.EventHandler(this.button1_Click);
            // 
            // signalChart
            // 
            chartArea1.Name = "ChartArea1";
            this.signalChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.signalChart.Legends.Add(legend1);
            this.signalChart.Location = new System.Drawing.Point(13, 30);
            this.signalChart.Name = "signalChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.signalChart.Series.Add(series1);
            this.signalChart.Series.Add(series2);
            this.signalChart.Size = new System.Drawing.Size(416, 218);
            this.signalChart.TabIndex = 1;
            this.signalChart.Text = "chart1";
            // 
            // tbIterations
            // 
            this.tbIterations.Location = new System.Drawing.Point(135, 3);
            this.tbIterations.Name = "tbIterations";
            this.tbIterations.Size = new System.Drawing.Size(100, 20);
            this.tbIterations.TabIndex = 2;
            // 
            // comboWaveInDeviceA
            // 
            this.comboWaveInDeviceA.FormattingEnabled = true;
            this.comboWaveInDeviceA.Location = new System.Drawing.Point(588, 30);
            this.comboWaveInDeviceA.Name = "comboWaveInDeviceA";
            this.comboWaveInDeviceA.Size = new System.Drawing.Size(160, 21);
            this.comboWaveInDeviceA.TabIndex = 3;
            // 
            // comboWaveInDeviceB
            // 
            this.comboWaveInDeviceB.FormattingEnabled = true;
            this.comboWaveInDeviceB.Location = new System.Drawing.Point(588, 68);
            this.comboWaveInDeviceB.Name = "comboWaveInDeviceB";
            this.comboWaveInDeviceB.Size = new System.Drawing.Size(160, 21);
            this.comboWaveInDeviceB.TabIndex = 4;
            // 
            // correlationChart
            // 
            chartArea2.Name = "ChartArea1";
            this.correlationChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.correlationChart.Legends.Add(legend2);
            this.correlationChart.Location = new System.Drawing.Point(12, 254);
            this.correlationChart.Name = "correlationChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series2";
            this.correlationChart.Series.Add(series3);
            this.correlationChart.Series.Add(series4);
            this.correlationChart.Size = new System.Drawing.Size(416, 218);
            this.correlationChart.TabIndex = 5;
            this.correlationChart.Text = "chart2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 485);
            this.Controls.Add(this.correlationChart);
            this.Controls.Add(this.comboWaveInDeviceB);
            this.Controls.Add(this.comboWaveInDeviceA);
            this.Controls.Add(this.tbIterations);
            this.Controls.Add(this.signalChart);
            this.Controls.Add(this.btnShowDiagrams);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.signalChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.correlationChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShowDiagrams;
        private System.Windows.Forms.DataVisualization.Charting.Chart signalChart;
        private System.Windows.Forms.TextBox tbIterations;
        private System.Windows.Forms.ComboBox comboWaveInDeviceA;
        private System.Windows.Forms.ComboBox comboWaveInDeviceB;
        private System.Windows.Forms.DataVisualization.Charting.Chart correlationChart;
    }
}

