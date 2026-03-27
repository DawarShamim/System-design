using System;
using System.IO;
using Xunit;
using Moq;


namespace SRP.Tests
{
    // ── 1. Logger tests ───────────────────────────────────────────

    [Collection("Sequential")]
    public class LoggerTests
    {
        private readonly TextWriter _originalOut = Console.Out;

        [Fact]
        public void DebugLog_WritesMessageToConsole()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            new Logger().DebugLog("Test message");

            var result = output.ToString();
            Console.SetOut(_originalOut);
            Assert.Contains("Test message", result);
        }

        [Theory]
        [InlineData("Hello")]
        [InlineData("Sending email to user@test.com")]
        [InlineData("Error occurred")]
        public void DebugLog_VariousMessages_DoesNotThrow(string message)
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var ex = Record.Exception(() => new Logger().DebugLog(message));

            Console.SetOut(_originalOut);
            Assert.Null(ex);
        }

        [Fact]
        public void Logger_SingleResponsibility_OnlyHasLoggingMethod()
        {
            // SRP: Logger should only have one concern — logging
            var methods = typeof(Logger)
                .GetMethods(System.Reflection.BindingFlags.Public |
                            System.Reflection.BindingFlags.Instance |
                            System.Reflection.BindingFlags.DeclaredOnly);

            Assert.Single(methods);
            Assert.Equal("DebugLog", methods[0].Name);
        }
    }

    // ── 2. NotificationService tests ──────────────────────────────

    [Collection("Sequential")]
    public class NotificationServiceTests
    {
        private readonly TextWriter _originalOut = Console.Out;

        [Fact]
        public void SendEmail_WritesEmailAndMessageToConsole()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var service = new NotificationService(new Logger());
            service.SendEmail("user@test.com", "Welcome!");

            var result = output.ToString();
            Console.SetOut(_originalOut);
            Assert.Contains("user@test.com", result);
            Assert.Contains("Welcome!", result);
        }

        [Fact]
        public void SendEmail_LogsBeforeSending()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var service = new NotificationService(new Logger());
            service.SendEmail("user@test.com", "Hello");

            var result = output.ToString();
            Console.SetOut(_originalOut);

            // Logger output should appear before email sent output
            var logIndex  = result.IndexOf("Sending email");
            var sentIndex = result.IndexOf("Email sent");
            Assert.True(logIndex < sentIndex);
        }

        [Fact]
        public void SendEmail_DoesNotThrow()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var service = new NotificationService(new Logger());
            var ex = Record.Exception(() => service.SendEmail("a@b.com", "msg"));

            Console.SetOut(_originalOut);
            Assert.Null(ex);
        }

        [Fact]
        public void NotificationService_ImplementsINotificationService()
        {
            var service = new NotificationService(new Logger());
            Assert.IsAssignableFrom<INotificationService>(service);
        }

        [Fact]
        public void NotificationService_AcceptsLogger_ViaDependencyInjection()
        {
            // SRP: NotificationService doesn't create its own Logger —
            // logging is the Logger's responsibility, not NotificationService's
            var logger  = new Logger();
            var service = new NotificationService(logger);
            Assert.NotNull(service);
        }
    }

    // ── 3. UserService tests ──────────────────────────────────────

    [Collection("Sequential")]
    public class UserServiceTests
    {
        private readonly TextWriter _originalOut = Console.Out;

        [Fact]
        public void CreateUser_WritesUserCreatedToConsole()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var mockNotification = new Mock<INotificationService>();
            var userService = new UserService(mockNotification.Object);

            userService.CreateUser("Alice", "alice@test.com");

            var result = output.ToString();
            Console.SetOut(_originalOut);
            Assert.Contains("Alice", result);
        }

        [Fact]
        public void CreateUser_SendsWelcomeEmail_ViaNotificationService()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var mockNotification = new Mock<INotificationService>();
            var userService = new UserService(mockNotification.Object);

            userService.CreateUser("Alice", "alice@test.com");

            Console.SetOut(_originalOut);

            // SRP: UserService delegates email responsibility to INotificationService
            mockNotification.Verify(
                n => n.SendEmail("alice@test.com", "Welcome Alice"),
                Times.Once
            );
        }

        [Fact]
        public void CreateUser_SendsEmailWithCorrectWelcomeMessage()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var mockNotification = new Mock<INotificationService>();
            var userService = new UserService(mockNotification.Object);

            userService.CreateUser("Bob", "bob@test.com");

            Console.SetOut(_originalOut);
            mockNotification.Verify(
                n => n.SendEmail("bob@test.com", "Welcome Bob"),
                Times.Once
            );
        }

        [Fact]
        public void UpdateUser_DoesNotThrow()
        {
            var mockNotification = new Mock<INotificationService>();
            var userService = new UserService(mockNotification.Object);

            var ex = Record.Exception(() => userService.UpdateUser(1, "Alice"));
            Assert.Null(ex);
        }

        [Fact]
        public void DeleteUser_DoesNotThrow()
        {
            var mockNotification = new Mock<INotificationService>();
            var userService = new UserService(mockNotification.Object);

            var ex = Record.Exception(() => userService.DeleteUser(1));
            Assert.Null(ex);
        }

        [Fact]
        public void UpdateUser_DoesNotSendEmail()
        {
            var mockNotification = new Mock<INotificationService>();
            var userService = new UserService(mockNotification.Object);

            userService.UpdateUser(1, "Alice");

            // Updating a user should not trigger any notification
            mockNotification.Verify(
                n => n.SendEmail(It.IsAny<string>(), It.IsAny<string>()),
                Times.Never
            );
        }

        [Fact]
        public void DeleteUser_DoesNotSendEmail()
        {
            var mockNotification = new Mock<INotificationService>();
            var userService = new UserService(mockNotification.Object);

            userService.DeleteUser(1);

            mockNotification.Verify(
                n => n.SendEmail(It.IsAny<string>(), It.IsAny<string>()),
                Times.Never
            );
        }
    }

    // ── 4. SRP Compliance tests — the principle itself ────────────

    public class SRPComplianceTests
    {
        [Fact]
        public void UserService_DependsOnINotificationService_NotConcreteClass()
        {
            // SRP + DIP: UserService takes the abstraction, not NotificationService directly
            var ctorParams = typeof(UserService)
                .GetConstructors()[0]
                .GetParameters();

            Assert.Single(ctorParams);
            Assert.Equal(typeof(INotificationService), ctorParams[0].ParameterType);
        }

        [Fact]
        public void NotificationService_DependsOnLogger_ViaCtor()
        {
            // SRP: NotificationService doesn't own logging — Logger is injected
            var ctorParams = typeof(NotificationService)
                .GetConstructors()[0]
                .GetParameters();

            Assert.Single(ctorParams);
            Assert.Equal(typeof(Logger), ctorParams[0].ParameterType);
        }

        [Fact]
        public void UserService_CreateUser_OnlySendsOneEmail()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var mockNotification = new Mock<INotificationService>();
            var userService = new UserService(mockNotification.Object);

            userService.CreateUser("Charlie", "charlie@test.com");

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

            // Creating one user should send exactly one email — no more
            mockNotification.Verify(
                n => n.SendEmail(It.IsAny<string>(), It.IsAny<string>()),
                Times.Once
            );
        }
    }
}