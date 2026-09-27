using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Ordering_System
{
    public class Pizza : MenuItem
    {
        public Pizza(string name, double price)
            :base(name, price)
        { }

        public override void Serve()
        {
            Console.WriteLine("Slicing the pizza and serving it hot in a box...");
        }
    }
}
