using System;
using System.Collections.Generic;
using System.Linq;

namespace Gas_Holder
{

    public class SensorReading
    {
        public string SensorId { get; set; }
        public double Pressure { get; set; }
        public bool IsOnline { get; set; }

        public SensorReading(string sensorId, double pressure, bool isOnline)
        {
            this.SensorId = sensorId;
            this.Pressure = pressure;
            this.IsOnline = isOnline;
        }
    }
}