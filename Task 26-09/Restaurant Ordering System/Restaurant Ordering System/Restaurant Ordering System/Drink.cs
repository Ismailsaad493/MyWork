using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Ordering_System
{
    public class Drink : MenuItem
    {
        public Drink(string name, double price)
            : base(name, price)
        { }


        public override void Serve()
        {
            Console.WriteLine("Pouring the drink into a glass with ice...");
        }
    }
}
