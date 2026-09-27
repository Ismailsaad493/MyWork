using Restaurant_Ordering_System;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Enter the table number for this order: ");
            int tableNumber = int.Parse(Console.ReadLine());

            List<MenuItem> customerOrder = new List<MenuItem>();

            customerOrder.Add(new Pizza("Margherita", 12.99));
            customerOrder.Add(new Drink("Cola", 2.50));

            Console.WriteLine("\n--- Serving Table " + tableNumber + " ---");

            foreach (MenuItem item in customerOrder)
            {
                Console.WriteLine("Item: " + item.Name + " | $" + item.Price);
                item.Serve();
                Console.WriteLine("-----------------------");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Please type a valid number for the table!");
        }

        Console.ReadLine();
    }
}
