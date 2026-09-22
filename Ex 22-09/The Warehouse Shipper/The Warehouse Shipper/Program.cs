using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Warehouse_Shipper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> inventory = new List<String>();
            inventory.Add("Laptop");
            inventory.Add("Monitor");
            inventory.Add("Keyboard");

            int totalStock = 100;

            try
            {
                Console.WriteLine("Enter the inventory index(0,1, or 2)to ship:");
                int itemIndex = int.Parse(Console.ReadLine());

                string selectedItem = inventory[itemIndex];

                Console.WriteLine("How many boxes are we splitting the 100 items into?");

                int boxes = int.Parse(Console.ReadLine());
                int perbox = totalStock / boxes;

                Console.WriteLine("Success! Shipping " + perbox + " " + selectedItem + " in each box.");
            }

            catch (FormatException)
            {
                Console.WriteLine("Error: Please type numbers only!");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Error: That inventory index does not exist!");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: You cannot ship itema in 0 boxes!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: System failure!");
            }

            Console.ReadLine();











        }

    }
}
