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
    public string NameSpace { get; private set; }

    /// <summary>
    /// CSファイル出力先ディレクトリパス
    /// </summary>
    public string OutputPath { get; private set; }

    /// <summary>
    /// DB接続情報：ホスト名
    /// </summary>
    public string HostName { get; private set; }

    /// <summary>
    /// DB接続情報：ユーザーID
    /// </summary>
    public string UserID { get; private set; }

    /// <summary>
    /// DB接続情報：パスワード
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// DB接続情報：データベース名
    /// </summary>
    public string Database { get; private set; }

    /// <summary>
    /// DB接続情報：ポート番号
    /// </summary>
    public int Port { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="nameSpace">CSファイルのクラスに設定する名前空間</param>
    /// <param name="outputPath">出力先ディレクトリパス</param>
    /// <param name="hostName">サーバーホスト</param>
    /// <param name="userID">ユーザーID</param>
    /// <param name="password">パスワード</param>
    /// <param name="database">データベース名</param>
    /// <param name="port">ポート番号(初期値：5432)</param>
    public InputParamModel(string nameSpace, string outputPath, string hostName, string userID, string password, string database, int port)
    {
      NameSpace = nameSpace;
      OutputPath = outputPath;
      HostName = hostName;
      UserID = userID;
      Password = password;
      Database = database;
      Port = port;
    }
  }
}
