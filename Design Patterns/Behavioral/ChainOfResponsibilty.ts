abstract class SupportHandler {
  protected nextHandler?: SupportHandler

  setNext(handler: SupportHandler) {
    this.nextHandler = handler
    return handler
  }

  abstract handle(request: string): void
}


class Level1Support extends SupportHandler {
  handle(request: string) {
    if (request === "password reset") {
      console.log("Level 1 resolved password reset")
    } else if (this.nextHandler) {
      this.nextHandler.handle(request)
    }
  }
}

class Level2Support extends SupportHandler {
  handle(request: string) {
    if (request === "technical issue") {
      console.log("Level 2 resolved technical issue")
    } else if (this.nextHandler) {
      this.nextHandler.handle(request)
    }
  }
}
class Manager extends SupportHandler {
  handle(request: string) {
    if (request === "refund") {
      console.log("Manager approved refund")
    } else {
      console.log("Request cannot be handled")
    }
  }
}
const level1 = new Level1Support()
const level2 = new Level2Support()
const manager = new Manager()

level1.setNext(level2).setNext(manager)

level1.handle("technical issue")
level1.handle("refund")