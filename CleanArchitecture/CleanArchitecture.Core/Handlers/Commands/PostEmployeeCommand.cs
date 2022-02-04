namespace CleanArchitecture.Core.Handlers.Commands;

public sealed record class PostEmployeeCommand(CreateEmployeeDTO Model) : ICommand;
