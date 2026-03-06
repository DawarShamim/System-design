// Instead of repeating all steps, we clone the existing object. 

class Car {
    brand?: string
    model?: string
    sunroof?: boolean
    navigation?: boolean
    heatedSeats?: boolean

    describe() {
        console.log(this)
    }

    clone(): Car {
        return Object.assign(new Car(), this)
    }
}

class CarBuilder {

    private car: Car

    constructor() {
        this.car = new Car()
    }

    setBrand(brand: string): CarBuilder {
        this.car.brand = brand
        return this
    }

    setModel(model: string): CarBuilder {
        this.car.model = model
        return this
    }

    addSunroof(): CarBuilder {
        this.car.sunroof = true
        return this
    }

    addNavigation(): CarBuilder {
        this.car.navigation = true
        return this
    }

    addHeatedSeats(): CarBuilder {
        this.car.heatedSeats = true
        return this
    }

    build(): Car {
        return this.car
    }
}


const car = new CarBuilder()
    .setBrand("Toyota")
    .setModel("SUV")
    .addSunroof()
    .addNavigation()
    .build()

car.describe()

const car3 = car.clone()
car3.brand= "Honda"
car3.describe()
