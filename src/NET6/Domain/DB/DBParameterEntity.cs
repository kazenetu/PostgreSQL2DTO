using Domain.Exceptions;
using System.Collections.Generic;

namespace Domain.DB
{
  /// <summary>
  /// DB接続パラメータエンティティ
  /// </summary>
  public class DBParameterEntity
  {
    /// <summary>
    /// サーバーホスト
    /// </summary>
    public string HostName { get; init; }

    /// <summary>
    ///　ユーザーID
    /// </summary>
    public string UserID { get; init; }

    /// <summary>
    /// パスワード
    /// </summary>
    public string Password { get; init; }

    /// <summary>
    /// データベース名
    /// </summary>
    public string Database { get; init; }

    /// <summary>
    /// ポート番号
    /// </summary>
    /// <value></value>
    public int Port { get; init; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private DBParameterEntity()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="hostName">サーバーホスト</param>
    /// <param name="userID">ユーザーID</param>
    /// <param name="password">パスワード</param>
    /// <param name="database">データベース名</param>
    /// <param name="port">ポート番号</param>
    /// <returns>DB接続パラメータエンティティ インスタンス</returns>
    public static DBParameterEntity Create(string hostName, string userID, string password, string database, int? port)
    {
      // パラメーターチェック
      var exceptionMessages = new List<DomainExceptionMessage>();
      if (string.IsNullOrEmpty(hostName)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(hostName)}[{hostName}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(userID)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(userID)}[{userID}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(password)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(password)}[{password}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(database)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(database)}[{database}]", ExceptionType.Empty));
      if (!port.HasValue) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(port)}[{port}]", ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      return new DBParameterEntity()
      {
        HostName = hostName,
        UserID = userID,
        Password = password,
        Database = database,
        Port = port.Value
      };
    }
  }
}