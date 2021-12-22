using System.Collections.Generic;

namespace Logic.Interface
{
    /// <summary>
    /// テーブル
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// テーブル名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// コメント
        /// </summary>
        string Comment { get; }

        /// <summary>
        /// カラムリスト
        /// </summary>
        List<IColumn> Columns { get; }
    }

}
