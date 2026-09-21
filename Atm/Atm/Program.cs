using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double balance = 1000.00;
            int choice = 0;

            Console.WriteLine("Welcome to C# bank!\n");

            do
            {
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Exit");
                Console.WriteLine("5. Choose an option");

                choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Your Current Balance is: $" + balance);
                        break;

                    case 2:
                        Console.Write("Enter Your Amount to Deposit:");
                        double depositAmount = double.Parse(Console.ReadLine());

                        balance += depositAmount;
                        Console.WriteLine("Deposit Successful. Your new balance is: $" + balance);
                        break;

                    case 3:
                        Console.WriteLine("Enter your Amount to Withdraw:");
                        double withdrawAmount = double.Parse(Console.ReadLine());

                        if (withdrawAmount > balance)
                        {
                            Console.WriteLine("Error, Insuficient balance" + balance);
                        }
                        else
                        {
                            Console.WriteLine("Success, Withdrawn Succesfull");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Thank you for using C# bank. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;



                }



            } while (choice != 4);
        }
    }
}
        
