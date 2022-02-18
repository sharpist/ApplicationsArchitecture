namespace CleanArchitecture.Core.Features.Commands.Employees;

public sealed record class UpdateEmployeeCommand(UpdateEmployeeDTO Model) : ICommand;
