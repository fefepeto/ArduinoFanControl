namespace ArduinoFanControl
{
    partial class ArduinoController
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
            this.label1 = new System.Windows.Forms.Label();
            this.COM_p = new System.Windows.Forms.ComboBox();
            this.TextR = new System.Windows.Forms.Label();
            this.Temperature = new System.Windows.Forms.GroupBox();
            this.Voltage = new System.Windows.Forms.GroupBox();
            this.LoadGB = new System.Windows.Forms.GroupBox();
            this.Frequency = new System.Windows.Forms.GroupBox();
            this.RPM = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Due = new System.Windows.Forms.GroupBox();
            this.Config = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose port:";
            // 
            // COM_p
            // 
            this.COM_p.FormattingEnabled = true;
            this.COM_p.Location = new System.Drawing.Point(86, 10);
            this.COM_p.Name = "COM_p";
            this.COM_p.Size = new System.Drawing.Size(94, 21);
            this.COM_p.TabIndex = 1;
            this.COM_p.SelectedIndexChanged += new System.EventHandler(this.COM_p_SelectedIndexChanged);
            // 
            // TextR
            // 
            this.TextR.AutoSize = true;
            this.TextR.Location = new System.Drawing.Point(13, 62);
            this.TextR.Name = "TextR";
            this.TextR.Size = new System.Drawing.Size(35, 13);
            this.TextR.TabIndex = 3;
            this.TextR.Text = "label2";
            this.TextR.Click += new System.EventHandler(this.TextR_Click);
            // 
            // Temperature
            // 
            this.Temperature.AutoSize = true;
            this.Temperature.Location = new System.Drawing.Point(13, 96);
            this.Temperature.Name = "Temperature";
            this.Temperature.Size = new System.Drawing.Size(200, 153);
            this.Temperature.TabIndex = 5;
            this.Temperature.TabStop = false;
            this.Temperature.Text = "Temperature";
            // 
            // Voltage
            // 
            this.Voltage.AutoSize = true;
            this.Voltage.Location = new System.Drawing.Point(13, 255);
            this.Voltage.Name = "Voltage";
            this.Voltage.Size = new System.Drawing.Size(200, 167);
            this.Voltage.TabIndex = 6;
            this.Voltage.TabStop = false;
            this.Voltage.Text = "Voltage";
            // 
            // LoadGB
            // 
            this.LoadGB.AutoSize = true;
            this.LoadGB.Location = new System.Drawing.Point(13, 428);
            this.LoadGB.Name = "LoadGB";
            this.LoadGB.Size = new System.Drawing.Size(200, 134);
            this.LoadGB.TabIndex = 7;
            this.LoadGB.TabStop = false;
            this.LoadGB.Text = "Load";
            // 
            // Frequency
            // 
            this.Frequency.AutoSize = true;
            this.Frequency.Location = new System.Drawing.Point(219, 96);
            this.Frequency.Name = "Frequency";
            this.Frequency.Size = new System.Drawing.Size(200, 153);
            this.Frequency.TabIndex = 8;
            this.Frequency.TabStop = false;
            this.Frequency.Text = "Frequency";
            // 
            // RPM
            // 
            this.RPM.AutoSize = true;
            this.RPM.Location = new System.Drawing.Point(219, 255);
            this.RPM.Name = "RPM";
            this.RPM.Size = new System.Drawing.Size(200, 167);
            this.RPM.TabIndex = 9;
            this.RPM.TabStop = false;
            this.RPM.Text = "RPM";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // Due
            // 
            this.Due.AutoSize = true;
            this.Due.Location = new System.Drawing.Point(219, 428);
            this.Due.Name = "Due";
            this.Due.Size = new System.Drawing.Size(200, 134);
            this.Due.TabIndex = 10;
            this.Due.TabStop = false;
            this.Due.Text = "ArduinoDueFanController";
            // 
            // Config
            // 
            this.Config.AutoSize = true;
            this.Config.Location = new System.Drawing.Point(340, 565);
            this.Config.Name = "Config";
            this.Config.Size = new System.Drawing.Size(79, 23);
            this.Config.TabIndex = 11;
            this.Config.Text = "Configuration";
            this.Config.UseVisualStyleBackColor = true;
            this.Config.Click += new System.EventHandler(this.Config_Click);
            // 
            // ArduinoController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(430, 600);
            this.Controls.Add(this.Config);
            this.Controls.Add(this.Due);
            this.Controls.Add(this.RPM);
            this.Controls.Add(this.Frequency);
            this.Controls.Add(this.LoadGB);
            this.Controls.Add(this.Voltage);
            this.Controls.Add(this.Temperature);
            this.Controls.Add(this.TextR);
            this.Controls.Add(this.COM_p);
            this.Controls.Add(this.label1);
            this.Name = "ArduinoController";
            this.Text = "ArduinoDue";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox COM_p;
        private System.Windows.Forms.Label TextR;
        private System.Windows.Forms.GroupBox Temperature;
        private System.Windows.Forms.GroupBox Voltage;
        private System.Windows.Forms.GroupBox LoadGB;
        private System.Windows.Forms.GroupBox Frequency;
        private System.Windows.Forms.GroupBox RPM;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox Due;
        private System.Windows.Forms.Button Config;
    }
}

