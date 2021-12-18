namespace CleanArchitecture.Domain.Exceptions;

public class QueryHandlerNotFoundException : Exception
{
    public QueryHandlerNotFoundException() : base() { }
    public QueryHandlerNotFoundException(string message) : base(message) { }
    public QueryHandlerNotFoundException(string message, Exception inner) : base(message, inner) { }
}
