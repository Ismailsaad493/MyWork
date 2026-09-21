using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exception_21_09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* List<string> fruits = new List<string> { "Apple", "Banana", "Cherry" };

             try
             {
                 string fruit = fruits[5];

                 Console.WriteLine("Fruit: " + fruit);
             }
             catch (ArgumentOutOfRangeException exception)
             {
                 Console.WriteLine("Safety Net Caught It: " + exception.Message);
             }

             Console.ReadLine(); 

            //format Exception

            try
            {
                int number;
                Console.Write("Enter a Number : ");
                number = int.Parse(Console.ReadLine());

                Console.WriteLine("Number : " + number);

            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ReadLine(); */

            try
            {

                Console.WriteLine("ENter the firstnumber:");
                int num1 = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the second number:");
                int num2 = int.Parse(Console.ReadLine());

                int result = num1 / num2;

                Console.WriteLine("Success! The resuth is:" + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Something went wrong. Did you type a word instead of a number?");
            }

            Console.ReadLine();
        }

    }
}