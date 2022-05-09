using Domain.DB;
using Domain.Exceptions;
using System.Collections.Generic;
using System;
using Xunit;

namespace PostgreSQL2DTOTest.Domain.DB
{
  /// <summary>
  /// DB接続パラメータエンティティのテスト
  /// </summary>
  public class TestDBParameterEntity : IDisposable
  {
    /// <summary>
    /// Setup
    /// </summary>
    public TestDBParameterEntity()
    {
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose()
    {
    }

    [Fact]
    public void ExceptionAllNull()
    {
      string hostName = null;
      string userID = null;
      string password = null;
      string database = null;
      int? port = null;

      var ex = Assert.ThrowsAny<DomainException>(() => DBParameterEntity.Create(hostName, userID, password, database, port));
      Assert.Equal(5, ex.Messages.Count);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal("hostName[]", ex.Messages[0].Target);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[1].MessageID);
      Assert.Equal("userID[]", ex.Messages[1].Target);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[2].MessageID);
      Assert.Equal("password[]", ex.Messages[2].Target);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[3].MessageID);
      Assert.Equal("database[]", ex.Messages[3].Target);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[4].MessageID);
      Assert.Equal("port[]", ex.Messages[4].Target);
    }

    [Fact]
    public void ExceptionHostNameNull()
    {
      string hostName = null;
      string userID = "User";
      string password = "Pass";
      string database = "DB";
      int? port = 0;

      var ex = Assert.ThrowsAny<DomainException>(() => DBParameterEntity.Create(hostName, userID, password, database, port));
      Assert.Single(ex.Messages);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal("hostName[]", ex.Messages[0].Target);
    }

    [Fact]
    public void ExceptionUserIDNull()
    {
      string hostName = "Host";
      string userID = null;
      string password = "Pass";
      string database = "DB";
      int? port = 0;

      var ex = Assert.ThrowsAny<DomainException>(() => DBParameterEntity.Create(hostName, userID, password, database, port));
      Assert.Single(ex.Messages);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal("userID[]", ex.Messages[0].Target);
    }

    [Fact]
    public void ExceptionPasswordNull()
    {
      string hostName = "Host";
      string userID = "User";
      string password = null;
      string database = "DB";
      int? port = 0;

      var ex = Assert.ThrowsAny<DomainException>(() => DBParameterEntity.Create(hostName, userID, password, database, port));
      Assert.Single(ex.Messages);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal("password[]", ex.Messages[0].Target);
    }

    [Fact]
    public void ExceptionDatabaseNull()
    {
      string hostName = "Host";
      string userID = "User";
      string password = "Pass";
      string database = null;
      int? port = 0;

      var ex = Assert.ThrowsAny<DomainException>(() => DBParameterEntity.Create(hostName, userID, password, database, port));
      Assert.Single(ex.Messages);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal("database[]", ex.Messages[0].Target);
    }

    [Fact]
    public void ExceptionPortNull()
    {
      string hostName = "Host";
      string userID = "User";
      string password = "Pass";
      string database = "DB";
      int? port = null;

      var ex = Assert.ThrowsAny<DomainException>(() => DBParameterEntity.Create(hostName, userID, password, database, port));
      Assert.Single(ex.Messages);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal("port[]", ex.Messages[0].Target);
    }

    [Fact]
    public void Success()
    {
      string hostName = "Host";
      string userID = "User";
      string password = "Pass";
      string database = "DB";
      int? port = 0;

      var instance = DBParameterEntity.Create(hostName, userID, password, database, port);

      Assert.Equal("Host", instance.HostName);
      Assert.Equal("User", instance.UserID);
      Assert.Equal("Pass", instance.Password);
      Assert.Equal("DB", instance.Database);
      Assert.Equal(0, instance.Port);
    }
  }
}