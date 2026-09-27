public class Phone : Gadget
{
    public Phone(string brand, double price) : base(brand, price)
    {
    }

    public override void TurnOn()
    {
        Console.WriteLine("Swiping up to unlock the phone screen...");
    }
}