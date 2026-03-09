interface Visitor {
  visitBook(book: Book): void
  visitElectronics(electronics: Electronics): void
}

interface Product {
  accept(visitor: Visitor): void
}

class Book implements Product {
  constructor(public price: number) {}

  accept(visitor: Visitor) {
    visitor.visitBook(this)
  }
}
class Electronics implements Product {
  constructor(public price: number) {}

  accept(visitor: Visitor) {
    visitor.visitElectronics(this)
  }
}

class TaxVisitor implements Visitor {
  visitBook(book: Book) {
    const tax = book.price * 0.05
    console.log(`Book tax: $${tax}`)
  }

  visitElectronics(e: Electronics) {
    const tax = e.price * 0.18
    console.log(`Electronics tax: $${tax}`)
  }
}

const products: Product[] = [
  new Book(30),
  new Electronics(500)
]

const taxVisitor = new TaxVisitor()

for (const product of products) {
  product.accept(taxVisitor)
}