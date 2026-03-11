// D - Dependency Inversion Principle (DIP)

// Bad Implementation Example:

using System;

namespace DIP
{
    // Bad Implementation Example:
    class EmailServiceBad
    {
        public void SendMessage(string msg)
        {
            Console.WriteLine($"Email sent: {msg}");
        }
    }

    class NotificationBad
    {
        private readonly EmailServiceBad _emailService = new EmailServiceBad();

        public void Notify(string msg)
        { _emailService.SendMessage(msg); }
    }

    // Actual Implementation

    // Abstraction
    interface IMessageService
    { void SendMessage(string msg); }

    // Concrete implementation
    class EmailService : IMessageService
    {
        public void SendMessage(string msg)
        { Console.WriteLine($"Email sent: {msg}"); }
    }

    // High-level module depends on abstraction
    class Notification
    {
        private readonly IMessageService _service;

        public Notification(IMessageService service)
        { _service = service; }

        public void Notify(string msg)
        { _service.SendMessage(msg); }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IMessageService emailService = new EmailService();
            Notification notification = new Notification(emailService);
            notification.Notify("Hello");
        }
    }


}