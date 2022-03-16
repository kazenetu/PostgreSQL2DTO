namespace Domain.Classes
{
  /// <summary>
  /// プロパティエンティティ
  /// </summary>
  public class PropertyEntity
  {
    /// <summary>
    /// 名称
    /// </summary>
    /// <value>プロパティ名</value>
    public string Name { get; private set; }

    /// <summary>
    /// 型名
    /// </summary>
    /// <value>型名称</value>
    public string TypeName { get; private set; }

    /// <summary>
    /// コメント
    /// </summary>
    /// <value>コメント文字列</value>
    public string Comment { get; private set; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private PropertyEntity()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="name">クラス名称</param>
    /// <param name="typeName">型名称</param>
    /// <param name="comment">コメント文字列</param>
    /// <returns>プロパティエンティティ インスタンス</returns>
    public static PropertyEntity Create(string name, string typeName, string comment)
    {
      return new PropertyEntity()
      {
        Name = name,
        TypeName = typeName,
        Comment = comment
      };
    }
  }
}


