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
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnShowDiagrams = new System.Windows.Forms.Button();
            this.signalChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tbAngle = new System.Windows.Forms.TextBox();
            this.correlationChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.signalChartGroupBox = new System.Windows.Forms.GroupBox();
            this.correlationChartGroupBox = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.comboAsioDevice = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnAsioControlPanel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxScaling = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.signalChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.correlationChart)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Legend = "Legend1";
            series8.Name = "Series2";
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series9.Legend = "Legend1";
            series9.Name = "Series3";
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series10.Legend = "Legend1";
            series10.Name = "Series4";
            this.signalChart.Series.Add(series7);
            this.signalChart.Series.Add(series8);
            this.signalChart.Series.Add(series9);
            this.signalChart.Series.Add(series10);
            this.signalChart.Size = new System.Drawing.Size(760, 170);
            this.signalChart.TabIndex = 1;
            this.signalChart.Text = "chart1";
            // 
            // tbAngle
            // 
            this.tbAngle.Location = new System.Drawing.Point(135, 30);
            this.tbAngle.Name = "tbAngle";
            this.tbAngle.Size = new System.Drawing.Size(100, 20);
            this.tbAngle.TabIndex = 2;
            this.tbAngle.Text = "0";
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
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series11.Legend = "Legend1";
            series11.Name = "Series1";
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series12.Legend = "Legend1";
            series12.Name = "Series2";
            this.correlationChart.Series.Add(series11);
            this.correlationChart.Series.Add(series12);
            this.correlationChart.Size = new System.Drawing.Size(760, 210);
            this.correlationChart.TabIndex = 5;
            this.correlationChart.Text = "chart2";
            // 
            // signalChartGroupBox
            // 
            this.signalChartGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.signalChartGroupBox.Location = new System.Drawing.Point(9, 13);
            this.signalChartGroupBox.Name = "signalChartGroupBox";
            this.signalChartGroupBox.Size = new System.Drawing.Size(95, 139);
            this.signalChartGroupBox.TabIndex = 6;
            this.signalChartGroupBox.TabStop = false;
            this.signalChartGroupBox.Text = "groupBox1";
            // 
            // correlationChartGroupBox
            // 
            this.correlationChartGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.correlationChartGroupBox.Location = new System.Drawing.Point(8, 3);
            this.correlationChartGroupBox.Name = "correlationChartGroupBox";
            this.correlationChartGroupBox.Size = new System.Drawing.Size(95, 154);
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
            this.splitContainer4.SplitterDistance = 760;
            this.splitContainer4.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(909, 390);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboAsioDevice
            // 
            this.comboAsioDevice.FormattingEnabled = true;
            this.comboAsioDevice.Location = new System.Drawing.Point(354, 32);
            this.comboAsioDevice.Name = "comboAsioDevice";
            this.comboAsioDevice.Size = new System.Drawing.Size(160, 21);
            this.comboAsioDevice.TabIndex = 5;
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
            this.splitContainer2.Panel1.Controls.Add(this.textBoxScaling);
            this.splitContainer2.Panel1.Controls.Add(this.btnAsioControlPanel);
            this.splitContainer2.Panel1.Controls.Add(this.btnShowDiagrams);
            this.splitContainer2.Panel1.Controls.Add(this.tbAngle);
            this.splitContainer2.Panel1.Controls.Add(this.comboAsioDevice);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(917, 485);
            this.splitContainer2.SplitterDistance = 65;
            this.splitContainer2.TabIndex = 9;
            // 
            // btnAsioControlPanel
            // 
            this.btnAsioControlPanel.Location = new System.Drawing.Point(520, 30);
            this.btnAsioControlPanel.Name = "btnAsioControlPanel";
            this.btnAsioControlPanel.Size = new System.Drawing.Size(75, 23);
            this.btnAsioControlPanel.TabIndex = 6;
            this.btnAsioControlPanel.Text = "ASIO CTRL";
            this.btnAsioControlPanel.UseVisualStyleBackColor = true;
            this.btnAsioControlPanel.Click += new System.EventHandler(this.btnAsioControlPanel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(903, 384);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxScaling
            // 
            this.textBoxScaling.Location = new System.Drawing.Point(242, 30);
            this.textBoxScaling.Name = "textBoxScaling";
            this.textBoxScaling.Size = new System.Drawing.Size(100, 20);
            this.textBoxScaling.TabIndex = 7;
            this.textBoxScaling.Text = "0,1";
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
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowDiagrams;
        private System.Windows.Forms.DataVisualization.Charting.Chart signalChart;
        private System.Windows.Forms.TextBox tbAngle;
        private System.Windows.Forms.DataVisualization.Charting.Chart correlationChart;
        private System.Windows.Forms.GroupBox signalChartGroupBox;
        private System.Windows.Forms.GroupBox correlationChartGroupBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox comboAsioDevice;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Button btnAsioControlPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxScaling;
    }
}

