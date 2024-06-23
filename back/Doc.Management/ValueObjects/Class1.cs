using System;

namespace Doc.Management.ValueObjects;
public record DocumentKey
{
    private string _value;

    private DocumentKey(string value)
    {
        _value = value;
    }

    public static implicit operator string(DocumentKey id) => id._value;

    public static DocumentKey NewDocumentKey()
    {
        var now = DateTime.UtcNow;
        return new DocumentKey($"{now.Year}/{now.ToString("MMMM")}/{now.ToString("dd")}/{Guid.NewGuid()}");
    }

    public static DocumentKey Parse(string value)
    {
        //TODO : check format

        return new DocumentKey(value);
    }
}
