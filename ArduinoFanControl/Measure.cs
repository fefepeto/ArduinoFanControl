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
        SerialPort port = new SerialPort();

        public struct Data
        {
            public int RPM;
            public short df;
        }

        public Data current;
        int maxRPM;
        Graphics baseGraph;

        List<Point> Points = new List<Point>();
        List<Point> drawing = new List<Point>();

        delegate void SetTextCallback(string text, Label l);

        public void SetText(string text, Label l)
        {
            if (l.InvokeRequired)
            {
                SetTextCallback d = new ArduinoFanControl.Measure.SetTextCallback(SetText);
                this.Invoke(d, new object[] { text, l });
            }
            else
            {
                l.Text = text;
            }
        }

        public Measure(SerialPort ser)
        {
            InitializeComponent();
            port.PortName = ser.PortName;
            port.BaudRate = ser.BaudRate;
            port.Parity = ser.Parity;
            port.StopBits = ser.StopBits;
            port.DataBits = ser.DataBits;
            port.Handshake = ser.Handshake;
            port.RtsEnable = ser.RtsEnable;
            port.DtrEnable = ser.DtrEnable;
            port.ReadTimeout = 1000;
            port.WriteTimeout = 1000;
        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            string input = port.ReadLine();
            if (input.Split(' ').Length == 2)
            {
                current.df = short.Parse(input.Split(' ')[0]);
                current.RPM = int.Parse(input.Split(' ')[1]);
                if (current.RPM > maxRPM)
                {
                    maxRPM = current.RPM;
                    SetText(maxRPM.ToString(), MaxRpm);
                }
                Point p = new Point(current.df, current.RPM);
                Points.Add(p);
                Point dp;
                if (current.RPM > 0) dp = new Point(current.df * 3, (current.RPM * 500) / maxRPM);
                else dp = new Point(current.df * 3, 0);
                drawing.Add(dp);
                for (int i = 0; i < drawing.Count; i++)
                {
                    Point tmp = drawing[i];
                    if (maxRPM != 0) tmp.Y = 500 - (Points[i].Y * 500) / maxRPM;
                    else tmp.Y = 500;
                    drawing[i] = tmp;
                }
                Paint(Graph.CreateGraphics(), ref drawing);
            }
        }

        private void MeasureButton_Click(object sender, EventArgs e)
        {
            if (PWMc.Checked) port.WriteLine("M"+Channel.SelectedIndex.ToString()+"1");
            else port.WriteLine("M" + Channel.SelectedIndex.ToString() + "0");
            MeasureButton.Enabled = false;
            port.DataReceived += port_DataReceived;
        }

        private void Measure_Load(object sender, EventArgs e)
        {
            Graph_Paint(sender, new PaintEventArgs(Graph.CreateGraphics(), new Rectangle(0, 0, Graph.Width, Graph.Height)));
            baseGraph = Graph.CreateGraphics();

            if (!port.IsOpen) port.Open();
        }

        private void Paint(Graphics gr, ref List<Point> point)
        {
            Graphics graph = gr;
            graph.Clear(SystemColors.Window);

            Pen pen = new Pen(Color.Black);

            for (int y = 0; y < 22; y++)
            {
                gr.DrawLine(pen, y * 30, 0, y * 30, 501);
            }

            for (int x = 0; x < 11; x++)
            {
                gr.DrawLine(pen, 0, x * 50, 631, x * 50);
            }

            SolidBrush b = new SolidBrush(Color.Red);

            foreach (Point p in point)
            {
                gr.FillRectangle(b, p.X - 1, p.Y - 1, 3, 3);
            }
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
