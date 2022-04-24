using Domain.Classes;
using Domain.CSFiles;
using Domain.DB;
using Domain.Exceptions;
using Infrastructure.CSFiles;
using PostgreSQL2DTOTest.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace PostgreSQL2DTOTest.InfrastructureTest
{
  /// <summary>
  /// CSファイル出力リポジトリのテスト
  /// </summary>
  public class TestCSFileRepository : IDisposable
  {
    /// <summary>
    /// テスト対象
    /// </summary>
    private CSFileRepository repository;

    /// <summary>
    /// Setup
    /// </summary>
    public TestCSFileRepository()
    {
      repository = new CSFileRepository();

      Directory.CreateDirectory("CSOutputs");
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose()
    {
      Directory.Delete("CSOutputs", true);
    }

    [Fact, Trait("Category", "InfrastructureTest")]
    public void ExceptionAllNG()
    {
      var classEntities = new List<ClassEntity>();
      FileDataEntity fileDataEntity = null;

      var ex = Assert.ThrowsAny<DomainException>(() => repository.Generate(classEntities, fileDataEntity));
      Assert.Equal(2, ex.MessageIds.Count);
    }

    [Fact, Trait("Category", "InfrastructureTest")]
    public void ExceptionClassEntityZero()
    {
      var classEntities = new List<ClassEntity>();
      var fileDataEntity = FileDataEntity.Create("DB.Dto", "CSOutputs");

      var ex = Assert.ThrowsAny<DomainException>(() => repository.Generate(classEntities, fileDataEntity));
      Assert.Single(ex.MessageIds);
    }

    [Fact, Trait("Category", "InfrastructureTest")]
    public void ExceptionFileDataEntityNull()
    {
      var mockDBRepository = new MockDBRepository();
      var classEntities = mockDBRepository.GetClasses(DBParameterEntity.Create("HostName", "UserID", "Password", "Database", 0));
      FileDataEntity fileDataEntity = null;

      var ex = Assert.ThrowsAny<DomainException>(() => repository.Generate(classEntities, fileDataEntity));
      Assert.Single(ex.MessageIds);
    }

    [Fact, Trait("Category", "InfrastructureTest")]
    public void CreateFiles()
    {
      var mockDBRepository = new MockDBRepository();
      var classEntities = mockDBRepository.GetClasses(DBParameterEntity.Create("HostName", "UserID", "Password", "Database", 0));
      var fileDataEntity = FileDataEntity.Create("CSOutputs", "DB.Dto");

      var messages = repository.Generate(classEntities, fileDataEntity);
      Assert.Equal(2, messages.Count);
      Assert.Equal($"  >>m_test... finish{Environment.NewLine}", messages[0]);
      Assert.Equal($"  >>t_test... finish{Environment.NewLine}", messages[1]);

      var fileNames = Directory.GetFiles(fileDataEntity.OutputPath).OrderBy(filename => filename).ToList();
      Assert.Equal(2, fileNames.Count);
      Assert.Equal($"{fileDataEntity.OutputPath}{Path.DirectorySeparatorChar}MTest.cs", fileNames[0]);
      Assert.Equal($"{fileDataEntity.OutputPath}{Path.DirectorySeparatorChar}TTest.cs", fileNames[1]);
    }
  }
}
