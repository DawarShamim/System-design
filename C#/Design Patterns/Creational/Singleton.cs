using System;

namespace Singleton
{
  class DB_Connector
  {
    private static DB_Connector? instance = null;
    private string connector;

    // Private constructor
    private DB_Connector()
    {
      connector = "Connected";
    }

    public static DB_Connector GetInstance()
    {
      instance ??= new DB_Connector();
      return instance;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      // Usage
      DB_Connector connection1 = DB_Connector.GetInstance();
      DB_Connector connection2 = DB_Connector.GetInstance();

      Console.WriteLine(connection1 == connection2); // True
    }
  }
}