using System;
using System.Text.RegularExpressions;

namespace Doc.Management.ValueObjects;

public record DocumentKey
{
    private static readonly Regex _documentKeyRegex = new Regex(
        "^[0-9]{4}/[0-9]{2}/[0-9]{2}/.*$",
        RegexOptions.Compiled,
        TimeSpan.FromSeconds(5)
    );
    private readonly string _value;

    internal DocumentKey(string value)
    {
        _value = value;
    }

    public static implicit operator string(DocumentKey id) => id._value;

    public static DocumentKey NewDocumentKey()
    {
        var now = DateTime.UtcNow;
        return new DocumentKey(
            $"{now.Year}/{now.ToString("MM")}/{now.ToString("dd")}/{Guid.NewGuid()}"
        );
    }

    public static DocumentKey Parse(string value)
    {
        if (!_documentKeyRegex.IsMatch(value))
        {
            throw new ArgumentException("invalid document key");
        }

        return new DocumentKey(value);
    }
}
