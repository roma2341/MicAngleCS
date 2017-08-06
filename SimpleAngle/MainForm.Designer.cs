namespace SimpleAngle
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.rtbConfig = new System.Windows.Forms.RichTextBox();
            this.comboAsioDevice = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cbTestMode = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProcessAngle = new System.Windows.Forms.Button();
            this.textBoxScaling = new System.Windows.Forms.TextBox();
            this.btnAsioControlPanel = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.signalChart.ChartAreas.Add(chartArea1);
            this.signalChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.signalChart.Legends.Add(legend1);
            this.signalChart.Location = new System.Drawing.Point(0, 0);
            this.signalChart.Name = "signalChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Series4";
            this.signalChart.Series.Add(series1);
            this.signalChart.Series.Add(series2);
            this.signalChart.Series.Add(series3);
            this.signalChart.Series.Add(series4);
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
            chartArea2.Name = "ChartArea1";
            this.correlationChart.ChartAreas.Add(chartArea2);
            this.correlationChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.correlationChart.Legends.Add(legend2);
            this.correlationChart.Location = new System.Drawing.Point(0, 0);
            this.correlationChart.Name = "correlationChart";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "Series2";
            this.correlationChart.Series.Add(series5);
            this.correlationChart.Series.Add(series6);
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
            this.tabControl1.Controls.Add(this.tabPage3);
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnSave);
            this.tabPage3.Controls.Add(this.rtbConfig);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(909, 390);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(301, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rtbConfig
            // 
            this.rtbConfig.Location = new System.Drawing.Point(6, 6);
            this.rtbConfig.Name = "rtbConfig";
            this.rtbConfig.Size = new System.Drawing.Size(288, 105);
            this.rtbConfig.TabIndex = 0;
            this.rtbConfig.Text = "З(X:0;y:10;A:1,0)\nМ(X:0;y:0)\nМ(X:1;y:0)\nМ(X:2;y:0)\nsamplingRate(44100)";
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
            this.splitContainer2.Panel1.Controls.Add(this.cbTestMode);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.btnProcessAngle);
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
            // cbTestMode
            // 
            this.cbTestMode.AutoSize = true;
            this.cbTestMode.Checked = true;
            this.cbTestMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTestMode.Location = new System.Drawing.Point(732, 32);
            this.cbTestMode.Name = "cbTestMode";
            this.cbTestMode.Size = new System.Drawing.Size(76, 17);
            this.cbTestMode.TabIndex = 11;
            this.cbTestMode.Text = "Test mode";
            this.cbTestMode.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Scaling";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Rotate graphic";
            // 
            // btnProcessAngle
            // 
            this.btnProcessAngle.Location = new System.Drawing.Point(611, 29);
            this.btnProcessAngle.Name = "btnProcessAngle";
            this.btnProcessAngle.Size = new System.Drawing.Size(103, 23);
            this.btnProcessAngle.TabIndex = 8;
            this.btnProcessAngle.Text = "processAngle";
            this.btnProcessAngle.UseVisualStyleBackColor = true;
            this.btnProcessAngle.Click += new System.EventHandler(this.btnProcessAngle_Click);
            // 
            // textBoxScaling
            // 
            this.textBoxScaling.Location = new System.Drawing.Point(242, 30);
            this.textBoxScaling.Name = "textBoxScaling";
            this.textBoxScaling.Size = new System.Drawing.Size(100, 20);
            this.textBoxScaling.TabIndex = 7;
            this.textBoxScaling.Text = "0,1";
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox rtbConfig;
        private System.Windows.Forms.Button btnProcessAngle;
        private System.Windows.Forms.CheckBox cbTestMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

