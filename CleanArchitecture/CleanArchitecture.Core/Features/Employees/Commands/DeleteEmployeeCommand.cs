namespace CleanArchitecture.Core.Features.Employees.Commands;

public sealed record class DeleteEmployeeCommand(int Id) : ICommand;
