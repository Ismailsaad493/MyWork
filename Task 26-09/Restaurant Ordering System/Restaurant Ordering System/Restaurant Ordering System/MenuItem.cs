using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Ordering_System
{
    public class MenuItem
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public MenuItem (string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }

        public virtual void Serve()
        {
            Console.WriteLine("Serving a generic item to the table");
        }

    }
}
