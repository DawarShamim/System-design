class Vehicle {
    constructor(protected engine: string, public brand: string) {}

    changeEngine(engine :string ): void {
        this.engine =  engine
        console.log(`Changed to ${this.engine} `);
    }

    viewEngine(): void {
        console.log(`this car has a ${this.engine} Engine `);
    }
}


const new_Vehicle = new Vehicle('1500cc',"Toyota")
//engine can only be view or modified outside class via specific function 
console.log(new_Vehicle)
new_Vehicle.viewEngine()
new_Vehicle.changeEngine( "1800cc")

//brand can be modified directly since it is not encapsulated
new_Vehicle.brand = "Honda"
console.log(new_Vehicle)