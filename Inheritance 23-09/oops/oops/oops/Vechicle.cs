using System;
using System.Collections.Generic;
using System.Text;

namespace oops
{
    public class Vechicle
    {
        public int speed { get; set; }

        public void showSpeed()
        {
            Console.WriteLine("Speed :" + speed);

            Console.WriteLine("---------------------------");
        }
    }
}
