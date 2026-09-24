using System;
using System.Collections.Generic;
using System.Text;

namespace Polymorphism
{
    public class CashPayment : Payment
    {
        public CashPayment(double amount, string PayerName)
             : base(amount, PayerName)
        {     
        }

        public override void ProcessPayment()
        {
            Console.WriteLine($"Processing cash payment for '{payer}' of '{Amount}' dollars. ");
            Console.WriteLine("-------------------------------------------------------------");
        }
    }
}
