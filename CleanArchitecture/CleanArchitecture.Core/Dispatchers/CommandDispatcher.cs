namespace CleanArchitecture.Core.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider provider;

    public CommandDispatcher(IServiceProvider provider)
    {
        this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public async Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
    {
        if (command is null) throw new ArgumentNullException(nameof(command));

        using var scope = provider.CreateScope();

        var validatorFactory = scope.ServiceProvider.GetRequiredService<IValidatorFactory>();

        var validator = validatorFactory.GetValidator<TCommand>();
        var context = new ValidationContext<TCommand>(command);
        var result = await validator.ValidateAsync(context, cancellationToken);

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CommandDispatcher>>();

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("{command} validation status: {result}", command.GetType(), result);
        }

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
            throw new InvalidRequestBodyException
            {
                Errors = errors
            };
        }

        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        if (handler is null) throw new CommandHandlerNotFoundException($"Command handler not found, commandType:{typeof(TCommand).Name}.");

        await handler.Execute(command, cancellationToken);
    }
}
