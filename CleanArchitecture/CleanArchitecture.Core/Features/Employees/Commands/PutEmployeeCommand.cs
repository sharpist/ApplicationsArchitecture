namespace CleanArchitecture.Core.Features.Employees.Commands;

public sealed record class PutEmployeeCommand(int Id, string Name, string Department) : ICommand;
