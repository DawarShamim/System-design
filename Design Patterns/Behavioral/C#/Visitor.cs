using System;
using System.Collections.Generic;
interface IVisitor
{
  void VisitBook(Book book);
  void VisitElectronics(Electronics electronics);
}
interface IProduct
{
  void Accept(IVisitor visitor);
}
class Book : IProduct
{
  public int price;
  public Book(int price) { this.price = price; }

  public void Accept(IVisitor visitor) { visitor.VisitBook(this); }
}
class Electronics : IProduct
{
  public int price;
  public Electronics(int price)
  { this.price = price; }

  public void Accept(IVisitor visitor) { visitor.VisitElectronics(this); }
}

class TaxVisitor : IVisitor
{
  public void VisitBook(Book book)
  {
    var tax = book.price * 0.05;
    Console.WriteLine($"Book tax: ${tax}");
  }

  public void VisitElectronics(Electronics e)
  {
    var tax = e.price * 0.18;
    Console.WriteLine($"Electronics tax: ${tax}");
  }
}

class Program
{
  public static void Main(string[] args)
  {
    List<IProduct> products = new List<IProduct>
        {
            new Book(30),
            new Electronics(500)
        };

    TaxVisitor taxVisitor = new TaxVisitor();
    products.ForEach(product => product.Accept(taxVisitor));
  }
}