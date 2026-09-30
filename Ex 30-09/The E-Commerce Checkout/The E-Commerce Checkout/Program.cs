internal class program
{
    static void Main(string[]args)
    {
        double cartTotal = 150.0;
        Func<double, double> applyDiscount = (price) => price * 0.9;

        Predicate<double> isEligibleForFreeShipping = (price) => price > 50.0;

        Console.WriteLine("--- Checkout Processing ---");
        Console.WriteLine("Original Price: $" + cartTotal);

        double finalPrice = applyDiscount(cartTotal);

        bool freeShipping = isEligibleForFreeShipping(finalPrice);


    }
}