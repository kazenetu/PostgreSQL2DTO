using Domain.Classes;
using Domain.CSFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Infrastructure.CSFiles.Templates;

namespace Infrastructure.CSFiles
{
  /// <summary>
  /// CSファイル出力リポジトリ
  /// </summary>
  public class CSFileRepository : ICSFileRepository
  {
    /// <summary>
    /// CSファイル出力
    /// </summary>
    /// <param name="classEntities">出力対象のクラスエンティティリスト</param>
    /// <param name="fileDataEntity">出力情報</param>
    /// <returns>出力ファイル名リスト</returns>
    public ReadOnlyCollection<string> Generate(List<ClassEntity> classEntities, FileDataEntity fileDataEntity)
    {
      var result = new List<string>();

      foreach(var classEntity in classEntities)
      {
        var createCS = new CreateCS(classEntity, fileDataEntity.NameSpace);
        var filePath = Path.Combine(fileDataEntity.OutputPath, createCS.FileName);

        // ファイル出力
        var message = new StringBuilder();
        message.Append($"  >>{classEntity.Name}... ");
        CreateFile(createCS,filePath,fileDataEntity.NameSpace);
        message.AppendLine("finish");

        result.Add(message.ToString());
      }

      return result.AsReadOnly();
    }

    /// <summary>
    /// ファイル出力
    /// </summary>
    /// <param name="createCS">C#ソースコード生成クラス</param>
    /// <param name="filePath">C#ファイルパス</param>
    /// <param name="nameSpace">名前空間</param>
    private void CreateFile(CreateCS createCS,string filePath,string nameSpace)
    {
      var csSource = ((ITransformText)createCS).TransformText();

      // ファイル出力
      using (FileStream fs = File.OpenWrite(filePath))
      {
        Byte[] info = new UTF8Encoding(true).GetBytes(csSource);
        fs.Write(info, 0, info.Length);
      }
    }
  }
}
