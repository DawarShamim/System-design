using System;

class RoomService
{
    public bool CheckAvailability(string roomType)
    {
        Console.WriteLine($"Checking availability for {roomType}");
        return true;
    }
}

class PaymentService
{
    public void ProcessPayment(int amount)
    {
        Console.WriteLine($"Processing payment of {amount}");
    }
}

class NotificationService
{
    public void SendConfirmation()
    {
        Console.WriteLine("Booking confirmation sent");
    }
}

class HotelBookingFacade
{
    private readonly RoomService _roomService = new RoomService();
    private readonly PaymentService _paymentService = new PaymentService();
    private readonly NotificationService _notificationService = new NotificationService();

    public void BookRoom(string roomType, int amount)
    {
        if (_roomService.CheckAvailability(roomType))
        {
            _paymentService.ProcessPayment(amount);
            _notificationService.SendConfirmation();
            Console.WriteLine("Room booked successfully");
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        HotelBookingFacade booking = new HotelBookingFacade();
        booking.BookRoom("Deluxe", 200);
    }
}