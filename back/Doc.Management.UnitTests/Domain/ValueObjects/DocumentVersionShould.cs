using System;
using Doc.Management.Documents.Commands;
using Doc.Management.ValueObjects;
using Xunit;

namespace Doc.Management.UnitTests.Domain.ValueObjects;

public class DocumentVersionShould
{
    [Fact]
    public void ParseCorrectDocumentVersion()
    {
        //Arrange
        var version = new Version(1, 2);

        //Act
        var documentVersion = DocumentVersion.Parse(version);

        //Assert
        Assert.Equal(version, documentVersion);
    }

    [Theory]
    [InlineData(VersionIncrementType.Minor, 0, 1)]
    [InlineData(VersionIncrementType.Major, 1, 0)]
    public void GenerateNewDocumentVersion(
        VersionIncrementType versionIncrementType,
        int expectedMajor,
        int expectedMinor
    )
    {
        //Arrange
        //Act
        var documentVersion = DocumentVersion.NewVersion(versionIncrementType);

        //Assert
        Assert.Equal(new Version(expectedMajor, expectedMinor), documentVersion);
    }

    // Generate unit test for Increment method in #DocumentVersion.cs file
    [Theory]
    [InlineData(VersionIncrementType.Major, 1, 3, 2, 0)]
    [InlineData(VersionIncrementType.Major, 2, 0, 3, 0)]
    [InlineData(VersionIncrementType.Minor, 1, 3, 1, 4)]
    [InlineData(VersionIncrementType.Minor, 2, 0, 2, 1)]
    public void IncrementDocumentVersion(
        VersionIncrementType versionIncrementType,
        int baseMajor,
        int baseMinor,
        int expectedMajor,
        int expectedMinor
    )
    {
        //Arrange
        var baseVersion = DocumentVersion.Parse(new Version(baseMajor, baseMinor));

        //Act
        var incrementedVersion = baseVersion.Increment(versionIncrementType);

        //Assert
        Assert.Equal(new Version(expectedMajor, expectedMinor), incrementedVersion);
    }
}
