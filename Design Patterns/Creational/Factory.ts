class Vehicle {
    constructor(
        public model: number,
        public make: string,
        public engine_capacity: number,
        public wheels?: number

    ) { }

    Accelerate() {
        console.log(`this Vehicle accelerate with ${this.engine_capacity * 0.1} m/s`)

    }
    Brake() {
        console.log(`this Vehicle can brake`)
    }
    Description() {
        console.log(`this Vehicle have ${this.wheels}`)
    }
}

type VehicleConstructor = new (
    model: number,
    make: string,
    engine_capacity: number
) => Vehicle

class Car extends Vehicle {
    public wheels = 4;
    public trunkCapacity = 400 // liters

    openTrunk() {
        console.log(`Opening trunk with capacity ${this.trunkCapacity}L`)
    }

    turnOnAC() {
        console.log("Car AC turned on ❄️")
    }
}

class Bike extends Vehicle {
    public wheels = 2;
    public hasKickStart = true

    kickStart() {
        console.log("Bike started using kick-start")
    }

    helmetReminder() {
        console.log("⚠️ Please wear your helmet!")
    }
}

class Truck extends Vehicle {
    public wheels = 10;
    public cargoCapacity = 10000 // kg

    loadCargo(weight: number) {
        console.log(`Loading ${weight} kg of cargo`)
    }

    attachTrailer() {
        console.log("Trailer attached to truck")
    }
}
class VehicleFactory {

    private static registry: Map<string, VehicleConstructor> = new Map()

    static registerVehicle(type: string, constructor: VehicleConstructor) {
        this.registry.set(type, constructor)
    }

    static createVehicle(
        type: string,
        model: number,
        make: string,
        engine_capacity: number
    ): Vehicle {

        const VehicleClass = this.registry.get(type)

        if (!VehicleClass) {
            throw new Error("Vehicle type not registered")
        }

        return new VehicleClass(model, make, engine_capacity)
    }
}

VehicleFactory.registerVehicle("car", Car)

VehicleFactory.registerVehicle("bike", Bike)
VehicleFactory.registerVehicle("truck", Truck)


const Veh_A = VehicleFactory.createVehicle("car", 1992, "Toyota", 1500)
const Veh_B = VehicleFactory.createVehicle("bike", 2021, "Honda", 700)
const Veh_C = VehicleFactory.createVehicle("car", 2020, "Skoda", 1800)

Veh_A.Description()
Veh_B.Description()