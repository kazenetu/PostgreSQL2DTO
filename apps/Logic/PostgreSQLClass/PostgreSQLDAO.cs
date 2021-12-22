using System.Collections.Generic;
using System.Text;
using Logic.Interface;
using Npgsql;

namespace Logic.PostgreSQLClass
{
    /// <summary>
    /// PostgreSQLアクセスクラス
    /// </summary>
    class PostgreSQLDAO
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PostgreSQLDAO()
        {
        }

        /// <summary>
        ////テーブルリスト取得
        /// </summary>
        /// <param name="hostName">サーバーホスト</param>
        /// <param name="userID">ユーザーID</param>
        /// <param name="password">パスワード</param>
        /// <param name="database">データベース名</param>
        /// <param name="port">ポート番号</param>
        /// <returns>テーブルリスト</returns>
        public List<ITable> GetTables(string hostName, string userID, string password, string database, int port)
        {
            // 接続文字列作成
            var connectionString = $"Server={hostName};Port={port};User Id={userID};Password={password};Database={database}";

            // テーブルリストを返す
            return GetTables(connectionString);
        }

        /// <summary>
        /// PostgreSQLからテーブルリストを作成し、返す
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>テーブルリスト</returns>
        private List<ITable> GetTables(string connectionString)
        {

            var sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.AppendLine("  table_name,");
            sql.AppendLine("  coalesce(pd.description,table_name) as table_comment,");
            sql.AppendLine("  information_schema.columns.data_type,");
            sql.AppendLine("  column_name,");
            sql.AppendLine("  coalesce(pdc.description,column_name) as column_comment");
            sql.AppendLine("from information_schema.columns");
            sql.AppendLine("inner join pg_stat_user_tables");
            sql.AppendLine("  on table_name = pg_stat_user_tables.relname");
            sql.AppendLine("left join pg_description pd");
            sql.AppendLine("  on pg_stat_user_tables.relid = pd.objoid and pd.objsubid = 0");
            sql.AppendLine("left join pg_description pdc");
            sql.AppendLine("  on pg_stat_user_tables.relid = pdc.objoid and pdc.objsubid = ordinal_position");
            sql.AppendLine("order by table_name,ordinal_position;");

            var result = new List<ITable>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    // SELECT
                    cmd.CommandText = sql.ToString();
                    using (var reader = cmd.ExecuteReader())
                    {
                        var prevTableName = string.Empty;
                        ITable tempTable = null;
                        while (reader.Read())
                        {
                            var tableName = reader["table_name"].ToString();

                            // テーブルが切り替わったら設定と再生成
                            if (prevTableName != tableName)
                            {
                                prevTableName = tableName;
                                tempTable = new Table(tableName, reader["table_comment"].ToString());
                                result.Add(tempTable);
                            }

                            // カラム追加
                            var columnName = reader["column_name"].ToString();
                            var dataType = reader["data_type"].ToString();
                            var columnComment = reader["column_comment"].ToString();
                            tempTable.Columns.Add(new Column(columnName, dataType, columnComment));
                        }
                    }
                }
            }
            return result;
        }
    }
}


