using System;
using Doc.Management.ValueObjects;
using Xunit;

namespace Doc.Management.UnitTests.Domain.ValueObjects;

public class DocumentKeyShould
{
    [Theory]
    [InlineData("2024/06/25/okfkdokdokodokd")]
    public void ParseCorrectDocumentKey(string key)
    {
        //Arrange
        //Act
        var documentKey = DocumentKey.Parse(key);

        //Assert
        Assert.Equal(key, documentKey);
    }

    [Theory]
    [InlineData("pouet")]
    public void ThrowsArgumentException_When_DocumentKeyIsIncorrect(string key)
    {
        //Arrange
        //Act
        //Assert
        Assert.Throws<ArgumentException>(() => DocumentKey.Parse(key));
    }

    [Fact]
    public void GenerateNewKey()
    {
        var key = DocumentKey.NewDocumentKey();

        Assert.NotEqual(string.Empty, key);
    }
}
