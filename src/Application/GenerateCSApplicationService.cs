using Application.Model;
using Domain.CSFiles;
using Domain.DB;
using TinyDIContainer;

namespace Application
{
  /// <summary>
  /// CSファイル作成アプリケーションサービス
  /// </summary>
  public class GenerateCSApplicationService
  {
    /// <summary>
    /// DBリポジトリ
    /// </summary>
    private IDBRepository dbRepository;

    /// <summary>
    /// CSファイル出力リポジトリ
    /// </summary>
    private ICSFileRepository csFileRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public GenerateCSApplicationService()
    {
      // DIコンテナからリポジトリインスタンスを取得
      dbRepository = DIContainer.CreateInstance<IDBRepository>();
      csFileRepository = DIContainer.CreateInstance<ICSFileRepository>();
    }

    /// <summary>
    /// DB情報からCSファイル生成
    /// </summary>
    /// <param name="inputParamModel">DB情報や出力ファイル設定のインスタンス</param>
    /// <returns>生成結果メッセージリスト</returns>
    public GeneretedFileResultsModel GenerateCSFileFromDB(InputParamModel inputParamModel)
    {
      var classes = dbRepository.GetClasses(DBParameterEntity.Create(inputParamModel.HostName, inputParamModel.UserID, inputParamModel.Password, inputParamModel.Database, inputParamModel.Port));
      var messages = csFileRepository.Generate(classes, FileDataEntity.Create(inputParamModel.OutputPath, inputParamModel.NameSpace));

      return new GeneretedFileResultsModel(messages);
    }
  }
}
