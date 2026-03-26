import { PayProcessor, PaymentProcessor, CreditCardPayment, PayPalPayment, CryptoPayment, ApplePayPayment } from "../OCP";
import type { IPaymentMethod } from "../OCP";
// ─── Bad Implementation Tests ───────────────────────────────────────────────

describe("PayProcessor (Bad Implementation - OCP Violation)", () => {
    let processor: PayProcessor;
    let consoleSpy: jest.SpyInstance;

    beforeEach(() => {
        processor = new PayProcessor();
        consoleSpy = jest.spyOn(console, "log").mockImplementation();
    });

    afterEach(() => {
        consoleSpy.mockRestore();
    });

    it("should process Credit Card payment", () => {
        processor.process("Credit Card", 100);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 100 using Credit Card");
    });

    it("should process Apple Pay payment", () => {
        processor.process("Apple Pay", 50);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 50 using Apple Pay");
    });

    it("should process Paypal payment", () => {
        processor.process("Paypal", 75);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 75 using Paypal");
    });

    it("should handle invalid payment method", () => {
        processor.process("Bitcoin", 200);
        expect(consoleSpy).toHaveBeenCalledWith("Invalid Payment Method");
    });

    it("should handle empty string as invalid payment method", () => {
        processor.process("", 100);
        expect(consoleSpy).toHaveBeenCalledWith("Invalid Payment Method");
    });
});


// ─── Good Implementation Tests (OCP Compliant) ──────────────────────────────

describe("CreditCardPayment", () => {
    let consoleSpy: jest.SpyInstance;

    beforeEach(() => {
        consoleSpy = jest.spyOn(console, "log").mockImplementation();
    });

    afterEach(() => {
        consoleSpy.mockRestore();
    });

    it("should log correct message when paying with Credit Card", () => {
        const payment = new CreditCardPayment();
        payment.pay(200);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 200 using Credit Card");
    });

    it("should handle zero amount", () => {
        const payment = new CreditCardPayment();
        payment.pay(0);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 0 using Credit Card");
    });
});


describe("PayPalPayment", () => {
    let consoleSpy: jest.SpyInstance;

    beforeEach(() => {
        consoleSpy = jest.spyOn(console, "log").mockImplementation();
    });

    afterEach(() => {
        consoleSpy.mockRestore();
    });

    it("should log correct message when paying with PayPal", () => {
        const payment = new PayPalPayment();
        payment.pay(150);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 150 using PayPal");
    });
});


describe("CryptoPayment", () => {
    let consoleSpy: jest.SpyInstance;

    beforeEach(() => {
        consoleSpy = jest.spyOn(console, "log").mockImplementation();
    });

    afterEach(() => {
        consoleSpy.mockRestore();
    });

    it("should log correct message when paying with Crypto", () => {
        const payment = new CryptoPayment();
        payment.pay(500);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 500 using Crypto");
    });
});


describe("ApplePayPayment (Extension)", () => {
    let consoleSpy: jest.SpyInstance;

    beforeEach(() => {
        consoleSpy = jest.spyOn(console, "log").mockImplementation();
    });

    afterEach(() => {
        consoleSpy.mockRestore();
    });

    it("should log correct message when paying with Apple Pay", () => {
        const payment = new ApplePayPayment();
        payment.pay(300);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 300 using Apple Pay");
    });
});


describe("PaymentProcessor", () => {
    let processor: PaymentProcessor;
    let consoleSpy: jest.SpyInstance;

    beforeEach(() => {
        processor = new PaymentProcessor();
        consoleSpy = jest.spyOn(console, "log").mockImplementation();
    });

    afterEach(() => {
        consoleSpy.mockRestore();
    });

    it("should delegate payment to CreditCardPayment", () => {
        processor.process(new CreditCardPayment(), 200);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 200 using Credit Card");
    });

    it("should delegate payment to PayPalPayment", () => {
        processor.process(new PayPalPayment(), 99);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 99 using PayPal");
    });

    it("should delegate payment to CryptoPayment", () => {
        processor.process(new CryptoPayment(), 1000);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 1000 using Crypto");
    });

    it("should delegate payment to ApplePayPayment", () => {
        processor.process(new ApplePayPayment(), 200);
        expect(consoleSpy).toHaveBeenCalledWith("Paid 200 using Apple Pay");
    });

    it("should work with any custom IPaymentMethod implementation (mock)", () => {
        const mockPayment: IPaymentMethod = { pay: jest.fn() };
        processor.process(mockPayment, 450);
        expect(mockPayment.pay).toHaveBeenCalledWith(450);
    });

    it("should call pay exactly once per process call", () => {
        const mockPayment: IPaymentMethod = { pay: jest.fn() };
        processor.process(mockPayment, 100);
        expect(mockPayment.pay).toHaveBeenCalledTimes(1);
    });
});