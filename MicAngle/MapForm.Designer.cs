namespace MicAngle
{
    partial class MapForm
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
            this.components = new System.ComponentModel.Container();
            this.tbLatitude = new System.Windows.Forms.TextBox();
            this.tbLongtitude = new System.Windows.Forms.TextBox();
            this.labelLatitude = new System.Windows.Forms.Label();
            this.labelLongtitude = new System.Windows.Forms.Label();
            this.toolTipLongtitude = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipLatitude = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTestMap = new System.Windows.Forms.Button();
            this.gbCoordType = new System.Windows.Forms.GroupBox();
            this.rbGeo = new System.Windows.Forms.RadioButton();
            this.rbDecart = new System.Windows.Forms.RadioButton();
            this.mapBrowser = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbCoordType.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLatitude
            // 
            this.tbLatitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLatitude.Location = new System.Drawing.Point(77, 3);
            this.tbLatitude.Name = "tbLatitude";
            this.tbLatitude.Size = new System.Drawing.Size(235, 20);
            this.tbLatitude.TabIndex = 0;
            this.tbLatitude.Text = "49,226718";
            // 
            // tbLongtitude
            // 
            this.tbLongtitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLongtitude.Location = new System.Drawing.Point(77, 29);
            this.tbLongtitude.Name = "tbLongtitude";
            this.tbLongtitude.Size = new System.Drawing.Size(235, 20);
            this.tbLongtitude.TabIndex = 1;
            this.tbLongtitude.Text = "28,4062673";
            // 
            // labelLatitude
            // 
            this.labelLatitude.AutoSize = true;
            this.labelLatitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLatitude.Location = new System.Drawing.Point(3, 0);
            this.labelLatitude.Name = "labelLatitude";
            this.labelLatitude.Size = new System.Drawing.Size(68, 26);
            this.labelLatitude.TabIndex = 2;
            this.labelLatitude.Text = "Latitude(x)";
            // 
            // labelLongtitude
            // 
            this.labelLongtitude.AutoSize = true;
            this.labelLongtitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLongtitude.Location = new System.Drawing.Point(3, 26);
            this.labelLongtitude.Name = "labelLongtitude";
            this.labelLongtitude.Size = new System.Drawing.Size(68, 154);
            this.labelLongtitude.TabIndex = 3;
            this.labelLongtitude.Text = "Longtitude(y)";
            // 
            // toolTipLongtitude
            // 
            this.toolTipLongtitude.Tag = "Довгота";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mapBrowser);
            this.splitContainer1.Size = new System.Drawing.Size(813, 515);
            this.splitContainer1.SplitterDistance = 315;
            this.splitContainer1.TabIndex = 7;
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
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer2.Size = new System.Drawing.Size(315, 515);
            this.splitContainer2.SplitterDistance = 180;
            this.splitContainer2.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labelLatitude, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelLongtitude, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbLatitude, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbLongtitude, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(315, 180);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnTestMap);
            this.flowLayoutPanel1.Controls.Add(this.gbCoordType);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(315, 331);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnTestMap
            // 
            this.btnTestMap.Location = new System.Drawing.Point(3, 3);
            this.btnTestMap.Name = "btnTestMap";
            this.btnTestMap.Size = new System.Drawing.Size(182, 23);
            this.btnTestMap.TabIndex = 6;
            this.btnTestMap.Text = "Тест карти";
            this.btnTestMap.UseVisualStyleBackColor = true;
            this.btnTestMap.Click += new System.EventHandler(this.btnTestMap_Click);
            // 
            // gbCoordType
            // 
            this.gbCoordType.Controls.Add(this.rbGeo);
            this.gbCoordType.Controls.Add(this.rbDecart);
            this.gbCoordType.Location = new System.Drawing.Point(3, 32);
            this.gbCoordType.Name = "gbCoordType";
            this.gbCoordType.Size = new System.Drawing.Size(200, 100);
            this.gbCoordType.TabIndex = 7;
            this.gbCoordType.TabStop = false;
            this.gbCoordType.Text = "Coord Type";
            // 
            // rbGeo
            // 
            this.rbGeo.AutoSize = true;
            this.rbGeo.Checked = true;
            this.rbGeo.Location = new System.Drawing.Point(108, 20);
            this.rbGeo.Name = "rbGeo";
            this.rbGeo.Size = new System.Drawing.Size(45, 17);
            this.rbGeo.TabIndex = 1;
            this.rbGeo.TabStop = true;
            this.rbGeo.Tag = "1";
            this.rbGeo.Text = "Geo";
            this.rbGeo.UseVisualStyleBackColor = true;
            this.rbGeo.Click += new System.EventHandler(this.rbCoordType_Clicked);
            // 
            // rbDecart
            // 
            this.rbDecart.AutoSize = true;
            this.rbDecart.Location = new System.Drawing.Point(16, 20);
            this.rbDecart.Name = "rbDecart";
            this.rbDecart.Size = new System.Drawing.Size(57, 17);
            this.rbDecart.TabIndex = 0;
            this.rbDecart.Tag = "0";
            this.rbDecart.Text = "Decart";
            this.rbDecart.UseVisualStyleBackColor = true;
            this.rbDecart.Click += new System.EventHandler(this.rbCoordType_Clicked);
            // 
            // mapBrowser
            // 
            this.mapBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapBrowser.Location = new System.Drawing.Point(0, 0);
            this.mapBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.mapBrowser.Name = "mapBrowser";
            this.mapBrowser.Size = new System.Drawing.Size(494, 515);
            this.mapBrowser.TabIndex = 7;
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 581);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MapForm";
            this.Text = "MapForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapForm_FormClosing);
            this.Load += new System.EventHandler(this.MapForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbCoordType.ResumeLayout(false);
            this.gbCoordType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbLatitude;
        private System.Windows.Forms.TextBox tbLongtitude;
        private System.Windows.Forms.Label labelLatitude;
        private System.Windows.Forms.Label labelLongtitude;
        private System.Windows.Forms.ToolTip toolTipLongtitude;
        private System.Windows.Forms.ToolTip toolTipLatitude;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser mapBrowser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnTestMap;
        private System.Windows.Forms.GroupBox gbCoordType;
        private System.Windows.Forms.RadioButton rbGeo;
        private System.Windows.Forms.RadioButton rbDecart;
    }
}