namespace CleanArchitecture.Core.Features.Employees.Commands;

public sealed record class UpdateEmployeeCommand(UpdateEmployeeDTO Model) : ICommand;
