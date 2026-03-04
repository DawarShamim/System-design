using System;

namespace ISP
{
  // BAD IMPLEMENTATION
  interface IPrinter
  {
    void Print();
    void Scan();
    void Fax();
  }

  class MultiFunctionPrinter : IPrinter
  {
    public void Print() { Console.WriteLine("Printing document"); }
    public void Scan() { Console.WriteLine("Scanning document"); }
    public void Fax() { Console.WriteLine("Faxing document"); }
  }

  class SimplePrinter : IPrinter
  {
    public void Print() { Console.WriteLine("Printing document"); }

    public void Scan()
    {
      throw new NotImplementedException("SimplePrinter cannot scan");
    }

    public void Fax()
    {
      throw new NotImplementedException("SimplePrinter cannot fax");
    }
  }

  // Actual IMPLEMENTATION (ISP Applied)

  interface IAPrinter
  {
    void Print();
  }

  interface IScanner
  {
    void Scan();
  }

  interface IFax
  {
    void Fax();
  }


  interface IMultiPrinter : IAPrinter, IScanner, IFax { }

  public class MultiFuncPrinter : IMultiPrinter
  {
    public void Print()
    {
      Console.WriteLine("Printing document");
    }

    public void Scan()
    {
      Console.WriteLine("Scanning document");
    }

    public void Fax()
    {
      Console.WriteLine("Faxing document");
    }
  }

  public class ASimplePrinter : IAPrinter
  {
    public void Print()
    {
      Console.WriteLine("Printing document");
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      IAPrinter simple = new ASimplePrinter();
      simple.Print();

      IMultiPrinter multi = new MultiFuncPrinter();
      multi.Fax();
    }
  }
}