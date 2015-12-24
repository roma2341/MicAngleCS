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
            this.tbLongtitude = new System.Windows.Forms.TextBox();
            this.tbLatitude = new System.Windows.Forms.TextBox();
            this.labelLongtitude = new System.Windows.Forms.Label();
            this.labelLatitude = new System.Windows.Forms.Label();
            this.toolTipLongtitude = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipLatitude = new System.Windows.Forms.ToolTip(this.components);
            this.btnConvertDecartToGeo = new System.Windows.Forms.Button();
            this.btnConvertGeoToDecart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbLongtitude
            // 
            this.tbLongtitude.Location = new System.Drawing.Point(106, 28);
            this.tbLongtitude.Name = "tbLongtitude";
            this.tbLongtitude.Size = new System.Drawing.Size(100, 20);
            this.tbLongtitude.TabIndex = 0;
            // 
            // tbLatitude
            // 
            this.tbLatitude.Location = new System.Drawing.Point(106, 58);
            this.tbLatitude.Name = "tbLatitude";
            this.tbLatitude.Size = new System.Drawing.Size(100, 20);
            this.tbLatitude.TabIndex = 1;
            // 
            // labelLongtitude
            // 
            this.labelLongtitude.AutoSize = true;
            this.labelLongtitude.Location = new System.Drawing.Point(20, 31);
            this.labelLongtitude.Name = "labelLongtitude";
            this.labelLongtitude.Size = new System.Drawing.Size(68, 13);
            this.labelLongtitude.TabIndex = 2;
            this.labelLongtitude.Text = "Longtitude(x)";
            // 
            // labelLatitude
            // 
            this.labelLatitude.AutoSize = true;
            this.labelLatitude.Location = new System.Drawing.Point(20, 61);
            this.labelLatitude.Name = "labelLatitude";
            this.labelLatitude.Size = new System.Drawing.Size(56, 13);
            this.labelLatitude.TabIndex = 3;
            this.labelLatitude.Text = "Latitude(y)";
            // 
            // toolTipLongtitude
            // 
            this.toolTipLongtitude.Tag = "Довгота";
            // 
            // btnConvertDecartToGeo
            // 
            this.btnConvertDecartToGeo.Location = new System.Drawing.Point(23, 84);
            this.btnConvertDecartToGeo.Name = "btnConvertDecartToGeo";
            this.btnConvertDecartToGeo.Size = new System.Drawing.Size(183, 23);
            this.btnConvertDecartToGeo.TabIndex = 4;
            this.btnConvertDecartToGeo.Text = "Декартові координати в гео";
            this.btnConvertDecartToGeo.UseVisualStyleBackColor = true;
            this.btnConvertDecartToGeo.Click += new System.EventHandler(this.btnConvertDecartToGeo_Click);
            // 
            // btnConvertGeoToDecart
            // 
            this.btnConvertGeoToDecart.Location = new System.Drawing.Point(23, 114);
            this.btnConvertGeoToDecart.Name = "btnConvertGeoToDecart";
            this.btnConvertGeoToDecart.Size = new System.Drawing.Size(182, 23);
            this.btnConvertGeoToDecart.TabIndex = 5;
            this.btnConvertGeoToDecart.Text = "Гео-координати в декартові";
            this.btnConvertGeoToDecart.UseVisualStyleBackColor = true;
            this.btnConvertGeoToDecart.Click += new System.EventHandler(this.btnConvertGeoToDecart_Click);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 581);
            this.Controls.Add(this.btnConvertGeoToDecart);
            this.Controls.Add(this.btnConvertDecartToGeo);
            this.Controls.Add(this.labelLatitude);
            this.Controls.Add(this.labelLongtitude);
            this.Controls.Add(this.tbLatitude);
            this.Controls.Add(this.tbLongtitude);
            this.Name = "MapForm";
            this.Text = "MapForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapForm_FormClosing);
            this.Load += new System.EventHandler(this.MapForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLongtitude;
        private System.Windows.Forms.TextBox tbLatitude;
        private System.Windows.Forms.Label labelLongtitude;
        private System.Windows.Forms.Label labelLatitude;
        private System.Windows.Forms.ToolTip toolTipLongtitude;
        private System.Windows.Forms.ToolTip toolTipLatitude;
        private System.Windows.Forms.Button btnConvertDecartToGeo;
        private System.Windows.Forms.Button btnConvertGeoToDecart;
    }
}