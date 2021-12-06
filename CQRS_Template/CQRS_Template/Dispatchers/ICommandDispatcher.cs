namespace CQRS_Template.Dispatchers;

public interface ICommandDispatcher
{
    Task Execute<TCommand>(TCommand command) where TCommand : ICommand;
}
