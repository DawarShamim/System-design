//Encapsulates algorithms so they can be swapped dynamically.
// Open/close Principle
using System;

interface IPaymentMethod
{
  void Pay(int amount);
}

class CreditCardPayment : IPaymentMethod
{
  public void Pay(int amount)
  {
    Console.WriteLine($"Paid {amount} using Credit Card");
  }
}

class PayPalPayment : IPaymentMethod
{
  public void Pay(int amount)
  {
    Console.WriteLine($"Paid {amount} using PayPal");
  }
}

class CryptoPayment : IPaymentMethod
{
  public void Pay(int amount)
  {
    Console.WriteLine($"Paid {amount} using Crypto");
  }
}


class ApplePayPayment : IPaymentMethod
{
  public void Pay(int amount)
  {
    Console.WriteLine($"Paid {amount} using Apple Pay");
  }
}

class PaymentProcessor
{
  public void Process(IPaymentMethod paymentMethod, int amount)
  {
    paymentMethod.Pay(amount);
  }
}

class Program_
{
  public static void Main(string[] args)
  {

    PaymentProcessor processor = new PaymentProcessor();
    processor.Process(new CreditCardPayment(), 200);
    processor.Process(new ApplePayPayment(), 200);

  }
}