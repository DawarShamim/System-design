//  lets you provide a substitute or placeholder for another object.
using System;
namespace Proxy
{
  public class BankAccount
  {
    public void Withdraw(int amount)
    {
      Console.WriteLine($"Withdrawing $${amount} from bank account");
    }
  }

  public class ATMProxy
  {
    private readonly BankAccount _account;
    public ATMProxy(BankAccount account)
    { _account = account; }
    public void Withdraw(int amount, int pin)
    {
      if (pin != 1234)
      {
        Console.WriteLine("Invalid PIN");
        return;
      }

      Console.WriteLine("PIN verified");
      _account.Withdraw(amount);
    }
  }
  class Program
  {
    public static void Main(string[] args)
    {

      var account = new BankAccount();
      var atm = new ATMProxy(account);

      atm.Withdraw(200, 1234);
    }
  }
};