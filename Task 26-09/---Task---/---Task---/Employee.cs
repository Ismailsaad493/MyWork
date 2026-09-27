using System;
using System.Collections.Generic;
using System.Text;

namespace ___Task___
{
    public class Employee
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public Employee(string name, int id)
        {
            this.Name = name;
            this.ID = id;
        }
        public virtual void DoWork()
        {
            Console.WriteLine("Doing generic office tasks...");
        }
    }
}
