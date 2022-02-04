namespace CleanArchitecture.Core.Handlers.Commands;

public sealed record class PutEmployeeCommand(UpdateEmployeeDTO Model) : ICommand;
