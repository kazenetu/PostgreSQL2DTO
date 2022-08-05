using System.Collections.Generic;
using Domain.Exceptions;

namespace Application.Model
{
  /// <summary>
  /// 入力パラメータモデル
  /// </summary>
  public class InputParamModel
  {
    /// <summary>
    /// CSファイルのクラスに設定する名前空間
    /// </summary>
    public string NameSpace { get; init; }

    /// <summary>
    /// CSファイル出力先ディレクトリパス
    /// </summary>
    public string OutputPath { get; init; }

    /// <summary>
    /// DB接続情報：ホスト名
    /// </summary>
    public string HostName { get; init; }

    /// <summary>
    /// DB接続情報：ユーザーID
    /// </summary>
    public string UserID { get; init; }

    /// <summary>
    /// DB接続情報：パスワード
    /// </summary>
    public string Password { get; init; }

    /// <summary>
    /// DB接続情報：データベース名
    /// </summary>
    public string Database { get; init; }

    /// <summary>
    /// DB接続情報：ポート番号
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// スネークケースのままとするか
    /// </summary>
    public bool UseSnakeCase { get; init; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="nameSpace">CSファイルのクラスに設定する名前空間</param>
    /// <param name="outputPath">出力先ディレクトリパス</param>
    /// <param name="useSnakeCase">スネークケースのままとするか</param>
    /// <param name="hostName">サーバーホスト</param>
    /// <param name="userID">ユーザーID</param>
    /// <param name="password">パスワード</param>
    /// <param name="database">データベース名</param>
    /// <param name="port">ポート番号(初期値：5432)</param>
    public InputParamModel(string nameSpace, string outputPath, bool useSnakeCase, string hostName, string userID, string password, string database, int? port)
    {
      // パラメーターチェック
      var exceptionMessages = new List<DomainExceptionMessage>();
      if (string.IsNullOrEmpty(nameSpace)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(nameSpace)}[{nameSpace}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(outputPath)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(outputPath)}[{outputPath}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(hostName)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(hostName)}[{hostName}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(userID)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(userID)}[{userID}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(password)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(password)}[{password}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(database)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(database)}[{database}]", ExceptionType.Empty));
      if (!port.HasValue) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(port)}[{port}]", ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      NameSpace = nameSpace;
      OutputPath = outputPath;
      HostName = hostName;
      UserID = userID;
      Password = password;
      Database = database;
      Port = port.Value;
      UseSnakeCase = useSnakeCase;
    }
  }
}
