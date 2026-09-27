public class Laptop : Gadget
{
    public Laptop(string brand, double price) : base(brand, price)
    {
    }

    public override void TurnOn()
    {
        Console.WriteLine("Booting up the desktop operating system...");
    }
}