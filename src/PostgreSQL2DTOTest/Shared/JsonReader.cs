using System.IO;
using System.Text.Json;

namespace PostgreSQL2DTOTest.Shared
{
  /// <summary>
  /// Jsonを読み込むクラス
  /// </summary>
  /// <typeparam name="U">デシリアライズするクラス</typeparam>
  public class JsonReader<U>
  {
    /// <summary>
    /// セットアップ済み
    /// </summary>
    private static bool Setuped = false;

    /// <summary>
    /// デシリアライズしたクラスインスタンス
    /// </summary>
    /// <returns></returns>
    public static U Instance { get; private set; } = default(U);

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private JsonReader()
    {
    }

    /// <summary>
    /// Jsonを読み込む
    /// </summary>
    /// <param name="filePath">ファイルパス</param>
    public static void ReadJsonFile(string filePath)
    {
      if(Setuped) return;

      string jsonString = File.ReadAllText(filePath);
      Instance = JsonSerializer.Deserialize<U>(jsonString)!;

      Setuped = true;
    }

  }
}