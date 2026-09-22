using System;

namespace SmartATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int balance = 1000;

            try
            {
                Console.WriteLine("How many freinds are splitting the 1000 rupees?");
                int friends = int.Parse(Console.ReadLine());
                int splitAmount = balance / friends;
                Console.WriteLine("Each person gets" + splitAmount + "rupees.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please type a number, not a word!");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: You cannot split money with 0 friends!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: System failure.");
            }
            Console.ReadLine();
        }
    }
}
