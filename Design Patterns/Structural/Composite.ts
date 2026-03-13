// use for treating objects or group of object the same way  
interface ISmartDevice {
    turnOn(): void
    turnOff(): void
}

class Device implements ISmartDevice {
    constructor(private name: string) { }
    turnOn() { console.log(this.name + " turned ON") }
    turnOff() { console.log(this.name + " turned OFF") }
}

class DeviceGroup implements ISmartDevice {
    private devices: ISmartDevice[] = []

    constructor(private name: string) { }

    add(device: ISmartDevice) { this.devices.push(device) }

    turnOn() {
        console.log("Turning ON group: " + this.name)
        this.devices.forEach(d => d.turnOn())
    }

    turnOff() {
        console.log("Turning OFF group: " + this.name)
        this.devices.forEach(d => d.turnOff())
    }
}

const livingRoom = new DeviceGroup("livingRoom")
const TV = new Device("TV")
const Cleaner = new Device("Cleaner")
const CoffeeMaker = new Device("CoffeeMaker")

livingRoom.add(TV)
livingRoom.add(Cleaner)
livingRoom.add(CoffeeMaker)
livingRoom.turnOff()
TV.turnOn()

