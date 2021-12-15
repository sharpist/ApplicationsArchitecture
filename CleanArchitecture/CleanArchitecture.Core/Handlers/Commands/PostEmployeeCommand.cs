namespace CleanArchitecture.Core.Handlers.Commands;

public record class PostEmployeeCommand(CreateOrUpdateEmployeeDTO Model) : ICommand;
