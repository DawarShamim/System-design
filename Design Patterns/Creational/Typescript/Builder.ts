// Instead of creating a complex object with single line or with too many parameters building it step by step
class Car {
    brand?: string
    model?: string
    sunroof?: boolean
    navigation?: boolean
    heatedSeats?: boolean

    describe() {
        console.log(this)
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

// instead of initializing with parameters
// const car =new Car("Toyota","SUV",true,true,false,true)

const car = new CarBuilder()
    .setBrand("Toyota")
    .setModel("SUV")
    .addSunroof()
    .addNavigation()
    .build()

car.describe()

const car2 = new CarBuilder()
    .setBrand("Toyota")
    .setModel("Sedan")
    .addHeatedSeats()
    .build()

car2.describe()
