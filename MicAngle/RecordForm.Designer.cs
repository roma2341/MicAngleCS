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
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboAsioDrivers = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAsio = new System.Windows.Forms.RadioButton();
            this.rbWaveIn = new System.Windows.Forms.RadioButton();
            this.comboWaveInDevices = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
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
            this.chart3.Size = new System.Drawing.Size(671, 183);
            this.chart3.TabIndex = 10;
            this.chart3.Text = "chart3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(608, 611);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Зчитати";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // chart1
            // 
            chartArea2.CursorX.AutoScroll = false;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(12, 40);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "firstLeft";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "firstRight";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "secondLeft";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "secondRight";
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(869, 369);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
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
            // rbWaveIn
            // 
            this.rbWaveIn.AutoSize = true;
            this.rbWaveIn.Location = new System.Drawing.Point(36, 9);
            this.rbWaveIn.Name = "rbWaveIn";
            this.rbWaveIn.Size = new System.Drawing.Size(14, 13);
            this.rbWaveIn.TabIndex = 1;
            this.rbWaveIn.UseVisualStyleBackColor = true;
            // 
            // comboWaveInDevices
            // 
            this.comboWaveInDevices.FormattingEnabled = true;
            this.comboWaveInDevices.Location = new System.Drawing.Point(366, 13);
            this.comboWaveInDevices.Name = "comboWaveInDevices";
            this.comboWaveInDevices.Size = new System.Drawing.Size(272, 21);
            this.comboWaveInDevices.TabIndex = 12;
            // 
            // RecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 641);
            this.Controls.Add(this.comboWaveInDevices);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.comboAsioDrivers);
            this.Name = "RecordForm";
            this.Text = "RecordForm";
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox comboAsioDrivers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbWaveIn;
        private System.Windows.Forms.RadioButton rbAsio;
        private System.Windows.Forms.ComboBox comboWaveInDevices;
    }
}