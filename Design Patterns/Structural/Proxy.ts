//  lets you provide a substitute or placeholder for another object.

class BankAccount {
  withdraw(amount: number) {
    console.log(`Withdrawing $${amount} from bank account`)
  }
}

class ATMProxy {
  constructor(private account: BankAccount) {}

  withdraw(amount: number, pin: number) {
    if (pin !== 1234) {
      console.log("Invalid PIN")
      return
    }

    console.log("PIN verified")
    this.account.withdraw(amount)
  }
}

const account = new BankAccount()
const atm = new ATMProxy(account)

atm.withdraw(200, 1234)