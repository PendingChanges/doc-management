using Doc.Management.CQRS;

namespace Doc.Management.CommandHandlers
{
    public static class ErrorBuilder
    {
        public static DomainError AggregateNotFound() => new(Errors.AGGREGATE_NOT_FOUND.CODE, Errors.AGGREGATE_NOT_FOUND.MESSAGE);
    }
}
