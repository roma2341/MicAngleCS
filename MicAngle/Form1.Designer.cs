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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.btnProcessAngle = new System.Windows.Forms.Button();
            this.rtbSettings = new System.Windows.Forms.RichTextBox();
            this.btnInputData = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chartMaximum = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbMicRow = new System.Windows.Forms.RadioButton();
            this.rbMicPares = new System.Windows.Forms.RadioButton();
            this.btnToggleRecordForm = new System.Windows.Forms.Button();
            this.btnToggleMap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMaximum)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProcessAngle
            // 
            this.btnProcessAngle.Location = new System.Drawing.Point(518, 29);
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
            this.rtbSettings.Size = new System.Drawing.Size(406, 154);
            this.rtbSettings.TabIndex = 2;
            this.rtbSettings.Text = "samplingRate(44100)\nЗ(X:0;y:10;A:1,0)\nМ(X:0;y:0)\nМ(X:0,3;y:0)\nchannels(2)\nchannel" +
    "Offset(0)\nshift(0,0,0,0)\ndelays(0,1,0,0)";
            this.rtbSettings.TextChanged += new System.EventHandler(this.rtbSettings_TextChanged);
            // 
            // btnInputData
            // 
            this.btnInputData.Location = new System.Drawing.Point(437, 29);
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
            this.lblResult.Location = new System.Drawing.Point(625, 29);
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
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.btnToggleRecordForm);
            this.splitContainer1.Panel2.Controls.Add(this.btnToggleMap);
            this.splitContainer1.Panel2.Controls.Add(this.rtbSettings);
            this.splitContainer1.Panel2.Controls.Add(this.lblResult);
            this.splitContainer1.Panel2.Controls.Add(this.btnProcessAngle);
            this.splitContainer1.Panel2.Controls.Add(this.btnInputData);
            this.splitContainer1.Size = new System.Drawing.Size(952, 564);
            this.splitContainer1.SplitterDistance = 406;
            this.splitContainer1.TabIndex = 5;
            // 
            // chartMaximum
            // 
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.AxisX.Title = "Номер зсуву";
            chartArea1.AxisY.Title = "Сумма сигналів";
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Azure;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chartMaximum.ChartAreas.Add(chartArea1);
            this.chartMaximum.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartMaximum.Legends.Add(legend1);
            this.chartMaximum.Location = new System.Drawing.Point(0, 0);
            this.chartMaximum.Name = "chartMaximum";
            this.chartMaximum.Size = new System.Drawing.Size(952, 406);
            this.chartMaximum.TabIndex = 1;
            this.chartMaximum.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(518, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "dffffff";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbMicRow);
            this.groupBox1.Controls.Add(this.rbMicPares);
            this.groupBox1.Location = new System.Drawing.Point(669, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(127, 73);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Спосіб визначення";
            // 
            // rbMicRow
            // 
            this.rbMicRow.AutoSize = true;
            this.rbMicRow.Location = new System.Drawing.Point(7, 49);
            this.rbMicRow.Name = "rbMicRow";
            this.rbMicRow.Size = new System.Drawing.Size(103, 17);
            this.rbMicRow.TabIndex = 1;
            this.rbMicRow.Text = "Ряд мікрофонів";
            this.rbMicRow.UseVisualStyleBackColor = true;
            // 
            // rbMicPares
            // 
            this.rbMicPares.AutoSize = true;
            this.rbMicPares.Checked = true;
            this.rbMicPares.Location = new System.Drawing.Point(7, 25);
            this.rbMicPares.Name = "rbMicPares";
            this.rbMicPares.Size = new System.Drawing.Size(110, 17);
            this.rbMicPares.TabIndex = 0;
            this.rbMicPares.TabStop = true;
            this.rbMicPares.Text = "Пари мікрофонів";
            this.rbMicPares.UseVisualStyleBackColor = true;
            // 
            // btnToggleRecordForm
            // 
            this.btnToggleRecordForm.Location = new System.Drawing.Point(519, 59);
            this.btnToggleRecordForm.Name = "btnToggleRecordForm";
            this.btnToggleRecordForm.Size = new System.Drawing.Size(100, 23);
            this.btnToggleRecordForm.TabIndex = 6;
            this.btnToggleRecordForm.Text = "ФормаЗапису";
            this.btnToggleRecordForm.UseVisualStyleBackColor = true;
            this.btnToggleRecordForm.Click += new System.EventHandler(this.btnToggleRecordForm_Click);
            // 
            // btnToggleMap
            // 
            this.btnToggleMap.Location = new System.Drawing.Point(437, 59);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Button btnToggleRecordForm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbMicPares;
        public System.Windows.Forms.RadioButton rbMicRow;
        private System.Windows.Forms.Label label1;
    }
}

