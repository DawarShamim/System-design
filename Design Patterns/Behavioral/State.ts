interface IState {
    insertCoin(): void
    selectItem(): void
}


class NoCoinState implements IState {
    constructor(private machine: VendingMachine) { }

    insertCoin() {
        console.log("Coin inserted")
        this.machine.setState(this.machine.hasCoinState)
    }

    selectItem() { console.log("Insert coin first") }
}

class HasCoinState implements IState {
    constructor(private machine: VendingMachine) { }

    insertCoin() { console.log("Coin already inserted") }

    selectItem() {
        console.log("Dispensing item")
        this.machine.setState(this.machine.noCoinState)
    }
}


class VendingMachine {
    noCoinState: IState
    hasCoinState: IState
    private currentState: IState

    constructor() {
        this.noCoinState = new NoCoinState(this)
        this.hasCoinState = new HasCoinState(this)
        this.currentState = this.noCoinState
    }

    setState(state: IState) { this.currentState = state }
    insertCoin() { this.currentState.insertCoin() }
    selectItem() { this.currentState.selectItem() }
}

const machine = new VendingMachine()

machine.selectItem() // Insert coin first
machine.insertCoin() // Coin inserted
machine.selectItem() // Dispensing item