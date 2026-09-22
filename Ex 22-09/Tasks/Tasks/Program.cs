using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> employees = new Dictionary<int, string>();
            employees.Add(101, "Saim");
            employees.Add(102, "Saad");
            employees.Add(103, "Anfal");
            try
            {
                Console.WriteLine("Enter the Employee ID to look up:");
                int searchId = int.Parse(Console.ReadLine());

                Console.WriteLine("Employee Found:" + employees[searchId]);
            } 
            catch (FormatException)
            {
                Console.WriteLine("Error: Employee IDs only contain numbers!");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Error: That Employee ID does not exist in the system.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: System failure.");
            }
            Console.ReadLine();
        }
    }
}
