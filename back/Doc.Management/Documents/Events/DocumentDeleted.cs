using System;

namespace Doc.Management.Documents.Events;

public sealed record DocumentDeleted(Guid Id, string UserId);
