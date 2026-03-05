// Abstraction 

abstract class Vehicle {
    constructor(public brand: string) {}

    // Abstract method (no implementation)
    abstract startEngine(): void;

    // Concrete method (has implementation)
    stopEngine() {
        console.log("Engine stopped");
    }
}

class Car extends Vehicle {
    startEngine(): void {
        console.log(`${this.brand} car engine started with key ignition`);
    }
}

class Bike extends Vehicle {
    startEngine(): void {
        console.log(`${this.brand} bike started with self-start`);
    }
}

// Usage
const myCar = new Car("Toyota");
myCar.startEngine();
myCar.stopEngine();