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
using System.IO;

namespace ArduinoFanControl
{
    public partial class ArduinoController : Form
    {
        public Computer thisPC;
        public SerialPort serial = new SerialPort();
        List<Configuration.fileStructure> fs;
        public string recieved;
        private System.Windows.Forms.NotifyIcon icon;
        string Name;
        bool transferFinished;
        int ends = 0;

        static Measure m;

        public enum mode { B, C, M, P, I, H, R, D };

        public struct serialCom
        {
            public mode m;
            public string[] args;
        }

        public serialCom command = new serialCom();

        Measure.result[] res = new Measure.result[6];

        public enum display
        {
            Current,
            Max,
            Min
        }
        public struct sensorData
        {
            public ISensor sensor;
            public Label label;
            public display disp;
            public int CId;
        }

        public struct DueSensors
        {
            public string Name;
            public float current;
            public float max;
            public float min;
            public Label label;
            public display disp;
        }

        DueSensors[] dueTemp = new DueSensors[4];
        DueSensors[] dueFan = new DueSensors[6];

        List<sensorData> Temperatures = new List<sensorData>();
        List<sensorData> Voltages = new List<sensorData>();
        List<sensorData> Loads = new List<sensorData>();
        List<sensorData> Frequencies = new List<sensorData>();
        List<sensorData> RPMs = new List<sensorData>();
        List<DueSensors> DueSen = new List<DueSensors>();

        delegate void SetTextCallback(string text, Label l);
        
        public void SetText(string text, Label l)
        {
            if (l.InvokeRequired)
            {
                SetTextCallback d = new ArduinoFanControl.ArduinoController.SetTextCallback(SetText);
                this.Invoke(d, new object[] { text, l });
            }
            else
            {
                l.Text = text;
            }
        }
        
