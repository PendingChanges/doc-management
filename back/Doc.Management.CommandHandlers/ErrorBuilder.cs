using Doc.Management.CQRS;

namespace Doc.Management.CommandHandlers
{
    public static class ErrorBuilder
    {
        public static DomainError AggregateNotFound() => new(Errors.AggregateNotFound.CODE, Errors.AggregateNotFound.MESSAGE);
    }
}
