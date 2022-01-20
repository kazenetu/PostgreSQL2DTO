using Logic;
using System;

namespace console
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length < 4)
      {
        Console.WriteLine("Input parameters \"OutputPath ServerName UserID Password DatabaseName [portNo]\"");
        return;
      }
      var outputPath = args[0];
      var hostName = args[1];
      var userID = args[2];
      var password = args[3];
      var database = args[4];
      int port = 5432;
      if (args.Length > 5)
      {
        port = int.Parse(args[4]);
      }
      Entry.CreateSources(outputPath, hostName, userID, password, database, port);
    }
  }
}
