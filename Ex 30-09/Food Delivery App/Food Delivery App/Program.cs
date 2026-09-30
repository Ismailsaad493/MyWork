using Food_Delivery_App;

internal class program
{
    static void Main(string[]args)
    {
        Restaurant myRestaurant = new Restaurant();

        myRestaurant.OnOrderReady += () =>
        {
            Console.WriteLine("Driver App: Starting the bike, heading to the restaurant!");
        };
        myRestaurant.OnOrderReady += () =>
        {
            Console.WriteLine("Driver App: Stant!");
        };
        myRestaurant.CookFood();

        Console.ReadLine();
    }
}