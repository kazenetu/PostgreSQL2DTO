using Application;
using Application.Model;
using System;

namespace Presentation.ConsoleApp
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

      //DI設定
      SettingDIContainer.SetDI();

      // パラメータ取得
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

      // Appication呼び出し
      var inputParamModel = new InputParamModel(nameSpace, outputPath, hostName, userID, password, database, port);
      var messages = new GenerateCSApplicationService().GenerateCSFileFromDB(inputParamModel).Messages;

      // ファイル生成結果を取得
      foreach (var message in messages)
      {
        Console.WriteLine(message);
      }
    }
  }
}
