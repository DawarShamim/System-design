// lets you attach new behaviors to objects by placing these objects inside
// special wrapper objects that contain the behaviors

interface ICoffee {
    cost(): number
    description(): string
}

// Base component
class SimpleCoffee implements ICoffee {
    cost() { return 2 }
    description() { return "Coffee" }
}

// Decorators
abstract class CoffeeDecorator implements ICoffee {
    constructor(protected coffee: ICoffee) {}
    abstract cost(): number
    abstract description(): string
}

// Concrete decorators
class Milk extends CoffeeDecorator {
    cost() { return this.coffee.cost() + 0.5 }
    description() { return this.coffee.description() + ", Milk" }
}

class Sugar extends CoffeeDecorator {
    cost() { return this.coffee.cost() + 0.25 }
    description() { return this.coffee.description() + ", Sugar" }
}

class Vanilla extends CoffeeDecorator {
    cost() { return this.coffee.cost() + 0.75 }
    description() { return this.coffee.description() + ", Vanilla" }
}

let coffee: ICoffee = new SimpleCoffee()
console.log(coffee.description(), `$${coffee.cost()}`)

coffee = new Milk(coffee)
console.log(coffee.description(), `$${coffee.cost()}`)

coffee = new Sugar(coffee)
coffee = new Vanilla(coffee)
console.log(coffee.description(), `$${coffee.cost()}`)
