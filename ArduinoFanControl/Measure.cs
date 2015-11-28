using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoFanControl
{
    public partial class Measure : Form
    {
        SerialPort port;

        public struct Data
        {
            public int RPM;
            public short df;
        }

        public Data current;

        public Measure(SerialPort ser)
        {
            InitializeComponent();
            port = ser;
            if (!port.IsOpen) port.Open();
            port.DataReceived += port_DataReceived;
        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            string input = port.ReadLine();
        }

        private void MeasureButton_Click(object sender, EventArgs e)
        {
            if (PWMc.Checked) port.WriteLine("M"+Channel.SelectedIndex.ToString()+"1");
            else port.WriteLine("M" + Channel.SelectedIndex.ToString() + "0");
            MeasureButton.Enabled = false;
        }

        private void Measure_Load(object sender, EventArgs e)
        {
            Graph_Paint(sender, new PaintEventArgs(Graph.CreateGraphics(), new Rectangle(0, 0, Graph.Width, Graph.Height)));
        }

        private void Graph_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black);

            for (int y = 0; y < 22; y++)
            {
                g.DrawLine(p, y * 30, 0, y * 30, 501);
            }

            for (int x = 0; x < 11; x++)
            {
                g.DrawLine(p, 0, x * 50, 631, x * 50);
            }
        }
    }
}
