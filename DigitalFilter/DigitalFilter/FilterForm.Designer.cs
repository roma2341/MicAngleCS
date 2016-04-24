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
            this.lbFilters = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxFilterMode = new System.Windows.Forms.GroupBox();
            this.rbFilterBackward = new System.Windows.Forms.RadioButton();
            this.rbFilterForward = new System.Windows.Forms.RadioButton();
            this.rbAllPass = new System.Windows.Forms.RadioButton();
            this.groupBoxFilterMode.SuspendLayout();
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
            // lbFilters
            // 
            this.lbFilters.FormattingEnabled = true;
            this.lbFilters.Location = new System.Drawing.Point(383, 42);
            this.lbFilters.Name = "lbFilters";
            this.lbFilters.Size = new System.Drawing.Size(249, 199);
            this.lbFilters.TabIndex = 9;
            this.lbFilters.SelectedIndexChanged += new System.EventHandler(this.lbFilters_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(383, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Filters";
            // 
            // groupBoxFilterMode
            // 
            this.groupBoxFilterMode.Controls.Add(this.rbAllPass);
            this.groupBoxFilterMode.Controls.Add(this.rbFilterBackward);
            this.groupBoxFilterMode.Controls.Add(this.rbFilterForward);
            this.groupBoxFilterMode.Location = new System.Drawing.Point(208, 194);
            this.groupBoxFilterMode.Name = "groupBoxFilterMode";
            this.groupBoxFilterMode.Size = new System.Drawing.Size(106, 113);
            this.groupBoxFilterMode.TabIndex = 11;
            this.groupBoxFilterMode.TabStop = false;
            this.groupBoxFilterMode.Text = "Filter Mode";
            // 
            // rbFilterBackward
            // 
            this.rbFilterBackward.AutoSize = true;
            this.rbFilterBackward.Location = new System.Drawing.Point(6, 53);
            this.rbFilterBackward.Name = "rbFilterBackward";
            this.rbFilterBackward.Size = new System.Drawing.Size(73, 17);
            this.rbFilterBackward.TabIndex = 13;
            this.rbFilterBackward.Text = "Backward";
            this.rbFilterBackward.UseVisualStyleBackColor = true;
            this.rbFilterBackward.CheckedChanged += new System.EventHandler(this.rbFilter_CheckedChanged);
            // 
            // rbFilterForward
            // 
            this.rbFilterForward.AutoSize = true;
            this.rbFilterForward.Checked = true;
            this.rbFilterForward.Location = new System.Drawing.Point(6, 30);
            this.rbFilterForward.Name = "rbFilterForward";
            this.rbFilterForward.Size = new System.Drawing.Size(63, 17);
            this.rbFilterForward.TabIndex = 12;
            this.rbFilterForward.TabStop = true;
            this.rbFilterForward.Text = "Forward";
            this.rbFilterForward.UseVisualStyleBackColor = true;
            this.rbFilterForward.CheckedChanged += new System.EventHandler(this.rbFilter_CheckedChanged);
            // 
            // rbAllPass
            // 
            this.rbAllPass.AutoSize = true;
            this.rbAllPass.Location = new System.Drawing.Point(7, 76);
            this.rbAllPass.Name = "rbAllPass";
            this.rbAllPass.Size = new System.Drawing.Size(59, 17);
            this.rbAllPass.TabIndex = 14;
            this.rbAllPass.TabStop = true;
            this.rbAllPass.Text = "AllPass";
            this.rbAllPass.UseVisualStyleBackColor = true;
            this.rbAllPass.CheckedChanged += new System.EventHandler(this.rbFilter_CheckedChanged);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 351);
            this.Controls.Add(this.groupBoxFilterMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbFilters);
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
            this.groupBoxFilterMode.ResumeLayout(false);
            this.groupBoxFilterMode.PerformLayout();
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
        private System.Windows.Forms.ListBox lbFilters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbFilterBackward;
        private System.Windows.Forms.RadioButton rbFilterForward;
        private System.Windows.Forms.GroupBox groupBoxFilterMode;
        private System.Windows.Forms.RadioButton rbAllPass;
    }
}