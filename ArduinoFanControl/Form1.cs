﻿using System;
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
        List<Configuration.fileStructure> fs;
        public enum display
        {
            Current,
            Max,
            Min
        };
        public struct sensorData
        {
            public ISensor sensor;
            public Label label;
            public display disp;
        };

        struct DueSensors
        {
            public string Name;
            public float current;
            public float max;
            public float min;
            public Label label;
            public display disp;
        };

        DueSensors[] dueTemp = new DueSensors[4];
        DueSensors[] dueFan = new DueSensors[6];

        List<sensorData> Temperatures = new List<sensorData>();
        List<sensorData> Voltages = new List<sensorData>();
        List<sensorData> Loads = new List<sensorData>();
        List<sensorData> Frequencies = new List<sensorData>();
        List<sensorData> RPMs = new List<sensorData>();
        List<DueSensors> DueSen = new List<DueSensors>();

        public int searchIndex(string Name, List<Configuration.fileStructure> files)
        {
            int id = 0;
            if (files.Count > 0)
            {
                while (files[id].Name != Name && id < files.Count) id++;
            }
            if (id < files.Count) return id;
            else return 255;
        }

        public int searchIndexAlias(string Name, List<Configuration.fileStructure> files)
        {
            int id = 0;
            while ((files[id].Name != Name || files[id].Alias != Name) && id < files.Count) id++;
            if (id < files.Count) return id;
            else return 255;
        }

        private void ListSensors(ref sensorData current, int idx)
        {
            switch (current.sensor.SensorType)
            {
                case SensorType.Temperature:
                    current.label = new Label();
                    current.label.Name = current.sensor.SensorType.ToString() + (Temperatures.Count).ToString();
                    if (idx < 255) current.label.Text = fs[idx].Alias;
                    else current.label.Text = current.sensor.Name;
                    switch (current.disp)
                    {
                        case display.Current:
                            current.label.Text += " = " + current.sensor.Value.ToString();
                            break;
                        case display.Max:
                            current.label.Text += " = " + current.sensor.Max.ToString();
                            break;
                        case display.Min:
                            current.label.Text += " = " + current.sensor.Min.ToString();
                            break;
                        default:
                            break;
                    }
                    current.label.AutoSize = true;
                    current.label.Location = new System.Drawing.Point(6, 16 + Temperatures.Count * 13);
                    current.label.TabIndex = Temperatures.Count;
                    current.label.Click += label_Click;
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
                        case display.Current:
                            current.label.Text += " = " + current.sensor.Value.ToString();
                            break;
                        case display.Max:
                            current.label.Text += " = " + current.sensor.Max.ToString();
                            break;
                        case display.Min:
                            current.label.Text += " = " + current.sensor.Min.ToString();
                            break;
                        default:
                            break;
                    }
                    current.label.AutoSize = true;
                    current.label.Location = new System.Drawing.Point(6, 16 + Voltages.Count * 13);
                    current.label.TabIndex = Voltages.Count;
                    current.label.Click += label_Click;
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
                        case display.Current:
                            current.label.Text += " = " + current.sensor.Value.ToString();
                            break;
                        case display.Max:
                            current.label.Text += " = " + current.sensor.Max.ToString();
                            break;
                        case display.Min:
                            current.label.Text += " = " + current.sensor.Min.ToString();
                            break;
                        default:
                            break;
                    }
                    current.label.AutoSize = true;
                    current.label.Location = new System.Drawing.Point(6, 16 + Loads.Count * 13);
                    current.label.TabIndex = Loads.Count;
                    current.label.Click += label_Click;
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
                        case display.Current:
                            current.label.Text += " = " + current.sensor.Value.ToString();
                            break;
                        case display.Max:
                            current.label.Text += " = " + current.sensor.Max.ToString();
                            break;
                        case display.Min:
                            current.label.Text += " = " + current.sensor.Min.ToString();
                            break;
                        default:
                            break;
                    }
                    current.label.AutoSize = true;
                    current.label.Location = new System.Drawing.Point(6, 16 + Frequencies.Count * 13);
                    current.label.TabIndex = Frequencies.Count;
                    current.label.Click += label_Click;
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
                        case display.Current:
                            current.label.Text += " = " + current.sensor.Value.ToString();
                            break;
                        case display.Max:
                            current.label.Text += " = " + current.sensor.Max.ToString();
                            break;
                        case display.Min:
                            current.label.Text += " = " + current.sensor.Min.ToString();
                            break;
                        default:
                            break;
                    }
                    current.label.AutoSize = true;
                    current.label.Location = new System.Drawing.Point(6, 16 + RPMs.Count * 13);
                    current.label.TabIndex = RPMs.Count;
                    current.label.Click += label_Click;
                    this.RPM.Controls.Add(current.label);
                    RPMs.Add(current);
                    break;
                default:
                    break;
            }
        }

        void init()
        {
            System.IO.StreamReader sr = new System.IO.StreamReader("config.ini");
            int idx = 0;
            fs = new List<Configuration.fileStructure>();
            string red = sr.ReadLine();
            while (red != null)
            {
                if (red.Split('=')[0].TrimEnd(' ') == "LastUsedPort")
                {
                    if (red.Split('=')[1].TrimStart(' ').StartsWith("COM")) serial.PortName = red.Split('=')[1].TrimStart(' ');
                }
                else
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
                red = sr.ReadLine();
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
                        if (idx < 255) { if (!fs[idx].hide) ListSensors(ref current, idx); }
                        else ListSensors(ref current, idx);
                    }
                }

                foreach (var sensor in hardwareItem.Sensors)
                {
                    sensorData current = new sensorData();
                    current.sensor = sensor;
                    idx = searchIndex(sensor.Name, fs);
                    if (idx < 255) { if (!fs[idx].hide) ListSensors(ref current, idx); }
                    else ListSensors(ref current, idx);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                idx = searchIndex("DueTemperature" + (i + 1).ToString(), fs);
                if (idx < 255)
                {
                    if (!fs[idx].hide)
                    {
                        DueSensors current = new DueSensors();
                        current.label = new Label();
                        current.label.Name = "DueTemperature" + (i + 1).ToString();
                        dueTemp[i].Name = "DueTemperature" + (i + 1).ToString();
                        idx = searchIndex("DueTemperature" + (i + 1).ToString(), fs);
                        if (idx < 255) current.label.Text = fs[idx].Alias;
                        else current.label.Text = "DueTemperature" + (i + 1).ToString();
                        current.disp = display.Current;
                        switch (current.disp)
                        {
                            case display.Current:
                                current.label.Text += " = " + dueTemp[i].current.ToString();
                                break;
                            case display.Max:
                                current.label.Text += " = " + dueTemp[i].max.ToString();
                                break;
                            case display.Min:
                                current.label.Text += " = " + dueTemp[i].min.ToString();
                                break;
                            default:
                                break;
                        }
                        current.label.AutoSize = true;
                        current.label.Location = new System.Drawing.Point(6, 16 + Due.Controls.Count * 13);
                        current.label.Click += label_Click;
                        this.Due.Controls.Add(current.label);
                        DueSen.Add(current);
                    }
                }
                else
                {
                    DueSensors current = new DueSensors();
                    current.label = new Label();
                    current.label.Name = "DueTemperature" + (i + 1).ToString();
                    dueTemp[i].Name = "DueTemperature" + (i + 1).ToString();
                    idx = searchIndex("DueTemperature" + (i + 1).ToString(), fs);
                    if (idx < 255) current.label.Text = fs[idx].Alias;
                    else current.label.Text = "DueTemperature" + (i + 1).ToString();
                    current.disp = display.Current;
                    switch (current.disp)
                    {
                        case display.Current:
                            current.label.Text += " = " + dueTemp[i].current.ToString();
                            break;
                        case display.Max:
                            current.label.Text += " = " + dueTemp[i].max.ToString();
                            break;
                        case display.Min:
                            current.label.Text += " = " + dueTemp[i].min.ToString();
                            break;
                        default:
                            break;
                    }
                    current.label.AutoSize = true;
                    current.label.Location = new System.Drawing.Point(6, 16 + Due.Controls.Count * 13);
                    current.label.Click += label_Click;
                    this.Due.Controls.Add(current.label);
                    DueSen.Add(current);
                }
            }
            for (int i = 0; i < 6; i++)
            {
                idx = searchIndex("DueTemperature" + (i + 1).ToString(), fs);
                if (idx < 255)
                {
                    if (!fs[idx].hide)
                    {
                        DueSensors current = new DueSensors();
                        current.label = new Label();
                        current.label.Name = "DueFan" + (i + 1).ToString();
                        dueFan[i].Name = "DueFan" + (i + 1).ToString();
                        idx = searchIndex("DueFan" + (i + 1).ToString(), fs);
                        if (idx < 255) current.label.Text = fs[idx].Alias;
                        else current.label.Text = "DueFan" + (i + 1).ToString();
                        current.disp = display.Current;
                        switch (current.disp)
                        {
                            case display.Current:
                                current.label.Text += " = " + dueFan[i].current.ToString();
                                break;
                            case display.Max:
                                current.label.Text += " = " + dueFan[i].max.ToString();
                                break;
                            case display.Min:
                                current.label.Text += " = " + dueFan[i].min.ToString();
                                break;
                            default:
                                break;
                        }
                        current.label.AutoSize = true;
                        current.label.Location = new System.Drawing.Point(6, 16 + Due.Controls.Count * 13);
                        current.label.Click += label_Click;
                        this.Due.Controls.Add(current.label);
                        DueSen.Add(current);
                    }
                }
                else
                {
                    DueSensors current = new DueSensors();
                    current.label = new Label();
                    current.label.Name = "DueFan" + (i + 1).ToString();
                    dueFan[i].Name = "DueFan" + (i + 1).ToString();
                    idx = searchIndex("DueFan" + (i + 1).ToString(), fs);
                    if (idx < 255) current.label.Text = fs[idx].Alias;
                    else current.label.Text = "DueFan" + (i + 1).ToString();
                    current.disp = display.Current;
                    switch (current.disp)
                    {
                        case display.Current:
                            current.label.Text += " = " + dueFan[i].current.ToString();
                            break;
                        case display.Max:
                            current.label.Text += " = " + dueFan[i].max.ToString();
                            break;
                        case display.Min:
                            current.label.Text += " = " + dueFan[i].min.ToString();
                            break;
                        default:
                            break;
                    }
                    current.label.AutoSize = true;
                    current.label.Location = new System.Drawing.Point(6, 16 + Due.Controls.Count * 13);
                    current.label.Click += label_Click;
                    this.Due.Controls.Add(current.label);
                    DueSen.Add(current);
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

        void label_Click(object sender, EventArgs e)
        {
            int idx;
            sensorData aux;
            DueSensors aux2;
            Label l = sender as Label;
            string tmp = l.Name;
            switch (tmp[0])
            {
                case 'L':
                    tmp.TrimStart(SensorType.Load.ToString().ToCharArray());
                    idx = int.Parse(tmp);
                    aux = Loads[idx];
                    aux.disp = aux.disp + 1;
                    Loads[idx] = aux;
                    break;
                case 'F':
                    tmp.TrimStart(SensorType.Fan.ToString().ToCharArray());
                    idx = int.Parse(tmp);
                    aux = RPMs[idx];
                    aux.disp = aux.disp + 1;
                    RPMs[idx] = aux;
                    break;
                case 'C':
                    tmp.TrimStart(SensorType.Clock.ToString().ToCharArray());
                    idx = int.Parse(tmp);
                    aux = Frequencies[idx];
                    aux.disp = aux.disp + 1;
                    Frequencies[idx] = aux;
                    break;
                case 'T':
                    tmp.TrimStart(SensorType.Temperature.ToString().ToCharArray());
                    idx = int.Parse(tmp);
                    aux = Temperatures[idx];
                    aux.disp = aux.disp + 1;
                    Temperatures[idx] = aux;
                    break;
                case 'V':
                    tmp.TrimStart(SensorType.Voltage.ToString().ToCharArray());
                    idx = int.Parse(tmp);
                    aux = Voltages[idx];
                    aux.disp = aux.disp + 1;
                    Voltages[idx] = aux;
                    break;
                case 'D':
                    switch (tmp[3])
                    {
                        case 'F':
                            tmp.TrimStart("DueFan".ToCharArray());
                            idx = int.Parse(tmp);
                            aux2 = DueSen[idx + 4];
                            aux.disp = aux2.disp + 1;
                            DueSen[idx + 4] = aux2;
                            break;
                        case 'T':
                            tmp.TrimStart("DueTemperature".ToCharArray());
                            idx = int.Parse(tmp);
                            aux2 = DueSen[idx + 4];
                            aux.disp = aux2.disp + 1;
                            DueSen[idx + 4] = aux2;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
