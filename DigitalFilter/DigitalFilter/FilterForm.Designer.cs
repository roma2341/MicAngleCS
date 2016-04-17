namespace DigitalFilter
{
    partial class FilterForm
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
            this.btnDoFilter = new System.Windows.Forms.Button();
            this.btnAssignFilteredSignal = new System.Windows.Forms.Button();
            this.cbAutoApply = new System.Windows.Forms.CheckBox();
            this.rtbFilterKoff = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFilterOrder = new System.Windows.Forms.TextBox();
            this.lblFilterStep = new System.Windows.Forms.Label();
            this.tbFilterIterationsCount = new System.Windows.Forms.TextBox();
            this.btnResetSignal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDoFilter
            // 
            this.btnDoFilter.Location = new System.Drawing.Point(208, 164);
            this.btnDoFilter.Name = "btnDoFilter";
            this.btnDoFilter.Size = new System.Drawing.Size(75, 23);
            this.btnDoFilter.TabIndex = 0;
            this.btnDoFilter.Text = "Do filter";
            this.btnDoFilter.UseVisualStyleBackColor = true;
            this.btnDoFilter.Click += new System.EventHandler(this.btnDoFilter_Click);
            // 
            // btnAssignFilteredSignal
            // 
            this.btnAssignFilteredSignal.Location = new System.Drawing.Point(13, 198);
            this.btnAssignFilteredSignal.Name = "btnAssignFilteredSignal";
            this.btnAssignFilteredSignal.Size = new System.Drawing.Size(75, 23);
            this.btnAssignFilteredSignal.TabIndex = 1;
            this.btnAssignFilteredSignal.Text = "Apply";
            this.btnAssignFilteredSignal.UseVisualStyleBackColor = true;
            this.btnAssignFilteredSignal.Click += new System.EventHandler(this.btnAssignFilteredSignal_Click);
            // 
            // cbAutoApply
            // 
            this.cbAutoApply.AutoSize = true;
            this.cbAutoApply.Location = new System.Drawing.Point(290, 169);
            this.cbAutoApply.Name = "cbAutoApply";
            this.cbAutoApply.Size = new System.Drawing.Size(75, 17);
            this.cbAutoApply.TabIndex = 2;
            this.cbAutoApply.Text = "auto apply";
            this.cbAutoApply.UseVisualStyleBackColor = true;
            // 
            // rtbFilterKoff
            // 
            this.rtbFilterKoff.Location = new System.Drawing.Point(12, 42);
            this.rtbFilterKoff.Name = "rtbFilterKoff";
            this.rtbFilterKoff.Size = new System.Drawing.Size(343, 116);
            this.rtbFilterKoff.TabIndex = 3;
            this.rtbFilterKoff.Text = "-0.13558101563092881, 0.60427293583084785, 0.60427293583084785,-0.135581015630928" +
    "81";
            this.rtbFilterKoff.TextChanged += new System.EventHandler(this.rtbFilterKoff_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Filter order";
            // 
            // tbFilterOrder
            // 
            this.tbFilterOrder.Location = new System.Drawing.Point(76, 15);
            this.tbFilterOrder.Name = "tbFilterOrder";
            this.tbFilterOrder.Size = new System.Drawing.Size(93, 20);
            this.tbFilterOrder.TabIndex = 5;
            this.tbFilterOrder.Text = "3";
            // 
            // lblFilterStep
            // 
            this.lblFilterStep.AutoSize = true;
            this.lblFilterStep.Location = new System.Drawing.Point(17, 168);
            this.lblFilterStep.Name = "lblFilterStep";
            this.lblFilterStep.Size = new System.Drawing.Size(74, 13);
            this.lblFilterStep.TabIndex = 6;
            this.lblFilterStep.Text = "Filter iterations";
            // 
            // tbFilterIterationsCount
            // 
            this.tbFilterIterationsCount.Location = new System.Drawing.Point(97, 164);
            this.tbFilterIterationsCount.Name = "tbFilterIterationsCount";
            this.tbFilterIterationsCount.Size = new System.Drawing.Size(100, 20);
            this.tbFilterIterationsCount.TabIndex = 7;
            this.tbFilterIterationsCount.Text = "1";
            // 
            // btnResetSignal
            // 
            this.btnResetSignal.Location = new System.Drawing.Point(97, 198);
            this.btnResetSignal.Name = "btnResetSignal";
            this.btnResetSignal.Size = new System.Drawing.Size(75, 23);
            this.btnResetSignal.TabIndex = 8;
            this.btnResetSignal.Text = "Reset";
            this.btnResetSignal.UseVisualStyleBackColor = true;
            this.btnResetSignal.Click += new System.EventHandler(this.btnResetSignal_Click);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 351);
            this.Controls.Add(this.btnResetSignal);
            this.Controls.Add(this.tbFilterIterationsCount);
            this.Controls.Add(this.lblFilterStep);
            this.Controls.Add(this.tbFilterOrder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbFilterKoff);
            this.Controls.Add(this.cbAutoApply);
            this.Controls.Add(this.btnAssignFilteredSignal);
            this.Controls.Add(this.btnDoFilter);
            this.Name = "FilterForm";
            this.Text = "FilterForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDoFilter;
        private System.Windows.Forms.Button btnAssignFilteredSignal;
        private System.Windows.Forms.CheckBox cbAutoApply;
        private System.Windows.Forms.RichTextBox rtbFilterKoff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFilterOrder;
        private System.Windows.Forms.Label lblFilterStep;
        private System.Windows.Forms.TextBox tbFilterIterationsCount;
        private System.Windows.Forms.Button btnResetSignal;
    }
}