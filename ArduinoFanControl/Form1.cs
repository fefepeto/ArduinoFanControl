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

        struct DueSensors
        {
            public string Name;
            public float current;
            public float max;
            public float min;
        };

        DueSensors[] dueTemp = new DueSensors[4];
        DueSensors[] dueFan = new DueSensors[6];

        List<sensorData> Temperatures = new List<sensorData>();
        List<sensorData> Voltages = new List<sensorData>();
        List<sensorData> Loads = new List<sensorData>();
        List<sensorData> Frequencies = new List<sensorData>();
        List<sensorData> RPMs = new List<sensorData>();

        public int searchIndex(string Name, List<Configuration.fileStructure> files)
        {
            int id = 0;
            while (files[id].Name != Name && id < files.Count) id++;
            if (id < files.Count) return id;
            else return 255;
        }

        void init()
        {
            System.IO.StreamReader sr = new System.IO.StreamReader("config.ini");
            int idx = 0;
            List<Configuration.fileStructure> fs = new List<Configuration.fileStructure>();
            string red = sr.ReadLine();
            while (red != "")
            {
                Configuration.fileStructure cfs;
                string curr = red.Split('=')[0];
                curr = curr.TrimEnd(' ');
                cfs.Name = curr;
                red = red.Split('=')[1].TrimStart(' ');
                curr = red.Split(' ')[0];
                cfs.Alias = curr;
                cfs.hide = red.Split(' ')[1] == "true";
                cfs.log = red.Split(' ')[2] == "true";
                fs.Add(cfs);
            }

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
                        idx = searchIndex(sensor.Name, fs);
                        if (idx < 255) if (!fs[idx].hide) switch (current.sensor.SensorType)
                                {
                                    case SensorType.Temperature:
                                        current.label = new Label();
                                        current.label.Name = current.sensor.SensorType.ToString() + (Temperatures.Count).ToString();
                                        if (idx < 255) current.label.Text = fs[idx].Alias;
                                        else current.label.Text = current.sensor.Name;
                                        switch (current.disp)
                                        {
                                            case sensorData.display.Current:
                                                current.label.Text += " = " + current.sensor.Value.ToString();
                                                break;
                                            case sensorData.display.Max:
                                                current.label.Text += " = " + current.sensor.Max.ToString();
                                                break;
                                            case sensorData.display.Min:
                                                current.label.Text += " = " + current.sensor.Min.ToString();
                                                break;
                                            default:
                                                break;
                                        }
                                        current.label.AutoSize = true;
                                        current.label.Location = new System.Drawing.Point(6, 16 + Temperatures.Count * 13);
                                        current.label.TabIndex = Temperatures.Count;
                                        this.Temperature.Controls.Add(current.label);
                                        Temperatures.Add(current);
                                        break;
                                    case SensorType.Voltage:
                                        current.label = new Label();
                                        current.label.Name = current.sensor.SensorType.ToString() + (Voltages.Count).ToString();
                                        if (idx < 255) current.label.Text = fs[idx].Alias;
                                        else current.label.Text = current.sensor.Name;
                                        switch (current.disp)
                                        {
                                            case sensorData.display.Current:
                                                current.label.Text += " = " + current.sensor.Value.ToString();
                                                break;
                                            case sensorData.display.Max:
                                                current.label.Text += " = " + current.sensor.Max.ToString();
                                                break;
                                            case sensorData.display.Min:
                                                current.label.Text += " = " + current.sensor.Min.ToString();
                                                break;
                                            default:
                                                break;
                                        }
                                        current.label.AutoSize = true;
                                        current.label.Location = new System.Drawing.Point(6, 16 + Voltages.Count * 13);
                                        current.label.TabIndex = Voltages.Count;
                                        this.Voltage.Controls.Add(current.label);
                                        Voltages.Add(current);
                                        break;
                                    case SensorType.Load:
                                        current.label = new Label();
                                        current.label.Name = current.sensor.SensorType.ToString() + (Loads.Count).ToString();
                                        if (idx < 255) current.label.Text = fs[idx].Alias;
                                        else current.label.Text = current.sensor.Name;
                                        switch (current.disp)
                                        {
                                            case sensorData.display.Current:
                                                current.label.Text += " = " + current.sensor.Value.ToString();
                                                break;
                                            case sensorData.display.Max:
                                                current.label.Text += " = " + current.sensor.Max.ToString();
                                                break;
                                            case sensorData.display.Min:
                                                current.label.Text += " = " + current.sensor.Min.ToString();
                                                break;
                                            default:
                                                break;
                                        }
                                        current.label.AutoSize = true;
                                        current.label.Location = new System.Drawing.Point(6, 16 + Loads.Count * 13);
                                        current.label.TabIndex = Loads.Count;
                                        this.LoadGB.Controls.Add(current.label);
                                        Loads.Add(current);
                                        break;
                                    case SensorType.Clock:
                                        current.label = new Label();
                                        current.label.Name = current.sensor.SensorType.ToString() + (Frequencies.Count).ToString();
                                        if (idx < 255) current.label.Text = fs[idx].Alias;
                                        else current.label.Text = current.sensor.Name;
                                        switch (current.disp)
                                        {
                                            case sensorData.display.Current:
                                                current.label.Text += " = " + current.sensor.Value.ToString();
                                                break;
                                            case sensorData.display.Max:
                                                current.label.Text += " = " + current.sensor.Max.ToString();
                                                break;
                                            case sensorData.display.Min:
                                                current.label.Text += " = " + current.sensor.Min.ToString();
                                                break;
                                            default:
                                                break;
                                        }
                                        current.label.AutoSize = true;
                                        current.label.Location = new System.Drawing.Point(6, 16 + Frequencies.Count * 13);
                                        current.label.TabIndex = Frequencies.Count;
                                        Frequencies.Add(current);
                                        this.Frequency.Controls.Add(current.label);
                                        break;
                                    case SensorType.Fan:
                                        current.label = new Label();
                                        current.label.Name = current.sensor.SensorType.ToString() + (RPMs.Count).ToString();
                                        if (idx < 255) current.label.Text = fs[idx].Alias;
                                        else current.label.Text = current.sensor.Name;
                                        switch (current.disp)
                                        {
                                            case sensorData.display.Current:
                                                current.label.Text += " = " + current.sensor.Value.ToString();
                                                break;
                                            case sensorData.display.Max:
                                                current.label.Text += " = " + current.sensor.Max.ToString();
                                                break;
                                            case sensorData.display.Min:
                                                current.label.Text += " = " + current.sensor.Min.ToString();
                                                break;
                                            default:
                                                break;
                                        }
                                        current.label.AutoSize = true;
                                        current.label.Location = new System.Drawing.Point(6, 16 + RPMs.Count * 13);
                                        current.label.TabIndex = RPMs.Count;
                                        this.RPM.Controls.Add(current.label);
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
                    idx = searchIndex(sensor.Name, fs);
                    if (idx < 255) if (!fs[idx].hide) switch (current.sensor.SensorType)
                            {
                                case SensorType.Temperature:
                                    current.label = new Label();
                                    current.label.Name = current.sensor.SensorType.ToString() + (Temperatures.Count).ToString();
                                    if (idx < 255) current.label.Text = fs[idx].Alias;
                                    else current.label.Text = current.sensor.Name;
                                    switch (current.disp)
                                    {
                                        case sensorData.display.Current:
                                            current.label.Text += " = " + current.sensor.Value.ToString();
                                            break;
                                        case sensorData.display.Max:
                                            current.label.Text += " = " + current.sensor.Max.ToString();
                                            break;
                                        case sensorData.display.Min:
                                            current.label.Text += " = " + current.sensor.Min.ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                    current.label.AutoSize = true;
                                    current.label.Location = new System.Drawing.Point(6, 16 + Temperatures.Count * 13);
                                    current.label.TabIndex = Temperatures.Count;
                                    Temperatures.Add(current);
                                    this.Temperature.Controls.Add(current.label);
                                    break;
                                case SensorType.Voltage:
                                    current.label = new Label();
                                    current.label.Name = current.sensor.SensorType.ToString() + (Voltages.Count).ToString();
                                    if (idx < 255) current.label.Text = fs[idx].Alias;
                                    else current.label.Text = current.sensor.Name;
                                    switch (current.disp)
                                    {
                                        case sensorData.display.Current:
                                            current.label.Text += " = " + current.sensor.Value.ToString();
                                            break;
                                        case sensorData.display.Max:
                                            current.label.Text += " = " + current.sensor.Max.ToString();
                                            break;
                                        case sensorData.display.Min:
                                            current.label.Text += " = " + current.sensor.Min.ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                    current.label.AutoSize = true;
                                    current.label.Location = new System.Drawing.Point(6, 16 + Voltages.Count * 13);
                                    current.label.TabIndex = Voltages.Count;
                                    Voltages.Add(current);
                                    this.Voltage.Controls.Add(current.label);
                                    break;
                                case SensorType.Load:
                                    current.label = new Label();
                                    current.label.Name = current.sensor.SensorType.ToString() + (Loads.Count).ToString();
                                    if (idx < 255) current.label.Text = fs[idx].Alias;
                                    else current.label.Text = current.sensor.Name;
                                    switch (current.disp)
                                    {
                                        case sensorData.display.Current:
                                            current.label.Text += " = " + current.sensor.Value.ToString();
                                            break;
                                        case sensorData.display.Max:
                                            current.label.Text += " = " + current.sensor.Max.ToString();
                                            break;
                                        case sensorData.display.Min:
                                            current.label.Text += " = " + current.sensor.Min.ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                    current.label.AutoSize = true;
                                    current.label.Location = new System.Drawing.Point(6, 16 + Loads.Count * 13);
                                    current.label.TabIndex = Loads.Count;
                                    Loads.Add(current);
                                    this.LoadGB.Controls.Add(current.label);
                                    break;
                                case SensorType.Clock:
                                    current.label = new Label();
                                    current.label.Name = current.sensor.SensorType.ToString() + (Frequencies.Count).ToString();
                                    if (idx < 255) current.label.Text = fs[idx].Alias;
                                    else current.label.Text = current.sensor.Name;
                                    switch (current.disp)
                                    {
                                        case sensorData.display.Current:
                                            current.label.Text += " = " + current.sensor.Value.ToString();
                                            break;
                                        case sensorData.display.Max:
                                            current.label.Text += " = " + current.sensor.Max.ToString();
                                            break;
                                        case sensorData.display.Min:
                                            current.label.Text += " = " + current.sensor.Min.ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                    current.label.AutoSize = true;
                                    current.label.Location = new System.Drawing.Point(6, 16 + Frequencies.Count * 13);
                                    current.label.TabIndex = Frequencies.Count;
                                    Frequencies.Add(current);
                                    this.Frequency.Controls.Add(current.label);
                                    break;
                                case SensorType.Fan:
                                    current.label = new Label();
                                    current.label.Name = current.sensor.SensorType.ToString() + (RPMs.Count).ToString();
                                    if (idx < 255) current.label.Text = fs[idx].Alias;
                                    else current.label.Text = current.sensor.Name;
                                    switch (current.disp)
                                    {
                                        case sensorData.display.Current:
                                            current.label.Text += " = " + current.sensor.Value.ToString();
                                            break;
                                        case sensorData.display.Max:
                                            current.label.Text += " = " + current.sensor.Max.ToString();
                                            break;
                                        case sensorData.display.Min:
                                            current.label.Text += " = " + current.sensor.Min.ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                    current.label.AutoSize = true;
                                    current.label.Location = new System.Drawing.Point(6, 16 + RPMs.Count * 13);
                                    current.label.TabIndex = RPMs.Count;
                                    RPMs.Add(current);
                                    this.RPM.Controls.Add(current.label);
                                    break;
                                default:
                                    break;
                            }
                }

                for (int i = 0; i < 4; i++)
                {
                    idx = searchIndex("DueTemperature" + (i + 1).ToString(), fs);
                    if (idx < 255) if (!fs[idx].hide)
                        {
                            sensorData current;
                            current.label = new Label();
                            current.label.Name = "DueTemperature" + (i + 1).ToString();
                            dueTemp[i].Name = "DueTemperature" + (i + 1).ToString();
                            idx = searchIndex("DueTemperature" + (i + 1).ToString(), fs);
                            if (idx < 255) current.label.Text = fs[idx].Alias;
                            else current.label.Text = "DueTemperature" + (i + 1).ToString();
                            current.disp = sensorData.display.Current;
                            switch (current.disp)
                            {
                                case sensorData.display.Current:
                                    current.label.Text += " = " + dueTemp[i].current.ToString();
                                    break;
                                case sensorData.display.Max:
                                    current.label.Text += " = " + dueTemp[i].max.ToString();
                                    break;
                                case sensorData.display.Min:
                                    current.label.Text += " = " + dueTemp[i].min.ToString();
                                    break;
                                default:
                                    break;
                            }
                            current.label.AutoSize = true;
                            current.label.Location = new System.Drawing.Point(6, 16 + Due.Controls.Count * 13);
                            this.Due.Controls.Add(current.label);
                        }
                }
                for (int i = 0; i < 6; i++)
                {
                    idx = searchIndex("DueTemperature" + (i + 1).ToString(), fs);
                    if (idx < 255) if (!fs[idx].hide)
                        {
                            sensorData current;
                            current.label = new Label();
                            current.label.Name = "DueFan" + (i + 1).ToString();
                            dueFan[i].Name = "DueFan" + (i + 1).ToString();
                            idx = searchIndex("DueFan" + (i + 1).ToString(), fs);
                            if (idx < 255) current.label.Text = fs[idx].Alias;
                            else current.label.Text = "DueFan" + (i + 1).ToString();
                            current.disp = sensorData.display.Current;
                            switch (current.disp)
                            {
                                case sensorData.display.Current:
                                    current.label.Text += " = " + dueFan[i].current.ToString();
                                    break;
                                case sensorData.display.Max:
                                    current.label.Text += " = " + dueFan[i].max.ToString();
                                    break;
                                case sensorData.display.Min:
                                    current.label.Text += " = " + dueFan[i].min.ToString();
                                    break;
                                default:
                                    break;
                            }
                            current.label.AutoSize = true;
                            current.label.Location = new System.Drawing.Point(6, 16 + Due.Controls.Count * 13);
                            this.Due.Controls.Add(current.label);
                        }
                }
            }
        }

        public ArduinoController()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thisPC = new Computer();
            thisPC.CPUEnabled = true;
            thisPC.GPUEnabled = true;
            thisPC.HDDEnabled = true;
            thisPC.MainboardEnabled = true;
            thisPC.RAMEnabled = true;
            thisPC.Open();

            string[] ports = SerialPort.GetPortNames();
            foreach (string s in ports) COM_p.Items.Add(s);
            init();
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
            configwindow.FormClosed += configwindow_FormClosed;
        }

        void configwindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            init();
        }
    }
}
