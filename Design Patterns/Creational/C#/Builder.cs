using System;

namespace Builder
{
  class House
  {
    public int Floors { get; set; }
    public int Rooms { get; set; }
    public int Windows { get; set; }
    public int Doors { get; set; }
    public string KitchenStyle { get; set; }

    public void ShowHouse()
    {
      Console.WriteLine($"Floors: {Floors}");
      Console.WriteLine($"Rooms: {Rooms}");
      Console.WriteLine($"Windows: {Windows}");
      Console.WriteLine($"Doors: {Doors}");
      Console.WriteLine($"Kitchen Style: {KitchenStyle}");
    }
  }

  class HouseBuilder
  {
    private readonly House _house = new House();

    public HouseBuilder AddFloors(int floors)
    {
      _house.Floors = floors;
      Console.WriteLine($"Added {floors} floors to the house");
      return this;
    }

    public HouseBuilder AddRooms(int rooms)
    {
      _house.Rooms = rooms;
      Console.WriteLine($"Added {rooms} floors to the house");

      return this;
    }

    public HouseBuilder AddWindows(int windows)
    {
      _house.Windows = windows;
      Console.WriteLine($"Added {windows} windows to the house");
      return this;
    }

    public HouseBuilder AddDoors(int doors)
    {
      _house.Doors = doors;
      Console.WriteLine($"Added {doors} doors to the house");
      return this;
    }

    public HouseBuilder SetKitchenStyle(string style)
    {
      _house.KitchenStyle = style;
      Console.WriteLine($"Created {style} styled Kitchen in the house");
      return this;
    }

    public House Build()
    {
      return _house;
    }
  }

  class Program
  {
    static void Main()
    {
      House house = new HouseBuilder()
          .AddFloors(2)
          .AddRooms(4)
          .AddWindows(6)
          .AddDoors(2)
          .SetKitchenStyle("Modern")
          .Build();

      house.ShowHouse();
    }
  }
}