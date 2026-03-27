using System;
using Xunit;
using Moq;

namespace OCP.Tests
{
    // ── 1. CreditCardPayment tests ────────────────────────────────

    [Collection("Sequential")]
    public class CreditCardPaymentTests
    {
        private readonly System.IO.TextWriter _originalOut = Console.Out;

        [Fact]
        public void Pay_WritesCorrectAmountAndMethod()
        {
            var output = new System.IO.StringWriter();
            Console.SetOut(output);

            new CreditCardPayment().Pay(100);

            var result = output.ToString();
            Console.SetOut(_originalOut);   // ← restore BEFORE assert (so failures don't leave Console broken)
            Assert.Contains("100", result);
            Assert.Contains("Credit Card", result);
        }

        [Fact]
        public void CreditCardPayment_ImplementsIPaymentMethod()
        {
            Assert.IsAssignableFrom<IPaymentMethod>(new CreditCardPayment());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(500)]
        [InlineData(9999)]
        public void Pay_VariousAmounts_DoesNotThrow(int amount)
        {
            var output = new System.IO.StringWriter();
            Console.SetOut(output);

            var ex = Record.Exception(() => new CreditCardPayment().Pay(amount));

            Console.SetOut(_originalOut);   // ← always restore
            Assert.Null(ex);
        }
    }
    // ── 2. PayPalPayment tests ────────────────────────────────────

    [Collection("Sequential")]
    public class PayPalPaymentTests
    {
        [Fact]
        public void Pay_WritesCorrectAmountAndMethod()
        {
            var payment = new PayPalPayment();
            using var output = new System.IO.StringWriter();
            Console.SetOut(output);

            payment.Pay(200);

            var result = output.ToString();
            Assert.Contains("200", result);
            Assert.Contains("PayPal", result);
        }

        [Fact]
        public void PayPalPayment_ImplementsIPaymentMethod()
        {
            Assert.IsAssignableFrom<IPaymentMethod>(new PayPalPayment());
        }
    }

    // ── 3. CryptoPayment tests ────────────────────────────────────

    [Collection("Sequential")]
    public class CryptoPaymentTests
    {
        [Fact]
        public void Pay_WritesCorrectAmountAndMethod()
        {
            var payment = new CryptoPayment();
            using var output = new System.IO.StringWriter();
            Console.SetOut(output);

            payment.Pay(300);

            var result = output.ToString();
            Assert.Contains("300", result);
            Assert.Contains("Crypto", result);
        }

        [Fact]
        public void CryptoPayment_ImplementsIPaymentMethod()
        {
            Assert.IsAssignableFrom<IPaymentMethod>(new CryptoPayment());
        }
    }

    // ── 4. ApplePayPayment tests (the OCP extension) ──────────────

    [Collection("Sequential")]
    public class ApplePayPaymentTests
    {
        [Fact]
        public void Pay_WritesCorrectAmountAndMethod()
        {
            var payment = new ApplePayPayment();
            using var output = new System.IO.StringWriter();
            Console.SetOut(output);

            payment.Pay(400);

            var result = output.ToString();
            Assert.Contains("400", result);
            Assert.Contains("Apple Pay", result);
        }

        [Fact]
        public void ApplePayPayment_ImplementsIPaymentMethod()
        {
            // OCP: new payment type added by extension, not modifying existing code
            Assert.IsAssignableFrom<IPaymentMethod>(new ApplePayPayment());
        }
    }

    // ── 5. PaymentProcessor tests ─────────────────────────────────

    public class PaymentProcessorTests
    {
        // Uses Moq — no Console involved, safe to run in parallel

        [Fact]
        public void Process_CallsPayOnce_WithCorrectAmount()
        {
            var mockPayment = new Mock<IPaymentMethod>();
            var processor = new PaymentProcessor();

            processor.Process(mockPayment.Object, 150);

            mockPayment.Verify(p => p.Pay(150), Times.Once);
        }

        [Fact]
        public void Process_CalledMultipleTimes_CallsPayEachTime()
        {
            var mockPayment = new Mock<IPaymentMethod>();
            var processor = new PaymentProcessor();

            processor.Process(mockPayment.Object, 100);
            processor.Process(mockPayment.Object, 200);
            processor.Process(mockPayment.Object, 300);

            mockPayment.Verify(p => p.Pay(It.IsAny<int>()), Times.Exactly(3));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(50)]
        [InlineData(1000)]
        public void Process_VariousAmounts_ForwardsExactAmountToPaymentMethod(int amount)
        {
            var mockPayment = new Mock<IPaymentMethod>();
            var processor = new PaymentProcessor();

            processor.Process(mockPayment.Object, amount);

            mockPayment.Verify(p => p.Pay(amount), Times.Once);
        }
    }

    // ── 6. OCP Compliance tests — the principle itself ────────────

    public class OCPComplianceTests
    {
        [Fact]
        public void AllPaymentTypes_ImplementIPaymentMethod()
        {
            // Every payment class satisfies the same interface —
            // PaymentProcessor never needs to change when new ones are added
            Assert.IsAssignableFrom<IPaymentMethod>(new CreditCardPayment());
            Assert.IsAssignableFrom<IPaymentMethod>(new PayPalPayment());
            Assert.IsAssignableFrom<IPaymentMethod>(new CryptoPayment());
            Assert.IsAssignableFrom<IPaymentMethod>(new ApplePayPayment());
        }

        [Fact]
        public void PaymentProcessor_WorksWithAnyIPaymentMethod_WithoutModification()
        {
            // OCP core: processor accepts any payment type through the abstraction
            // Adding ApplePayPayment required zero changes to PaymentProcessor
            var processor = new PaymentProcessor();
            IPaymentMethod[] allPayments =
            {
                new CreditCardPayment(),
                new PayPalPayment(),
                new CryptoPayment(),
                new ApplePayPayment()
            };

            foreach (var payment in allPayments)
            {
                var ex = Record.Exception(() => processor.Process(payment, 100));
                Assert.Null(ex);
            }
        }

        [Fact]
        public void IPaymentMethod_HasExactlyOneMethod_Pay()
        {
            // The interface contract is minimal and stable —
            // new payment types extend behaviour without touching this
            var methods = typeof(IPaymentMethod).GetMethods();
            Assert.Single(methods);
            Assert.Equal("Pay", methods[0].Name);
        }
    }
}