namespace CleanArchitecture.Core.Handlers;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Execute(TQuery query, CancellationToken cancellationToken = default(CancellationToken));
}
