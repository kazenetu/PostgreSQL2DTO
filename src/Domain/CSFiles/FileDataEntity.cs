namespace Domain.CSFiles
{
  /// <summary>
  /// ファイル作成パラメータエンティティ
  /// </summary>
  public class FileDataEntity
  {
    /// <summary>
    /// 出力パス
    /// </summary>
    public string OutputPath { get; private set; }

    /// <summary>
    /// 名前空間
    /// </summary>
    public string NameSpace { get; private set; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private FileDataEntity()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="outputPath">出力パス</param>
    /// <param name="nameSpace">名前空間</param>
    /// <returns>ファイル作成パラメータエンティティ インスタンス</returns>
    public static FileDataEntity Create(string outputPath, string nameSpace)
    {
      return new FileDataEntity()
      {
        OutputPath = outputPath,
        NameSpace = nameSpace
      };
    }
  }
}