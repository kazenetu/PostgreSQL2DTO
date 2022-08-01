using Domain.Classes;
using Domain.DB;
using Domain.Exceptions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using System.Text;

namespace Infrastructure.DB
{
  /// <summary>
  /// DBリポジトリ
  /// </summary>
  public class DBRepository : IDBRepository
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DBRepository()
    {
    }

    /// <summary>
    ////クラスエンティティリスト取得
    /// </summary>
    /// <param name="hostName">サーバーホスト</param>
    /// <param name="userID">ユーザーID</param>
    /// <param name="password">パスワード</param>
    /// <param name="database">データベース名</param>
    /// <param name="port">ポート番号</param>
    /// <returns>クラスエンティティリスト</returns>
    public List<ClassEntity> GetClasses(DBParameterEntity parameterEntity)
    {
      // 接続文字列作成
      var connectionString = $"Server={parameterEntity.HostName};Port={parameterEntity.Port};User Id={parameterEntity.UserID};Password={parameterEntity.Password};Database={parameterEntity.Database}";

      // テーブルリストを取得
      List<Table> tables;
      try
      {
        tables = GetTables(connectionString);
      }
      catch (Exception exception)
      {
        var exceptionMessages = new List<DomainExceptionMessage>();
        exceptionMessages.Add(new DomainExceptionMessage($"{connectionString}", ExceptionType.DBError));
        throw new DomainException(exceptionMessages.AsReadOnly(), exception);
      }

      // クラスエンティティリスト作成
      var result = new List<ClassEntity>();
      foreach (var table in tables)
      {
        var properties = table.Columns.Select(column => PropertyEntity.Create(column.Name, column.DataType, column.Comment)).ToList();
        result.Add(ClassEntity.Create(table.Name, table.Comment, properties.AsReadOnly()));
      }

      return result;
    }

    /// <summary>
    /// PostgreSQLからテーブルリストを作成し、返す
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns>テーブルリスト</returns>
    private List<Table> GetTables(string connectionString)
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

      var result = new List<Table>();
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
            Table tempTable = null;
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