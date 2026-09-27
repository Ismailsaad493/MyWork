using System;
using System.Collections.Generic;
using System.Text;

namespace ___Task___
{
    public class Developer : Employee
    {
        public Developer(string name, int id)
            :base (name, id)
        {
        }

        public override void DoWork()
        {
            Console.WriteLine("Planning the next software release...");
        }
    }
}
