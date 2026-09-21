using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace The_Department_Portal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] subjects = new string[3];
            subjects[0] = "C#";
            subjects[1] = "Database";
            subjects[2] = "React";

            Console.WriteLine("--- Department Subjects ---");

            for (int i = 0; i < subjects.Length; i++)
            {
                Console.WriteLine(subjects[i]);
            }
            Console.WriteLine();

            Stack<string> notices = new Stack<string>();
            notices.Push("Fees dus Friday");
            notices.Push("Exam Tomorrow");

            Console.WriteLine("--- Notice Board ---");

            
            while (notices.Count > 0)
            {
                Console.WriteLine("Latest Notice: " + notices.Pop());
            }
            Console.WriteLine();

            Dictionary<int, int> marklist = new Dictionary<int, int>();
            {
                marklist.Add(101, 85);
                marklist.Add(102, 92);
                marklist.Add(103, 85);
            }

            Console.WriteLine("Enter Student Roll Number to Check marks: ");
            int searchRoll = int.Parse(Console.ReadLine());

            if (marklist.ContainsKey(searchRoll))
            {
                Console.WriteLine("Roll Number " + searchRoll + " scored " + marklist[searchRoll] + " marks.");
            }
            else
            {
                Console.WriteLine("Error: Roll Number not found in the system.");
            }

            Console.ReadLine();
        }
    }
}