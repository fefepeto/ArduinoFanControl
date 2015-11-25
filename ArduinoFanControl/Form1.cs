using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;


namespace ArduinoFanControl
{
    public partial class ArduinoController : Form
    {
        public Computer thisPC;
        SerialPort serial;
        string input = "";

        List<SensorData> Temperatures = new List<SensorData>();
        List<SensorData> Voltages = new List<SensorData>();
        List<SensorData> Loads = new List<SensorData>();
        List<SensorData> Frequencies = new List<SensorData>();
        List<SensorData> RPMs = new List<SensorData>();

        public ArduinoController()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string s in ports) COM_p.Items.Add(s);

            thisPC = new Computer();
            thisPC.CPUEnabled = true;
            thisPC.GPUEnabled = true;
            thisPC.HDDEnabled = true;
            thisPC.MainboardEnabled = true;
            thisPC.RAMEnabled = true;
            thisPC.Open();

            foreach (var hardwareItem in thisPC.Hardware)
            {
                hardwareItem.Update();
                foreach (IHardware subHardware in hardwareItem.SubHardware)
                {
                    subHardware.Update();
                    foreach (var sensor in subHardware.Sensors)
                    {
                        addNewSensorData(sensor);
                    }
                }

                foreach (var sensor in hardwareItem.Sensors)
                {
                    addNewSensorData(sensor);
                }
            }

            timer1.Enabled = true;
        }

        private void addNewSensorData(ISensor sensor)
        {
            SensorData current = new SensorData(sensor);
            current.label.AutoSize = true;

            switch (current.sensor.SensorType)
            {
                case SensorType.Temperature:
                    current.label.Name = current.sensor.SensorType.ToString() + (Temperatures.Count).ToString();
                    this.Temperature.Controls.Add(current.label);
                    current.label.Location = new System.Drawing.Point(6, 16 + Temperatures.Count * 13);
                    current.label.TabIndex = Temperatures.Count;
                    Temperatures.Add(current);
                    break;
                case SensorType.Voltage:
                    current.label.Name = current.sensor.SensorType.ToString() + (Voltages.Count).ToString();
                    this.Voltage.Controls.Add(current.label);
                    current.label.Location = new System.Drawing.Point(6, 16 + Voltages.Count * 13);
                    current.label.TabIndex = Voltages.Count;
                    Voltages.Add(current);
                    break;
                case SensorType.Load:
                    current.label.Name = current.sensor.SensorType.ToString() + (Loads.Count).ToString();
                    this.LoadGB.Controls.Add(current.label);
                    current.label.Location = new System.Drawing.Point(6, 16 + Loads.Count * 13);
                    current.label.TabIndex = Loads.Count;
                    Loads.Add(current);
                    break;
                case SensorType.Clock:
                    current.label.Name = current.sensor.SensorType.ToString() + (Frequencies.Count).ToString();
                    this.Frequency.Controls.Add(current.label);
                    current.label.Location = new System.Drawing.Point(6, 16 + Frequencies.Count * 13);
                    current.label.TabIndex = Frequencies.Count;
                    Frequencies.Add(current);
                    break;
                case SensorType.Fan:
                    current.label.Name = current.sensor.SensorType.ToString() + (RPMs.Count).ToString();
                    this.RPM.Controls.Add(current.label);
                    current.label.Location = new System.Drawing.Point(6, 16 + RPMs.Count * 13);
                    current.label.TabIndex = RPMs.Count;
                    RPMs.Add(current);
                    break;
                default:
                    break;
            }

            current.update();
        }

        private void COM_p_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_p.Enabled = false;
            serial = new SerialPort(COM_p.SelectedItem.ToString(), 38400);
            serial.Parity = Parity.None;
            serial.StopBits = StopBits.One;
            serial.DataBits = 8;
            serial.Handshake = Handshake.None;
            serial.RtsEnable = true;
            serial.DtrEnable = true;
            serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);
            serial.ReadTimeout = 500;
            serial.WriteTimeout = 500;
            if (!serial.IsOpen) serial.Open();
        }

        void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            /*int red = port.ReadChar();
            if (char.IsLetterOrDigit((char)(red))) input = input + (char)(red);
            if (red == 10 || red == 13) TextR.Text = input;
            input = "";*/
            input = port.ReadLine();
        }

        private void TextR_Click(object sender, EventArgs e)
        {
            TextR.Text = input;
        }


        private void Config_Click(object sender, EventArgs e)
        {
            Configuration configwindow = new Configuration(ref thisPC, ref serial);
            configwindow.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var sensor in Temperatures)
            {
                sensor.update();
            }

            foreach (var sensor in Voltages)
            {
                sensor.update();
            }

            foreach (var sensor in Loads)
            {
                sensor.update();
            }

            foreach (var sensor in Frequencies)
            {
                sensor.update();
            }

            foreach (var sensor in RPMs)
            {
                sensor.update();
            }
        }
    }
}
