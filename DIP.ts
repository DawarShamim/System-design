// D - Dependency Inversion Principle (DIP)

// Bad Implementation Example:
interface MessageService {
  sendMessage(msg: string): void;
}

// Direct dependency on concrete class
class NotificationBad {
  private emailService = new EmailService();

  notify(msg: string) {
    this.emailService.sendMessage(msg);
  }
}

class EmailServiceBad {
  sendMessage(msg: string) {
    console.log(`Email sent: ${msg}`);
  }
}

const notificationB = new NotificationBad();
notificationB.notify('Hello');



// Actual Implementation

interface MessageService {
  sendMessage(msg: string): void;
}

class EmailService implements MessageService {
  sendMessage(msg: string) {
    console.log(`Email sent: ${msg}`);
  }
}

class NotificationA {
  constructor(private service: MessageService) {}
  notify(msg: string) {
    this.service.sendMessage(msg);
  }
}

const notification = new NotificationA(new EmailService());
notification.notify('Hello');