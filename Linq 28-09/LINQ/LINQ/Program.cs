using LINQ;
using System.Linq;

List<Product> warehouse = new List<Product>();
warehouse.Add(new Product("Laptop", 1200.00));
warehouse.Add(new Product("Mouse", 50.00));
warehouse.Add(new Product("Keyboard", 400.00));
warehouse.Add(new Product("Monitor", 7200.00));

List<Product> expensiveItems = warehouse.Where(p => p.Price > 100).ToList();

Console.WriteLine("--- Expensive Items (Over $100) ---");

foreach (Product item in expensiveItems) 
{
    Console.WriteLine(item.Name + " | $" + item.Price);
}

Console.ReadLine();