public class Gadget
{
    public string Brand { get; set; }
    public double Price { get; set; }

    public Gadget(string brand, double price)
    {
        this.Brand = brand;
        this.Price = price;
    }

    public virtual void TurnOn()
    {
        Console.WriteLine("Powering on the generic gadget...");
    }
}