using System;
using Doc.Management.Documents.Commands;

namespace Doc.Management.ValueObjects;

public record DocumentVersion
{
    private readonly Version _version;

    internal DocumentVersion(Version version)
    {
        _version = version;
    }

    public static implicit operator Version(DocumentVersion version) => version._version;

    public static DocumentVersion NewVersion(VersionIncrementType versionIncrementType)
    {
        var version = versionIncrementType switch
        {
            VersionIncrementType.Major => new Version(1, 0),
            VersionIncrementType.Minor => new Version(0, 1),
            _ => throw new ArgumentException("invalid version increment type")
        };
        return new DocumentVersion(version);
    }

    public DocumentVersion Increment(VersionIncrementType versionIncrementType)
    {
        var version = versionIncrementType switch
        {
            VersionIncrementType.Major => new Version(_version.Major + 1, 0),
            VersionIncrementType.Minor => new Version(_version.Major, _version.Minor + 1),
            _ => throw new ArgumentException("invalid version increment type")
        };
        return new DocumentVersion(version);
    }

    public static DocumentVersion Parse(Version version) => new DocumentVersion(version);
}
