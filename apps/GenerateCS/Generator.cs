using Interface;
using System;
using System.IO;
using System.Text;

namespace GenerateCS
{
    /// <summary>
    /// DBテーブル情報からC#ソースコード生成クラス
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>スタティックメソッドを使用するため非公開</remarks>
        private Generator()
        {
        }

        /// <summary>
        /// C#用DTOクラス生成
        /// </summary>
        /// <param name="table">DBテーブル情報</param>
        /// <param name="outputPath">出力ディレクトリパス</param>
        public static void Generate(ITable table, string outputPath)
        {
            try
            {
                // C#ソースコードを取得
                var createCS = new Templates.CreateCS(table);
                var csSource = ((ITransformText)createCS).TransformText();

                // ファイル出力
                var filePath = Path.Combine(outputPath, createCS.FileName);
                using (FileStream fs = File.OpenWrite(filePath))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(csSource);
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
