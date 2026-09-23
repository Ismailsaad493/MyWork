using System;
using System.Collections.Generic;
using System.Text;

namespace oops
{
    public class Vechile
    {
        public int speed { get; set; }
        public void ShowSpeed()
        {
            Console.WriteLine("Speed :" + speed);

            Console.WriteLine("---------------------");
        }
    }
}
