namespace CleanArchitecture.Core.Dispatchers;

public interface ICommandDispatcher
{
    Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand;
}
