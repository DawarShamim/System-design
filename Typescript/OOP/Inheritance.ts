class Car_INH {
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

class Sedan extends Car_INH {
    constructor(
        model: number,
        make: string,
        engine_capacity: number,
        public trunk_capacity: number
    ) {
        super(model, make, engine_capacity); // must call first
    }
    Describe() {
        console.log(`this sedan have a ${this.engine_capacity} Engine with ${this.trunk_capacity}L Trunk Capacity `)
    }
}

class SUV extends Car_INH {
    constructor(
        model: number,
        make: string,
        engine_capacity: number,
        public transmission:string
    ) {
        super(model, make, engine_capacity); // must call first
    }
    Describe() {
        console.log(`this sedan have a ${this.engine_capacity} Engine with ${this.transmission} `)
    }
}
