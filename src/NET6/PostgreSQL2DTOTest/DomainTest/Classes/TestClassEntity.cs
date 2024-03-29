using Domain.Classes;
using Domain.Exceptions;
using Domain.DB;
using PostgreSQL2DTOTest.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace PostgreSQL2DTOTest.Domain.Classes
{
  /// <summary>
  /// クラスエンティティのテスト
  /// </summary>
  public class TestClassEntity : IDisposable
  {
    /// <summary>
    /// Setup
    /// </summary>
    public TestClassEntity()
    {
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose()
    {
    }

    /// <summary>
    ///  DBリポジトリのモックからプロパティエンティティリストを取得する
    /// </summary>
    /// <returns>プロパティエンティティリスト</returns>
    private List<PropertyEntity> GetMockProperty()
    {
      var mockDBRepository = new MockDBRepository();
      var entities = mockDBRepository.GetClasses(DBParameterEntity.Create("HostName", "UserID", "Password", "Database", 0));
      return entities.Where(entity => entity.Name=="m_test").FirstOrDefault()?.Properties.ToList();
    }

    [Fact]
    public void ExceptionAllNull()
    {
      string name = null;
      string comment = null;
      var properties = new List<PropertyEntity>();

      var ex = Assert.ThrowsAny<DomainException>(() => ClassEntity.Create(name, comment, properties.AsReadOnly()));
      Assert.Equal(3, ex.Messages.Count);

      Assert.Equal(ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal("name[]", ex.Messages[0].Target);

      Assert.Equal(ExceptionType.Empty, ex.Messages[1].MessageID);
      Assert.Equal("comment[]", ex.Messages[1].Target);

      Assert.Equal(ExceptionType.Empty, ex.Messages[2].MessageID);
      Assert.Equal($"properties[{properties.AsReadOnly()}]", ex.Messages[2].Target);
    }

    [Fact]
    public void ExceptionNameNull()
    {
      string name = null;
      string comment = "コメント";
      var properties = GetMockProperty();

      var ex = Assert.ThrowsAny<DomainException>(() => ClassEntity.Create(name, comment, properties.AsReadOnly()));
      Assert.Single(ex.Messages);

      Assert.Equal(ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal("name[]", ex.Messages[0].Target);
    }

    [Fact]
    public void ExceptionCommentNull()
    {
      string name = "name";
      string comment = null;
      var properties = GetMockProperty();

      var ex = Assert.ThrowsAny<DomainException>(() => ClassEntity.Create(name, comment, properties.AsReadOnly()));
      Assert.Single(ex.Messages);

      Assert.Equal(ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal("comment[]", ex.Messages[0].Target);
    }

    [Fact]
    public void ExceptionPropetiesZero()
    {
      string name = "name";
      string comment = "コメント";
      var properties = new List<PropertyEntity>();

      var ex = Assert.ThrowsAny<DomainException>(() => ClassEntity.Create(name, comment, properties.AsReadOnly()));
      Assert.Single(ex.Messages);

      Assert.Equal(ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal($"properties[{properties.AsReadOnly()}]", ex.Messages[0].Target);
    }

    [Fact]
    public void Success()
    {
      string name = "name";
      string comment = "コメント";
      var properties = GetMockProperty();

      var instance = ClassEntity.Create(name, comment, properties.AsReadOnly());
      Assert.Equal("name", instance.Name);
      Assert.Equal("コメント", instance.Comment);
      Assert.Equal(2, instance.Properties.Count);
    }
  }
}
