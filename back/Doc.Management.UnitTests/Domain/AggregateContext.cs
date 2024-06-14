using System.Collections.Generic;
using System.Linq;
using Doc.Management.CQRS;

namespace Doc.Management.UnitTests.Domain
{
    public class AggregateContext
    {
        public Aggregate? Aggregate { get; set; }

        public AggregateResult? Result { get; set; }

        public List<object> GetEvents() => Result?.GetEvents().ToList() ?? new List<object>();

        public List<DomainError> GetErrors() => Result?.GetErrors().ToList() ?? new List<DomainError>();
    }
}
