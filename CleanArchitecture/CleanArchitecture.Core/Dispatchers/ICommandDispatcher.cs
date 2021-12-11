namespace CleanArchitecture.Core.Dispatchers;

public interface ICommandDispatcher
{
    Task Execute<TCommand>(TCommand command) where TCommand : ICommand;
}
