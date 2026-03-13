// I - Interface Segregation Principle (ISP)

// Bad Implementation Example:
interface IPrinter {
  print(): void;
  scan(): void;
  fax(): void;
}

class MultiFunctionPrinter implements IPrinter {
  print() { console.log("Printing document") }
  scan() { console.log("Scanning document") }
  fax() { console.log("Faxing document") }
}

class SimplePrinter implements IPrinter {
  print() { console.log("Printing document") }
  scan() { throw new Error("SimplePrinter cannot scan") }
  fax() { throw new Error("SimplePrinter cannot fax") }
}



// Actual Implementation
interface IAPrinter {
  print(): void;
}

interface IScanner {
  scan(): void;
}

class AMultiPrinter implements IAPrinter, IScanner {
  print() { console.log("Printing document") }
  scan() { console.log("Scanning document") }
}

class ASimplePrinter implements IAPrinter {
  print() { console.log("Printing document") }
}



