using Doc.Management.CQRS;
using Doc.Management.ValueObjects;
using MediatR;

namespace Doc.Management.CommandHandlers
{
    public class WrappedCommand<TCommand, TAggregate> : IRequest<TAggregate>
        where TCommand : ICommand
        where TAggregate : Aggregate
    {
        public WrappedCommand(TCommand command, UserId userId)
        {
            Command = command;
            UserId = userId;

        }
        public TCommand Command { get; }

        public UserId UserId { get; }
    }
}
