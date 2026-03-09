interface Command {
    execute(): void
    undo(): void
}

class Light {
    turnOn() { console.log("Light ON") }
    turnOff() { console.log("Light OFF") }
}
class LightOnCommand implements Command {
    constructor(private light: Light) { }

    execute() { this.light.turnOn() }
    undo() { this.light.turnOff() }
}

class LightOffCommand implements Command {
    constructor(private light: Light) { }

    execute() { this.light.turnOff() }
    undo() { this.light.turnOn() }
}

class RemoteControl {
    private history: Command[] = []

    pressButton(command: Command) {
        command.execute()
        this.history.push(command)
    }

    undo() {
        const command = this.history.pop()
        if (command) { command.undo() }
    }
}

const light = new Light()

const lightOn = new LightOnCommand(light)
const lightOff = new LightOffCommand(light)

const remote = new RemoteControl()

remote.pressButton(lightOn)
remote.pressButton(lightOff)

remote.undo()
remote.undo() 