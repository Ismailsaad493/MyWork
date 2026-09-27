

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Gadget> shoppingCart = new List<Gadget>();

            try
            {
                Console.Write("Enter your maximum budget: ");
                double budget = double.Parse(Console.ReadLine());

                shoppingCart.Add(new Phone("Apple", 999.99));
                shoppingCart.Add(new Laptop("Dell", 1200.50));

                Console.WriteLine("\n--- Processing Cart ---");

                foreach (Gadget item in shoppingCart)
                {
                    Console.WriteLine("Item: " + item.brand + " | Price: $" + item.Price);
                    item.TurnOn();
                    Console.WriteLine("-----------------------");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid number for your budget!");
            }

            Console.ReadLine();
        }
    }
}