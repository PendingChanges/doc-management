using Doc.Management.ValueObjects;
using System;

namespace Doc.Management.Documents.DataModels;

public record DocumentDocument(string Id,  string Key, string Name, string FileNameWithoutExtension, string Extension, Version Version);
