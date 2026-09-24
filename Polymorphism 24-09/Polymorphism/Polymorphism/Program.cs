/*using Polymorphism;

PaymentDetails paymentdetails = new PaymentDetails();

//Credit Card details
paymentdetails.MakePayment("1234-5678-0987", "Saad");

//debit Card details
paymentdetails.MakePayment("7655-1234-1234", "Saad", "1234");

//Cash details
paymentdetails.MakePayment(150.00);

Console.ReadLine(); */

using Polymorphism;

Payment payment1 = new CreditCardPayment(200.00, "Saad", "1234-5678-9123-4566");

Payment payment2 = new DebitCardPayment(150.00, "Saim", "4321-9876-9876-5464");

Payment payment3 = new CashPayment(100.00, "Hema");

Payment test = new Test(120.00, "Anfal");

payment1.ProcessPayment();
payment2.ProcessPayment();
payment3.ProcessPayment();
test.ProcessPayment();

Console.ReadLine();






