using System;
using System.IO;
using Proxy;
using Xunit;


namespace DesignPatterns.Proxy.Tests
{
    // ── 1. BankAccount tests ──────────────────────────────────────

    [Collection("Sequential")]
    public class BankAccountTests
    {
        private readonly TextWriter _originalOut = Console.Out;

        [Fact]
        public void Withdraw_WritesAmountToConsole()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            new BankAccount().Withdraw(200);

            var result = output.ToString();
            Console.SetOut(_originalOut);
            Assert.Contains("200", result);
        }

        [Fact]
        public void Withdraw_DoesNotThrow()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var ex = Record.Exception(() => new BankAccount().Withdraw(100));

            Console.SetOut(_originalOut);
            Assert.Null(ex);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(9999)]
        public void Withdraw_VariousAmounts_DoesNotThrow(int amount)
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var ex = Record.Exception(() => new BankAccount().Withdraw(amount));

            Console.SetOut(_originalOut);
            Assert.Null(ex);
        }
    }

    // ── 2. ATMProxy — valid PIN tests ─────────────────────────────

    [Collection("Sequential")]
    public class ATMProxyValidPinTests
    {
        private readonly TextWriter _originalOut = Console.Out;

        [Fact]
        public void Withdraw_WithCorrectPin_WritesVerifiedToConsole()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var atm = new ATMProxy(new BankAccount());
            atm.Withdraw(200, 1234);

            var result = output.ToString();
            Console.SetOut(_originalOut);
            Assert.Contains("PIN verified", result);
        }

        [Fact]
        public void Withdraw_WithCorrectPin_CallsBankAccountWithdraw()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var atm = new ATMProxy(new BankAccount());
            atm.Withdraw(300, 1234);

            var result = output.ToString();
            Console.SetOut(_originalOut);
            Assert.Contains("300", result);
        }

        [Fact]
        public void Withdraw_WithCorrectPin_DoesNotThrow()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var atm = new ATMProxy(new BankAccount());
            var ex = Record.Exception(() => atm.Withdraw(100, 1234));

            Console.SetOut(_originalOut);
            Assert.Null(ex);
        }

        [Fact]
        public void Withdraw_WithCorrectPin_PinVerifiedAppearsBeforeWithdrawal()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var atm = new ATMProxy(new BankAccount());
            atm.Withdraw(200, 1234);

            var result = output.ToString();
            Console.SetOut(_originalOut);

            // Proxy verifies PIN first, then delegates to real object
            var pinIndex      = result.IndexOf("PIN verified");
            var withdrawIndex = result.IndexOf("200");
            Assert.True(pinIndex < withdrawIndex);
        }
    }

    // ── 3. ATMProxy — invalid PIN tests ──────────────────────────

    [Collection("Sequential")]
    public class ATMProxyInvalidPinTests
    {
        private readonly TextWriter _originalOut = Console.Out;

        [Fact]
        public void Withdraw_WithWrongPin_WritesInvalidPinToConsole()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var atm = new ATMProxy(new BankAccount());
            atm.Withdraw(200, 9999);

            var result = output.ToString();
            Console.SetOut(_originalOut);
            Assert.Contains("Invalid PIN", result);
        }

        [Fact]
        public void Withdraw_WithWrongPin_DoesNotCallBankAccountWithdraw()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var atm = new ATMProxy(new BankAccount());
            atm.Withdraw(200, 9999);

            var result = output.ToString();
            Console.SetOut(_originalOut);

            // BankAccount.Withdraw should never be reached with wrong PIN
            Assert.DoesNotContain("Withdrawing", result);
        }

        [Fact]
        public void Withdraw_WithWrongPin_DoesNotThrow()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var atm = new ATMProxy(new BankAccount());
            var ex = Record.Exception(() => atm.Withdraw(200, 0000));

            Console.SetOut(_originalOut);
            Assert.Null(ex);
        }

        [Theory]
        [InlineData(0000)]
        [InlineData(1111)]
        [InlineData(9999)]
        [InlineData(-1)]
        public void Withdraw_VariousWrongPins_NeverCallsBankAccount(int wrongPin)
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var atm = new ATMProxy(new BankAccount());
            atm.Withdraw(200, wrongPin);

            var result = output.ToString();
            Console.SetOut(_originalOut);
            Assert.DoesNotContain("Withdrawing", result);
        }
    }

    // ── 4. Proxy Pattern Compliance tests ─────────────────────────

    public class ProxyComplianceTests
    {
        [Fact]
        public void ATMProxy_ReceivesBankAccount_ViaDependencyInjection()
        {
            // Proxy wraps the real object — injected, not created internally
            var ctorParams = typeof(ATMProxy)
                .GetConstructors()[0]
                .GetParameters();

            Assert.Single(ctorParams);
            Assert.Equal(typeof(BankAccount), ctorParams[0].ParameterType);
        }

        [Fact]
        public void ATMProxy_HasWithdrawMethod()
        {
            // Proxy exposes the same operation as the real subject
            var method = typeof(ATMProxy).GetMethod("Withdraw");
            Assert.NotNull(method);
        }

        [Fact]
        public void ATMProxy_WithdrawMethod_HasAmountAndPinParameters()
        {
            // Proxy adds extra behaviour (PIN check) on top of the real operation
            var parameters = typeof(ATMProxy)
                .GetMethod("Withdraw")!
                .GetParameters();

            Assert.Equal(2, parameters.Length);
            Assert.Equal("amount", parameters[0].Name);
            Assert.Equal("pin",    parameters[1].Name);
        }

        [Fact]
        public void BankAccount_WithdrawMethod_OnlyHasAmountParameter()
        {
            // Real subject has no PIN awareness — that's the proxy's job
            var parameters = typeof(BankAccount)
                .GetMethod("Withdraw")!
                .GetParameters();

            Assert.Single(parameters);
            Assert.Equal("amount", parameters[0].Name);
        }
    }
}