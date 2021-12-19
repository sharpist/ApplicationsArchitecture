namespace CleanArchitecture.Core.Dispatchers;

public interface IQueryDispatcher
{
    Task<TResult> Execute<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default(CancellationToken)) where TQuery : IQuery<TResult>;
}
