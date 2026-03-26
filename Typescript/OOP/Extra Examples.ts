// Abstraction + Inheritance + Encapsulation + Polymorphism

abstract class Vehicle {
    constructor(protected brand: string) {}

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
    // Function Overriding
    startEngine(): void {
        console.log(`${this.brand} bike started with self-start`);
    }

    changeBrand(brand :string ): void {
        this.brand =  brand
        console.log(`Changed to ${this.brand} `);
    }

}

// Usage
const myCar = new Sedan("Toyota", 450);
myCar.startEngine();
myCar.overhaulEngine(1300);
myCar.overhaulEngine(1300,1800);
myCar.OpenTrunk();

myCar.stopEngine();

const newBike = new Bike("United");
newBike.changeBrand("Honda")