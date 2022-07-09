using Domain.DB;
using Domain.Exceptions;
using Infrastructure.DB;
using System;
using System.Linq;
using Xunit;

namespace PostgreSQL2DTOTest.InfrastructureTest
{
  /// <summary>
  /// DBリポジトリのテスト
  /// </summary>
  /// <remarks>
  /// 別途用意したDBを利用する場合は「src\PostgreSQL2DTOTest\Config\db.json」の接続情報を書き換えること
  /// ※「docker_dev」を使用することを推奨
  /// </remarks>
  public class TestDBRepository : IDisposable
  {
    /// <summary>
    /// DB.json デシリアライズクラス
    /// </summary>
    class SettingConnection
    {
      public string HostName { get; set; }
      public string UserID { get; set; }
      public string Password { get; set; }
      public string Database { get; set; }
      public int Port { get; set; }
    }

    /// <summary>
    /// テスト対象
    /// </summary>
    private DBRepository repository;

    /// <summary>
    /// Setup
    /// </summary>
    public TestDBRepository()
    {
      Shared.JsonReader<SettingConnection>.ReadJsonFile("./Config/db.json");
      repository = new DBRepository();
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose()
    {
    }

    [Fact, Trait("Category", "InfrastructureTest")]
    public void ExceptionDBConnect()
    {
      var connection = Shared.JsonReader<SettingConnection>.Instance;
      var dbParameter = DBParameterEntity.Create(connection.HostName, "UserID", "Password", "Database", 0);

      Assert.ThrowsAny<DomainException>(() => repository.GetClasses(dbParameter));
    }

    [Fact, Trait("Category", "InfrastructureTest")]
    public void DBConnect()
    {
      var connection = Shared.JsonReader<SettingConnection>.Instance;
      var dbParameter = DBParameterEntity.Create(connection.HostName, connection.UserID, connection.Password, connection.Database + " ;MinPoolSize=10;MaxPoolSize=100;ConnectionLifeTime=60", connection.Port);

      var classes = repository.GetClasses(dbParameter);

      // Class Count
      Assert.Equal(2, classes.Count);

      // MTest Property Count
      var mTest = classes.Where(item => item.Name == "m_test").FirstOrDefault();
      Assert.NotNull(mTest);
      Assert.Equal(5, mTest.Properties.Count);

      // TTest Property Count
      var tTest = classes.Where(item => item.Name == "t_test").FirstOrDefault();
      Assert.NotNull(tTest);
      Assert.Equal(10, tTest.Properties.Count);
    }
  }
}
