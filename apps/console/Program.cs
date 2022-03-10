using Logic;
using System;

namespace console
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length < 6)
      {
        Console.WriteLine();
        Console.WriteLine("Input parameters! \"NameSpace OutputPath ServerName UserID Password DatabaseName [portNo]\"");
        Console.WriteLine();
        return;
      }
      var nameSpace = args[0];
      var outputPath = args[1];
      var hostName = args[2];
      var userID = args[3];
      var password = args[4];
      var database = args[5];
      int port = 5432;
      if (args.Length > 6)
      {
        port = int.Parse(args[5]);
      }
      Entry.CreateSources(nameSpace, outputPath, hostName, userID, password, database, port);
    }
  }
}
