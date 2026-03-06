

class Car {
    constructor(
        public brand: string,
    ) {
    }
    startEngine(): void {
        console.log(`${this.brand} car engine started with key ignition`);
    }
    // function Overloading
      // overload signatures
    overhaulEngine(capacity: number): void;
    overhaulEngine(capacity: number, newCapacity: number): void;

    // single implementation
    overhaulEngine(capacity: number, newCapacity?: number): void {
        if (newCapacity) {
            console.log(`${this.brand} engine upgraded from ${capacity} to ${newCapacity}`);
        } else {
            console.log(`${this.brand} engine overhauled with capacity ${capacity}`);
        }
    }
}


class EV extends Car {
    constructor(
        brand: string,
    ) {
        super(brand)
    }
    // function overriding
    startEngine(): void {
        console.log(`${this.brand} car engine started with Push Button ignition`);
    }
}


class Conventional extends Car {
    constructor(
        brand: string,
    ) {
        super(brand)
    }
}



const EV_Tesla = new EV("TESLA")

EV_Tesla.startEngine()

const Conv_HONDA = new Car("HONDA")

Conv_HONDA.startEngine()
Conv_HONDA.overhaulEngine(1500)
Conv_HONDA.overhaulEngine(1500,1800)
