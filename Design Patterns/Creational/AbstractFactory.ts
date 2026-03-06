// Abstract Factory creates a whole family of Products i.e. DErived classes changes based on the parent factory selected

interface Button {
    render(): void
}

interface Checkbox {
    check(): void
}


class WindowsButton implements Button {
    render() {
        console.log("Rendering Windows Button")
    }
}

class WindowsCheckbox implements Checkbox {
    check() {
        console.log("Checking Windows Checkbox")
    }
}

class MacButton implements Button {
    render() {
        console.log("Rendering Mac Button")
    }
}

class MacCheckbox implements Checkbox {
    check() {
        console.log("Checking Mac Checkbox")
    }
}

interface GUIFactory {
    createButton(): Button
    createCheckbox(): Checkbox
}

class WindowsFactory implements GUIFactory {

    createButton(): Button {
        return new WindowsButton()
    }

    createCheckbox(): Checkbox {
        return new WindowsCheckbox()
    }
}

class MacFactory implements GUIFactory {

    createButton(): Button {
        return new MacButton()
    }

    createCheckbox(): Checkbox {
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