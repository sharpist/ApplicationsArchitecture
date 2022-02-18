namespace CleanArchitecture.Core.Handlers.CommandHandlers;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task Execute(TCommand command, CancellationToken cancellationToken = default(CancellationToken));
}
