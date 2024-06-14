using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Doc.Management.CQRS;
using Doc.Management.ValueObjects;

namespace Doc.Management.CommandHandlers;

internal abstract class SingleAggregateCommandHandler<TCommand, TAggregate> : IRequestHandler<WrappedCommand<TCommand, TAggregate>, TAggregate>
    where TAggregate : Aggregate
    where TCommand : ICommand
{
    protected readonly IWriteEvents EventWriter;
    protected readonly IReadAggregates AggregateReader;

    protected SingleAggregateCommandHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader)
    {
        EventWriter = eventWriter;
        AggregateReader = aggregateReader;
    }

    public async Task<TAggregate> Handle(WrappedCommand<TCommand, TAggregate> request, CancellationToken cancellationToken)
    {
        var command = request.Command;

        var aggregate = await LoadAggregate(command, request.UserId, cancellationToken);

        if (aggregate == null)
        {
            throw new DomainException(new[] { ErrorBuilder.AggregateNotFound() });
        }

        var aggregateResult = ExecuteCommand(aggregate, command, request.UserId);

        if (aggregateResult.HasErrors)
        {
            throw new DomainException(aggregateResult.GetErrors());
        }

        await EventWriter.StoreAsync(aggregate.Id, aggregate.Version, aggregateResult.GetEvents(), cancellationToken);

        return aggregate;
    }

    protected abstract Task<TAggregate?> LoadAggregate(TCommand command, UserId ownerId, CancellationToken cancellationToken);

    protected abstract AggregateResult ExecuteCommand(TAggregate aggregate, TCommand command, UserId ownerId);
}
