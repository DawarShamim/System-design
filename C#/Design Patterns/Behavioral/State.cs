using System;

interface IState
{
    void InsertCoin();
    void SelectItem();
}

class NoCoinState : IState
{
    private readonly VendingMachine Machine;

    public NoCoinState(VendingMachine Machine)
    {
        this.Machine = Machine;
    }

    public void InsertCoin()
    {
        Console.WriteLine("Coin inserted");
        this.Machine.SetState(this.Machine.hasCoinState);
    }

    public void SelectItem()
    {
        Console.WriteLine("Insert coin first");
    }
}

class HasCoinState : IState
{
    private readonly VendingMachine Machine;

    public HasCoinState(VendingMachine Machine)
    {
        this.Machine = Machine;
    }

    public void InsertCoin()
    {
        Console.WriteLine("Coin already inserted");
    }

    public void SelectItem()
    {
        Console.WriteLine("Dispensing item");
        this.Machine.SetState(this.Machine.noCoinState);
    }
}

class VendingMachine
{
    public IState noCoinState;
    public IState hasCoinState;
    private IState currentState;

    public VendingMachine()
    {
        this.noCoinState = new NoCoinState(this);
        this.hasCoinState = new HasCoinState(this);
        this.currentState = this.noCoinState;
    }

    public void SetState(IState state) { this.currentState = state; }
    public void InsertCoin() { this.currentState.InsertCoin(); }
    public void SelectItem() { this.currentState.SelectItem(); }
}

class Program
{
    public static void Main(string[] args)
    {
        VendingMachine machine = new VendingMachine();
        machine.SelectItem();  // Output: Insert coin first
        machine.InsertCoin();  // Output: Coin inserted
        machine.SelectItem();  // Output: Dispensing item
    }
}