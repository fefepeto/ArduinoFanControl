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
            this.Temperature = new System.Windows.Forms.GroupBox();
            this.Voltage = new System.Windows.Forms.GroupBox();
            this.LoadGB = new System.Windows.Forms.GroupBox();
            this.Frequency = new System.Windows.Forms.GroupBox();
            this.RPM = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Due = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fanToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.measureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profileSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 570);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose port:";
            // 
            // COM_p
            // 
            this.COM_p.FormattingEnabled = true;
            this.COM_p.Location = new System.Drawing.Point(82, 567);
            this.COM_p.Name = "COM_p";
            this.COM_p.Size = new System.Drawing.Size(94, 21);
            this.COM_p.TabIndex = 1;
            this.COM_p.SelectedIndexChanged += new System.EventHandler(this.COM_p_SelectedIndexChanged);
            // 
            // Temperature
            // 
            this.Temperature.AutoSize = true;
            this.Temperature.Location = new System.Drawing.Point(12, 37);
            this.Temperature.Name = "Temperature";
            this.Temperature.Size = new System.Drawing.Size(200, 153);
            this.Temperature.TabIndex = 5;
            this.Temperature.TabStop = false;
            this.Temperature.Text = "Temperature";
            // 
            // Voltage
            // 
            this.Voltage.AutoSize = true;
            this.Voltage.Location = new System.Drawing.Point(12, 196);
            this.Voltage.Name = "Voltage";
            this.Voltage.Size = new System.Drawing.Size(200, 167);
            this.Voltage.TabIndex = 6;
            this.Voltage.TabStop = false;
            this.Voltage.Text = "Voltage";
            // 
            // LoadGB
            // 
            this.LoadGB.AutoSize = true;
            this.LoadGB.Location = new System.Drawing.Point(12, 369);
            this.LoadGB.Name = "LoadGB";
            this.LoadGB.Size = new System.Drawing.Size(200, 134);
            this.LoadGB.TabIndex = 7;
            this.LoadGB.TabStop = false;
            this.LoadGB.Text = "Load";
            // 
            // Frequency
            // 
            this.Frequency.AutoSize = true;
            this.Frequency.Location = new System.Drawing.Point(218, 37);
            this.Frequency.Name = "Frequency";
            this.Frequency.Size = new System.Drawing.Size(200, 153);
            this.Frequency.TabIndex = 8;
            this.Frequency.TabStop = false;
            this.Frequency.Text = "Frequency";
            // 
            // RPM
            // 
            this.RPM.AutoSize = true;
            this.RPM.Location = new System.Drawing.Point(218, 196);
            this.RPM.Name = "RPM";
            this.RPM.Size = new System.Drawing.Size(200, 167);
            this.RPM.TabIndex = 9;
            this.RPM.TabStop = false;
            this.RPM.Text = "RPM";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Due
            // 
            this.Due.AutoSize = true;
            this.Due.Location = new System.Drawing.Point(218, 369);
            this.Due.Name = "Due";
            this.Due.Size = new System.Drawing.Size(200, 134);
            this.Due.TabIndex = 10;
            this.Due.TabStop = false;
            this.Due.Text = "ArduinoDueFanController";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.fanToolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(430, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // fanToolsToolStripMenuItem
            // 
            this.fanToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.measureToolStripMenuItem,
            this.profileSetupToolStripMenuItem,
            this.configurationToolStripMenuItem});
            this.fanToolsToolStripMenuItem.Name = "fanToolsToolStripMenuItem";
            this.fanToolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.fanToolsToolStripMenuItem.Text = "Tools";
            // 
            // measureToolStripMenuItem
            // 
            this.measureToolStripMenuItem.Name = "measureToolStripMenuItem";
            this.measureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.measureToolStripMenuItem.Text = "Measure";
            this.measureToolStripMenuItem.Click += new System.EventHandler(this.measureToolStripMenuItem_Click);
            // 
            // profileSetupToolStripMenuItem
            // 
            this.profileSetupToolStripMenuItem.Name = "profileSetupToolStripMenuItem";
            this.profileSetupToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.profileSetupToolStripMenuItem.Text = "Profile Setup";
            this.profileSetupToolStripMenuItem.Click += new System.EventHandler(this.profileSetupToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // DispTip
            // 
            this.DispTip.AutomaticDelay = 250;
            this.DispTip.ToolTipTitle = "Displayed Value";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ArduinoController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(430, 600);
            this.Controls.Add(this.Due);
            this.Controls.Add(this.RPM);
            this.Controls.Add(this.Frequency);
            this.Controls.Add(this.LoadGB);
            this.Controls.Add(this.Voltage);
            this.Controls.Add(this.Temperature);
            this.Controls.Add(this.COM_p);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ArduinoController";
            this.Text = "ArduinoDue";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.ArduinoController_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox COM_p;
        private System.Windows.Forms.GroupBox Temperature;
        private System.Windows.Forms.GroupBox Voltage;
        private System.Windows.Forms.GroupBox LoadGB;
        private System.Windows.Forms.GroupBox Frequency;
        private System.Windows.Forms.GroupBox RPM;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox Due;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fanToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profileSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolTip DispTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

