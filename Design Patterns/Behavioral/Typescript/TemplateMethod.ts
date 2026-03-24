abstract class OrderProcessor {
    processOrder() {
        this.verifyOrder()
        this.processPayment()
        this.packItems()
        this.shipOrder()
        this.sendConfirmation()
    }

    verifyOrder() { console.log("Order verified") }

    abstract processPayment(): void
    abstract packItems(): void
    abstract shipOrder(): void

    sendConfirmation() { console.log("Confirmation email sent") }
}

class StandardOrder extends OrderProcessor {
    processPayment() { console.log("Processing credit card payment") }

    packItems() { console.log("Packing physical items") }

    shipOrder() { console.log("Shipping via standard delivery") }
}

class DigitalOrder extends OrderProcessor {
    processPayment() { console.log("Processing online payment") }

    packItems() { console.log("No packaging required") }

    shipOrder() { console.log("Sending download link") }
}

const order = new StandardOrder()
order.processOrder()