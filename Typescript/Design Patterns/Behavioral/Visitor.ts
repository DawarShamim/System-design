interface IVisitor {
  visitBook(book: Book): void
  visitElectronics(electronics: Electronics): void
}

interface IProduct {
  accept(visitor: IVisitor): void
}

class Book implements IProduct {
  constructor(public price: number) {}

  accept(visitor: IVisitor) {
    visitor.visitBook(this)
  }
}
class Electronics implements IProduct {
  constructor(public price: number) {}

  accept(visitor: IVisitor) {
    visitor.visitElectronics(this)
  }
}

class TaxVisitor implements IVisitor {
  visitBook(book: Book) {
    const tax = book.price * 0.05
    console.log(`Book tax: $${tax}`)
  }

  visitElectronics(e: Electronics) {
    const tax = e.price * 0.18
    console.log(`Electronics tax: $${tax}`)
  }
}

const products: IProduct[] = [
  new Book(30),
  new Electronics(500)
]

const taxVisitor = new TaxVisitor()

for (const product of products) {
  product.accept(taxVisitor)
}