using Interface;
using System;
using System.Linq;
using System.Text;

namespace GenerateCS.Templates
{
  /// <summary>
  /// C#ソースコード生成クラス
  /// </summary>
  public partial class CreateCS : ITransformText
  {
    /// <summary>
    /// 作成対象テーブル情報
    /// </summary>
    private ITable table;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="table">作成対象テーブル情報</param>
    public CreateCS(ITable table) { this.table = table; }

    /// <summary>
    /// ファイル名
    /// </summary>
    public string FileName { get { return $"{GetCSName(table.Name)}.cs"; } }

    /// <summary>
    /// テーブル名やカラム名からC#用名称を取得
    /// </summary>
    /// <param name="name">DBから取得したテーブル名やカラム名</param>
    /// <returns>C#用名称</returns>
    private string GetCSName(string name)
    {
      var words = name.Split('_').Select(word => { return word.ToUpper()[0].ToString() + (word.Length >= 2 ? word.Substring(1) : string.Empty); });
      return string.Concat(words);
    }

    /// <summary>
    /// C#コメントの取得
    /// </summary>
    /// <param name="comment">DBから取得したテーブルやカラムのコメント</param>
    /// <param name="indent">スペースインデント</param>
    /// <returns></returns>
    private string GetCSComment(string comment, string indent = "")
    {
      var result = new StringBuilder();
      var comments = comment.Replace("\r", string.Empty).Split("\n");
      if (comments.Any())
      {
        result.AppendLine();
        result.AppendLine($"{indent}/// <summary>");
        result.AppendLine(string.Join(Environment.NewLine, comments.Select(text => { return $"{indent}/// {text}"; })));
        result.Append($"{indent}/// </summary>");
      }
      return result.ToString();
    }
  }
}
