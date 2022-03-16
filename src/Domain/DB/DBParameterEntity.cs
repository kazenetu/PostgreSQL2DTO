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
    public string HostName { get; private set; }

    /// <summary>
    ///　ユーザーID
    /// </summary>
    public string UserID { get; private set; }

    /// <summary>
    /// パスワード
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// データベース名
    /// </summary>
    public string Database { get; private set; }

    /// <summary>
    /// ポート番号
    /// </summary>
    /// <value></value>
    public int Port { get; private set; }

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
    public static DBParameterEntity Create(string hostName, string userID, string password, string database, int port)
    {
      return new DBParameterEntity()
      {
        HostName = hostName,
        UserID = userID,
        Password = password,
        Database = database,
        Port = port
      };
    }
  }
}