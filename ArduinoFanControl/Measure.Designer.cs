namespace ArduinoFanControl
{
    partial class Measure
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
            this.Graph = new System.Windows.Forms.PictureBox();
            this.OK = new System.Windows.Forms.Button();
            this.MeasureButton = new System.Windows.Forms.Button();
            this.Channel = new System.Windows.Forms.DomainUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PWMc = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MaxRpm = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).BeginInit();
            this.SuspendLayout();
            // 
            // Graph
            // 
            this.Graph.Location = new System.Drawing.Point(36, 12);
            this.Graph.Name = "Graph";
            this.Graph.Size = new System.Drawing.Size(631, 501);
            this.Graph.TabIndex = 0;
            this.Graph.TabStop = false;
            this.Graph.Paint += new System.Windows.Forms.PaintEventHandler(this.Graph_Paint);
            // 
            // OK
            // 
            this.OK.Enabled = false;
            this.OK.Location = new System.Drawing.Point(592, 606);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // MeasureButton
            // 
            this.MeasureButton.Location = new System.Drawing.Point(511, 606);
            this.MeasureButton.Name = "MeasureButton";
            this.MeasureButton.Size = new System.Drawing.Size(75, 23);
            this.MeasureButton.TabIndex = 2;
            this.MeasureButton.Text = "Measure";
            this.MeasureButton.UseVisualStyleBackColor = true;
            this.MeasureButton.Click += new System.EventHandler(this.MeasureButton_Click);
            // 
            // Channel
            // 
            this.Channel.Items.Add("1");
            this.Channel.Items.Add("2");
            this.Channel.Items.Add("3");
            this.Channel.Items.Add("4");
            this.Channel.Items.Add("5");
            this.Channel.Items.Add("6");
            this.Channel.Location = new System.Drawing.Point(171, 539);
            this.Channel.Name = "Channel";
            this.Channel.Size = new System.Drawing.Size(38, 20);
            this.Channel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 541);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select fan channel:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 567);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "The fan is PWM capable:";
            // 
            // PWMc
            // 
            this.PWMc.AutoSize = true;
            this.PWMc.Location = new System.Drawing.Point(171, 566);
            this.PWMc.Name = "PWMc";
            this.PWMc.Size = new System.Drawing.Size(95, 17);
            this.PWMc.TabIndex = 6;
            this.PWMc.Text = "PWM Capable";
            this.PWMc.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 520);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(642, 520);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "210";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 520);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Duty Factor";
            // 
            // MaxRpm
            // 
            this.MaxRpm.AutoSize = true;
            this.MaxRpm.Location = new System.Drawing.Point(5, 9);
            this.MaxRpm.Name = "MaxRpm";
            this.MaxRpm.Size = new System.Drawing.Size(25, 13);
            this.MaxRpm.TabIndex = 10;
            this.MaxRpm.Text = "100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-2, 267);
            this.label7.MaximumSize = new System.Drawing.Size(33, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 26);
            this.label7.TabIndex = 11;
            this.label7.Text = "RevsRPM";
            // 
            // Measure
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 641);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MaxRpm);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PWMc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Channel);
            this.Controls.Add(this.MeasureButton);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Graph);
            this.Name = "Measure";
            this.Text = "Measure";
            this.Load += new System.EventHandler(this.Measure_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button MeasureButton;
        private System.Windows.Forms.DomainUpDown Channel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox PWMc;
        private System.Windows.Forms.PictureBox Graph;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label MaxRpm;
        private System.Windows.Forms.Label label7;
    }
}