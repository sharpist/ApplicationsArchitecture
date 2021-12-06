namespace CQRS_Template.Handlers;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Execute(TQuery query);
}
