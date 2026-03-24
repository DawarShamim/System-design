using System;

abstract class SupportHandler
{
  protected SupportHandler nextHandler;

  public SupportHandler SetNext(SupportHandler handler)
  {
    this.nextHandler = handler;
    return handler;
  }
  public abstract void Handle(string request);
}

class Level1Support : SupportHandler
{
  public override void Handle(string request)
  {
    if (request == "password reset")
      Console.WriteLine("Level 1 resolved password reset");
    else if (this.nextHandler != null)
      this.nextHandler.Handle(request);
  }
}

class Level2Support : SupportHandler
{
  public override void Handle(string request)
  {
    if (request == "technical issue")
      Console.WriteLine("Level 2 resolved technical issue");
    else if (this.nextHandler != null)
      this.nextHandler.Handle(request);
  }
}
class Manager : SupportHandler
{
  public override void Handle(string request)
  {
    if (request == "refund")
      Console.WriteLine("Manager approved refund");
    else
      Console.WriteLine("Request cannot be handled");
  }
}
class Program
{
  public static void Main(string[] args)
  {
    Level1Support level1 = new Level1Support();
    Level2Support level2 = new Level2Support();
    Manager manager = new Manager();

    level1.SetNext(level2).SetNext(manager);

    level1.Handle("technical issue");  // Output: Level 2 resolved technical issue
    level1.Handle("refund");           // Output: Manager approved refund
  }
}