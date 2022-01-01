namespace CleanArchitecture.Domain.Exceptions;

public class InvalidRequestBodyException : Exception
{
    public InvalidRequestBodyException() : base() { }
    public InvalidRequestBodyException(string message) : base(message) { }
    public InvalidRequestBodyException(string message, Exception inner) : base(message, inner) { }

    public string[] Errors { get; set; }
}
