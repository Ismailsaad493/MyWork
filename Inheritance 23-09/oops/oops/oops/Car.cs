using System;
using System.Collections.Generic;
using System.Text;

namespace oops
{
    public class Car : Vechicle
    {
        public string Model;

        public void ShowModel()
        {
            Console.WriteLine("Model :" + Model);

            Console.WriteLine("---------------------------");

        }
    }
}
