using System;
using System.Collections.Generic;
interface ICommand
{
  void Execute();
  void Undo();
}

class Light
{
  public void TurnOn() { Console.WriteLine("Light ON"); }
  public void TurnOff() { Console.WriteLine("Light OFF"); }
}
class LightOnCommand : ICommand
{
  private readonly Light light;

  public LightOnCommand(Light light)
  {
    this.light = light;
  }


  public void Execute() { this.light.TurnOn(); }
  public void Undo() { this.light.TurnOff(); }
}

class LightOffCommand : ICommand
{
  private readonly Light light;

  public LightOffCommand(Light light)
  {
    this.light = light;
  }

  public void Execute() { this.light.TurnOff(); }
  public void Undo() { this.light.TurnOn(); }
}

class RemoteControl
{
  private List<ICommand> history = new List<ICommand>();

  public void PressButton(ICommand command)
  {
    command.Execute();
    this.history.Add(command);
  }

  public void Undo()
  {
    if (this.history.Count == 0) return;

    var command = this.history[this.history.Count - 1];
    this.history.RemoveAt(this.history.Count - 1);

    if (command != null) { command.Undo(); }
  }
}
class Program
{
  public static void Main(string[] args)
  {

    Light light = new Light();

    LightOnCommand lightOn = new LightOnCommand(light);
    LightOffCommand lightOff = new LightOffCommand(light);

    RemoteControl remote = new RemoteControl();

    remote.PressButton(lightOn);
    remote.PressButton(lightOff);

    remote.Undo();
    remote.Undo();
  }
}