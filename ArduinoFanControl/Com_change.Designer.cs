namespace ArduinoFanControl
{
    partial class Com_change
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.COM_changer = new System.Windows.Forms.ComboBox();
            this.OK = new System.Windows.Forms.Button();
            this.Refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please connect the USB cable to the programming port of the Arduino.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Choose the right port:";
            // 
            // COM_changer
            // 
            this.COM_changer.FormattingEnabled = true;
            this.COM_changer.Location = new System.Drawing.Point(128, 30);
            this.COM_changer.Name = "COM_changer";
            this.COM_changer.Size = new System.Drawing.Size(121, 21);
            this.COM_changer.TabIndex = 2;
            this.COM_changer.SelectedIndexChanged += new System.EventHandler(this.COM_changer_SelectedIndexChanged);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(272, 65);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 3;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(191, 65);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(75, 23);
            this.Refresh.TabIndex = 4;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // Com_change
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 100);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.COM_changer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Com_change";
            this.Text = "COM Change";
            this.Load += new System.EventHandler(this.Com_change_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox COM_changer;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Refresh;
    }
}