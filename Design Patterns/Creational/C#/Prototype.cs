using System;
class House
{
    public int Floors { get; set; }
    public int Rooms { get; set; }
    public int Doors { get; set; }
    public int Windows { get; set; }
    public string KitchenStyle { get; set; }
    public void Describe()
    {
        Console.WriteLine($"Floors: {Floors}");
        Console.WriteLine($"Rooms: {Rooms}");
        Console.WriteLine($"Doors: {Doors}");
        Console.WriteLine($"Windows: {Windows}");
        Console.WriteLine($"Kitchen Style: {KitchenStyle}");
    }

    public House Clone() { return (House)this.MemberwiseClone(); }
}

class HouseBuilder
{
    private readonly House _house = new();
    public HouseBuilder AddRooms(int rooms)
    {
        _house.Rooms = rooms;
        return this;
    }

    public HouseBuilder AddFloors(int floors)
    {
        _house.Floors = floors;
        return this;
    }

    public HouseBuilder AddDoors(int doors)
    {
        _house.Doors = doors;
        return this;
    }

    public HouseBuilder AddWindows(int windows)
    {
        _house.Windows = windows;
        return this;
    }

    public HouseBuilder CreateKitchen(string kitchen)
    {
        _house.KitchenStyle = kitchen;
        return this;
    }

    public House BuildHouse() { return _house; }
}

class Program
{
    static void Main()
    {
        House house = new HouseBuilder()
            .AddFloors(2)
            .AddRooms(5)
            .AddDoors(3)
            .AddWindows(8)
            .CreateKitchen("Modern")
            .BuildHouse();

        house.Describe();
        House house2 = house.Clone();
        house2.Doors = 2;
        house2.Describe();


    }
}