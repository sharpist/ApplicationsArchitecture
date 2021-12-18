namespace CleanArchitecture.Domain.Exceptions;

public class CommandHandlerNotFoundException : Exception
{
    public CommandHandlerNotFoundException() : base() { }
    public CommandHandlerNotFoundException(string message) : base(message) { }
    public CommandHandlerNotFoundException(string message, Exception inner) : base(message, inner) { }
}
