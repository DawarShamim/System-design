// O - Open/Closed Principle (OCP)
// Bad Implementation Example:
class PayProcessor {
  process(paymentMethod: string, amount: number): void {
    switch (paymentMethod) {
      case "Credit Card":
        console.log(`Paid ${amount} using Credit Card`);
        break;
      case "Apple Pay":
        console.log(`Paid ${amount} using Apple Pay`);
        break;
      case "Paypal":
        console.log(`Paid ${amount} using Paypal`);
        break;
      default:
        console.log(`Invalid Payment Method`);
    }
  }
}

// Example usage:
const BadProcessor = new PayProcessor();
BadProcessor.process("Credit Card", 100); // Paid 100 using Credit Card
BadProcessor.process("Apple Pay", 50);    // Paid 50 using Apple Pay
BadProcessor.process("Bitcoin", 200);     // Invalid Payment Method


// Actual Implementation Example:

interface PaymentMethod {
  pay(amount: number): void;
}

class CreditCardPayment implements PaymentMethod {
  pay(amount: number): void {
    console.log(`Paid ${amount} using Credit Card`);
  }
}

class PayPalPayment implements PaymentMethod {
  pay(amount: number): void {
    console.log(`Paid ${amount} using PayPal`);
  }
}

class CryptoPayment implements PaymentMethod {
  pay(amount: number): void {
    console.log(`Paid ${amount} using Crypto`);
  }
}

class PaymentProcessor {
  process(paymentMethod: PaymentMethod, amount: number): void {
    paymentMethod.pay(amount);
  }
}

//  Modified: Extension 
class ApplePayPayment implements PaymentMethod {
  pay(amount: number): void {
    console.log(`Paid ${amount} using Apple Pay`);
  }
}




const processor = new PaymentProcessor();
processor.process(new CreditCardPayment(), 200);
processor.process(new ApplePayPayment(), 200);





