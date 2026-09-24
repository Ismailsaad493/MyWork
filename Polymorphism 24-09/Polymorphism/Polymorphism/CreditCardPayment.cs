using System;
using System.Collections.Generic;
using System.Text;

namespace Polymorphism
{
    public class CreditCardPayment : Payment
    {
        public string CardNumber { get; set; }
        public CreditCardPayment(double amount, string payer, string CardNumber)
             :base(amount, payer)
        {
            this.CardNumber = CardNumber;
        }

        public override void ProcessPayment()
        {

            Console.WriteLine($"Processing credit card payment for '{payer}' of '{Amount}' dollars using card '{CardNumber}'.");
            Console.WriteLine("-------------------------------------------------------------");
        }
    }
}
