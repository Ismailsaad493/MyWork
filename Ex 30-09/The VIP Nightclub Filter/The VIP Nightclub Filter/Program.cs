using System;
using System.Linq; 
using System.Collections.Generic;

internal class program
{
    static void Main(string[]args)
    {
        List<int> guestAges = new List<int>();
        guestAges.Add(16);
        guestAges.Add(22);
        guestAges.Add(19);
        guestAges.Add(17);
        guestAges.Add(30);
        guestAges.Add(25);

        Func<int, bool> isAdult = (age) => age >= 18;

        List<int> approveGuests = guestAges.Where(isAdult).ToList();

        Console.WriteLine("--- Approved VIP Guests ---");

        foreach(int age in approveGuests)
        {
            Console.WriteLine("Guest Age is " + age);
        }
        Console.ReadLine();
    }

}