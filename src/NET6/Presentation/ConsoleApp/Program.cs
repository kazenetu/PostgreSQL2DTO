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
        Console.WriteLine("Input parameters! \"NameSpace OutputPath ServerName UserID Password DatabaseName [portNo] ['useSnakeCase']\"");
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
      var useSnakeCase = false;

      if (args.Length > 6)
      {
        port = int.Parse(args[5]);
      }
      if(args.Length >= 7 && args[6] == "useSnakeCase")
      {
        useSnakeCase = true;
      }

      try
      {
        // Appication呼び出し
        useSnakeCase = true;
        var inputParamModel = new InputParamModel(nameSpace, outputPath, useSnakeCase, hostName, userID, password, database, port);
        var messages = new GenerateCSApplicationService().GenerateCSFileFromDB(inputParamModel).Messages;

        // ファイル生成結果を取得
        foreach (var message in messages)
        {
          Console.WriteLine(message);
        }
      }
      catch (ExceptionModel exceptionModel)
      {
        Console.WriteLine("---Exception!!---");
        Console.WriteLine(exceptionModel);
      }
      catch (Exception ex)
      {
        Console.WriteLine("---Exception!!---");
        Console.WriteLine(new ExceptionModel(ex));
      }
    }
  }
}
