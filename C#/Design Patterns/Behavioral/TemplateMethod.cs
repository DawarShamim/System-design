using System;
abstract class OrderProcessor
{
    public void ProcessOrder()
    {
        this.VerifyOrder();
        this.ProcessPayment();
        this.PackItems();
        this.ShipOrder();
        this.SendConfirmation();
    }

    protected void VerifyOrder() { Console.WriteLine("Order verified"); }

    protected abstract void ProcessPayment();
    protected abstract void PackItems();
    protected abstract void ShipOrder();
    protected void SendConfirmation() { Console.WriteLine("Confirmation email sent"); }
}

class StandardOrder : OrderProcessor
{
    protected override void ProcessPayment() { Console.WriteLine("Processing credit card payment"); }
    protected override void PackItems() { Console.WriteLine("Packing physical items"); }
    protected override void ShipOrder() { Console.WriteLine("Shipping via standard delivery"); }
}

class DigitalOrder : OrderProcessor
{
    protected override void ProcessPayment() { Console.WriteLine("Processing online payment"); }
    protected override void PackItems() { Console.WriteLine("No packaging required"); }
    protected override void ShipOrder() { Console.WriteLine("Sending download link"); }
}

class Program
{
    public static void Main(string[] args)
    {
        StandardOrder order = new StandardOrder();
        order.ProcessOrder();
    }
}