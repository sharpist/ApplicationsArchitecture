namespace CQRS_Template.Repositories;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity);

    Task<IEnumerable<T>> ReadAsync();

    IQueryable<T> Read(System.Linq.Expressions.Expression<Func<T, Boolean>> predicate);

    Task<T> FindAsync(Int32 id);

    Task UpdateAsync(T entity);

    Task<T> DeleteAsync(Int32 id);

    Int32 Count();
}
