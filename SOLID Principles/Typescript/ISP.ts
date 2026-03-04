// I - Interface Segregation Principle (ISP)

// Bad Implementation Example:
interface Printer {
  print(): void;
  scan(): void;
  fax(): void;
}

class MultiFunctionPrinter implements Printer {
  print() { console.log("Printing document") }
  scan() { console.log("Scanning document") }
  fax() { console.log("Faxing document") }
}

class SimplePrinter implements Printer {
  print() { console.log("Printing document") }
  scan() { throw new Error("SimplePrinter cannot scan") }
  fax() { throw new Error("SimplePrinter cannot fax") }
}



// Actual Implementation
interface APrinter {
  print(): void;
}

interface Scanner {
  scan(): void;
}

class AMultiPrinter implements APrinter, Scanner {
  print() { }
  scan() { }
}

class ASimplePrinter implements APrinter {
  print() { }
}



