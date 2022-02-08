namespace CleanArchitecture.Core.Features.Employees.Commands;

public sealed record class PostEmployeeCommand(string Name, string Department) : ICommand;
