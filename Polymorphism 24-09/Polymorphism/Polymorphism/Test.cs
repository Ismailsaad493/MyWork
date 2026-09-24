using System;
using System.Collections.Generic;
using System.Text;

namespace Polymorphism
{
    public class Test : Payment
    {
         public Test(double amount, string PayerName)
             : base(amount, PayerName)
         {
         }

        public void ProcessPayment()
        {
            Console.WriteLine($"Processing test payment for '{payer}' of '{Amount}'dollars.");
            Console.WriteLine("-------------------------------------------------------------");
        }
    }
}
