//Encapsulates algorithms so they can be swapped dynamically.
// Open/close Principle
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


class ApplePayPayment implements PaymentMethod {
    pay(amount: number): void {
        console.log(`Paid ${amount} using Apple Pay`);
    }
}

class PaymentProcessor {
  process(paymentMethod: PaymentMethod, amount: number): void {
    paymentMethod.pay(amount);
  }
}



const processor = new PaymentProcessor();
processor.process(new CreditCardPayment(), 200);
processor.process(new ApplePayPayment(), 200);