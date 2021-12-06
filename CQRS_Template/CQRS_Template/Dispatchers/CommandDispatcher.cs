namespace CQRS_Template.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider provider;

    public CommandDispatcher(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public async Task Execute<TCommand>(TCommand command) where TCommand : ICommand
    {
        if (command is null) throw new ArgumentNullException(nameof(command));

        using var scope = provider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        if (handler is null) throw new CommandHandlerNotFoundException($"Command handler not found, commandType:{typeof(TCommand).Name}.");

        await handler.Execute(command);
    }
}

public class CommandHandlerNotFoundException : Exception
{
    public CommandHandlerNotFoundException()
    {
    }

    public CommandHandlerNotFoundException(string message) : base(message)
    {
    }

    public CommandHandlerNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}
