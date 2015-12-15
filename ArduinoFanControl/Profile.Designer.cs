namespace ArduinoFanControl
{
    partial class Profile
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
            this.ProfileChart = new System.Windows.Forms.PictureBox();
            this.ProfileBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PWMc = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MaxOfSpeeds = new System.Windows.Forms.RadioButton();
            this.SumOfSpeeds = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.Lowest = new System.Windows.Forms.NumericUpDown();
            this.Highest = new System.Windows.Forms.NumericUpDown();
            this.FanSel = new System.Windows.Forms.ListBox();
            this.TempSel = new System.Windows.Forms.ListBox();
            this.OK = new System.Windows.Forms.Button();
            this.Program = new System.Windows.Forms.Button();
            this.Hyst = new System.Windows.Forms.NumericUpDown();
            this.tempdifference = new System.Windows.Forms.GroupBox();
            this.TD20 = new System.Windows.Forms.RadioButton();
            this.TD10 = new System.Windows.Forms.RadioButton();
            this.Clear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProfileChart)).BeginInit();
            this.ProfileBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lowest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Highest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hyst)).BeginInit();
            this.tempdifference.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProfileChart
            // 
            this.ProfileChart.ContextMenuStrip = this.ProfileBox;
            this.ProfileChart.Location = new System.Drawing.Point(43, 9);
            this.ProfileChart.Name = "ProfileChart";
            this.ProfileChart.Size = new System.Drawing.Size(571, 421);
            this.ProfileChart.TabIndex = 0;
            this.ProfileChart.TabStop = false;
            this.ProfileChart.Click += new System.EventHandler(this.ProfileChart_Click);
            this.ProfileChart.Paint += new System.Windows.Forms.PaintEventHandler(this.ProfileChart_Paint);
            // 
            // ProfileBox
            // 
            this.ProfileBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Copy,
            this.Paste});
            this.ProfileBox.Name = "ProfileBox";
            this.ProfileBox.Size = new System.Drawing.Size(103, 48);
            // 
            // Copy
            // 
            this.Copy.Name = "Copy";
            this.Copy.Size = new System.Drawing.Size(102, 22);
            this.Copy.Text = "Copy";
            this.Copy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // Paste
            // 
            this.Paste.Name = "Paste";
            this.Paste.Size = new System.Drawing.Size(102, 22);
            this.Paste.Text = "Paste";
            this.Paste.Click += new System.EventHandler(this.Paste_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "210";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 417);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "DF";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(307, 439);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Temperature [°C]";
            // 
            // PWMc
            // 
            this.PWMc.AutoSize = true;
            this.PWMc.Location = new System.Drawing.Point(43, 514);
            this.PWMc.Name = "PWMc";
            this.PWMc.Size = new System.Drawing.Size(95, 17);
            this.PWMc.TabIndex = 7;
            this.PWMc.Text = "PWM Capable";
            this.PWMc.UseVisualStyleBackColor = true;
            this.PWMc.CheckedChanged += new System.EventHandler(this.PWMc_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MaxOfSpeeds);
            this.groupBox1.Controls.Add(this.SumOfSpeeds);
            this.groupBox1.Location = new System.Drawing.Point(383, 489);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 41);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Method";
            // 
            // MaxOfSpeeds
            // 
            this.MaxOfSpeeds.AutoSize = true;
            this.MaxOfSpeeds.Location = new System.Drawing.Point(127, 19);
            this.MaxOfSpeeds.Name = "MaxOfSpeeds";
            this.MaxOfSpeeds.Size = new System.Drawing.Size(98, 17);
            this.MaxOfSpeeds.TabIndex = 1;
            this.MaxOfSpeeds.TabStop = true;
            this.MaxOfSpeeds.Text = "Max Of Speeds";
            this.MaxOfSpeeds.UseVisualStyleBackColor = true;
            this.MaxOfSpeeds.CheckedChanged += new System.EventHandler(this.MaxOfSpeeds_CheckedChanged);
            // 
            // SumOfSpeeds
            // 
            this.SumOfSpeeds.AutoSize = true;
            this.SumOfSpeeds.Location = new System.Drawing.Point(6, 19);
            this.SumOfSpeeds.Name = "SumOfSpeeds";
            this.SumOfSpeeds.Size = new System.Drawing.Size(99, 17);
            this.SumOfSpeeds.TabIndex = 0;
            this.SumOfSpeeds.TabStop = true;
            this.SumOfSpeeds.Text = "Sum Of Speeds";
            this.SumOfSpeeds.UseVisualStyleBackColor = true;
            this.SumOfSpeeds.CheckedChanged += new System.EventHandler(this.SumOfSpeeds_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(410, 465);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Hysteresis:";
            // 
            // Lowest
            // 
            this.Lowest.DecimalPlaces = 1;
            this.Lowest.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.Lowest.Location = new System.Drawing.Point(43, 437);
            this.Lowest.Maximum = new decimal(new int[] {
            705,
            0,
            0,
            65536});
            this.Lowest.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.Lowest.Name = "Lowest";
            this.Lowest.Size = new System.Drawing.Size(50, 20);
            this.Lowest.TabIndex = 11;
            this.Lowest.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.Lowest.ValueChanged += new System.EventHandler(this.Lowest_ValueChanged);
            // 
            // Highest
            // 
            this.Highest.DecimalPlaces = 1;
            this.Highest.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.Highest.Location = new System.Drawing.Point(564, 436);
            this.Highest.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.Highest.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.Highest.Name = "Highest";
            this.Highest.Size = new System.Drawing.Size(50, 20);
            this.Highest.TabIndex = 12;
            this.Highest.Value = new decimal(new int[] {
            495,
            0,
            0,
            65536});
            this.Highest.ValueChanged += new System.EventHandler(this.Highest_ValueChanged);
            // 
            // FanSel
            // 
            this.FanSel.FormattingEnabled = true;
            this.FanSel.Location = new System.Drawing.Point(43, 537);
            this.FanSel.Name = "FanSel";
            this.FanSel.Size = new System.Drawing.Size(120, 82);
            this.FanSel.TabIndex = 13;
            this.FanSel.SelectedIndexChanged += new System.EventHandler(this.FanSel_SelectedIndexChanged);
            // 
            // TempSel
            // 
            this.TempSel.FormattingEnabled = true;
            this.TempSel.Location = new System.Drawing.Point(383, 537);
            this.TempSel.Name = "TempSel";
            this.TempSel.Size = new System.Drawing.Size(231, 121);
            this.TempSel.TabIndex = 14;
            this.TempSel.SelectedIndexChanged += new System.EventHandler(this.TempSel_SelectedIndexChanged);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(539, 664);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 15;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Program
            // 
            this.Program.Location = new System.Drawing.Point(458, 664);
            this.Program.Name = "Program";
            this.Program.Size = new System.Drawing.Size(75, 23);
            this.Program.TabIndex = 16;
            this.Program.Text = "Program";
            this.Program.UseVisualStyleBackColor = true;
            this.Program.Click += new System.EventHandler(this.Program_Click);
            // 
            // Hyst
            // 
            this.Hyst.DecimalPlaces = 1;
            this.Hyst.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.Hyst.Location = new System.Drawing.Point(474, 463);
            this.Hyst.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Hyst.Name = "Hyst";
            this.Hyst.Size = new System.Drawing.Size(43, 20);
            this.Hyst.TabIndex = 17;
            this.Hyst.ValueChanged += new System.EventHandler(this.Hyst_ValueChanged);
            // 
            // tempdifference
            // 
            this.tempdifference.Controls.Add(this.TD20);
            this.tempdifference.Controls.Add(this.TD10);
            this.tempdifference.Location = new System.Drawing.Point(203, 490);
            this.tempdifference.Name = "tempdifference";
            this.tempdifference.Size = new System.Drawing.Size(132, 40);
            this.tempdifference.TabIndex = 19;
            this.tempdifference.TabStop = false;
            this.tempdifference.Text = "Temperature difference";
            // 
            // TD20
            // 
            this.TD20.AutoSize = true;
            this.TD20.Location = new System.Drawing.Point(79, 19);
            this.TD20.Name = "TD20";
            this.TD20.Size = new System.Drawing.Size(37, 17);
            this.TD20.TabIndex = 1;
            this.TD20.TabStop = true;
            this.TD20.Text = "19";
            this.TD20.UseVisualStyleBackColor = true;
            this.TD20.CheckedChanged += new System.EventHandler(this.TD20_CheckedChanged);
            // 
            // TD10
            // 
            this.TD10.AutoSize = true;
            this.TD10.Location = new System.Drawing.Point(7, 17);
            this.TD10.Name = "TD10";
            this.TD10.Size = new System.Drawing.Size(40, 17);
            this.TD10.TabIndex = 0;
            this.TD10.TabStop = true;
            this.TD10.Text = "9.5";
            this.TD10.UseVisualStyleBackColor = true;
            this.TD10.CheckedChanged += new System.EventHandler(this.TD10_CheckedChanged);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(43, 664);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 20;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Profile
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 694);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.tempdifference);
            this.Controls.Add(this.Hyst);
            this.Controls.Add(this.Program);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.TempSel);
            this.Controls.Add(this.FanSel);
            this.Controls.Add(this.Highest);
            this.Controls.Add(this.Lowest);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PWMc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProfileChart);
            this.Name = "Profile";
            this.Text = "Profile";
            this.Load += new System.EventHandler(this.Profile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProfileChart)).EndInit();
            this.ProfileBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lowest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Highest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hyst)).EndInit();
            this.tempdifference.ResumeLayout(false);
            this.tempdifference.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ProfileChart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox PWMc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton MaxOfSpeeds;
        private System.Windows.Forms.RadioButton SumOfSpeeds;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown Lowest;
        private System.Windows.Forms.NumericUpDown Highest;
        private System.Windows.Forms.ListBox FanSel;
        private System.Windows.Forms.ListBox TempSel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Program;
        private System.Windows.Forms.NumericUpDown Hyst;
        private System.Windows.Forms.GroupBox tempdifference;
        private System.Windows.Forms.RadioButton TD20;
        private System.Windows.Forms.RadioButton TD10;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.ContextMenuStrip ProfileBox;
        private System.Windows.Forms.ToolStripMenuItem Copy;
        private System.Windows.Forms.ToolStripMenuItem Paste;
    }
}