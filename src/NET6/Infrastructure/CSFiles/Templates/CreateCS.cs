using System;
using System.Linq;
using System.Text;
using Domain.Classes;

namespace Infrastructure.CSFiles.Templates
{
  /// <summary>
  /// C#ソースコード生成クラス
  /// </summary>
  public partial class CreateCS : ITransformText
  {
    /// <summary>
    /// 作成対象クラス情報
    /// </summary>
    private ClassEntity classEntity;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="classEntity">作成対象クラス情報</param>
    /// <param name="nameSpace">作成対象クラスのnamespace</param>
    public CreateCS(ClassEntity classEntity, string nameSpace)
    {
      this.classEntity = classEntity;
      NameSpace = nameSpace;
    }

    /// <summary>
    /// namespace
    /// </summary>
    public string NameSpace { private set; get; }


    /// <summary>
    /// ファイル名
    /// </summary>
    public string FileName { get { return $"{GetCSName(classEntity.Name)}.cs"; } }

    /// <summary>
    /// テーブル名やカラム名からC#用名称を取得
    /// </summary>
    /// <param name="name">DBから取得したテーブル名やカラム名</param>
    /// <returns>C#用名称</returns>
    private string GetCSName(string name)
    {
      var words = name.Split('_').Select(word => { return word.ToUpper()[0].ToString() + (word.Length >= 2 ? word.Substring(1).ToLower() : string.Empty); });
      return string.Concat(words);
    }

    /// <summary>
    /// C#コメントの取得
    /// </summary>
    /// <param name="comment">DBから取得したテーブルやカラムのコメント</param>
    /// <param name="indent">スペースインデント</param>
    /// <param name="addBlankLine">空行を追加するか</param>
    /// <returns></returns>
    private string GetCSComment(string comment, string indent = "", bool addBlankLine = true)
    {
      var result = new StringBuilder();
      var comments = comment.Replace("\r", string.Empty).Split("\n");
      if (comments.Any())
      {
        if (addBlankLine)
        {
          // 空行を挟むため、T4テンプレート上のインデントは無効になる
          result.AppendLine();
          result.AppendLine($"{indent}/// <summary>");
        }
        else
        {
          // 空行なしの場合、1行目のみT4テンプレートのインデントを活用する
          result.AppendLine($"/// <summary>");
        }
        result.AppendLine(string.Join(Environment.NewLine, comments.Select(text => { return $"{indent}/// {text}"; })));
        result.Append($"{indent}/// </summary>");
      }
      return result.ToString();
    }
  }
}
