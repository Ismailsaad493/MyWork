using System;
using System.Collections.Generic;
using System.Text;

namespace oops
{
    public class ECar : Clar
    {
        public int BatteryLevel { get; set; }
        public void ShowBattery()
        {
            Console.WriteLine("BatteryLevel :" + BatteryLevel);
        }
}
}
