namespace CleanArchitecture.Core.Handlers.Commands;

public sealed record class DeleteEmployeeCommand(DeleteEmployeeDTO Model) : ICommand;
