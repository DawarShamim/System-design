//Centralizes complex communication between objects.

interface IAirTrafficMediator {
    sendMessage(message: string, airplane: Airplane): void
}

class AirTrafficControl implements IAirTrafficMediator {
    private airplanes: Airplane[] = []

    registerAirplane(airplane: Airplane) { this.airplanes.push(airplane) }

    sendMessage(message: string, sender: Airplane) {
        for (const airplane of this.airplanes) {
            if (airplane !== sender) {
                airplane.receive(message)
            }
        }
    }
}


class Airplane {
    constructor(
        private mediator: IAirTrafficMediator,
        public name: string
    ) { }

    send(message: string) {
        console.log(`${this.name} sends: ${message}`)
        this.mediator.sendMessage(message, this)
    }

    receive(message: string) { console.log(`${this.name} receives: ${message}`) }
}

const tower = new AirTrafficControl()

const plane1 = new Airplane(tower, "Plane A")
const plane2 = new Airplane(tower, "Plane B")
const plane3 = new Airplane(tower, "Plane C")

tower.registerAirplane(plane1)
tower.registerAirplane(plane2)
tower.registerAirplane(plane3)

plane1.send("Requesting landing clearance")