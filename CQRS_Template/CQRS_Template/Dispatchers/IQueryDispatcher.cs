namespace CQRS_Template.Dispatchers;

public interface IQueryDispatcher
{
    Task<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
}
