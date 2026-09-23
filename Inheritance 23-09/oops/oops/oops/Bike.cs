using System;
using System.Collections.Generic;
using System.Text;

namespace oops
{
    public class Bike : Vechile
    {
        public string Type { get; set; }
        public void ShowType()
        {
            Console.WriteLine("Bike Type :" + Type);
        }
    }
}
