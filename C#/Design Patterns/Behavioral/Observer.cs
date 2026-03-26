using System;
using System.Collections.Generic;

interface IObserver
{
  void Update(object data);
}

interface ISubject
{
  void Attach(IObserver observer);
  void Detach(IObserver observer);
  void Notify();
}

class Subject : ISubject
{
  private List<IObserver> observers = new List<IObserver>();
  private string state = "";
  public void Attach(IObserver observer) { this.observers.Add(observer); }

  public void Detach(IObserver observer) { this.observers.RemoveAll(obs => obs != observer); }
  public void SetState(string state)
  {
    this.state = state;
    this.Notify();
  }
  public string GetState() { return this.state; }

  public void Notify()
  {
    foreach (var observer in this.observers)
      observer.Update(this.state);
  }
}

class Observer : IObserver
{
  private string name;

  public Observer(string name) { this.name = name; }

  public void Update(object data) { Console.WriteLine($"{this.name} received update: {data}"); }
}

class Program
{
  public static void Main(string[] args)
  {
    Subject subject = new Subject();
    Observer observer1 = new Observer("Observer 1");
    Observer observer2 = new Observer("Observer 2");

    subject.Attach(observer1);
    subject.Attach(observer2);
    subject.SetState("New State!");
  }
}