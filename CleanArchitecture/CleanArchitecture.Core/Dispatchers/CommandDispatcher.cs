namespace CleanArchitecture.Core.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider provider;

    public CommandDispatcher(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public async Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
    {
        if (command is null) throw new ArgumentNullException(nameof(command));

        using var scope = provider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        if (handler is null) throw new CommandHandlerNotFoundException($"Command handler not found, commandType:{typeof(TCommand).Name}.");

        await handler.Execute(command, cancellationToken);
    }
}
