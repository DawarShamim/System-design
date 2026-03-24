interface ICommand {
    execute(): void
    undo(): void
}

class Light {
    turnOn() { console.log("Light ON") }
    turnOff() { console.log("Light OFF") }
}
class LightOnCommand implements ICommand {
    constructor(private light: Light) { }

    execute() { this.light.turnOn() }
    undo() { this.light.turnOff() }
}

class LightOffCommand implements ICommand {
    constructor(private light: Light) { }

    execute() { this.light.turnOff() }
    undo() { this.light.turnOn() }
}

class RemoteControl {
    private history: ICommand[] = []

    pressButton(command: ICommand) {
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