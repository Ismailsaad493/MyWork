using System.Diagnostics.CodeAnalysis;

namespace ConsoleApplication

{
    class Program
    {
        static void Main(string[] args)
        {

            /* string name;
            char Section;
            int number;
            double percentage;

            name = "John Doe";
            Section = 'A';
            number = 42;
            percentage = 85.5;

            Console.WriteLine("Name:" + name);
            Console.WriteLine("Section:" + Section);
            Console.WriteLine("Number:" + number);
            Console.WriteLine("percentage:" + percentage);
            Console.ReadLine();

            string firstName;
            Console.Write("Enter your Name :");
            firstName = Console.ReadLine();

            char Sec;
            Console.Write("Enter your Section :");
            Section = Convert.ToChar(Console.ReadLine());

            double perc;
            Console.Write("Enter your percentage :");
            percentage = Convert.ToDouble(Console.ReadLine());

            int num;
            Console.Write("Enter your number :");
            number = int.Parse(Console.ReadLine()); */



            /*int mark1, mark2, sum;
            
            Console.WriteLine("Enter Your mark1 : ");
            mark1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Your mark2 : ");
            mark2 = int.Parse(Console.ReadLine());

            sum = mark1 + mark2;
            Console.Write();
            Console.Write"Sum: " + sum);
            Console.ReadLine();

            int difference;
            difference = mark1 - mark2;
            Console.Write();
            Console.Write("Difference: " + difference);
            Console.ReadLine();

            int product;
            product = mark1 * mark2;
            Console.WriteLine();
            Console.Write("Product: " + product);
            Console.ReadLine();

            int quotient;
            quotient = mark1 / mark2;
            Console.WriteLine();
            Console.Write("Quotient: " + quotient);
            Console.ReadLine();

            int remainder;
            remainder = mark2 % mark1;
            Console.WriteLine();
            Console.Write("Remainder: " + remainder);
            Console.ReadLine(); */


            /*int mark1, mark2, sum;

            Console.Write("Enter Your mark1 : ");
            mark1 = int.Parse(Console.ReadLine());

            Console.Write("Enter Your mark2 : ");
            mark2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Mark1 is equal to Mark2 : " + (mark1 == mark2));
            Console.ReadLine();

            Console.WriteLine("Mark1 is not equal toMark2 : " + (mark1 != mark2));
            Console.ReadLine();

            Console.WriteLine("Mark1 is less than Mark2 : " + (mark1 < mark2));
            Console.ReadLine(); 

            Console.WriteLine("Mark1 is less than equal to Mark2 : " + (mark1 <= mark2));
            Console.ReadLine();

            Console.WriteLine("Mark1 is greater than Mark2 : " + (mark1 > mark2));
            Console.ReadLine();

            Console.WriteLine("Mark1 is greater than or equal to Mark2 : " + (mark1 >= mark2));
            Console.ReadLine(); */



            /*int age;
            Console.Write("Enter Your Age : ");
            age = int.Parse(Console.ReadLine());
           

            if(age >= 18)
            {

                Console.WriteLine("You are eligible to vote");
            }
            else
            {
                Console.WriteLine("You are not eligible to vote");
            }
            Console.ReadLine();*/


            int marks;
            Console.WriteLine("Enter Your Marks : ");
            marks = int.Parse(Console.ReadLine());

            if (marks >= 90)
            {
                Console.WriteLine("Grade A");
            }
            else if (marks >= 75 && marks < 90)
            {
                Console.WriteLine("Grade B");

            }
            else if (marks >= 60 && marks < 75)
            {
                Console.WriteLine("Grade C");
            }
            else if (marks >= 40 && marks < 60)
            {
                Console.WriteLine("Grade D");
            }
            else
            {
                Console.WriteLine("Fail");
            }




            }
    }
}