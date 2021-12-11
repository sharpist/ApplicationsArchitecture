namespace CleanArchitecture.Core.Handlers.Commands;

public record class PostEmployeeCommand(CreateEmployeeDTO Model) : ICommand;
