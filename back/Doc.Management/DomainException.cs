using System;
using System.Collections.Generic;
using Doc.Management.CQRS;

namespace Doc.Management;

public class DomainException : Exception
{
    public DomainException(IEnumerable<DomainError>? errors)
    {
        DomainErrors = errors ?? new List<DomainError>();
    }

    public IEnumerable<DomainError> DomainErrors { get; }
}
