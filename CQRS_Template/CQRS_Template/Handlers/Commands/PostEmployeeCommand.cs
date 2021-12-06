namespace CQRS_Template.Handlers.Commands;

public record class PostEmployeeCommand(string Name, string Department) : ICommand;
