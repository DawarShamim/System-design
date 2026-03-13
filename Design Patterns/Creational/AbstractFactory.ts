// Abstract Factory creates a whole family of Products i.e. DErived classes changes based on the parent factory selected

interface IButton {
    render(): void
}

interface ICheckbox {
    check(): void
}


class WindowsButton implements IButton {
    render() {
        console.log("Rendering Windows Button")
    }
}

class WindowsCheckbox implements ICheckbox {
    check() {
        console.log("Checking Windows Checkbox")
    }
}

class MacButton implements IButton {
    render() {
        console.log("Rendering Mac Button")
    }
}

class MacCheckbox implements ICheckbox {
    check() {
        console.log("Checking Mac Checkbox")
    }
}

interface GUIFactory {
    createButton(): IButton
    createCheckbox(): ICheckbox
}

class WindowsFactory implements GUIFactory {

    createButton(): IButton {
        return new WindowsButton()
    }

    createCheckbox(): ICheckbox {
        return new WindowsCheckbox()
    }
}

class MacFactory implements GUIFactory {

    createButton(): IButton {
        return new MacButton()
    }

    createCheckbox(): ICheckbox {
        return new MacCheckbox()
    }
}


function createUI(factory: GUIFactory) {

    const button = factory.createButton()
    const checkbox = factory.createCheckbox()

    button.render()
    checkbox.check()
}

const windowsUI = new WindowsFactory()
createUI(windowsUI)

const macUI = new MacFactory()
createUI(macUI)