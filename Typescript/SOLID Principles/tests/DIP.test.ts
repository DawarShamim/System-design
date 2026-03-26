import {
  EmailServiceBad,
  EmailService,
  NotificationBad,
  NotificationA,
} from "../DIP";
import type { IMessageService } from "../DIP";

// ─── EmailServiceBad Tests ────────────────────────────────────────────────────

describe("EmailServiceBad", () => {
  let consoleSpy: jest.SpyInstance;

  beforeEach(() => {
    consoleSpy = jest.spyOn(console, "log").mockImplementation();
  });

  afterEach(() => {
    consoleSpy.mockRestore();
  });

  it("should log the correct email message", () => {
    const service = new EmailServiceBad();
    service.sendMessage("Hello");
    expect(consoleSpy).toHaveBeenCalledWith("Email sent: Hello");
  });

  it("should handle empty string message", () => {
    const service = new EmailServiceBad();
    service.sendMessage("");
    expect(consoleSpy).toHaveBeenCalledWith("Email sent: ");
  });

  it("should be called exactly once per sendMessage call", () => {
    const service = new EmailServiceBad();
    service.sendMessage("Test");
    expect(consoleSpy).toHaveBeenCalledTimes(1);
  });
});

// ─── NotificationBad Tests ────────────────────────────────────────────────────

describe("NotificationBad (DIP Violation)", () => {
  let consoleSpy: jest.SpyInstance;

  beforeEach(() => {
    consoleSpy = jest.spyOn(console, "log").mockImplementation();
  });

  afterEach(() => {
    consoleSpy.mockRestore();
  });

  it("should send email when notify is called", () => {
    const notification = new NotificationBad();
    notification.notify("Hello");
    expect(consoleSpy).toHaveBeenCalledWith("Email sent: Hello");
  });

  it("should call console.log exactly once", () => {
    const notification = new NotificationBad();
    notification.notify("Hello");
    expect(consoleSpy).toHaveBeenCalledTimes(1);
  });
});

// ─── EmailService Tests ───────────────────────────────────────────────────────

describe("EmailService (Good)", () => {
  let consoleSpy: jest.SpyInstance;

  beforeEach(() => {
    consoleSpy = jest.spyOn(console, "log").mockImplementation();
  });

  afterEach(() => {
    consoleSpy.mockRestore();
  });

  it("should log the correct email message", () => {
    const service = new EmailService();
    service.sendMessage("Hello");
    expect(consoleSpy).toHaveBeenCalledWith("Email sent: Hello");
  });

  it("should handle special characters", () => {
    const service = new EmailService();
    service.sendMessage("Hello! @#$%");
    expect(consoleSpy).toHaveBeenCalledWith("Email sent: Hello! @#$%");
  });

  it("should be called exactly once per sendMessage call", () => {
    const service = new EmailService();
    service.sendMessage("Test");
    expect(consoleSpy).toHaveBeenCalledTimes(1);
  });
});

// ─── NotificationA Tests ─────────────────────────────────────────────────────

describe("NotificationA (DIP Compliant)", () => {
  let mockService: jest.Mocked<IMessageService>;

  beforeEach(() => {
    mockService = { sendMessage: jest.fn() };
  });

  it("should call sendMessage on the injected service", () => {
    const notification = new NotificationA(mockService);
    notification.notify("Hello");
    expect(mockService.sendMessage).toHaveBeenCalledWith("Hello");
  });

  it("should call sendMessage exactly once per notify call", () => {
    const notification = new NotificationA(mockService);
    notification.notify("Hello");
    expect(mockService.sendMessage).toHaveBeenCalledTimes(1);
  });

  it("should handle empty string message", () => {
    const notification = new NotificationA(mockService);
    notification.notify("");
    expect(mockService.sendMessage).toHaveBeenCalledWith("");
  });

  it("should work with any IMessageService implementation (SMS mock)", () => {
    const smsService: IMessageService = { sendMessage: jest.fn() };
    const notification = new NotificationA(smsService);
    notification.notify("SMS Alert");
    expect(smsService.sendMessage).toHaveBeenCalledWith("SMS Alert");
  });

  it("should work with any IMessageService implementation (Push mock)", () => {
    const pushService: IMessageService = { sendMessage: jest.fn() };
    const notification = new NotificationA(pushService);
    notification.notify("Push Alert");
    expect(pushService.sendMessage).toHaveBeenCalledWith("Push Alert");
  });
});

// ─── Integration: NotificationA + EmailService ───────────────────────────────

describe("NotificationA with EmailService (Integration)", () => {
  let consoleSpy: jest.SpyInstance;

  beforeEach(() => {
    consoleSpy = jest.spyOn(console, "log").mockImplementation();
  });

  afterEach(() => {
    consoleSpy.mockRestore();
  });

  it("should send email when notify is called", () => {
    const notification = new NotificationA(new EmailService());
    notification.notify("Hello");
    expect(consoleSpy).toHaveBeenCalledWith("Email sent: Hello");
  });

  it("should call console.log exactly once per notify", () => {
    const notification = new NotificationA(new EmailService());
    notification.notify("Hello");
    expect(consoleSpy).toHaveBeenCalledTimes(1);
  });
});