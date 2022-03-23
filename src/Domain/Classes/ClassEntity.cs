using System.Collections.ObjectModel;

namespace Domain.Classes
{
  /// <summary>
  /// クラスエンティティ
  /// </summary>
  public class ClassEntity
  {
    /// <summary>
    /// 名称
    /// </summary>
    /// <value>プロパティ名</value>
    public string Name { get; private set; }

    /// <summary>
    /// コメント
    /// </summary>
    /// <value>コメント文字列</value>
    public string Comment { get; private set; }

    /// <summary>
    /// プロパティリスト
    /// </summary>
    /// <returns>プロパティリスト</returns>
    public ReadOnlyCollection<PropertyEntity> Properties { get; private set; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private ClassEntity()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="name">クラス名称</param>
    /// <param name="comment">コメント文字列</param>
    /// <param name="properties">プロパティリスト</param>
    /// <returns>クラスエンティティ インスタンス</returns>
    public static ClassEntity Create(string name, string comment, ReadOnlyCollection<PropertyEntity> properties)
    {
      return new ClassEntity()
      {
        Name = name,
        Comment = comment,
        Properties = properties
      };
    }
  }
}
