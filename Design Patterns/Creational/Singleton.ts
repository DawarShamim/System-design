class Logger {
  static instance: Logger | null = null;

  private logs: string[] = [];

  private constructor() {} // private constructor to prevent `new Logger()`

  static getInstance(): Logger {
    if (!Logger.instance) {
      Logger.instance = new Logger();
    }
    return Logger.instance;
  }

  log(message: string) {
    const timestamp = new Date().toISOString();
    this.logs.push(`[${timestamp}] ${message}`);
    console.log(`[${timestamp}] ${message}`);
  }

  getLogs() {
    return this.logs;
  }
}

// Usage
const logger1 = Logger.getInstance();
const logger2 = Logger.getInstance();

logger1.log("App started");
logger2.log("User clicked button");

console.log(logger1 === logger2); // true
console.log(logger1.getLogs()); // Both logs from same instance



class Config {
  static instance: Config | null = null;
  private settings: { [key: string]: any } = {};

  private constructor() {}

  static getInstance(): Config {
    if (!Config.instance) {
      Config.instance = new Config();
    }
    return Config.instance;
  }

  set(key: string, value: any) {
    this.settings[key] = value;
  }

  get(key: string) {
    return this.settings[key];
  }
}

// Usage
const config1 = Config.getInstance();
config1.set("theme", "dark");

const config2 = Config.getInstance();
console.log(config2.get("theme"));
