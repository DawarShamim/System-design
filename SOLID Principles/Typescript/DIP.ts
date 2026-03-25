// D - Dependency Inversion Principle (DIP)

// ─── Shared Interface ─────────────────────────────────────────────────────────

interface IMessageService {
  sendMessage(msg: string): void;
}

// ─── Bad Implementation ───────────────────────────────────────────────────────

class EmailServiceBad {
  sendMessage(msg: string) {
    console.log(`Email sent: ${msg}`);
  }
}

// Bad: directly depends on concrete EmailServiceBad, not an abstraction
class NotificationBad {
  private emailService = new EmailServiceBad();

  notify(msg: string) {
    this.emailService.sendMessage(msg);
  }
}

// ─── Good Implementation ──────────────────────────────────────────────────────

class EmailService implements IMessageService {
  sendMessage(msg: string) {
    console.log(`Email sent: ${msg}`);
  }
}

// Good: depends on IMessageService abstraction, injected via constructor
class NotificationA {
  constructor(private service: IMessageService) {}

  notify(msg: string) {
    this.service.sendMessage(msg);
  }
}

export { EmailServiceBad, EmailService, NotificationBad, NotificationA };
export type { IMessageService };