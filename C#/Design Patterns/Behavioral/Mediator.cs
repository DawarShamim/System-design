using System;
using System.Collections.Generic;

interface IAirTrafficMediator
{
  void RegisterAirplane(Airplane airplane);
  void SendMessage(string message, Airplane sender);
}

class AirTrafficControl : IAirTrafficMediator
{
  private List<Airplane> airplanes = new List<Airplane>();

  public void RegisterAirplane(Airplane airplane) { this.airplanes.Add(airplane); }

  public void SendMessage(string message, Airplane sender)
  {
    this.airplanes.ForEach(airplane => { if (airplane != sender) { airplane.Receive(message); } });
  }
}

class Airplane
{
  private readonly IAirTrafficMediator mediator;
  public readonly string name;

  public Airplane(IAirTrafficMediator mediator, string name)
  {
    this.mediator = mediator;
    this.name = name;
  }

  public void Send(string message)
  {
    Console.WriteLine($"{this.name} sends: {message}");
    this.mediator.SendMessage(message, this);
  }

  public void Receive(string message) { Console.WriteLine($"{this.name} receives: {message}"); }
}
class Program
{
  public static void Main(string[] args)
  {

    AirTrafficControl tower = new AirTrafficControl();

    Airplane plane1 = new Airplane(tower, "Plane A");
    Airplane plane2 = new Airplane(tower, "Plane B");
    Airplane plane3 = new Airplane(tower, "Plane C");

    tower.RegisterAirplane(plane1);
    tower.RegisterAirplane(plane2);
    tower.RegisterAirplane(plane3);

    plane1.Send("Requesting landing clearance");
  }
}