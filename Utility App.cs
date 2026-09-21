using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {


            // Utility App


            Console.Write("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("Hello " + name + "!Welcome to the utility App");
            Console.WriteLine();

            int choice = 0;
            do
            {
                Console.WriteLine(" 1.Check Even or Odd");
                Console.WriteLine(" 2.Print Multiplication Table");
                Console.WriteLine(" 3.Print Star Pattern");
                Console.WriteLine(" 4.Exit");
                Console.Write("Choose an option: ");

                choice = int.Parse(Console.ReadLine());         

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter a Number");
                        int num1 = int.Parse(Console.ReadLine());

                        if (num1 % 2 == 0)
                        {
                            Console.WriteLine(num1 + " is Even ");
                        }
                        else
                        {
                            Console.WriteLine(num1 + " is odd number ");
                        }
                        break;
                    case 2:

                        Console.Write("Enter a TableNumber:");
                        int tableNum = int.Parse(Console.ReadLine());

                        for (int i = 1; i <= 10; i++)
                        {
                            Console.WriteLine(tableNum + " x " + i + " = " + (tableNum * i));
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter the Number Rows");
                        int rows = int.Parse(Console.ReadLine());
                        for (int i = 1; i <= rows; i++)
                        {
                            for (int j = 1; j <= i; j++)
                            {
                                Console.Write("*");
                            }
                            Console.WriteLine();
                        }
                        break;

                    case 4:
                        Console.WriteLine("Goodbye!" + name + "Thank you for using the utility App");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            } while (choice != 4);



            // Bank App


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
                    
                switch(choice)
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
