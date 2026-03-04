// O - Open/Closed Principle (OCP)
// Bad Implementation Example:
using System;
namespace OCP
{

  public class PayProcessor
  {
    public void Process(string PaymentMethod, int amount)
    {
      switch (PaymentMethod)
      {
        case "Credit Card":
          Console.WriteLine($"Paid {amount} using Credit Card");
          break;
        case "Apple Pay":
          Console.WriteLine($"Paid {amount} using Apple Pay");
          break;
        case "Paypal":
          Console.WriteLine($"Paid {amount} using Paypal");
          break;
        default:
          Console.WriteLine("Invalid Payment Method");
          break;
      }
    }
  }

  // Actual Implementation Example:

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

  class PaymentProcessor
  {
    public void Process(IPaymentMethod PaymentMethod, int amount)
    {
      PaymentMethod.Pay(amount);
    }
  }

  //  Modified: Extension 
  class ApplePayPayment : IPaymentMethod
  {
    public void Pay(int amount)
    {
      Console.WriteLine($"Paid {amount} using Apple Pay");
    }
  }


  class Program
  {
    static void Main(string[] args)
    {
      var processor = new PaymentProcessor();

      processor.Process(new CreditCardPayment(), 200);
      processor.Process(new ApplePayPayment(), 150);
      processor.Process(new CryptoPayment(), 500);
    }
  }

}