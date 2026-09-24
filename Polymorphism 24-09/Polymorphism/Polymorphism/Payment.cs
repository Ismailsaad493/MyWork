using System;
using System.Collections.Generic;
using System.Text;

namespace Polymorphism
{
    public class Payment
    {
        public double Amount { get; set; }
        public string payer { get; set; }

        public Payment(double amount, string payer)
        {
            this.Amount = amount;
            this.payer = payer;
        }

        public virtual void ProcessPayment()
        {
            Console.WriteLine($"Processing payment for '{payer}' in the general way");
            Console.WriteLine("-------------------------------------------------------------");
        }
    }
}
