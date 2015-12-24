namespace MicAngle
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnProcessAngle = new System.Windows.Forms.Button();
            this.rtbSettings = new System.Windows.Forms.RichTextBox();
            this.btnInputData = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chartMaximum = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnToggleMap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMaximum)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProcessAngle
            // 
            this.btnProcessAngle.Location = new System.Drawing.Point(512, 19);
            this.btnProcessAngle.Name = "btnProcessAngle";
            this.btnProcessAngle.Size = new System.Drawing.Size(101, 23);
            this.btnProcessAngle.TabIndex = 0;
            this.btnProcessAngle.Text = "Визначити кут";
            this.btnProcessAngle.UseVisualStyleBackColor = true;
            this.btnProcessAngle.Click += new System.EventHandler(this.btnProcessAngle_Click);
            // 
            // rtbSettings
            // 
            this.rtbSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.rtbSettings.Location = new System.Drawing.Point(0, 0);
            this.rtbSettings.Name = "rtbSettings";
            this.rtbSettings.Size = new System.Drawing.Size(409, 83);
            this.rtbSettings.TabIndex = 2;
            this.rtbSettings.Text = "З(X:-100;y:100;A:1,0;F:44100)\nМ(X:10;y:-10)\nМ(X:10,47;y:-10)";
            // 
            // btnInputData
            // 
            this.btnInputData.Location = new System.Drawing.Point(431, 19);
            this.btnInputData.Name = "btnInputData";
            this.btnInputData.Size = new System.Drawing.Size(75, 23);
            this.btnInputData.TabIndex = 3;
            this.btnInputData.Text = "btnInputData";
            this.btnInputData.UseVisualStyleBackColor = true;
            this.btnInputData.Click += new System.EventHandler(this.btnInputData_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(619, 24);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(13, 13);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "0";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chartMaximum);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnToggleMap);
            this.splitContainer1.Panel2.Controls.Add(this.rtbSettings);
            this.splitContainer1.Panel2.Controls.Add(this.lblResult);
            this.splitContainer1.Panel2.Controls.Add(this.btnProcessAngle);
            this.splitContainer1.Panel2.Controls.Add(this.btnInputData);
            this.splitContainer1.Size = new System.Drawing.Size(952, 564);
            this.splitContainer1.SplitterDistance = 477;
            this.splitContainer1.TabIndex = 5;
            // 
            // chartMaximum
            // 
            chartArea4.Area3DStyle.IsRightAngleAxes = false;
            chartArea4.AxisX.Title = "Номер зсуву";
            chartArea4.AxisY.Title = "Сумма сигналів";
            chartArea4.BackColor = System.Drawing.Color.Black;
            chartArea4.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            chartArea4.BackSecondaryColor = System.Drawing.Color.Azure;
            chartArea4.CursorX.IsUserSelectionEnabled = true;
            chartArea4.Name = "ChartArea1";
            this.chartMaximum.ChartAreas.Add(chartArea4);
            this.chartMaximum.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.chartMaximum.Legends.Add(legend4);
            this.chartMaximum.Location = new System.Drawing.Point(0, 0);
            this.chartMaximum.Name = "chartMaximum";
            series7.BorderWidth = 4;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Legend = "Legend1";
            series7.Name = "Сума сигналів";
            series8.BorderWidth = 4;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series8.Legend = "Legend1";
            series8.MarkerColor = System.Drawing.Color.Red;
            series8.MarkerSize = 10;
            series8.Name = "Максимальна сума сигналів";
            series8.YValuesPerPoint = 2;
            this.chartMaximum.Series.Add(series7);
            this.chartMaximum.Series.Add(series8);
            this.chartMaximum.Size = new System.Drawing.Size(952, 477);
            this.chartMaximum.TabIndex = 1;
            this.chartMaximum.Text = "chart1";
            // 
            // btnToggleMap
            // 
            this.btnToggleMap.Location = new System.Drawing.Point(431, 49);
            this.btnToggleMap.Name = "btnToggleMap";
            this.btnToggleMap.Size = new System.Drawing.Size(75, 23);
            this.btnToggleMap.TabIndex = 5;
            this.btnToggleMap.Text = "Показати карту";
            this.btnToggleMap.UseVisualStyleBackColor = true;
            this.btnToggleMap.Click += new System.EventHandler(this.btnToggleMap_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 564);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMaximum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProcessAngle;
        private System.Windows.Forms.RichTextBox rtbSettings;
        private System.Windows.Forms.Button btnInputData;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMaximum;
        private System.Windows.Forms.Button btnToggleMap;
    }
}

