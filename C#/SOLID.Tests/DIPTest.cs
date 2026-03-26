using Xunit;
using Moq;
using System;

// ============================================================
//  Tests for DIP.cs — Dependency Inversion Principle
//  Test project: C#/SOLID.Tests
//  Run with:     dotnet test  (from C#/ folder)
// ============================================================

namespace DIP.Tests
{
    // ── 1. IMessageService contract tests ────────────────────────
    // Any class that implements IMessageService must honour SendMessage.
    // We verify this with a Mock so tests never rely on Console output.

    public class IMessageServiceContractTests
    {
        [Fact]
        public void SendMessage_IsCalled_WhenNotifyInvoked()
        {
            // Arrange
            var mockService = new Mock<IMessageService>();
            var notification = new Notification(mockService.Object);

            // Act
            notification.Notify("Hello");

            // Assert — SendMessage was called exactly once with the correct message
            mockService.Verify(s => s.SendMessage("Hello"), Times.Once);
        }

        [Fact]
        public void SendMessage_IsNeverCalled_WhenNotifyNotInvoked()
        {
            // Arrange
            var mockService = new Mock<IMessageService>();
            var notification = new Notification(mockService.Object);

            // Assert — no interaction without calling Notify
            mockService.Verify(s => s.SendMessage(It.IsAny<string>()), Times.Never);
        }
    }

    // ── 2. Notification (high-level module) tests ─────────────────

    public class NotificationTests
    {
        [Fact]
        public void Notify_CallsServiceWithSameMessage()
        {
            // Arrange
            var mockService = new Mock<IMessageService>();
            var notification = new Notification(mockService.Object);

            // Act
            notification.Notify("Test message");

            // Assert
            mockService.Verify(s => s.SendMessage("Test message"), Times.Once);
        }

        [Fact]
        public void Notify_CalledMultipleTimes_CallsServiceEachTime()
        {
            // Arrange
            var mockService = new Mock<IMessageService>();
            var notification = new Notification(mockService.Object);

            // Act
            notification.Notify("First");
            notification.Notify("Second");
            notification.Notify("Third");

            // Assert — service called 3 times total
            mockService.Verify(s => s.SendMessage(It.IsAny<string>()), Times.Exactly(3));
        }

        [Fact]
        public void Notify_WithEmptyString_StillCallsService()
        {
            // Arrange
            var mockService = new Mock<IMessageService>();
            var notification = new Notification(mockService.Object);

            // Act
            notification.Notify(string.Empty);

            // Assert — empty string is still a valid call
            mockService.Verify(s => s.SendMessage(string.Empty), Times.Once);
        }

        [Theory]
        [InlineData("Hello")]
        [InlineData("Alert: system down")]
        [InlineData("Password reset link")]
        public void Notify_VariousMessages_AlwaysForwardsToService(string message)
        {
            // Arrange
            var mockService = new Mock<IMessageService>();
            var notification = new Notification(mockService.Object);

            // Act
            notification.Notify(message);

            // Assert
            mockService.Verify(s => s.SendMessage(message), Times.Once);
        }
    }

    // ── 3. EmailService (concrete implementation) tests ───────────
    // EmailService writes to Console — we capture output to verify.

    public class EmailServiceTests
    {
        [Fact]
        public void SendMessage_WritesEmailSentToConsole()
        {
            // Arrange
            var emailService = new EmailService();
            var output = new System.IO.StringWriter();
            Console.SetOut(output);

            // Act
            emailService.SendMessage("Welcome!");

            // Assert
            Assert.Contains("Email sent", output.ToString());
            Assert.Contains("Welcome!", output.ToString());

            // Cleanup — restore Console
            Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput())
                { AutoFlush = true });
        }

        [Fact]
        public void SendMessage_ImplementsIMessageService()
        {
            // EmailService must be assignable to IMessageService (DIP compliance check)
            var emailService = new EmailService();
            Assert.IsAssignableFrom<IMessageService>(emailService);
        }
    }

    // ── 4. DIP Compliance tests ───────────────────────────────────
    // These tests prove the PRINCIPLE itself — Notification never
    // directly depends on a concrete class.

    public class DIPComplianceTests
    {
        [Fact]
        public void Notification_CanAcceptAnyIMessageServiceImplementation()
        {
            // Arrange — swap in a completely different service (SMS)
            var smsService = new Mock<IMessageService>();
            smsService.Setup(s => s.SendMessage(It.IsAny<string>()));

            // Act — Notification works with SMS without any code change
            var notification = new Notification(smsService.Object);
            notification.Notify("SMS alert");

            // Assert
            smsService.Verify(s => s.SendMessage("SMS alert"), Times.Once);
        }

        [Fact]
        public void Notification_WorksWithCustomFakeService()
        {
            // Arrange — inline fake without Moq
            var fakeService = new FakeMessageService();
            var notification = new Notification(fakeService);

            // Act
            notification.Notify("Test via fake");

            // Assert
            Assert.Equal("Test via fake", fakeService.LastMessage);
            Assert.Equal(1, fakeService.CallCount);
        }

        [Fact]
        public void Notification_DependsOnAbstraction_NotConcrete()
        {
            // Reflection check: Notification's constructor param is the interface,
            // not EmailService directly.
            var ctorParams = typeof(Notification)
                .GetConstructors()[0]
                .GetParameters();

            Assert.Single(ctorParams);
            Assert.Equal(typeof(IMessageService), ctorParams[0].ParameterType);
        }
    }

    // ── Helper fake used in DIPComplianceTests ────────────────────
    internal class FakeMessageService : IMessageService
    {
        public string? LastMessage { get; private set; }
        public int CallCount { get; private set; }

        public void SendMessage(string msg)
        {
            LastMessage = msg;
            CallCount++;
        }
    }
}