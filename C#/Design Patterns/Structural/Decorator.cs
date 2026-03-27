using System;

namespace Decorator
{
  interface ICoffee
  {
    decimal Cost();
    string Description();
  }

  class SimpleCoffee : ICoffee
  {
    public decimal Cost() { return 3.0m; }
    public string Description()
    { return "Ingredients: Coffee Beans"; }
  }

  abstract class CoffeeDecorator : ICoffee
  {
    protected ICoffee coffee;

    public CoffeeDecorator(ICoffee coffee)
    { this.coffee = coffee; }

    public abstract decimal Cost();
    public abstract string Description();
  }

  class Latte : CoffeeDecorator
  {
    public Latte(ICoffee coffee) : base(coffee) { }

    public override decimal Cost()
    { return coffee.Cost() + 0.75m; }

    public override string Description()
    { return $"{coffee.Description()}, Milk"; }
  }

  class Americano : CoffeeDecorator
  {
    public Americano(ICoffee coffee) : base(coffee) { }

    public override decimal Cost()
    { return coffee.Cost() + 0.50m; }

    public override string Description()
    { return $"{coffee.Description()}, Water"; }
  }

  class Program
  {
    public static void Main(string[] args)
    {
      ICoffee coffee = new SimpleCoffee();
      Console.WriteLine($"{coffee.Description()} | Cost: {coffee.Cost()}");

      coffee = new Latte(coffee);
      Console.WriteLine($"{coffee.Description()} | Cost: {coffee.Cost()}");

      coffee = new Americano(coffee);
      Console.WriteLine($"{coffee.Description()} | Cost: {coffee.Cost()}");
    }
  }

}