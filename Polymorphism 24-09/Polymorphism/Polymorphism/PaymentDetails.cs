using System;
using System.Collections.Generic;
using System.Text;

namespace Polymorphism
{
    public class PaymentDetails
    {
        public void MakePayment(string cardnumber, string cardholderName)
        {
            Console.WriteLine($"Proccessing credit payment for'{cardholderName}' using card no '{cardnumber}'.");
            Console.WriteLine("-----------------------------------------------");
        }

        public void MakePayment(string cardnumber, string cardholderName, string pin)
        {
            Console.WriteLine($"Proccessing debit card payment for'{cardholderName}' using card no '{cardnumber}'.");
            Console.WriteLine("-----------------------------------------------");


        }

        public void MakePayment(double cashAmount)
        {
            Console.WriteLine($"Proccessing cash payment for'{cashAmount}' dollars.");
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
