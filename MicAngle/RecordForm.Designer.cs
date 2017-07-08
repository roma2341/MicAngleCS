namespace MicAngle
{
    partial class RecordForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.chartSignal = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboAsioDrivers = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbWaveIn = new System.Windows.Forms.RadioButton();
            this.rbAsio = new System.Windows.Forms.RadioButton();
            this.comboWaveInDeviceA = new System.Windows.Forms.ComboBox();
            this.rtbSignal = new System.Windows.Forms.RichTextBox();
            this.btnSignalToChart = new System.Windows.Forms.Button();
            this.comboWaveInDeviceB = new System.Windows.Forms.ComboBox();
            this.btnTextInputToChart = new System.Windows.Forms.Button();
            this.buttonMictest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSignal)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart3
            // 
            this.chart3.AccessibleName = "";
            chartArea1.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart3.Legends.Add(legend1);
            this.chart3.Location = new System.Drawing.Point(21, 422);
            this.chart3.Name = "chart3";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Кореляція";
            this.chart3.Series.Add(series1);
            this.chart3.Size = new System.Drawing.Size(380, 183);
            this.chart3.TabIndex = 10;
            this.chart3.Text = "chart3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(497, 611);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Зчитати";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // chartSignal
            // 
            chartArea2.CursorX.AutoScroll = false;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.Name = "ChartArea1";
            this.chartSignal.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartSignal.Legends.Add(legend2);
            this.chartSignal.Location = new System.Drawing.Point(12, 40);
            this.chartSignal.Name = "chartSignal";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Lime;
            series2.Legend = "Legend1";
            series2.Name = "firstLeft";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.Color = System.Drawing.Color.Lime;
            series3.Legend = "Legend1";
            series3.Name = "firstRight";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Red;
            series4.Legend = "Legend1";
            series4.Name = "secondLeft";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series5.Color = System.Drawing.Color.Red;
            series5.Legend = "Legend1";
            series5.Name = "secondRight";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.Blue;
            series6.Legend = "Legend1";
            series6.Name = "thirdLeft";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.Blue;
            series7.Legend = "Legend1";
            series7.Name = "thirdRight";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series8.Color = System.Drawing.Color.Black;
            series8.Legend = "Legend1";
            series8.Name = "fourthLeft";
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series9.Color = System.Drawing.Color.Black;
            series9.Legend = "Legend1";
            series9.Name = "fourthRight";
            this.chartSignal.Series.Add(series2);
            this.chartSignal.Series.Add(series3);
            this.chartSignal.Series.Add(series4);
            this.chartSignal.Series.Add(series5);
            this.chartSignal.Series.Add(series6);
            this.chartSignal.Series.Add(series7);
            this.chartSignal.Series.Add(series8);
            this.chartSignal.Series.Add(series9);
            this.chartSignal.Size = new System.Drawing.Size(869, 369);
            this.chartSignal.TabIndex = 7;
            this.chartSignal.Text = "chart1";
            // 
            // comboAsioDrivers
            // 
            this.comboAsioDrivers.FormattingEnabled = true;
            this.comboAsioDrivers.Location = new System.Drawing.Point(11, 13);
            this.comboAsioDrivers.Name = "comboAsioDrivers";
            this.comboAsioDrivers.Size = new System.Drawing.Size(272, 21);
            this.comboAsioDrivers.TabIndex = 6;
            this.comboAsioDrivers.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbWaveIn);
            this.groupBox1.Controls.Add(this.rbAsio);
            this.groupBox1.Location = new System.Drawing.Point(289, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(57, 36);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // rbWaveIn
            // 
            this.rbWaveIn.AutoSize = true;
            this.rbWaveIn.Location = new System.Drawing.Point(36, 9);
            this.rbWaveIn.Name = "rbWaveIn";
            this.rbWaveIn.Size = new System.Drawing.Size(14, 13);
            this.rbWaveIn.TabIndex = 1;
            this.rbWaveIn.UseVisualStyleBackColor = true;
            // 
            // rbAsio
            // 
            this.rbAsio.AutoSize = true;
            this.rbAsio.Checked = true;
            this.rbAsio.Location = new System.Drawing.Point(6, 9);
            this.rbAsio.Name = "rbAsio";
            this.rbAsio.Size = new System.Drawing.Size(14, 13);
            this.rbAsio.TabIndex = 0;
            this.rbAsio.TabStop = true;
            this.rbAsio.UseVisualStyleBackColor = true;
            // 
            // comboWaveInDeviceA
            // 
            this.comboWaveInDeviceA.FormattingEnabled = true;
            this.comboWaveInDeviceA.Location = new System.Drawing.Point(366, 13);
            this.comboWaveInDeviceA.Name = "comboWaveInDeviceA";
            this.comboWaveInDeviceA.Size = new System.Drawing.Size(249, 21);
            this.comboWaveInDeviceA.TabIndex = 12;
            // 
            // rtbSignal
            // 
            this.rtbSignal.Location = new System.Drawing.Point(423, 422);
            this.rtbSignal.Name = "rtbSignal";
            this.rtbSignal.Size = new System.Drawing.Size(428, 183);
            this.rtbSignal.TabIndex = 13;
            this.rtbSignal.Text = "";
            this.rtbSignal.WordWrap = false;
            // 
            // btnSignalToChart
            // 
            this.btnSignalToChart.Location = new System.Drawing.Point(724, 611);
            this.btnSignalToChart.Name = "btnSignalToChart";
            this.btnSignalToChart.Size = new System.Drawing.Size(139, 23);
            this.btnSignalToChart.TabIndex = 14;
            this.btnSignalToChart.Text = "Відобразити сигнал";
            this.btnSignalToChart.UseVisualStyleBackColor = true;
            this.btnSignalToChart.Click += new System.EventHandler(this.btnSignalToChart_Click);
            // 
            // comboWaveInDeviceB
            // 
            this.comboWaveInDeviceB.FormattingEnabled = true;
            this.comboWaveInDeviceB.Location = new System.Drawing.Point(632, 12);
            this.comboWaveInDeviceB.Name = "comboWaveInDeviceB";
            this.comboWaveInDeviceB.Size = new System.Drawing.Size(249, 21);
            this.comboWaveInDeviceB.TabIndex = 15;
            // 
            // btnTextInputToChart
            // 
            this.btnTextInputToChart.Location = new System.Drawing.Point(590, 611);
            this.btnTextInputToChart.Name = "btnTextInputToChart";
            this.btnTextInputToChart.Size = new System.Drawing.Size(128, 23);
            this.btnTextInputToChart.TabIndex = 16;
            this.btnTextInputToChart.Text = "Текст на графік";
            this.btnTextInputToChart.UseVisualStyleBackColor = true;
            this.btnTextInputToChart.Click += new System.EventHandler(this.btnTextInputToChart_Click);
            // 
            // buttonMictest
            // 
            this.buttonMictest.Location = new System.Drawing.Point(416, 611);
            this.buttonMictest.Name = "buttonMictest";
            this.buttonMictest.Size = new System.Drawing.Size(75, 23);
            this.buttonMictest.TabIndex = 17;
            this.buttonMictest.Text = "Mictest";
            this.buttonMictest.UseVisualStyleBackColor = true;
            this.buttonMictest.Click += new System.EventHandler(this.buttonMictest_Click);
            // 
            // RecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 651);
            this.Controls.Add(this.buttonMictest);
            this.Controls.Add(this.btnTextInputToChart);
            this.Controls.Add(this.comboWaveInDeviceB);
            this.Controls.Add(this.btnSignalToChart);
            this.Controls.Add(this.rtbSignal);
            this.Controls.Add(this.comboWaveInDeviceA);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chartSignal);
            this.Controls.Add(this.comboAsioDrivers);
            this.Name = "RecordForm";
            this.Text = "RecordForm";
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSignal)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSignal;
        private System.Windows.Forms.ComboBox comboAsioDrivers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbWaveIn;
        private System.Windows.Forms.RadioButton rbAsio;
        private System.Windows.Forms.ComboBox comboWaveInDeviceA;
        private System.Windows.Forms.RichTextBox rtbSignal;
        private System.Windows.Forms.Button btnSignalToChart;
        private System.Windows.Forms.ComboBox comboWaveInDeviceB;
        private System.Windows.Forms.Button btnTextInputToChart;
        private System.Windows.Forms.Button buttonMictest;
    }
}