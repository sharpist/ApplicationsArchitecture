namespace CleanArchitecture.Core.Handlers.Commands;

public record class PutEmployeeCommand(UpdateEmployeeDTO Model) : ICommand;
