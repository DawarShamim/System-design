using System;

namespace SRP
{
  // Bad Implementation Example
  class UserServiceBad
  {
    public static void CreateUser(string name)
    { Console.WriteLine("User created: " + name); }

    public void SendEmailNotification(string userEmail)
    {
      //  notification responsibility
    }
  }

  public interface INotificationService
  { void SendEmail(string email, string message); }

  public class Logger
  {
    public void DebugLog(string message)
    { Console.WriteLine(message); }
  }

  class NotificationService : INotificationService
  {
    private readonly Logger _logger;
    public NotificationService(Logger logger)
    { _logger = logger; }
    public void SendEmail(string email, string message)
    {
      _logger.DebugLog("Sending email to " + email);
      Console.WriteLine("Email sent to " + email + " : " + message);
    }
  }

  class UserService
  {
    private readonly INotificationService _notificationService;

    public UserService(INotificationService notificationService)
    { _notificationService = notificationService; }

    public void CreateUser(string name, string email)
    {
      Console.WriteLine("User " + name + " created");
      _notificationService.SendEmail(email, "Welcome " + name);
    }

    public void UpdateUser(int id, string name) { }

    public void DeleteUser(int id) { }
  }

  class Program
  {
    static void Main(string[] args)
    {
      var logger = new Logger();
      var notificationService = new NotificationService(logger);
      var userService = new UserService(notificationService);

      userService.CreateUser("Alice", "alice@example.com");
    }
  }
}