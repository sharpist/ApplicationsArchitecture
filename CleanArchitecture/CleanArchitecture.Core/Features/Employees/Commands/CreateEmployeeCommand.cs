namespace CleanArchitecture.Core.Features.Employees.Commands;

public sealed record class CreateEmployeeCommand(CreateEmployeeDTO Model) : ICommand;