        public int searchIndex(string Name, List<Configuration.fileStructure> files)
        {
            int id = 0;
            if (files.Count > 0)
            {
                while (id < files.Count) if (files[id].Name != Name) id++;
                    else break;
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
                    if (idx < 255)
                    {
                        current.label.Text = fs[idx].Alias;
                        current.CId = fs[idx].CId - 1;
                    }
                    else
                    {
                        current.label.Text = current.sensor.Name;
                        if (Temperatures.Count < 5) current.CId = Temperatures.Count;
                    }
                    switch (current.disp)
                    {
                        case display.Current:
                            current.label.Text += " = " + current.sensor.Value.ToString();
                            DispTip.SetToolTip(current.label, "Current");
                            break;
                        case display.Max:
                            current.label.Text += " = " + current.sensor.Max.ToString();
                            DispTip.SetToolTip(current.label, "Maximum");
                            break;
                        case display.Min:
                            current.label.Text += " = " + current.sensor.Min.ToString();
                            DispTip.SetToolTip(current.label, "Minimum");
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
                            DispTip.SetToolTip(current.label, "Current");
                            break;
                        case display.Max:
                            current.label.Text += " = " + current.sensor.Max.ToString();
                            DispTip.SetToolTip(current.label, "Maximum");
                            break;
                        case display.Min:
                            current.label.Text += " = " + current.sensor.Min.ToString();
                            DispTip.SetToolTip(current.label, "Minimum");
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
                            DispTip.SetToolTip(current.label, "Current");
                            break;
                        case display.Max:
                            current.label.Text += " = " + current.sensor.Max.ToString();
                            DispTip.SetToolTip(current.label, "Maximum");
                            break;
                        case display.Min:
                            current.label.Text += " = " + current.sensor.Min.ToString();
                            DispTip.SetToolTip(current.label, "Minimum");
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
                            DispTip.SetToolTip(current.label, "Current");
                            break;
                        case display.Max:
                            current.label.Text += " = " + current.sensor.Max.ToString();
                            DispTip.SetToolTip(current.label, "Maximum");
                            break;
                        case display.Min:
                            current.label.Text += " = " + current.sensor.Min.ToString();
                            DispTip.SetToolTip(current.label, "Minimum");
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
                            DispTip.SetToolTip(current.label, "Current");
                            break;
                        case display.Max:
                            current.label.Text += " = " + current.sensor.Max.ToString();
                            DispTip.SetToolTip(current.label, "Maximum");
                            break;
                        case display.Min:
                            current.label.Text += " = " + current.sensor.Min.ToString();
                            DispTip.SetToolTip(current.label, "Minimum");
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
            Temperatures.Clear();
            Voltages.Clear();
            Frequencies.Clear();
            RPMs.Clear();
            Loads.Clear();
            DueSen.Clear();
            Temperature.Controls.Clear();
            Voltage.Controls.Clear();
            Frequency.Controls.Clear();
            RPM.Controls.Clear();
            LoadGB.Controls.Clear();
            Due.Controls.Clear();
            System.IO.StreamReader sr = new System.IO.StreamReader("config.ini");
            int idx = 0;
            fs = new List<Configuration.fileStructure>();
            string red = sr.ReadLine();
            while (red != null)
            {
                if (red.Split('=')[0].TrimEnd(' ') == "LastUsedPort")
                {
                    if (red.Split('=')[1].TrimStart(' ').StartsWith("COM")) if (!serial.IsOpen) serial.PortName = red.Split('=')[1].TrimStart(' ');
                }
                else
                {
                    Configuration.fileStructure cfs;
                    string curr = red.Split('=')[0];
                    curr = curr.TrimEnd(' ');
                    cfs.Name = curr;
                    red = red.Split('=')[1].TrimStart(' ');
                    curr = red.Split(',')[0];
                    cfs.Alias = curr;
                    cfs.hide = red.Split(',')[1] == "true";
                    cfs.log = red.Split(',')[2] == "true";
                    if (red.Split(',').Length == 4) cfs.CId = int.Parse(red.Split(',')[3]);
                    else cfs.CId = -1;
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
                        if (sensor.SensorType == SensorType.Clock || sensor.SensorType == SensorType.Fan || sensor.SensorType == SensorType.Load || sensor.SensorType == SensorType.Temperature || sensor.SensorType == SensorType.Voltage)
                        {
                            idx = searchIndex(sensor.Name, fs);
                            if (idx < 255)
                            {
                                current.CId = fs[idx].CId;
                                if (!fs[idx].hide) ListSensors(ref current, idx);
                            }
                            else ListSensors(ref current, idx);
                        }
                    }
                }

                foreach (var sensor in hardwareItem.Sensors)
                {
                    sensorData current = new sensorData();
                    current.sensor = sensor;
                    if (sensor.SensorType == SensorType.Clock || sensor.SensorType == SensorType.Fan || sensor.SensorType == SensorType.Load || sensor.SensorType == SensorType.Temperature || sensor.SensorType == SensorType.Voltage)
                    {
                        idx = searchIndex(sensor.Name, fs);
                        if (idx < 255)
                        {
                            if (!fs[idx].hide) ListSensors(ref current, idx);
                            if (fs[idx].CId != 0) current.CId = fs[idx].CId;
                        }
                        else ListSensors(ref current, idx);
                    }
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
                                DispTip.SetToolTip(current.label, "Current");
                                break;
                            case display.Max:
                                current.label.Text += " = " + dueTemp[i].max.ToString();
                                DispTip.SetToolTip(current.label, "Maximum");
                                break;
                            case display.Min:
                                current.label.Text += " = " + dueTemp[i].min.ToString();
                                DispTip.SetToolTip(current.label, "Minimum");
                                break;
                            default:
                                break;
                        }
                        current.label.AutoSize = true;
                        current.label.Location = new System.Drawing.Point(6, 16 + Due.Controls.Count * 13);
                        current.label.Click += label_Click;
                        this.Due.Controls.Add(current.label);
                        DueSen.Add(current);
                        dueTemp[i].label = current.label;
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
                            DispTip.SetToolTip(current.label, "Current");
                            break;
                        case display.Max:
                            current.label.Text += " = " + dueTemp[i].max.ToString();
                            DispTip.SetToolTip(current.label, "Maximum");
                            break;
                        case display.Min:
                            current.label.Text += " = " + dueTemp[i].min.ToString();
                            DispTip.SetToolTip(current.label, "Minimum");
                            break;
                        default:
                            break;
                    }
                    current.label.AutoSize = true;
                    current.label.Location = new System.Drawing.Point(6, 16 + Due.Controls.Count * 13);
                    current.label.Click += label_Click;
                    this.Due.Controls.Add(current.label);
                    DueSen.Add(current);
                    dueTemp[i].label = current.label;
                }
            }
            for (int i = 0; i < 6; i++)
            {
                idx = searchIndex("DueFan" + (i + 1).ToString(), fs);
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
                                DispTip.SetToolTip(current.label, "Current");
                                break;
                            case display.Max:
                                current.label.Text += " = " + dueFan[i].max.ToString();
                                DispTip.SetToolTip(current.label, "Maximum");
                                break;
                            case display.Min:
                                current.label.Text += " = " + dueFan[i].min.ToString();
                                DispTip.SetToolTip(current.label, "Minimum");
                                break;
                            default:
                                break;
                        }
                        current.label.AutoSize = true;
                        current.label.Location = new System.Drawing.Point(6, 16 + Due.Controls.Count * 13);
                        current.label.Click += label_Click;
                        this.Due.Controls.Add(current.label);
                        DueSen.Add(current);
                        dueFan[i].label = current.label;
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
                                DispTip.SetToolTip(current.label, "Current");
                                break;
                            case display.Max:
                                current.label.Text += " = " + dueFan[i].max.ToString();
                                DispTip.SetToolTip(current.label, "Maximum");
                                break;
                            case display.Min:
                                current.label.Text += " = " + dueFan[i].min.ToString();
                                DispTip.SetToolTip(current.label, "Minimum");
                                break;
                            default:
                                break;
                        }
                    current.label.AutoSize = true;
                    current.label.Location = new System.Drawing.Point(6, 16 + Due.Controls.Count * 13);
                    current.label.Click += label_Click;
                    this.Due.Controls.Add(current.label);
                    DueSen.Add(current);
                    dueFan[i].label = current.label;
                }
            }
            sr.Close();
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

            icon = new NotifyIcon(this.components);

            icon.Icon = new System.Drawing.Icon(this.Icon, new Size(16, 16));
            icon.Text = this.Text;
            icon.Visible = true;
            icon.Click += icon_Click;

            string[] ports = SerialPort.GetPortNames();
            foreach (string s in ports) COM_p.Items.Add(s);
            init();
            timer1.Start();

            command.m = mode.C;
            string[] array = { "0 ", "0 ", "0 ", "0 ", "0 " };
            command.args = array;
        }

        void icon_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
        }

        void COM_init(string Name)
        {
            serial = new SerialPort(Name, 115200);
            serial.Parity = Parity.None;
            serial.StopBits = StopBits.One;
            serial.DataBits = 8;
            serial.Handshake = Handshake.None;
            serial.RtsEnable = true;
            serial.DtrEnable = true;
            serial.ReadTimeout = 1000;
            serial.WriteTimeout = 1000;
            serial.DataReceived += serial_DataReceived;
            while (serial.IsOpen) ;
            try
            {
                System.Threading.Thread.Sleep(200);
                serial.Open();
            }
            catch
            {
                while (!serial.IsOpen)
                {
                    System.Threading.Thread.Sleep(200);
                    string[] ports = SerialPort.GetPortNames();
                    foreach (string s in ports) if (s == Name) serial.Open();
                }
            }
        }

        private void COM_p_SelectedIndexChanged(object sender, EventArgs e)
        {
            Name = COM_p.SelectedItem.ToString();
            COM_init(Name);
            COM_p.Enabled = false;
        }

        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            transferFinished = false;
            if (!serial.IsOpen) serial.Open();
            try
            {
                recieved = serial.ReadLine();
                if (recieved.Split(' ').Length == 10)
                {
                    string[] inp = recieved.Split(' ');
                    for (int i = 0; i < inp.Length; i++)
                    {
                        float current = float.Parse(inp[i]);
                        DueSensors ds = new DueSensors();
                        ds = DueSen[i];
                        ds.current = current;
                        if (current > ds.max) ds.max = current;
                        if (current < ds.min) ds.min = current;
                        DueSen[i] = ds;
                    }
                }
            }
            catch
            {
                if (serial.IsOpen) serial.DiscardInBuffer();
            }
            switch (command.m)
            {
                case mode.C:
                    try
                    {
                        serial.Write("C ");
                    }
                    catch
                    {
                    }
                    for (int id = 0; id < command.args.Length; id++)
                    {
                        try
                        {
                            if (command.args[id] != null) serial.Write(command.args[id] + " ");
                            else serial.Write("0 ");
                        }
                        catch
                        {
                        }
                    }
                    break;
                case mode.D:
                    serial.Write("D");
                    serial.Close();
                    break;
                case mode.P:
                    serial.Write("P");
                    serial.DiscardInBuffer();
                    serial.Close();
                    break;
                case mode.M:
                default:
                    break;
            }
            transferFinished = true;
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
                    tmp = tmp.TrimStart(SensorType.Load.ToString().ToCharArray());
                    idx = int.Parse(tmp);
                    aux = Loads[idx];
                    switch (aux.disp)
                    {
                        case display.Current:
                            aux.disp = display.Max;
                            DispTip.SetToolTip(l, "Maximum");
                            break;
                        case display.Max:
                            aux.disp = display.Min;
                            DispTip.SetToolTip(l, "Minimum");
                            break;
                        case display.Min:
                            aux.disp = display.Current;
                            DispTip.SetToolTip(l, "Current");
                            break;
                    }
                    Loads[idx] = aux;
                    break;
                case 'F':
                    tmp = tmp.TrimStart(SensorType.Fan.ToString().ToCharArray());
                    idx = int.Parse(tmp);
                    aux = RPMs[idx];
                    switch (aux.disp)
                    {
                        case display.Current:
                            aux.disp = display.Max;
                            DispTip.SetToolTip(l, "Maximum");
                            break;
                        case display.Max:
                            aux.disp = display.Min;
                            DispTip.SetToolTip(l, "Minimum");
                            break;
                        case display.Min:
                            aux.disp = display.Current;
                            DispTip.SetToolTip(l, "Current");
                            break;
                    }
                    RPMs[idx] = aux;
                    break;
                case 'C':
                    tmp = tmp.TrimStart(SensorType.Clock.ToString().ToCharArray());
                    idx = int.Parse(tmp);
                    aux = Frequencies[idx];
                    switch (aux.disp)
                    {
                        case display.Current:
                            aux.disp = display.Max;
                            DispTip.SetToolTip(l, "Maximum");
                            break;
                        case display.Max:
                            aux.disp = display.Min;
                            DispTip.SetToolTip(l, "Minimum");
                            break;
                        case display.Min:
                            aux.disp = display.Current;
                            DispTip.SetToolTip(l, "Current");
                            break;
                    }
                    Frequencies[idx] = aux;
                    break;
                case 'T':
                    tmp = tmp.TrimStart(SensorType.Temperature.ToString().ToCharArray());
                    idx = int.Parse(tmp);
                    aux = Temperatures[idx];
                    switch (aux.disp)
                    {
                        case display.Current:
                            aux.disp = display.Max;
                            DispTip.SetToolTip(l, "Maximum");
                            break;
                        case display.Max:
                            aux.disp = display.Min;
                            DispTip.SetToolTip(l, "Minimum");
                            break;
                        case display.Min:
                            aux.disp = display.Current;
                            DispTip.SetToolTip(l, "Current");
                            break;
                    }
                    Temperatures[idx] = aux;
                    break;
                case 'V':
                    tmp = tmp.TrimStart(SensorType.Voltage.ToString().ToCharArray());
                    idx = int.Parse(tmp);
                    aux = Voltages[idx];
                    switch (aux.disp)
                    {
                        case display.Current:
                            aux.disp = display.Max;
                            DispTip.SetToolTip(l, "Maximum");
                            break;
                        case display.Max:
                            aux.disp = display.Min;
                            DispTip.SetToolTip(l, "Minimum");
                            break;
                        case display.Min:
                            aux.disp = display.Current;
                            DispTip.SetToolTip(l, "Current");
                            break;
                    }
                    Voltages[idx] = aux;
                    break;
                case 'D':
                    switch (tmp[3])
                    {
                        case 'F':
                            tmp = tmp.TrimStart("DueFan".ToCharArray());
                            idx = int.Parse(tmp) - 1;
                            aux2 = DueSen[idx + 4];
                            switch (aux2.disp)
                            {
                                case display.Current:
                                    aux2.disp = display.Max;
                                    DispTip.SetToolTip(l, "Maximum");
                                    break;
                                case display.Max:
                                    aux2.disp = display.Min;
                                    DispTip.SetToolTip(l, "Minimum");
                                    break;
                                case display.Min:
                                    aux2.disp = display.Current;
                                    DispTip.SetToolTip(l, "Current");
                                    break;
                            }
                            DueSen[idx + 4] = aux2;
                            break;
                        case 'T':
                            tmp = tmp.TrimStart("DueTemperature".ToCharArray());
                            idx = int.Parse(tmp) - 1;
                            aux2 = DueSen[idx];
                            switch (aux2.disp)
                            {
                                case display.Current:
                                    aux2.disp = display.Max;
                                    DispTip.SetToolTip(l, "Maximum");
                                    break;
                                case display.Max:
                                    aux2.disp = display.Min;
                                    DispTip.SetToolTip(l, "Minimum");
                                    break;
                                case display.Min:
                                    aux2.disp = display.Current;
                                    DispTip.SetToolTip(l, "Current");
                                    break;
                            }
                            DueSen[idx] = aux2;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Label l in Temperature.Controls)
            {
                int id = 0;
                while (id < Temperatures.Count && Temperatures[id].label != l) id++;
                Temperatures[id].sensor.Hardware.Update();
                switch (Temperatures[id].disp)
                {
                    case display.Current:
                        SetText(l.Text.Split('=')[0] + "= " + Temperatures[id].sensor.Value.ToString(), l);
                        break;
                    case display.Max:
                        SetText(l.Text.Split('=')[0] + "= " + Temperatures[id].sensor.Max.ToString(), l);
                        break;
                    case display.Min:
                        SetText(l.Text.Split('=')[0] + "= " + Temperatures[id].sensor.Min.ToString(), l);
                        break;
                    default:
                        break;
                }
            }
            foreach (Label l in Voltage.Controls)
            {
                int id = 0;
                while (Voltages[id].label != l) id++;
                Voltages[id].sensor.Hardware.Update();
                switch (Voltages[id].disp)
                {
                    case display.Current:
                        SetText(l.Text.Split('=')[0] + "= " + Voltages[id].sensor.Value.ToString(), l);
                        break;
                    case display.Max:
                        SetText(l.Text.Split('=')[0] + "= " + Voltages[id].sensor.Max.ToString(), l);
                        break;
                    case display.Min:
                        SetText(l.Text.Split('=')[0] + "= " + Voltages[id].sensor.Min.ToString(), l);
                        break;
                    default:
                        break;
                }
            }
            foreach (Label l in LoadGB.Controls)
            {
                int id = 0;
                while (Loads[id].label != l) id++;
                Loads[id].sensor.Hardware.Update();
                switch (Loads[id].disp)
                {
                    case display.Current:
                        SetText(l.Text.Split('=')[0] + "= " + Loads[id].sensor.Value.ToString(), l);
                        break;
                    case display.Max:
                        SetText(l.Text.Split('=')[0] + "= " + Loads[id].sensor.Max.ToString(), l);
                        break;
                    case display.Min:
                        SetText(l.Text.Split('=')[0] + "= " + Loads[id].sensor.Min.ToString(), l);
                        break;
                    default:
                        break;
                }
            }
            foreach (Label l in Frequency.Controls)
            {
                int id = 0;
                while (Frequencies[id].label != l) id++;
                Frequencies[id].sensor.Hardware.Update();
                switch (Frequencies[id].disp)
                {
                    case display.Current:
                        SetText(l.Text.Split('=')[0] + "= " + Frequencies[id].sensor.Value.ToString(), l);
                        break;
                    case display.Max:
                        SetText(l.Text.Split('=')[0] + "= " + Frequencies[id].sensor.Max.ToString(), l);
                        break;
                    case display.Min:
                        SetText(l.Text.Split('=')[0] + "= " + Frequencies[id].sensor.Min.ToString(), l);
                        break;
                    default:
                        break;
                }
            }
            foreach (Label l in RPM.Controls)
            {
                int id = 0;
                while (RPMs[id].label != l) id++;
                RPMs[id].sensor.Hardware.Update();
                switch (RPMs[id].disp)
                {
                    case display.Current:
                        SetText(l.Text.Split('=')[0] + "= " + RPMs[id].sensor.Value.ToString(), l);
                        break;
                    case display.Max:
                        SetText(l.Text.Split('=')[0] + "= " + RPMs[id].sensor.Max.ToString(), l);
                        break;
                    case display.Min:
                        SetText(l.Text.Split('=')[0] + "= " + RPMs[id].sensor.Min.ToString(), l);
                        break;
                    default:
                        break;
                }
            }
            foreach (Label l in Due.Controls)
            {
                int id = 0;
                while (DueSen[id].label != l) id++;
                switch (DueSen[id].disp)
                {
                    case display.Current:
                        SetText(l.Text.Split('=')[0] + "= " + DueSen[id].current.ToString(), l);
                        break;
                    case display.Max:
                        SetText(l.Text.Split('=')[0] + "= " + DueSen[id].max.ToString(), l);
                        break;
                    case display.Min:
                        SetText(l.Text.Split('=')[0] + "= " + DueSen[id].min.ToString(), l);
                        break;
                    default:
                        break;
                }
            }
            string[] temps = new string[5];
            for (int id = 0; id < Temperatures.Count; id++) if (Temperatures[id].CId > -1) temps[Temperatures[id].CId] = Temperatures[id].sensor.Value.ToString();
            if (command.m != mode.B || ends >= 6)
            {
                command.m = mode.C;
                command.args = temps;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration config = new Configuration(ref thisPC, ref serial);
            config.Show();
            config.FormClosed += config_FormClosed;
        }

        void config_FormClosed(object sender, FormClosedEventArgs e)
        {
            init();
        }

        private void measureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.m = mode.D;
            System.Threading.Thread.Sleep(4000);
            m = new Measure(serial);
            m.ShowDialog();
            if (m.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                res[m.res.id - 1] = m.res;
                if (!serial.IsOpen) serial.Open();
            }
        }

        private void profileSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //command.m = mode.P;
            serial.Close();
            System.Threading.Thread.Sleep(3000);
            Profile prof = new Profile(ref Temperatures, ref dueTemp, ref dueFan, serial, res);
            prof.Show();
            prof.FormClosed += prof_FormClosed;
        }

        void prof_FormClosed(object sender, FormClosedEventArgs e)
        {
            while (serial.IsOpen) ;
            try
            {
                serial.Open();
            }
            catch
            {
                System.Threading.Thread.Sleep(500);
                serial.Open();
            }
        }

        private void ArduinoController_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) this.ShowInTaskbar = false;
        }
    }
}
