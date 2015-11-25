using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;
using System.Windows.Forms;

namespace ArduinoFanControl
{
    public class SensorData
    {
        public SensorData(ISensor sensor)
        {
            this.sensor = sensor;
            this.label = new Label();
        }

        public void update()
        {
            sensor.Hardware.Update();
            switch (disp)
            {
                case SensorData.display.Current:
                    label.Text = sensor.Name + " = " + sensor.Value.ToString();
                    break;
                case SensorData.display.Max:
                    label.Text = sensor.Name + " = " + sensor.Max.ToString();
                    break;
                case SensorData.display.Min:
                    label.Text = sensor.Name + " = " + sensor.Min.ToString();
                    break;
                default:
                    break;
            }
        }

        public ISensor sensor;
        public Label label;
        public display disp;
        public enum display
        {
            Current,
            Max,
            Min
        };
    }
}
