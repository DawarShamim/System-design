// L - Liskov Substitution Principle (LSP)

// Bad Implementation Example:
namespace BAD_LSP
{
    class Bird
    {
        public virtual void Fly()
        {
            Console.WriteLine("Bird is flying");
        }
    }

    class Duck : Bird
    {
        public override void Fly()
        {
            Console.WriteLine("Duck is flying");
        }
    }

    class Penguin : Bird
    {
        public override void Fly()
        {
            throw new Exception("Penguins cannot fly");
        }
    }

    class BirdActions
    {
        public static void MakeBirdFly(Bird bird)
        {
            bird.Fly();
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            BirdActions.MakeBirdFly(new Duck());

            // This compiles but breaks at runtime
            // BirdActions.MakeBirdFly(new Penguin());
        }
    }
}


// Actual Implementation
namespace LSP
{
  interface IBird { }

  interface IFlyingBird : IBird
  {
    void Fly();
  }

  interface IWalkingBird : IBird
  {
    void Walk();
  }

  class Pigeon : IFlyingBird, IWalkingBird
  {
    public void Fly()
    {
      Console.WriteLine("Pigeon is flying");
    }

    public void Walk()
    {
      Console.WriteLine("Pigeon is walking");
    }
  }

  class Kiwi : IWalkingBird
  {
    public override void Walk() { Console.WriteLine("Kiwi is walking"); }
  }

  class BirdActions
  {
    public static void MakeFly(IFlyingBird bird)
    {
      bird.Fly();
    }

    public static void MakeWalk(IWalkingBird bird)
    {
      bird.Walk();
    }
  }
  class Program
  {
    public static void Main(string[] args)
    {
      var kiwi = new Kiwi();
      var pigeon = new Pigeon();

      BirdActions.MakeFly(pigeon);   // Pigeon flies
      BirdActions.MakeWalk(pigeon);  // Pigeon walks
      BirdActions.MakeWalk(kiwi);    // Kiwi walks
    }
  }

}