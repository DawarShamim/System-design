// Abstraction + Inheritance

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
    constructor (
        brand: string,
    ){

        super(brand)
    }
    startEngine(): void {
        console.log(`${this.brand} car engine started with key ignition`);
    }
}

class Sedan  extends Car{
 constructor (
        brand: string,
        public trunk_space : number
    ){

        super(brand)
    }
    OpenTrunk(): void {
        console.log(`This Sedan has a trunk space of ${this.trunk_space}L`);
    }
}

class Bike extends Vehicle {
    startEngine(): void {
        console.log(`${this.brand} bike started with self-start`);
    }
}

// Usage
const myCar = new Sedan("Toyota", 450);
myCar.startEngine();
myCar.OpenTrunk();

myCar.stopEngine();