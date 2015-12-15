using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoFanControl
{
    public partial class Profile : Form
    {
        StreamReader sr;
        List<ArduinoController.sensorData> temp = new List<ArduinoController.sensorData>();
        ArduinoController.DueSensors[] dueTemp = new ArduinoController.DueSensors[4];
        ArduinoController.DueSensors[] dueFan = new ArduinoController.DueSensors[6];

        SerialPort com = new SerialPort();

        Measure.result[] res = new Measure.result[6];

        private string send = "";

        delegate void SetTextCallback(string text, Label l);

        public void SetText(string text, Label l)
        {
            if (l.InvokeRequired)
            {
                SetTextCallback d = new Profile.SetTextCallback(SetText);
                this.Invoke(d, new object[] { text, l });
            }
            else
            {
                l.Text = text;
            }
        }

        delegate void SetValueCallback(int value, ProgressBar p);

        public void SetValue(int value, ProgressBar p)
        {
            if (p.InvokeRequired)
            {
                SetValueCallback d = new Profile.SetValueCallback(SetValue);
                this.Invoke(d, new object[] { value, p });
            }
            else
            {
                p.Value = value;
            }
        }

        public struct FanProfile
        {
            public bool PWM_capable;
            public bool SumOfSpeeds;
            public bool Manual;
            public short manual_df;
            public short starting_df;
            public int minimal_rpm;
            public int maximal_rpm;
            public float[] hysteresis;
            public float[,] temp;
            public short[,] df;
        }

        FanProfile[] fans = new FanProfile[6];
        FanProfile fan;
        Point[] drawing = new Point[20];

        public Profile(ref List<ArduinoController.sensorData> sensor, ref ArduinoController.DueSensors[] duetemp, ref ArduinoController.DueSensors[] duefan, SerialPort p, Measure.result[] inres)
        {
            InitializeComponent();
            temp = sensor;
            dueTemp = duetemp;
            dueFan = duefan;
            res = inres;
            com.PortName = p.PortName;
            com.BaudRate = p.BaudRate;
            com.Parity = p.Parity;
            com.StopBits = p.StopBits;
            com.DataBits = p.DataBits;
            com.Handshake = p.Handshake;
            com.RtsEnable = p.RtsEnable;
            com.DtrEnable = p.DtrEnable;
            com.ReadTimeout = 1000;
            com.WriteTimeout = 1000;
            com.WriteBufferSize = 12000;
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            for (int id = 0; id < 6; id++)
            {
                fans[id].hysteresis = new float[9];
                fans[id].temp = new float[9, 20];
                fans[id].df = new short[9, 20];
            }
            fan.hysteresis = new float[9];
            fan.temp = new float[9, 20];
            fan.df = new short[9, 20];

            int i = -1;
            sr = new StreamReader("profiles.ini");
            string red = sr.ReadLine();
            while (red != null)
            {
                if (red.Contains("FAN")) i++;
                else
                {
                    switch (red.Split('=')[0].TrimEnd(' '))
                    {
                        case "PWM_capable":
                            fans[i].PWM_capable = bool.Parse(red.Split('=')[1].TrimStart(' '));
                            break;
                        case "SumOfSpeeds":
                            fans[i].SumOfSpeeds = bool.Parse(red.Split('=')[1].TrimStart(' '));
                            break;
                        case "Manual":
                            fans[i].Manual = bool.Parse(red.Split('=')[1].TrimStart(' '));
                            break;
                        case "manual_df":
                            fans[i].manual_df = short.Parse(red.Split('=')[1].TrimStart(' '));
                            break;
                        case "starting_df":
                            fans[i].starting_df = short.Parse(red.Split('=')[1].TrimStart(' '));
                            break;
                        case "minimal_rpm":
                            fans[i].minimal_rpm = int.Parse(red.Split('=')[1].TrimStart(' '));
                            break;
                        case "maximal_rpm":
                            fans[i].maximal_rpm = int.Parse(red.Split('=')[1].TrimStart(' '));
                            break;
                        case "hysteresis":
                            string[] input = red.Split('=')[1].TrimStart(" {".ToCharArray()).TrimEnd('}').Split(',');
                            for (int j = 0; j < input.Length; j++)
                            {
                                input[j] = input[j].TrimStart(' ');
                                fans[i].hysteresis[j] = float.Parse(input[j]);
                            }
                            break;
                        case "temp":
                            string[] input2 = red.Split('=')[1].TrimStart(" {".TrimEnd('}').ToCharArray()).Split(';');
                            for (int j = 0; j < input2.Length; j++)
                            {
                                string[] input3 = input2[j].TrimStart(" {".ToCharArray()).TrimEnd('}').Split(',');
                                for (int k = 0; k < input3.Length; k++)
                                {
                                    input3[k] = input3[k].TrimStart(' ');
                                    fans[i].temp[j, k] = float.Parse(input3[k]);
                                }
                            }
                            break;
                        case "df":
                            string[] input4 = red.Split('=')[1].TrimStart(" {".TrimEnd('}').ToCharArray()).Split(';');
                            for (int j = 0; j < input4.Length; j++)
                            {
                                string[] input5 = input4[j].TrimStart(" {".ToCharArray()).TrimEnd('}').Split(',');
                                for (int k = 0; k < input5.Length; k++)
                                {
                                    input5[k] = input5[k].TrimStart(' ');
                                    fans[i].df[j, k] = short.Parse(input5[k]);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                red = sr.ReadLine();
            }
            sr.Close();

            string[] str = new string[5];
            string[] str2 = new string[4];
            string[] str3 = new string[6];
            for (int h = 0; h < temp.Count; h++)
            {
                str[temp[h].CId] = temp[h].label.Text.Split('=')[0].TrimEnd(' ');
            }
            for (int h = 0; h < str.Length; h++)
            {
                if (str[h] != null) TempSel.Items.Add(str[h]);
            }
            while (TempSel.Items.Count < 5) TempSel.Items.Add("");
            for (int h = 0; h < dueTemp.Length; h++)
            {
                str2[h] = dueTemp[h].label.Text.Split('=')[0].TrimEnd(' ');
            }
            for (int h = 0; h < str2.Length; h++)
            {
                if (str2[h] != null) TempSel.Items.Add(str2[h]);
            }
            for (int h = 0; h < dueFan.Length; h++)
            {
                str3[h] = dueFan[h].label.Text.Split('=')[0].TrimEnd(' ');
            }
            for (int h = 0; h < str3.Length; h++)
            {
                if (str3[h] != null) FanSel.Items.Add(str3[h]);
            }

            for (int id = 0; id < 6; id++)
            {
                if (res[id].id != 0)
                {
                    fans[id].maximal_rpm = res[id].maximal_RPM;
                    fans[id].minimal_rpm = res[id].minimal_RPM;
                    fans[id].PWM_capable = res[id].PWM_capable;
                    fans[id].starting_df = res[id].starting_df;
                }
            }

            Lowest.Value = 40;
            TD10.Checked = true;
            TD20.Checked = false;
            Hyst.Value = 0;
            Highest.Value = (decimal)49.5;
            SumOfSpeeds.Checked = false;
            MaxOfSpeeds.Checked = true;
            PWMc.Checked = false;
        }
        
        private void Program_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Are you sure you want to program these profiles?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                this.OK.Enabled = false;
                send = "P";
                for (int i = 0; i < 6; i++)
                {
                    if (fans[i].PWM_capable) send += "1 ";
                    else send += "0 ";
                    if (fans[i].SumOfSpeeds) send += "1 ";
                    else send += "0 ";
                    if (fans[i].Manual) send += "1 ";
                    else send += "0 ";
                    send += fans[i].manual_df.ToString() + " ";
                    send += fans[i].starting_df.ToString() + " ";
                    send += fans[i].minimal_rpm.ToString() + " ";
                    send += fans[i].maximal_rpm.ToString() + " ";
                    for (int j = 0; j < 9; j++)
                    {
                        send += fans[i].hysteresis[j].ToString("F1") + " ";
                        for (int k = 0; k < 20; k++)
                        {
                            send += fans[i].temp[j, k].ToString("F1") + " ";
                            send += fans[i].df[j, k].ToString() + " ";
                        }
                    }
                }
                send = send.TrimEnd(' ');
                if (!com.IsOpen) com.Open();
                com.DiscardInBuffer();
                com.Write(send);
                while (com.BytesToWrite > 0) ;
                com.Close();
                this.OK.Enabled = true;
            }
        }

        private void Lowest_ValueChanged(object sender, EventArgs e)
        {
            if (TD10.Checked) Highest.Value = Lowest.Value + (decimal)9.5;
            else if (TD20.Checked) Highest.Value = Lowest.Value + 19;
        }

        private void Highest_ValueChanged(object sender, EventArgs e)
        {
            if (TD10.Checked) Lowest.Value = Highest.Value - (decimal)9.5;
            else if (TD20.Checked) Lowest.Value = Highest.Value - 19;
        }

        private void TD20_CheckedChanged(object sender, EventArgs e)
        {
            if (TD10.Checked) Highest.Value = Lowest.Value + (decimal)9.5;
            else if (TD20.Checked) Highest.Value = Lowest.Value + 19;
        }

        private void TD10_CheckedChanged(object sender, EventArgs e)
        {
            if (TD10.Checked) Highest.Value = Lowest.Value + (decimal)(9.5);
            else if (TD20.Checked) Highest.Value = Lowest.Value + 19;
        }

        private void ProfileChart_Paint(object sender, PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            graph.Clear(SystemColors.Window);

            Pen pen = new Pen(Color.Black);

            for (int y = 0; y < 21; y++)
            {
                graph.DrawLine(pen, y * 30, 0, y * 30, 421);
            }

            for (int x = 0; x < 11; x++)
            {
                graph.DrawLine(pen, 0, x * 42, 571, x * 42);
            }
        }

        private void Painted(object sender, PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            graph.Clear(SystemColors.Window);

            Pen pen = new Pen(Color.Black);

            for (int y = 0; y < 21; y++)
            {
                graph.DrawLine(pen, y * 30, 0, y * 30, 421);
            }

            for (int x = 0; x < 11; x++)
            {
                graph.DrawLine(pen, 0, x * 42, 571, x * 42);
            }

            SolidBrush b = new SolidBrush(Color.Blue);
            for (int i = 0; i < drawing.Length; i++)
            {
                graph.FillRectangle(b, drawing[i].X - 1, drawing[i].Y - 1, 3, 3);
            }
            b.Color = Color.Red;
            graph.FillRectangle(b, 0, ProfileChart.Height - fans[FanSel.SelectedIndex].starting_df * 2, ProfileChart.Width, 2);
        }

        private void refresh()
        {
            if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0] == 0) Lowest.Value = 40;
            else Lowest.Value = (decimal)(fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0]);
            if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0] - fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 19] <= 10)
            {
                TD10.Checked = true;
                TD20.Checked = false;
                Highest.Value = 50;
            }
            else
            {
                TD10.Checked = false;
                TD20.Checked = true;
                Highest.Value = 60;
            }
            Hyst.Value = (decimal)fans[FanSel.SelectedIndex].hysteresis[TempSel.SelectedIndex];
            if (fans[FanSel.SelectedIndex].SumOfSpeeds)
            {
                SumOfSpeeds.Checked = true;
                MaxOfSpeeds.Checked = false;
            }
            else
            {
                SumOfSpeeds.Checked = false;
                MaxOfSpeeds.Checked = true;
            }
            PWMc.Checked = fans[FanSel.SelectedIndex].PWM_capable;
            ProfileChart_Paint(new object(), new PaintEventArgs(ProfileChart.CreateGraphics(), new Rectangle(0, 0, ProfileChart.Width, ProfileChart.Height)));
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            fan.hysteresis[0] = fans[FanSel.SelectedIndex].hysteresis[TempSel.SelectedIndex];
            fan.PWM_capable = fans[FanSel.SelectedIndex].PWM_capable;
            fan.SumOfSpeeds = fans[FanSel.SelectedIndex].SumOfSpeeds;
            for (int i = 0; i < 20; i++)
            {
                fan.temp[0, i] = fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i];
                fan.df[0, i] = fans[FanSel.SelectedIndex].df[TempSel.SelectedIndex, i];
            }
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            fans[FanSel.SelectedIndex].hysteresis[TempSel.SelectedIndex] = fan.hysteresis[0];
            fans[FanSel.SelectedIndex].PWM_capable = fan.PWM_capable;
            fans[FanSel.SelectedIndex].SumOfSpeeds = fan.SumOfSpeeds;
            for (int i = 0; i < 20; i++)
            {
                fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i] = fan.temp[0, i];
                fan.df[TempSel.SelectedIndex, i] = fan.df[0, i];
            }
            TempSel_SelectedIndexChanged(sender, e);
            Painted(sender, new PaintEventArgs(ProfileChart.CreateGraphics(), new Rectangle(0, 0, ProfileChart.Width, ProfileChart.Height)));
        }

        private void ProfileChart_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            int x = me.X, y = ProfileChart.Height - me.Y, dwy, dwx;
            if (x / 30.0 - Math.Floor(x / 30.0) < 0.5) dwx = (int)Math.Floor(x / 30.0);
            else dwx = (int)Math.Ceiling(x / 30.0);
            dwy = (int)(Math.Round(y / 2.0));
            if (dwy > fans[FanSel.SelectedIndex].starting_df)
            {
                if (dwx > 0)
                {
                    if (dwy > fans[FanSel.SelectedIndex].df[TempSel.SelectedIndex, dwx - 1])
                    {
                        drawing[dwx].Y = ProfileChart.Height - dwy * 2;
                        drawing[dwx].X = dwx * 30;
                        fans[FanSel.SelectedIndex].df[TempSel.SelectedIndex, dwx] = (short)dwy;
                        if (TD10.Checked) fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, dwx] = (float)0.5 * dwx + (float)Lowest.Value;
                        else fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, dwx] = (float)dwx + (float)Lowest.Value;
                        if (dwx < 19)
                        {
                            for (int i = dwx; i < drawing.Length - 1; i++)
                            {
                                if (ProfileChart.Height - dwy * 2 < drawing[i + 1].Y)
                                {
                                    drawing[i + 1].Y = ProfileChart.Height - dwy * 2;
                                    drawing[i + 1].X = (i + 1) * 30;
                                    fans[FanSel.SelectedIndex].df[TempSel.SelectedIndex, i + 1] = (short)dwy;
                                    if (TD10.Checked) fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i + 1] = (float)0.5 * (i + 1) + (float)Lowest.Value;
                                    else fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i + 1] = (float)i + 1 + (float)Lowest.Value;
                                }
                            }
                        }
                    }
                }
                else
                {
                    drawing[dwx].Y = ProfileChart.Height - dwy * 2;
                    drawing[dwx].X = dwx * 30;
                    fans[FanSel.SelectedIndex].df[TempSel.SelectedIndex, dwx] = (short)dwy;
                    if (TD10.Checked) fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, dwx] = (float)0.5 * dwx + (float)Lowest.Value;
                    else fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, dwx] = (float)dwx + (float)Lowest.Value;
                    if (dwx < 19)
                    {
                        for (int i = dwx; i < drawing.Length - 1; i++)
                        {
                            if (ProfileChart.Height - dwy * 2 < drawing[i + 1].Y)
                            {
                                drawing[i + 1].Y = ProfileChart.Height - dwy * 2;
                                drawing[i + 1].X = (i + 1) * 30;
                                fans[FanSel.SelectedIndex].df[TempSel.SelectedIndex, i + 1] = (short)dwy;
                                if (TD10.Checked) fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i + 1] = (float)0.5 * (i + 1) + (float)Lowest.Value;
                                else fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i + 1] = (float)i + 1 + (float)Lowest.Value;
                            }
                        }
                    }
                }
            }
            Painted(sender, new PaintEventArgs(ProfileChart.CreateGraphics(), new Rectangle(0, 0, ProfileChart.Width, ProfileChart.Height)));
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            fans[FanSel.SelectedIndex].hysteresis[TempSel.SelectedIndex] = 0;
            fans[FanSel.SelectedIndex].SumOfSpeeds = false;
            for (int i = 0; i < 20; i++)
            {
                fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i] = 0;
                fans[FanSel.SelectedIndex].df[TempSel.SelectedIndex, i] = 0;
            }
            refresh();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            StreamWriter sw = new StreamWriter("profiles.ini");
            for (int i = 0; i < 6; i++)
            {
                sw.WriteLine("[FAN" + i.ToString() + "]");
                sw.WriteLine("PWM_capable = " + fans[i].PWM_capable.ToString());
                sw.WriteLine("SumOfSpeeds = " + fans[i].SumOfSpeeds.ToString());
                sw.WriteLine("Manual = " + fans[i].Manual.ToString());
                sw.WriteLine("manual_df = " + fans[i].manual_df.ToString());
                sw.WriteLine("starting_df = " + fans[i].starting_df.ToString());
                sw.WriteLine("minimal_rpm = " + fans[i].minimal_rpm.ToString());
                sw.WriteLine("maximal_rpm = " + fans[i].maximal_rpm.ToString());
                sw.Write("hysteresis = {");
                for (int j = 0; j < 8; j++)
                {
                    sw.Write(fans[i].hysteresis[j].ToString() + ", ");
                }
                sw.WriteLine(fans[i].hysteresis[8].ToString() + "}");
                sw.Write("temp = {");
                for (int j = 0; j < 8; j++)
                {
                    sw.Write("{");
                    for (int k = 0; k < 19; k++)
                    {
                        sw.Write(fans[i].temp[j, k].ToString() + ", ");
                    }
                    sw.Write(fans[i].temp[j, 19].ToString() + "}; ");
                }

                sw.Write("{");
                for (int k = 0; k < 19; k++)
                {
                    sw.Write(fans[i].temp[8, k].ToString() + ", ");
                }
                sw.WriteLine(fans[i].temp[8, 19].ToString() + "}}");
                sw.Write("df = {");
                for (int j = 0; j < 8; j++)
                {
                    sw.Write("{");
                    for (int k = 0; k < 19; k++)
                    {
                        sw.Write(fans[i].df[j, k].ToString() + ", ");
                    }
                    sw.Write(fans[i].df[j, 19].ToString() + "}; ");
                }

                sw.Write("{");
                for (int k = 0; k < 19; k++)
                {
                    sw.Write(fans[i].df[8, k].ToString() + ", ");
                }
                sw.WriteLine(fans[i].df[8, 19].ToString() + "}}");
            }
            sw.Close();
            this.Close();
        }

        private void FanSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            PWMc.Checked = fans[FanSel.SelectedIndex].PWM_capable;
            SumOfSpeeds.Checked = fans[FanSel.SelectedIndex].SumOfSpeeds;
            MaxOfSpeeds.Checked = !fans[FanSel.SelectedIndex].SumOfSpeeds;
            if (TempSel.SelectedIndex >= 0)
            {
                if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 19] - fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0] > 9.5)
                {
                    TD10.Checked = false;
                    TD20.Checked = true;
                }
                else
                {
                    TD10.Checked = true;
                    TD20.Checked = false;
                }
                Hyst.Value = (decimal)fans[FanSel.SelectedIndex].hysteresis[TempSel.SelectedIndex];
                if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0] > 0) Lowest.Value = (decimal)fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0];
                for (int i = 0; i < drawing.Length; i++)
                {
                    if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 19] - fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0] > 10)
                        if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i] != 0) drawing[i].X = (int)((fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i] - (float)Lowest.Value) * 30);
                        else
                            if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i] != 0) drawing[i].X = (int)((fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i] - (float)Lowest.Value) * 60);
                    drawing[i].Y = ProfileChart.Height - fans[FanSel.SelectedIndex].df[TempSel.SelectedIndex, i] * 2;
                }
                Painted(sender, new PaintEventArgs(ProfileChart.CreateGraphics(), new Rectangle(0, 0, ProfileChart.Width, ProfileChart.Height)));
            }
        }

        private void TempSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FanSel.SelectedIndex >= 0)
            {
                if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 19] - fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0] > 9.5)
                {
                    TD10.Checked = false;
                    TD20.Checked = true;
                }
                else
                {
                    TD10.Checked = true;
                    TD20.Checked = false;
                }
                Hyst.Value = (decimal)fans[FanSel.SelectedIndex].hysteresis[TempSel.SelectedIndex];
                if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0] > 0) Lowest.Value = (decimal)fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0];
                for (int i = 0; i < drawing.Length; i++)
                {
                    if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 19] - fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, 0] > 10)
                    {
                        if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i] != 0) drawing[i].X = (int)((fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i] - (float)Lowest.Value) * 30);
                    }
                    else
                    {
                        if (fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i] != 0) drawing[i].X = (int)((fans[FanSel.SelectedIndex].temp[TempSel.SelectedIndex, i] - (float)Lowest.Value) * 60);
                    }
                    drawing[i].Y = ProfileChart.Height - fans[FanSel.SelectedIndex].df[TempSel.SelectedIndex, i] * 2;
                }
                Painted(sender, new PaintEventArgs(ProfileChart.CreateGraphics(), new Rectangle(0, 0, ProfileChart.Width, ProfileChart.Height)));
            }
        }

        private void Hyst_ValueChanged(object sender, EventArgs e)
        {
            if (FanSel.SelectedIndex >= 0 && TempSel.SelectedIndex >= 0) fans[FanSel.SelectedIndex].hysteresis[TempSel.SelectedIndex] = (float)Hyst.Value;
        }

        private void MaxOfSpeeds_CheckedChanged(object sender, EventArgs e)
        {
            if (FanSel.SelectedIndex >= 0) fans[FanSel.SelectedIndex].SumOfSpeeds = false;
        }

        private void SumOfSpeeds_CheckedChanged(object sender, EventArgs e)
        {
            if (FanSel.SelectedIndex >= 0) fans[FanSel.SelectedIndex].SumOfSpeeds = true;
        }

        private void PWMc_CheckedChanged(object sender, EventArgs e)
        {
            if (FanSel.SelectedIndex >= 0) fans[FanSel.SelectedIndex].PWM_capable = PWMc.Checked;
        }
    }
}
