namespace CleanArchitecture.Core.Handlers;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task Execute(TCommand command);
}
