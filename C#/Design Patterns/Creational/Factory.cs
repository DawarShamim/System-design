using System;
using System.Collections.Generic;

namespace Factory
{
    abstract class Logger
    { public abstract void Log(string msg); }

    class ConsoleLogger : Logger
    {
        public override void Log(string msg) { Console.WriteLine("Console Log: " + msg); }
    }

    class DBLogger : Logger
    {
        public override void Log(string msg) { Console.WriteLine("DB Log: " + msg); }
    }

    class DebugLogger : Logger
    {
        public override void Log(string msg) { Console.WriteLine("Debug Log: " + msg); }
    }

    class LogFactory
    {
        private static readonly Dictionary<string, Func<Logger>> loggers =
        new Dictionary<string, Func<Logger>>();
        public static void RegisterLogger(string type, Func<Logger> loggerCreator)
        { loggers[type] = loggerCreator; }

        public static Logger GetLogger(string type) { return loggers[type](); }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LogFactory.RegisterLogger("console", () => new ConsoleLogger());

            LogFactory.RegisterLogger("db", () => new DBLogger());
            LogFactory.RegisterLogger("debug", () => new DebugLogger());

            Logger logger = LogFactory.GetLogger("console");
            logger.Log("Application started");

            Logger logger2 = LogFactory.GetLogger("db");
            logger2.Log("Saving log to DB");
        }
    }
}