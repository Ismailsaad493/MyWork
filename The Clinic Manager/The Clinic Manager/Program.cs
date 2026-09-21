using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Clinic_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] doctors = new string[3];
            doctors[0] = "Dr. Ismail";
            doctors[0] = "Dr. Saim";
            doctors[0] = "Dr. Anfal";

            Console.WriteLine("---Available Doctors---");

            for(int i = 0; i < doctors.Length; i++)
            {
                Console.WriteLine(doctors[i]);
            }

            Queue<string> patients = new Queue<string>();
            patients.Enqueue("Zaid");
            patients.Enqueue("Tufeal");

            Console.WriteLine("---Waiting Room---");

            while ( patients.Count > 0)
            {
                Console.WriteLine("Now serving: " + patients.Dequeue());
            }
            Console.WriteLine();

            Dictionary<string, int> medicines = new Dictionary<string, int>();
            medicines.Add("Asprin", 10);
            medicines.Add("Dolo 650", 15);
            medicines.Add("Citrizin", 12);

            Console.WriteLine("---Pharmacy---");
            Console.WriteLine("Which medicine do you need?");
            string requestedmedicine = Console.ReadLine();

            if (medicines.ContainsKey(requestedmedicine))
            {
                Console.WriteLine("That will be " + medicines[requestedmedicine] + "dollars.");
            }
            else
            {
                Console.WriteLine("Sorry, that medicine is not available");
            }



            Console.ReadLine();
        }
    }
}
