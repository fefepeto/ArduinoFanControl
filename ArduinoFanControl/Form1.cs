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
        struct sensorData
        {
            public ISensor sensor;
            public Label label;
            public display disp;
            public enum display
            {
                Current,
                Max,
                Min
            };
        };

        List<sensorData> Temperatures = new List<sensorData>();
        List<sensorData> Voltages = new List<sensorData>();
        List<sensorData> Loads = new List<sensorData>();
        List<sensorData> Frequencies = new List<sensorData>();
        List<sensorData> RPMs = new List<sensorData>();

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
                        sensorData current = new sensorData();
                        current.sensor = sensor;
                        switch (current.sensor.SensorType)
                        {
                            case SensorType.Temperature:
                                current.label = new Label();
                                current.label.Name = current.sensor.SensorType.ToString() + (Temperatures.Count).ToString();
                                switch (current.disp)
                                {
                                    case sensorData.display.Current:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Value.ToString();
                                        break;
                                    case sensorData.display.Max:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Max.ToString();
                                        break;
                                    case sensorData.display.Min:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Min.ToString();
                                        break;
                                    default:
                                        break;
                                }
                                this.Temperature.Controls.Add(current.label);
                                current.label.AutoSize = true;
                                current.label.Location = new System.Drawing.Point(6, 16 + Temperatures.Count * 13);
                                current.label.TabIndex = Temperatures.Count;
                                Temperatures.Add(current);
                                break;
                            case SensorType.Voltage:
                                current.label = new Label();
                                current.label.Name = current.sensor.SensorType.ToString() + (Voltages.Count).ToString();
                                switch (current.disp)
                                {
                                    case sensorData.display.Current:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Value.ToString();
                                        break;
                                    case sensorData.display.Max:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Max.ToString();
                                        break;
                                    case sensorData.display.Min:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Min.ToString();
                                        break;
                                    default:
                                        break;
                                }
                                this.Voltage.Controls.Add(current.label);
                                current.label.AutoSize = true;
                                current.label.Location = new System.Drawing.Point(6, 16 + Voltages.Count * 13);
                                current.label.TabIndex = Voltages.Count;
                                Voltages.Add(current);
                                break;
                            case SensorType.Load:
                                current.label = new Label();
                                current.label.Name = current.sensor.SensorType.ToString() + (Loads.Count).ToString();
                                switch (current.disp)
                                {
                                    case sensorData.display.Current:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Value.ToString();
                                        break;
                                    case sensorData.display.Max:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Max.ToString();
                                        break;
                                    case sensorData.display.Min:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Min.ToString();
                                        break;
                                    default:
                                        break;
                                }
                                this.LoadGB.Controls.Add(current.label);
                                current.label.AutoSize = true;
                                current.label.Location = new System.Drawing.Point(6, 16 + Loads.Count * 13);
                                current.label.TabIndex = Loads.Count;
                                Loads.Add(current);
                                break;
                            case SensorType.Clock:
                                current.label = new Label();
                                current.label.Name = current.sensor.SensorType.ToString() + (Frequencies.Count).ToString();
                                switch (current.disp)
                                {
                                    case sensorData.display.Current:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Value.ToString();
                                        break;
                                    case sensorData.display.Max:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Max.ToString();
                                        break;
                                    case sensorData.display.Min:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Min.ToString();
                                        break;
                                    default:
                                        break;
                                }
                                this.Frequency.Controls.Add(current.label);
                                current.label.AutoSize = true;
                                current.label.Location = new System.Drawing.Point(6, 16 + Frequencies.Count * 13);
                                current.label.TabIndex = Frequencies.Count;
                                Frequencies.Add(current);
                                break;
                            case SensorType.Fan:
                                current.label = new Label();
                                current.label.Name = current.sensor.SensorType.ToString() + (RPMs.Count).ToString();
                                switch (current.disp)
                                {
                                    case sensorData.display.Current:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Value.ToString();
                                        break;
                                    case sensorData.display.Max:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Max.ToString();
                                        break;
                                    case sensorData.display.Min:
                                        current.label.Text = current.sensor.Name + " = " + current.sensor.Min.ToString();
                                        break;
                                    default:
                                        break;
                                }
                                this.RPM.Controls.Add(current.label);
                                current.label.AutoSize = true;
                                current.label.Location = new System.Drawing.Point(6, 16 + RPMs.Count * 13);
                                current.label.TabIndex = RPMs.Count;
                                RPMs.Add(current);
                                break;
                            default:
                                break;
                        }
                    }
                }

                foreach (var sensor in hardwareItem.Sensors)
                {
                    sensorData current = new sensorData();
                    current.sensor = sensor;
                    switch (current.sensor.SensorType)
                    {
                        case SensorType.Temperature:
                            current.label = new Label();
                            current.label.Name = current.sensor.SensorType.ToString() + (Temperatures.Count).ToString();
                            switch (current.disp)
                            {
                                case sensorData.display.Current:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Value.ToString();
                                    break;
                                case sensorData.display.Max:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Max.ToString();
                                    break;
                                case sensorData.display.Min:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Min.ToString();
                                    break;
                                default:
                                    break;
                            }
                            this.Temperature.Controls.Add(current.label);
                            current.label.AutoSize = true;
                            current.label.Location = new System.Drawing.Point(6, 16 + Temperatures.Count * 13);
                            current.label.TabIndex = Temperatures.Count;
                            Temperatures.Add(current);
                            break;
                        case SensorType.Voltage:
                            current.label = new Label();
                            current.label.Name = current.sensor.SensorType.ToString() + (Voltages.Count).ToString();
                            switch (current.disp)
                            {
                                case sensorData.display.Current:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Value.ToString();
                                    break;
                                case sensorData.display.Max:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Max.ToString();
                                    break;
                                case sensorData.display.Min:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Min.ToString();
                                    break;
                                default:
                                    break;
                            }
                            this.Voltage.Controls.Add(current.label);
                            current.label.AutoSize = true;
                            current.label.Location = new System.Drawing.Point(6, 16 + Voltages.Count * 13);
                            current.label.TabIndex = Voltages.Count;
                            Voltages.Add(current);
                            break;
                        case SensorType.Load:
                            current.label = new Label();
                            current.label.Name = current.sensor.SensorType.ToString() + (Loads.Count).ToString();
                            switch (current.disp)
                            {
                                case sensorData.display.Current:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Value.ToString();
                                    break;
                                case sensorData.display.Max:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Max.ToString();
                                    break;
                                case sensorData.display.Min:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Min.ToString();
                                    break;
                                default:
                                    break;
                            }
                            this.LoadGB.Controls.Add(current.label);
                            current.label.AutoSize = true;
                            current.label.Location = new System.Drawing.Point(6, 16 + Loads.Count * 13);
                            current.label.TabIndex = Loads.Count;
                            Loads.Add(current);
                            break;
                        case SensorType.Clock:
                            current.label = new Label();
                            current.label.Name = current.sensor.SensorType.ToString() + (Frequencies.Count).ToString();
                            switch (current.disp)
                            {
                                case sensorData.display.Current:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Value.ToString();
                                    break;
                                case sensorData.display.Max:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Max.ToString();
                                    break;
                                case sensorData.display.Min:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Min.ToString();
                                    break;
                                default:
                                    break;
                            }
                            this.Frequency.Controls.Add(current.label);
                            current.label.AutoSize = true;
                            current.label.Location = new System.Drawing.Point(6, 16 + Frequencies.Count * 13);
                            current.label.TabIndex = Frequencies.Count;
                            Frequencies.Add(current);
                            break;
                        case SensorType.Fan:
                            current.label = new Label();
                            current.label.Name = current.sensor.SensorType.ToString() + (RPMs.Count).ToString();
                            switch (current.disp)
                            {
                                case sensorData.display.Current:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Value.ToString();
                                    break;
                                case sensorData.display.Max:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Max.ToString();
                                    break;
                                case sensorData.display.Min:
                                    current.label.Text = current.sensor.Name + " = " + current.sensor.Min.ToString();
                                    break;
                                default:
                                    break;
                            }
                            this.RPM.Controls.Add(current.label);
                            current.label.AutoSize = true;
                            current.label.Location = new System.Drawing.Point(6, 16 + RPMs.Count * 13);
                            current.label.TabIndex = RPMs.Count;
                            RPMs.Add(current);
                            break;
                        default:
                            break;
                    }
                }
            }
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
    }
}
