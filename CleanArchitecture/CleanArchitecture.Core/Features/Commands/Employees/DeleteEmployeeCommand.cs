namespace CleanArchitecture.Core.Features.Commands.Employees;

public sealed record class DeleteEmployeeCommand(int Id) : ICommand;
