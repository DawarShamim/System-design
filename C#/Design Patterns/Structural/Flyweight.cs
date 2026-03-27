using System;
using System.Collections.Generic;
// Flyweight (Shared Object)
namespace Flyweight
{
  class SeatType
  {
    public string SeatClass;
    public int Price;
    public int Baggage;
    public SeatType(string seatClass, int price, int baggage)
    {
      SeatClass = seatClass;
      Price = price;
      Baggage = baggage;
    }
  }
  // Flyweight Factory
  class SeatFactory
  {
    private static Dictionary<string, SeatType> seatTypes = new Dictionary<string, SeatType>();
    public static SeatType GetSeatType(string seatClass, int price, int baggage)
    {
      if (!seatTypes.ContainsKey(seatClass))
      { seatTypes[seatClass] = new SeatType(seatClass, price, baggage); }
      return seatTypes[seatClass];
    }
  }

  // Context (Contains extrinsic data)
  class Seat
  {
    public string SeatNumber;
    public string Passenger;
    public SeatType SeatType;
    public Seat(string seatNumber, string passenger, SeatType seatType)
    {
      SeatNumber = seatNumber;
      Passenger = passenger;
      SeatType = seatType;
    }

    public void Book(string passenger)
    {
      Passenger = passenger;
      Console.WriteLine($"{passenger} booked seat {SeatNumber} ({SeatType.SeatClass})");
    }
  }
  // Client
  class Program
  {
    public static void Main(string[] args)
    {
      var economy = SeatFactory.GetSeatType("Economy", 200, 20);
      var business = SeatFactory.GetSeatType("Business", 800, 40);
      Seat seat1 = new Seat("1A", null, business);
      Seat seat2 = new Seat("1B", null, business);
      Seat seat3 = new Seat("10A", null, economy);
      seat1.Book("Alice");
      seat2.Book("John");
      seat3.Book("Bob");
    }
  }
}