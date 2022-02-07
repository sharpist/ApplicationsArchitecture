namespace CleanArchitecture.Core.Handlers.Commands;

public sealed record class DeleteEmployeeCommand(int Id) : ICommand;
