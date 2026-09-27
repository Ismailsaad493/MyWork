using System;
using System.Collections.Generic;
using System.Text;

namespace ___Task___
{
    public class Manager : Employee
    {
        public Manager(string name, int id)
          : base(name, id)
        {
        }

        public override void DoWork()
        {
            Console.WriteLine("Planning the next software release...");
        }


        }
}
