class Vehicle {
    constructor(
        public model: number,
        public make: string,
        public engine_capacity: number
    ) { }

    Accelerate() {
        console.log(`this car accelerate with ${this.engine_capacity * 0.1} m/s`)

    }
    Brake() {
        console.log(`this car accelerate with ${this.engine_capacity * 0.1} m/s`)
    }
}


const Veh_A  = new Vehicle( 1992,'Honda',1500)
const Veh_B = new Vehicle( 2010,'Toyota',1300)
const Veh_C  = new Vehicle( 2020,'Skoda',1800)