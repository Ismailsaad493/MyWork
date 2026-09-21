using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionEx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Dictionary<int, string> employee = new Dictionary<int, string>();
            employee.Add(101, "Saad");
            employee.Add(102, "Anfal");
            employee.Add(103, "Saim");

            Console.WriteLine("Employee List");

            foreach(int id in employee.Keys)
            {
                Console.WriteLine("ID:" + id + " | Name: " + employee[id]);
            }

            Console.WriteLine();

            Console.WriteLine("Enter an Employee ID");
            int searchId = int.Parse(Console.ReadLine());

            if (employee.ContainsKey(searchId))
            {
                Console.WriteLine("Search Result: " + employee[searchId]);
            }
            else
            {
                Console.WriteLine("Error: Employee ID " + searchId + "not found.");
            }

            Console.ReadLine();*/

            /*List<string> roadmap = new List<string>();
            roadmap.Add("Variables");
            roadmap.Add("Loops");
            roadmap.Add("Collections");

            Console.WriteLine("--Current Roadmap--");

            foreach (string topic in roadmap)
            {
                Console.WriteLine("-" + topic);
            }

            Console.WriteLine();

            Console.Write("Enter the next topic to learn: ");
            string nextTopic = Console.ReadLine();

            roadmap.Add(nextTopic);

            Console.WriteLine();
            Console.WriteLine("Success! You now have " + roadmap.Count + " topics in your roadmap.");

            Console.ReadLine();*/

            /*Queue<string> supportTickets = new Queue<string>();
            
                supportTickets.Enqueue("Saad");
                supportTickets.Enqueue("Saad");
                supportTickets.Enqueue("Saad");

            Console.WriteLine("---Tech Support Queue----");

            Console.WriteLine("Next on line:" + supportTickets.Peek());

            Console.WriteLine("Serve the Customer: " + supportTickets.Dequeue());

            Console.WriteLine("There are " + supportTickets.Count + "People waiting");

            Console.ReadLine();*/

            Dictionary<string, int> inventory = new Dictionary<string, int>();


            inventory.Add("Laptop", 5);
            inventory.Add("Mouse", 6);
            inventory.Add("Keyboard", 7);

            Console.WriteLine("--- Warehouse Inventory ---");

            foreach (string item in inventory.Keys)
            {
                Console.WriteLine("Item: " + item + " | Stock: " + inventory[item]);
            }
            Console.WriteLine();

            Console.WriteLine("Which item would you like to buy?");
            string purchase = Console.ReadLine();


            if (inventory.ContainsKey(purchase))
            {
                inventory[purchase] = inventory[purchase] - 1;
                Console.WriteLine("Success! You bought a " + purchase + " . Remaining stock:" + inventory[purchase]);    
            }
            else
            {
                Console.WriteLine("Sorry, we don't carry that item.");
            }

            Console.ReadLine();







        }
    }
}
