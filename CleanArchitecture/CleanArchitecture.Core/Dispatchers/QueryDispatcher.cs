namespace CleanArchitecture.Core.Dispatchers;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider provider;

    public QueryDispatcher(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public async Task<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        if (query is null) throw new ArgumentNullException(nameof(query));

        using var scope = provider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        if (handler is null) throw new QueryHandlerNotFoundException($"Query handler not found, queryType:{typeof(TQuery).Name}.");

        return await handler.Execute(query);
    }
}

public class QueryHandlerNotFoundException : Exception
{
    public QueryHandlerNotFoundException() : base() { }
    public QueryHandlerNotFoundException(string message) : base(message) { }
    public QueryHandlerNotFoundException(string message, Exception inner) : base(message, inner) { }
}
