﻿namespace SimpleAngle
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnShowDiagrams = new System.Windows.Forms.Button();
            this.signalChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tbIterations = new System.Windows.Forms.TextBox();
            this.correlationChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.signalChartGroupBox = new System.Windows.Forms.GroupBox();
            this.correlationChartGroupBox = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comboWaveInDeviceB = new System.Windows.Forms.ComboBox();
            this.comboWaveInDeviceA = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.signalChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.correlationChart)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnShowDiagrams
            // 
            this.btnShowDiagrams.Location = new System.Drawing.Point(3, 28);
            this.btnShowDiagrams.Name = "btnShowDiagrams";
            this.btnShowDiagrams.Size = new System.Drawing.Size(116, 23);
            this.btnShowDiagrams.TabIndex = 0;
            this.btnShowDiagrams.Text = "Show diagrams";
            this.btnShowDiagrams.UseVisualStyleBackColor = true;
            this.btnShowDiagrams.Click += new System.EventHandler(this.button1_Click);
            // 
            // signalChart
            // 
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.Name = "ChartArea1";
            this.signalChart.ChartAreas.Add(chartArea3);
            this.signalChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.signalChart.Legends.Add(legend3);
            this.signalChart.Location = new System.Drawing.Point(0, 0);
            this.signalChart.Name = "signalChart";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "Series2";
            this.signalChart.Series.Add(series5);
            this.signalChart.Series.Add(series6);
            this.signalChart.Size = new System.Drawing.Size(760, 170);
            this.signalChart.TabIndex = 1;
            this.signalChart.Text = "chart1";
            // 
            // tbIterations
            // 
            this.tbIterations.Location = new System.Drawing.Point(125, 30);
            this.tbIterations.Name = "tbIterations";
            this.tbIterations.Size = new System.Drawing.Size(100, 20);
            this.tbIterations.TabIndex = 2;
            // 
            // correlationChart
            // 
            chartArea4.Name = "ChartArea1";
            this.correlationChart.ChartAreas.Add(chartArea4);
            this.correlationChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.correlationChart.Legends.Add(legend4);
            this.correlationChart.Location = new System.Drawing.Point(0, 0);
            this.correlationChart.Name = "correlationChart";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Legend = "Legend1";
            series8.Name = "Series2";
            this.correlationChart.Series.Add(series7);
            this.correlationChart.Series.Add(series8);
            this.correlationChart.Size = new System.Drawing.Size(761, 210);
            this.correlationChart.TabIndex = 5;
            this.correlationChart.Text = "chart2";
            // 
            // signalChartGroupBox
            // 
            this.signalChartGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.signalChartGroupBox.Location = new System.Drawing.Point(9, 13);
            this.signalChartGroupBox.Name = "signalChartGroupBox";
            this.signalChartGroupBox.Size = new System.Drawing.Size(95, 70);
            this.signalChartGroupBox.TabIndex = 6;
            this.signalChartGroupBox.TabStop = false;
            this.signalChartGroupBox.Text = "groupBox1";
            // 
            // correlationChartGroupBox
            // 
            this.correlationChartGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.correlationChartGroupBox.Location = new System.Drawing.Point(8, 3);
            this.correlationChartGroupBox.Name = "correlationChartGroupBox";
            this.correlationChartGroupBox.Size = new System.Drawing.Size(95, 66);
            this.correlationChartGroupBox.TabIndex = 7;
            this.correlationChartGroupBox.TabStop = false;
            this.correlationChartGroupBox.Text = "groupBox1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(917, 416);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(909, 390);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(909, 390);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Size = new System.Drawing.Size(903, 384);
            this.splitContainer1.SplitterDistance = 170;
            this.splitContainer1.TabIndex = 0;
            // 
            // comboWaveInDeviceB
            // 
            this.comboWaveInDeviceB.FormattingEnabled = true;
            this.comboWaveInDeviceB.Location = new System.Drawing.Point(418, 30);
            this.comboWaveInDeviceB.Name = "comboWaveInDeviceB";
            this.comboWaveInDeviceB.Size = new System.Drawing.Size(160, 21);
            this.comboWaveInDeviceB.TabIndex = 6;
            // 
            // comboWaveInDeviceA
            // 
            this.comboWaveInDeviceA.FormattingEnabled = true;
            this.comboWaveInDeviceA.Location = new System.Drawing.Point(252, 29);
            this.comboWaveInDeviceA.Name = "comboWaveInDeviceA";
            this.comboWaveInDeviceA.Size = new System.Drawing.Size(160, 21);
            this.comboWaveInDeviceA.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnShowDiagrams);
            this.splitContainer2.Panel1.Controls.Add(this.comboWaveInDeviceB);
            this.splitContainer2.Panel1.Controls.Add(this.tbIterations);
            this.splitContainer2.Panel1.Controls.Add(this.comboWaveInDeviceA);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(917, 485);
            this.splitContainer2.SplitterDistance = 65;
            this.splitContainer2.TabIndex = 9;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.signalChart);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.signalChartGroupBox);
            this.splitContainer3.Size = new System.Drawing.Size(903, 170);
            this.splitContainer3.SplitterDistance = 760;
            this.splitContainer3.TabIndex = 7;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.correlationChart);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.correlationChartGroupBox);
            this.splitContainer4.Size = new System.Drawing.Size(903, 210);
            this.splitContainer4.SplitterDistance = 761;
            this.splitContainer4.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 485);
            this.Controls.Add(this.splitContainer2);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.signalChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.correlationChart)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowDiagrams;
        private System.Windows.Forms.DataVisualization.Charting.Chart signalChart;
        private System.Windows.Forms.TextBox tbIterations;
        private System.Windows.Forms.DataVisualization.Charting.Chart correlationChart;
        private System.Windows.Forms.GroupBox signalChartGroupBox;
        private System.Windows.Forms.GroupBox correlationChartGroupBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox comboWaveInDeviceB;
        private System.Windows.Forms.ComboBox comboWaveInDeviceA;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
    }
}
