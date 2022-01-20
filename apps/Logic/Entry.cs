using GenerateCS;
using Logic.PostgreSQLClass;
using System;

namespace Logic
{
  /// <summary>
  /// エントリクラス
  /// </summary>
  public class Entry
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <remarks>ファクトリメソッドを使用するため非公開</remarks>
    private Entry()
    {
    }

    /// <summary>
    /// C#のソースを作成する
    /// </summary>
    /// <param name="outputPath">出力先ディレクトリパス</param>
    /// <param name="hostName">サーバーホスト</param>
    /// <param name="userID">ユーザーID</param>
    /// <param name="password">パスワード</param>
    /// <param name="database">データベース名</param>
    /// <param name="port">ポート番号(初期値：5432)</param>
    public static void CreateSources(string outputPath, string hostName, string userID, string password, string database, int port = 5432)
    {
      Console.Write("get DB...");
      var tables = new PostgreSQLDAO().GetTables(hostName, userID, password, database, port);
      Console.WriteLine("ok");

      // ファイル出力
      Console.WriteLine("output C# files");
      foreach (var table in tables)
      {
        Console.Write($"  >>{table.Name}... ");
        Generator.Generate(table, outputPath);
        Console.WriteLine("ok");
      }
      Console.WriteLine("finish");
    }
  }
}
