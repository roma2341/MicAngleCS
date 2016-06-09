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
            this.labelZoom = new System.Windows.Forms.Label();
            this.tbZoom = new System.Windows.Forms.TextBox();
            this.tbTestAngle = new System.Windows.Forms.TextBox();
            this.labelTestAngle = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTestMap = new System.Windows.Forms.Button();
            this.btnProcessMap = new System.Windows.Forms.Button();
            this.gbCoordType = new System.Windows.Forms.GroupBox();
            this.rbGeo = new System.Windows.Forms.RadioButton();
            this.rbDecart = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeoMap = new System.Windows.Forms.TabPage();
            this.mapControl = new GMap.NET.WindowsForms.GMapControl();
            this.tabPageDecardMap = new System.Windows.Forms.TabPage();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
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
            this.tabControl1.SuspendLayout();
            this.tabPageGeoMap.SuspendLayout();
            this.tabPageDecardMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            this.SuspendLayout();
            // 
            // tbLatitude
            // 
            this.tbLatitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLatitude.Location = new System.Drawing.Point(77, 3);
            this.tbLatitude.Name = "tbLatitude";
            this.tbLatitude.Size = new System.Drawing.Size(248, 20);
            this.tbLatitude.TabIndex = 0;
            this.tbLatitude.Text = "49,226718";
            // 
            // tbLongtitude
            // 
            this.tbLongtitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLongtitude.Location = new System.Drawing.Point(77, 29);
            this.tbLongtitude.Name = "tbLongtitude";
            this.tbLongtitude.Size = new System.Drawing.Size(248, 20);
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
            this.labelLongtitude.Size = new System.Drawing.Size(68, 26);
            this.labelLongtitude.TabIndex = 3;
            this.labelLongtitude.Text = "Longtitude(y)";
            // 
            // toolTipLongtitude
            // 
            this.toolTipLongtitude.Tag = "Довгота";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(849, 584);
            this.splitContainer1.SplitterDistance = 328;
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
            this.splitContainer2.Size = new System.Drawing.Size(328, 584);
            this.splitContainer2.SplitterDistance = 204;
            this.splitContainer2.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelLatitude, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelLongtitude, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbLatitude, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbLongtitude, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelZoom, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbZoom, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbTestAngle, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelTestAngle, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(328, 204);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelZoom
            // 
            this.labelZoom.AutoSize = true;
            this.labelZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelZoom.Location = new System.Drawing.Point(3, 52);
            this.labelZoom.Name = "labelZoom";
            this.labelZoom.Size = new System.Drawing.Size(68, 26);
            this.labelZoom.TabIndex = 4;
            this.labelZoom.Text = "Zoom";
            // 
            // tbZoom
            // 
            this.tbZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbZoom.Location = new System.Drawing.Point(77, 55);
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(248, 20);
            this.tbZoom.TabIndex = 5;
            this.tbZoom.Text = "16";
            this.tbZoom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbZoom_KeyPress);
            // 
            // tbTestAngle
            // 
            this.tbTestAngle.Location = new System.Drawing.Point(77, 81);
            this.tbTestAngle.Name = "tbTestAngle";
            this.tbTestAngle.Size = new System.Drawing.Size(100, 20);
            this.tbTestAngle.TabIndex = 6;
            // 
            // labelTestAngle
            // 
            this.labelTestAngle.AutoSize = true;
            this.labelTestAngle.Location = new System.Drawing.Point(3, 78);
            this.labelTestAngle.Name = "labelTestAngle";
            this.labelTestAngle.Size = new System.Drawing.Size(58, 13);
            this.labelTestAngle.TabIndex = 7;
            this.labelTestAngle.Text = "Test Angle";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnTestMap);
            this.flowLayoutPanel1.Controls.Add(this.btnProcessMap);
            this.flowLayoutPanel1.Controls.Add(this.gbCoordType);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(328, 376);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnTestMap
            // 
            this.btnTestMap.Location = new System.Drawing.Point(3, 3);
            this.btnTestMap.Name = "btnTestMap";
            this.btnTestMap.Size = new System.Drawing.Size(73, 23);
            this.btnTestMap.TabIndex = 6;
            this.btnTestMap.Text = "Тест мапи";
            this.btnTestMap.UseVisualStyleBackColor = true;
            this.btnTestMap.Click += new System.EventHandler(this.btnTestMap_Click);
            // 
            // btnProcessMap
            // 
            this.btnProcessMap.Location = new System.Drawing.Point(82, 3);
            this.btnProcessMap.Name = "btnProcessMap";
            this.btnProcessMap.Size = new System.Drawing.Size(177, 23);
            this.btnProcessMap.TabIndex = 8;
            this.btnProcessMap.Text = "Відобразити обрахунки на мапі";
            this.btnProcessMap.UseVisualStyleBackColor = true;
            this.btnProcessMap.Click += new System.EventHandler(this.btnProcessMap_Click);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGeoMap);
            this.tabControl1.Controls.Add(this.tabPageDecardMap);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(517, 584);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageGeoMap
            // 
            this.tabPageGeoMap.Controls.Add(this.mapControl);
            this.tabPageGeoMap.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeoMap.Name = "tabPageGeoMap";
            this.tabPageGeoMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeoMap.Size = new System.Drawing.Size(509, 558);
            this.tabPageGeoMap.TabIndex = 1;
            this.tabPageGeoMap.Text = "Geo Map";
            this.tabPageGeoMap.UseVisualStyleBackColor = true;
            // 
            // mapControl
            // 
            this.mapControl.Bearing = 0F;
            this.mapControl.CanDragMap = true;
            this.mapControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.mapControl.GrayScaleMode = false;
            this.mapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.mapControl.LevelsKeepInMemmory = 5;
            this.mapControl.Location = new System.Drawing.Point(3, 3);
            this.mapControl.MarkersEnabled = true;
            this.mapControl.MaxZoom = 30;
            this.mapControl.MinZoom = 2;
            this.mapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mapControl.Name = "mapControl";
            this.mapControl.NegativeMode = false;
            this.mapControl.PolygonsEnabled = true;
            this.mapControl.RetryLoadTile = 0;
            this.mapControl.RoutesEnabled = true;
            this.mapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.mapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.mapControl.ShowTileGridLines = false;
            this.mapControl.Size = new System.Drawing.Size(503, 552);
            this.mapControl.TabIndex = 1;
            this.mapControl.Zoom = 0D;
            // 
            // tabPageDecardMap
            // 
            this.tabPageDecardMap.Controls.Add(this.pictureBoxMap);
            this.tabPageDecardMap.Location = new System.Drawing.Point(4, 22);
            this.tabPageDecardMap.Name = "tabPageDecardMap";
            this.tabPageDecardMap.Size = new System.Drawing.Size(509, 558);
            this.tabPageDecardMap.TabIndex = 2;
            this.tabPageDecardMap.Text = "Decard Map";
            this.tabPageDecardMap.UseVisualStyleBackColor = true;
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxMap.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(509, 558);
            this.pictureBoxMap.TabIndex = 0;
            this.pictureBoxMap.TabStop = false;
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 584);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MapForm";
            this.Text = "MapForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapForm_FormClosing);
            this.Load += new System.EventHandler(this.MapForm_Load);
            this.Resize += new System.EventHandler(this.MapForm_Resize);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeoMap.ResumeLayout(false);
            this.tabPageDecardMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnTestMap;
        private System.Windows.Forms.GroupBox gbCoordType;
        private System.Windows.Forms.RadioButton rbGeo;
        private System.Windows.Forms.RadioButton rbDecart;
        private System.Windows.Forms.Button btnProcessMap;
        private System.Windows.Forms.Label labelZoom;
        private System.Windows.Forms.TextBox tbZoom;
        private System.Windows.Forms.TextBox tbTestAngle;
        private System.Windows.Forms.Label labelTestAngle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeoMap;
        private GMap.NET.WindowsForms.GMapControl mapControl;
        private System.Windows.Forms.TabPage tabPageDecardMap;
        private System.Windows.Forms.PictureBox pictureBoxMap;
    }
}