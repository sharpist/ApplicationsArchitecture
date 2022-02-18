namespace CleanArchitecture.Core.Features.Commands.Employees;

public sealed record class CreateEmployeeCommand(CreateEmployeeDTO Model) : ICommand;
