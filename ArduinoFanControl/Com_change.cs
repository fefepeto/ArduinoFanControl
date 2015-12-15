using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace ArduinoFanControl
{
    public partial class Com_change : Form
    {
        public string Name;
        string usb;

        public Com_change(string USB)
        {
            InitializeComponent();
            usb = USB;
        }

        private void COM_changer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Name = COM_changer.SelectedItem.ToString();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            COM_changer.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string s in ports) COM_changer.Items.Add(s);
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Com_change_Load(object sender, EventArgs e)
        {
            label1.Text = "Please connect the USB cable to the " + usb + " port of the Arduino.";
            string[] ports = SerialPort.GetPortNames();
            foreach (string s in ports) COM_changer.Items.Add(s);
        }
    }
}
