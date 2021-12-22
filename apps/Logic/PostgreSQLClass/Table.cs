using System.Collections.Generic;
using Logic.Interface;

namespace Logic.PostgreSQLClass
{
    /// <summary>
    /// テーブル
    /// </summary>
    public class Table:ITable
    {
        /// <summary>
        /// テーブル名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// コメント
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// カラムリスト
        /// </summary>
        public List<IColumn> Columns { get; } = new List<IColumn>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">テーブル名</param>
        /// <param name="comment">コメント</param>
        public Table(string name,string comment)
        {
            Name = name;
            Comment = comment;
        }
    }

}