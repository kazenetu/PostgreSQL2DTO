using System;

namespace Interface
{
    /// <summary>
    /// カラム
    /// </summary>
    public interface IColumn
    {
        /// <summary>
        /// カラム名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// データ型
        /// </summary>
        string DataType { get; }

        /// <summary>
        /// コメント
        /// </summary>
        string Comment { get; }
    }

}
