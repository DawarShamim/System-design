// S - Single Responsibility Principle (SRP)

// Bad Implementation Example:
class UserServiceBad {
  createUser(name: string) {
    console.log("User created");
  }

  sendEmailNotification(userEmail: string) {
    // ❌ notification responsibility
  }
}


// Actual Implementation
interface INotificationService {
  sendEmail(email: string, message: string): void;
}


class Logger {
  debugLog(message: string) {
    console.log(message);
  }
}

class NotificationService implements INotificationService {
  constructor(private logger: Logger) { }

  sendEmail(email: string, message: string) {
    this.logger.debugLog(`Sending Email to ${email}`);
    console.log(`Email sent to ${email}: ${message}`);
  }
}



class UserService {
  constructor(private notificationService: INotificationService) { }

  createUser(name: string, email: string) {
    // User creation logic
    console.log(`User ${name} created`);

    // Send notification
    this.notificationService.sendEmail(email, `Welcome ${name}!`);
  }

  updateUser(id: number, name: string) { }
  deleteUser(id: number) { }
}


const logger = new Logger();
const notificationService = new NotificationService(logger);
const userService = new UserService(notificationService);

userService.createUser("Alice", "alice@example.com");