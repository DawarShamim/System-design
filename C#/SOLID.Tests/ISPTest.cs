using System;
using Xunit;

// ============================================================
//  Tests for ISP.cs — Interface Segregation Principle
//  Add this file to: C#/SOLID.Tests/ISPTest.cs
//  Run with: dotnet test (from C#/ folder)
// ============================================================
[CollectionDefinition("Sequential", DisableParallelization = true)]
public class SequentialCollection { }

namespace ISP.Tests
{
  // ── 1. BAD implementation — proves WHY ISP matters ───────────
  // SimplePrinter is forced to implement Scan/Fax it doesn't support.
  // These tests document that behavior (so you never accidentally "fix" it).

  [Collection("Sequential")]
  public class BadImplementationTests
  {



    [Fact]
    public void SimplePrinter_Scan_ThrowsNotImplementedException()
    {
      // ISP violation: SimplePrinter is FORCED to implement Scan
      // but throws at runtime — this is exactly the problem ISP solves
      var printer = new SimplePrinter();
      Assert.Throws<NotImplementedException>(() => printer.Scan());
    }

    [Fact]
    public void SimplePrinter_Fax_ThrowsNotImplementedException()
    {
      // Same ISP violation for Fax
      var printer = new SimplePrinter();
      Assert.Throws<NotImplementedException>(() => printer.Fax());
    }


    [Fact]
    public void SimplePrinter_Print_Works()
    {
      var printer = new SimplePrinter();

      using var output = new System.IO.StringWriter();
      Console.SetOut(output);

      printer.Print();

      Assert.Contains("Printing", output.ToString());
    }


    [Fact]
    public void MultiFunctionPrinter_AllMethods_Work()
    {
      // MultiFunctionPrinter supports everything — no violations here
      var printer = new MultiFunctionPrinter();
      var output = new System.IO.StringWriter();
      Console.SetOut(output);

      printer.Scan();
      printer.Print();
      printer.Fax();

      var result = output.ToString();
      Assert.Contains("Scanning", result);
      Assert.Contains("Printing", result);
      Assert.Contains("Faxing", result);
      Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }
  }

  // ── 2. ASimplePrinter — ISP-compliant simple printer ─────────

  public class ASimplePrinterTests
  {


    [Fact]
    public void ASimplePrinter_ImplementsIAPrinter()
    {
      // ASimplePrinter must satisfy IAPrinter — nothing more
      var printer = new ASimplePrinter();
      Assert.IsAssignableFrom<IAPrinter>(printer);
    }

    [Fact]
    public void ASimplePrinter_DoesNotImplementIScanner()
    {
      // ISP compliance: ASimplePrinter should NOT be an IScanner
      var printer = new ASimplePrinter();
      Assert.False(printer is IScanner);
    }

    [Fact]
    public void ASimplePrinter_DoesNotImplementIFax()
    {
      // ISP compliance: ASimplePrinter should NOT be an IFax
      var printer = new ASimplePrinter();
      Assert.False(printer is IFax);
    }
  }

  // ── 3. MultiFuncPrinter — ISP-compliant multi-function printer ─

  public class MultiFuncPrinterTests
  {


    [Fact]
    public void Scan_WritesOutputToConsole()
    {
      var printer = new MultiFuncPrinter();
      var output = new System.IO.StringWriter();
      Console.SetOut(output);

      printer.Scan();
      
      printer.Print();

      Assert.Contains("Scanning", output.ToString());
      
      Assert.Contains("Printing", output.ToString());
      Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }

   

    [Fact]
    public void Fax_WritesOutputToConsole()
    {
      var printer = new MultiFuncPrinter();
      var output = new System.IO.StringWriter();
      Console.SetOut(output);

      printer.Fax();

      Assert.Contains("Faxing", output.ToString());
      Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }

    [Fact]
    public void MultiFuncPrinter_ImplementsAllInterfaces()
    {
      // MultiFuncPrinter must satisfy every segregated interface
      var printer = new MultiFuncPrinter();
      Assert.IsAssignableFrom<IAPrinter>(printer);
      Assert.IsAssignableFrom<IScanner>(printer);
      Assert.IsAssignableFrom<IFax>(printer);
      Assert.IsAssignableFrom<IMultiPrinter>(printer);
    }
  }

  // ── 4. ISP Compliance tests — the principle itself ────────────

  public class ISPComplianceTests
  {
    [Fact]
    public void IAPrinter_OnlyExposesPrint_NotScanOrFax()
    {
      // IAPrinter must have exactly 1 method: Print
      var methods = typeof(IAPrinter).GetMethods();
      Assert.Single(methods);
      Assert.Equal("Print", methods[0].Name);
    }

    [Fact]
    public void IScanner_OnlyExposeScan()
    {
      var methods = typeof(IScanner).GetMethods();
      Assert.Single(methods);
      Assert.Equal("Scan", methods[0].Name);
    }

    [Fact]
    public void IFax_OnlyExposesFax()
    {
      var methods = typeof(IFax).GetMethods();
      Assert.Single(methods);
      Assert.Equal("Fax", methods[0].Name);
    }

    [Fact]
    public void IMultiPrinter_InheritsAllThreeInterfaces()
    {
      // IMultiPrinter composes the three small interfaces
      var interfaces = typeof(IMultiPrinter).GetInterfaces();
      Assert.Contains(interfaces, i => i == typeof(IAPrinter));
      Assert.Contains(interfaces, i => i == typeof(IScanner));
      Assert.Contains(interfaces, i => i == typeof(IFax));
    }

    [Fact]
    public void ASimplePrinter_CanBeUsedWhereIAPrinterExpected()
    {
      // You can pass ASimplePrinter anywhere an IAPrinter is required
      IAPrinter printer = new ASimplePrinter();
      var output = new System.IO.StringWriter();
      Console.SetOut(output);

      printer.Print();

      Assert.Contains("Printing", output.ToString());
      Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }

    [Fact]
    public void MultiFuncPrinter_CanBeUsedWhereAnyInterfaceExpected()
    {
      // MultiFuncPrinter can substitute for any of the segregated interfaces
      IAPrinter asPrinter = new MultiFuncPrinter();
      IScanner asScanner = new MultiFuncPrinter();
      IFax asFax = new MultiFuncPrinter();
      IMultiPrinter asMulti = new MultiFuncPrinter();

      Assert.NotNull(asPrinter);
      Assert.NotNull(asScanner);
      Assert.NotNull(asFax);
      Assert.NotNull(asMulti);
    }
  }
}




