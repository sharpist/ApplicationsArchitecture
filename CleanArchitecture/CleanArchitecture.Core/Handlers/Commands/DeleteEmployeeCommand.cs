namespace CleanArchitecture.Core.Handlers.Commands;

public record class DeleteEmployeeCommand(int Id) : ICommand;
