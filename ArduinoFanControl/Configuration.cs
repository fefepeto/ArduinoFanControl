using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;

namespace ArduinoFanControl
{
    public partial class Configuration : Form
    {
        Computer computer;
        List<ISensor> sensors = new List<ISensor>();
        List<TextBox> tb = new List<TextBox>();
        List<CheckBox> hidden = new List<CheckBox>();
        List<CheckBox> logged = new List<CheckBox>();
        public List<DomainUpDown> duds = new List<DomainUpDown>();
        SerialPort port;
        System.IO.StreamWriter sw;
        System.IO.StreamReader sr;
                        
        public Configuration(ref Computer cmp, ref SerialPort p)
        {
            InitializeComponent();
            computer = cmp;
            port = p;
        }

        public struct fileStructure
        {
            public string Name;
            public string Alias;
            public bool hide;
            public bool log;
            public int CId;
        };

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

        private void Configuration_Load(object sender, EventArgs e)
        {
            RowStyle row = new RowStyle(SizeType.Absolute, 22);
            table.RowStyles.Add(row);
            sr = new System.IO.StreamReader("config.ini");
            int idx = 0;
            List<fileStructure> fs = new List<fileStructure>();
            string red = sr.ReadLine();
            while (red != null)
            {
                if (red.Split('=')[0].TrimEnd(' ') == "LastUsedPort")
                {
                    
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
                    cfs.hide = bool.Parse(red.Split(' ')[1]);
                    cfs.log = bool.Parse(red.Split(' ')[2]);
                    if (red.Split(' ').Length == 4) cfs.CId = int.Parse(red.Split(' ')[3]);
                    else cfs.CId = 0;
                    fs.Add(cfs);
                }
                red = sr.ReadLine();
            }
            sr.Close();

            foreach (var hardwareItem in computer.Hardware)
            {
                hardwareItem.Update();
                foreach (IHardware subHardware in hardwareItem.SubHardware)
                {
                    subHardware.Update();
                    foreach (var sensor in subHardware.Sensors)
                    {
                        TextBox textb = new TextBox();
                        CheckBox hide = new CheckBox();
                        CheckBox log = new CheckBox();

                        switch (sensor.SensorType)
                        {
                            case SensorType.Temperature:
                                table.RowCount++;
                                textb.Name = "Sensor" + sensors.Count.ToString();
                                idx = searchIndex(sensor.Name + " " + sensor.SensorType.ToString(), fs);
                                if (idx < 255) textb.Text = fs[idx].Alias;
                                else textb.Text = sensor.Name + " " + sensor.SensorType.ToString();
                                textb.AcceptsReturn = true;
                                textb.Size = new System.Drawing.Size(100, 20);
                                textb.Location = new Point(3, 16 + 22 * tb.Count);
                                textb.TabIndex = table.RowCount * 3;
                                textb.Width = 200;
                                tb.Add(textb);
                                table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                                DomainUpDown dud = new DomainUpDown();
                                dud.Name = "TempCTRLIdx" + sensors.Count.ToString();
                                dud.Items.Add(1);
                                dud.Items.Add(2);
                                dud.Items.Add(3);
                                dud.Items.Add(4);
                                dud.Items.Add(5);
                                dud.Location = new Point((int)(table.Size.Width * 78.5 / 100 + 2), 16 + 22 * hidden.Count);
                                dud.Size = new Size(35, 14);
                                if (idx < 255) dud.SelectedItem = fs[idx].CId;
                                duds.Add(dud);
                                table.Controls.Add(dud, 1, table.RowCount - 1);
                                hide.Name = "SensorHide" + sensors.Count.ToString();
                                hide.Size = new System.Drawing.Size(15, 14);
                                hide.TabIndex = table.RowCount * 3 + 1;
                                hide.Text = "";
                                hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                                hide.AutoSize = true;
                                if (idx < 255) hide.Checked = fs[idx].hide;
                                hidden.Add(hide);
                                table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                                log.Name = "SensorLog" + sensors.Count.ToString();
                                log.Size = new System.Drawing.Size(15, 14);
                                log.TabIndex = table.RowCount * 3 + 2;
                                log.Text = "";
                                log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                                log.AutoSize = true;
                                if (idx < 255) log.Checked = fs[idx].log;
                                logged.Add(log);
                                table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
                                sensors.Add(sensor);
                                break;
                            case SensorType.Voltage:
                                table.RowCount++;
                                textb.Name = "Sensor" + sensors.Count.ToString();
                                idx = searchIndex(sensor.Name + " " + sensor.SensorType.ToString(), fs);
                                if (idx < 255) textb.Text = fs[idx].Alias;
                                else textb.Text = sensor.Name + " " + sensor.SensorType.ToString();
                                textb.AcceptsReturn = true;
                                textb.Size = new System.Drawing.Size(100, 20);
                                textb.Location = new Point(3, 16 + 22 * tb.Count);
                                textb.TabIndex = table.RowCount * 3;
                                textb.Width = 200;
                                tb.Add(textb);
                                table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                                hide.Name = "SensorHide" + sensors.Count.ToString();
                                hide.Size = new System.Drawing.Size(15, 14);
                                hide.TabIndex = table.RowCount * 3 + 1;
                                hide.Text = "";
                                hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                                hide.AutoSize = true;
                                if (idx < 255) hide.Checked = fs[idx].hide;
                                hidden.Add(hide);
                                table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                                log.Name = "SensorLog" + sensors.Count.ToString();
                                log.Size = new System.Drawing.Size(15, 14);
                                log.TabIndex = table.RowCount * 3 + 2;
                                log.Text = "";
                                log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                                log.AutoSize = true;
                                if (idx < 255) log.Checked = fs[idx].log;
                                logged.Add(log);
                                table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
                                sensors.Add(sensor);
                                break;
                            case SensorType.Load:
                                table.RowCount++;
                                textb.Name = "Sensor" + sensors.Count.ToString();
                                idx = searchIndex(sensor.Name + " " + sensor.SensorType.ToString(), fs);
                                if (idx < 255) textb.Text = fs[idx].Alias;
                                else textb.Text = sensor.Name + " " + sensor.SensorType.ToString();
                                textb.AcceptsReturn = true;
                                textb.Size = new System.Drawing.Size(100, 20);
                                textb.Location = new Point(3, 16 + 22 * tb.Count);
                                textb.TabIndex = table.RowCount * 3;
                                textb.Width = 200;
                                tb.Add(textb);
                                table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                                hide.Name = "SensorHide" + sensors.Count.ToString();
                                hide.Size = new System.Drawing.Size(15, 14);
                                hide.TabIndex = table.RowCount * 3 + 1;
                                hide.Text = "";
                                hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                                hide.AutoSize = true;
                                if (idx < 255) hide.Checked = fs[idx].hide;
                                hidden.Add(hide);
                                table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                                log.Name = "SensorLog" + sensors.Count.ToString();
                                log.Size = new System.Drawing.Size(15, 14);
                                log.TabIndex = table.RowCount * 3 + 2;
                                log.Text = "";
                                log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                                log.AutoSize = true;
                                if (idx < 255) log.Checked = fs[idx].log;
                                logged.Add(log);
                                table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
                                sensors.Add(sensor);
                                break;
                            case SensorType.Clock:
                                table.RowCount++;
                                textb.Name = "Sensor" + sensors.Count.ToString();
                                idx = searchIndex(sensor.Name + " " + sensor.SensorType.ToString(), fs);
                                if (idx < 255) textb.Text = fs[idx].Alias;
                                else textb.Text = sensor.Name + " " + sensor.SensorType.ToString();
                                textb.AcceptsReturn = true;
                                textb.Size = new System.Drawing.Size(100, 20);
                                textb.Location = new Point(3, 16 + 22 * tb.Count);
                                textb.TabIndex = table.RowCount * 3;
                                textb.Width = 200;
                                tb.Add(textb);
                                table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                                hide.Name = "SensorHide" + sensors.Count.ToString();
                                hide.Size = new System.Drawing.Size(15, 14);
                                hide.TabIndex = table.RowCount * 3 + 1;
                                hide.Text = "";
                                hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                                hide.AutoSize = true;
                                if (idx < 255) hide.Checked = fs[idx].hide;
                                hidden.Add(hide);
                                table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                                log.Name = "SensorLog" + sensors.Count.ToString();
                                log.Size = new System.Drawing.Size(15, 14);
                                log.TabIndex = table.RowCount * 3 + 2;
                                log.Text = "";
                                log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                                log.AutoSize = true;
                                if (idx < 255) log.Checked = fs[idx].log;
                                logged.Add(log);
                                table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
                                sensors.Add(sensor);
                                break;
                            case SensorType.Fan:
                                table.RowCount++;
                                textb.Name = "Sensor" + sensors.Count.ToString();
                                idx = searchIndex(sensor.Name + " " + sensor.SensorType.ToString(), fs);
                                if (idx < 255) textb.Text = fs[idx].Alias;
                                else textb.Text = sensor.Name + " " + sensor.SensorType.ToString();
                                textb.AcceptsReturn = true;
                                textb.Size = new System.Drawing.Size(100, 20);
                                textb.Location = new Point(3, 16 + 22 * tb.Count);
                                textb.TabIndex = table.RowCount * 3;
                                textb.Width = 200;
                                tb.Add(textb);
                                table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                                hide.Name = "SensorHide" + sensors.Count.ToString();
                                hide.Size = new System.Drawing.Size(15, 14);
                                hide.TabIndex = table.RowCount * 3 + 1;
                                hide.Text = "";
                                hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                                hide.AutoSize = true;
                                if (idx < 255) hide.Checked = fs[idx].hide;
                                hidden.Add(hide);
                                table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                                log.Name = "SensorLog" + sensors.Count.ToString();
                                log.Size = new System.Drawing.Size(15, 14);
                                log.TabIndex = table.RowCount * 3 + 2;
                                log.Text = "";
                                log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                                log.AutoSize = true;
                                if (idx < 255) log.Checked = fs[idx].log;
                                logged.Add(log);
                                table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
                                sensors.Add(sensor);
                                break;
                            default:
                                break;
                        }
                    }
                }

                foreach (var sensor in hardwareItem.Sensors)
                {
                    TextBox textb = new TextBox();
                    CheckBox hide = new CheckBox();
                    CheckBox log = new CheckBox();

                    switch (sensor.SensorType)
                    {
                        case SensorType.Temperature:
                            table.RowCount++;
                            textb.Name = "Sensor" + sensors.Count.ToString();
                            idx = searchIndex(sensor.Name + " " + sensor.SensorType.ToString(), fs);
                            if (idx < 255) textb.Text = fs[idx].Alias;
                            else textb.Text = sensor.Name + " " + sensor.SensorType.ToString();
                            textb.AcceptsReturn = true;
                            textb.Size = new System.Drawing.Size(100, 20);
                            textb.Location = new Point(3, 16 + 22 * tb.Count);
                            textb.TabIndex = table.RowCount * 3;
                            textb.Width = 200;
                            tb.Add(textb);
                            table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                            DomainUpDown dud = new DomainUpDown();
                            dud.Name = "TempCTRLIdx" + sensors.Count.ToString();
                            dud.Items.Add(1);
                            dud.Items.Add(2);
                            dud.Items.Add(3);
                            dud.Items.Add(4);
                            dud.Items.Add(5);
                            dud.Location = new Point((int)(table.Size.Width * 78.5 / 100 + 2), 16 + 22 * hidden.Count);
                            dud.Size = new Size(35, 14);
                            if (idx < 255) dud.SelectedItem = fs[idx].CId;
                            duds.Add(dud);
                            table.Controls.Add(dud, 1, table.RowCount - 1);
                            hide.Name = "SensorHide" + sensors.Count.ToString();
                            hide.Size = new System.Drawing.Size(15, 14);
                            hide.TabIndex = table.RowCount * 3 + 1;
                            hide.Text = "";
                            hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                            hide.AutoSize = true;
                            if (idx < 255) hide.Checked = fs[idx].hide;
                            hidden.Add(hide);
                            table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                            log.Name = "SensorLog" + sensors.Count.ToString();
                            log.Size = new System.Drawing.Size(15, 14);
                            log.TabIndex = table.RowCount * 3 + 2;
                            log.Text = "";
                            log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                            log.AutoSize = true;
                            if (idx < 255) log.Checked = fs[idx].log;
                            logged.Add(log);
                            table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
                            sensors.Add(sensor);
                            break;
                        case SensorType.Voltage:
                            table.RowCount++;
                            textb.Name = "Sensor" + sensors.Count.ToString();
                            idx = searchIndex(sensor.Name + " " + sensor.SensorType.ToString(), fs);
                            if (idx < 255) textb.Text = fs[idx].Alias;
                            else textb.Text = sensor.Name + " " + sensor.SensorType.ToString();
                            textb.AcceptsReturn = true;
                            textb.Size = new System.Drawing.Size(100, 20);
                            textb.Location = new Point(3, 16 + 22 * tb.Count);
                            textb.TabIndex = table.RowCount * 3;
                            textb.Width = 200;
                            tb.Add(textb);
                            table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                            hide.Name = "SensorHide" + sensors.Count.ToString();
                            hide.Size = new System.Drawing.Size(15, 14);
                            hide.TabIndex = table.RowCount * 3 + 1;
                            hide.Text = "";
                            hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                            hide.AutoSize = true;
                            if (idx < 255) hide.Checked = fs[idx].hide;
                            hidden.Add(hide);
                            table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                            log.Name = "SensorLog" + sensors.Count.ToString();
                            log.Size = new System.Drawing.Size(15, 14);
                            log.TabIndex = table.RowCount * 3 + 2;
                            log.Text = "";
                            log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                            log.AutoSize = true;
                            if (idx < 255) log.Checked = fs[idx].log;
                            logged.Add(log);
                            table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
                            sensors.Add(sensor);
                            break;
                        case SensorType.Load:
                            table.RowCount++;
                            textb.Name = "Sensor" + sensors.Count.ToString();
                            idx = searchIndex(sensor.Name + " " + sensor.SensorType.ToString(), fs);
                            if (idx < 255) textb.Text = fs[idx].Alias;
                            else textb.Text = sensor.Name + " " + sensor.SensorType.ToString();
                            textb.AcceptsReturn = true;
                            textb.Size = new System.Drawing.Size(100, 20);
                            textb.Location = new Point(3, 16 + 22 * tb.Count);
                            textb.TabIndex = table.RowCount * 3;
                            textb.Width = 200;
                            tb.Add(textb);
                            table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                            hide.Name = "SensorHide" + sensors.Count.ToString();
                            hide.Size = new System.Drawing.Size(15, 14);
                            hide.TabIndex = table.RowCount * 3 + 1;
                            hide.Text = "";
                            hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                            hide.AutoSize = true;
                            if (idx < 255) hide.Checked = fs[idx].hide;
                            hidden.Add(hide);
                            table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                            log.Name = "SensorLog" + sensors.Count.ToString();
                            log.Size = new System.Drawing.Size(15, 14);
                            log.TabIndex = table.RowCount * 3 + 2;
                            log.Text = "";
                            log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                            log.AutoSize = true;
                            if (idx < 255) log.Checked = fs[idx].log;
                            logged.Add(log);
                            table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
                            sensors.Add(sensor);
                            break;
                        case SensorType.Clock:
                            table.RowCount++;
                            textb.Name = "Sensor" + sensors.Count.ToString();
                            idx = searchIndex(sensor.Name + " " + sensor.SensorType.ToString(), fs);
                            if (idx < 255) textb.Text = fs[idx].Alias;
                            else textb.Text = sensor.Name + " " + sensor.SensorType.ToString();
                            textb.AcceptsReturn = true;
                            textb.Size = new System.Drawing.Size(100, 20);
                            textb.Location = new Point(3, 16 + 22 * tb.Count);
                            textb.TabIndex = table.RowCount * 3;
                            textb.Width = 200;
                            tb.Add(textb);
                            table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                            hide.Name = "SensorHide" + sensors.Count.ToString();
                            hide.Size = new System.Drawing.Size(15, 14);
                            hide.TabIndex = table.RowCount * 3 + 1;
                            hide.Text = "";
                            hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                            hide.AutoSize = true;
                            if (idx < 255) hide.Checked = fs[idx].hide;
                            hidden.Add(hide);
                            table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                            log.Name = "SensorLog" + sensors.Count.ToString();
                            log.Size = new System.Drawing.Size(15, 14);
                            log.TabIndex = table.RowCount * 3 + 2;
                            log.Text = "";
                            log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                            log.AutoSize = true;
                            if (idx < 255) log.Checked = fs[idx].log;
                            logged.Add(log);
                            table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
                            sensors.Add(sensor);
                            break;
                        case SensorType.Fan:
                            table.RowCount++;
                            textb.Name = "Sensor" + sensors.Count.ToString();
                            idx = searchIndex(sensor.Name + " " + sensor.SensorType.ToString(), fs);
                            if (idx < 255) textb.Text = fs[idx].Alias;
                            else textb.Text = sensor.Name + " " + sensor.SensorType.ToString();
                            textb.AcceptsReturn = true;
                            textb.Size = new System.Drawing.Size(100, 20);
                            textb.Location = new Point(3, 16 + 22 * tb.Count);
                            textb.TabIndex = table.RowCount * 3;
                            textb.Width = 200;
                            tb.Add(textb);
                            table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                            hide.Name = "SensorHide" + sensors.Count.ToString();
                            hide.Size = new System.Drawing.Size(15, 14);
                            hide.TabIndex = table.RowCount * 3 + 1;
                            hide.Text = "";
                            hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                            hide.AutoSize = true;
                            if (idx < 255) hide.Checked = fs[idx].hide;
                            hidden.Add(hide);
                            table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                            log.Name = "SensorLog" + sensors.Count.ToString();
                            log.Size = new System.Drawing.Size(15, 14);
                            log.TabIndex = table.RowCount * 3 + 2;
                            log.Text = "";
                            log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                            log.AutoSize = true;
                            if (idx < 255) log.Checked = fs[idx].log;
                            logged.Add(log);
                            table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
                            sensors.Add(sensor);
                            break;
                        default:
                            break;
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                TextBox textb = new TextBox();
                CheckBox hide = new CheckBox();
                CheckBox log = new CheckBox();

                table.RowCount++;
                textb.Name = "Sensor" + (sensors.Count + i + 1).ToString();
                idx = searchIndex("DueTemperature" + (i + 1).ToString(), fs);
                if (idx < 255) textb.Text = fs[idx].Alias;
                else textb.Text = "DueTemperature" + (i + 1).ToString();
                textb.AcceptsReturn = true;
                textb.Size = new System.Drawing.Size(100, 20);
                textb.Location = new Point(3, 16 + 22 * tb.Count);
                textb.TabIndex = table.RowCount * 3;
                textb.Width = 200;
                tb.Add(textb);
                table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                hide.Name = "SensorHide" + (sensors.Count + i + 1).ToString();
                hide.Size = new System.Drawing.Size(15, 14);
                hide.TabIndex = table.RowCount * 3 + 1;
                hide.Text = "";
                hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                hide.AutoSize = true;
                if (idx < 255) hide.Checked = fs[idx].hide;
                hidden.Add(hide);
                table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                log.Name = "SensorLog" + (sensors.Count + i + 1).ToString();
                log.Size = new System.Drawing.Size(15, 14);
                log.TabIndex = table.RowCount * 3 + 2;
                log.Text = "";
                log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                log.AutoSize = true;
                if (idx < 255) log.Checked = fs[idx].log;
                logged.Add(log);
                table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
            }

            for (int i = 0; i < 6; i++)
            {
                TextBox textb = new TextBox();
                CheckBox hide = new CheckBox();
                CheckBox log = new CheckBox();

                table.RowCount++;
                textb.Name = "Sensor" + (sensors.Count + i + 1).ToString();
                idx = searchIndex("DueFan" + (i + 1).ToString(), fs);
                if (idx < 255) textb.Text = fs[idx].Alias;
                else textb.Text = "DueFan" + (i + 1).ToString();
                textb.AcceptsReturn = true;
                textb.Size = new System.Drawing.Size(100, 20);
                textb.Location = new Point(3, 16 + 22 * tb.Count);
                textb.TabIndex = table.RowCount * 3;
                textb.Width = 200;
                tb.Add(textb);
                table.Controls.Add(tb[tb.Count - 1], 0, table.RowCount - 1);
                hide.Name = "SensorHide" + (sensors.Count + i + 5).ToString();
                hide.Size = new System.Drawing.Size(15, 14);
                hide.TabIndex = table.RowCount * 3 + 1;
                hide.Text = "";
                hide.Location = new Point(table.Size.Width * 85 / 100 + 2, 16 + 22 * hidden.Count);
                hide.AutoSize = true;
                if (idx < 255) hide.Checked = fs[idx].hide;
                hidden.Add(hide);
                table.Controls.Add(hidden[hidden.Count - 1], 2, table.RowCount - 1);
                log.Name = "SensorLog" + (sensors.Count + i + 5).ToString();
                log.Size = new System.Drawing.Size(15, 14);
                log.TabIndex = table.RowCount * 3 + 2;
                log.Text = "";
                log.Location = new Point((int)(table.Size.Width * 92.5 / 100 + 2), 16 + 22 * hidden.Count);
                log.AutoSize = true;
                if (idx < 255) log.Checked = fs[idx].log;
                logged.Add(log);
                table.Controls.Add(logged[logged.Count - 1], 3, table.RowCount - 1);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            bool sign = false;
            for (int first = 0; first < duds.Count; first++)
            {
                for (int second = first + 1; second < duds.Count; second++)
                {
                    sign |= duds[first] == duds[second];
                }
            }

            DialogResult dr;
            if (sign) dr = MessageBox.Show("Same ID selected for multiple temperatures!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                sw = new System.IO.StreamWriter("config.ini");
                sw.WriteLine("LastUsedPort = " + port.PortName);
                int i;
                for (i = 0; i < sensors.Count; i++)
                {
                    switch (sensors[i].SensorType)
                    {
                        case SensorType.Clock:
                        case SensorType.Control:
                        case SensorType.Data:
                        case SensorType.Factor:
                        case SensorType.Fan:
                        case SensorType.Flow:
                        case SensorType.Level:
                        case SensorType.Load:
                        case SensorType.Power:
                        case SensorType.Voltage:
                        default:
                            sw.WriteLine(sensors[i].Name + " = " + tb[i].Text + "," + hidden[i].Checked + "," + logged[i].Checked);
                            break;
                        case SensorType.Temperature:
                            int id = 0;
                            for (id = 0; id < duds.Count; id++)
                            {
                                DomainUpDown current = duds[id];
                                if (current.Name.Contains(i.ToString())) break;
                            }
                            if (duds[id].SelectedItem != null) sw.WriteLine(sensors[i].Name + " = " + tb[i].Text + "," + hidden[i].Checked + "," + logged[i].Checked + "," + (int.Parse(duds[id].SelectedItem.ToString())));
                            else sw.WriteLine(sensors[i].Name + " = " + tb[i].Text + "," + hidden[i].Checked + "," + logged[i].Checked + "," + "-1");
                            break;
                    }
                }
                int k;
                for (k = 0; k < 4; k++)
                {
                    sw.WriteLine("DueTemperature" + (k + 1).ToString() + " = " + tb[k + i].Text + "," + hidden[k + i].Checked + "," + logged[k + i].Checked);
                }
                for (int j = 0; j < 6; j++)
                {
                    sw.WriteLine("DueFan" + (j + 1).ToString() + " = " + tb[j + 4 + i].Text + "," + hidden[j + 4 + i].Checked + "," + logged[j + 4 + i].Checked);
                }
                sw.Close();
            }
            this.Close();
        }
    }
}
