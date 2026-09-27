using ___Task___;
using System.ComponentModel;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Enter the project deadline in days:");
            int days = int.Parse(Console.ReadLine());

            List<Employee> companyTeam = new List<Employee>();
            companyTeam.Add(new Developer("Saad", 101));
            companyTeam.Add(new Manager("Saim", 102));

            Console.WriteLine("\n--- Team Status for " + days + "-Day Project ---");


            foreach (Employee worker in companyTeam)
            {
                Console.Write(worker.Name + " (ID: " + worker.ID + ") is ");
                worker.DoWork();
            }

        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Please type a valid number for the days!");
        }
        }
    }

