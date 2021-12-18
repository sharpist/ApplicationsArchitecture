namespace CleanArchitecture.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity);

    Task<IEnumerable<T>> ReadAsync();

    IQueryable<T> Read(Expression<Func<T, Boolean>> predicate);

    Task<T?> FindAsync(Int32 id);

    Task UpdateAsync(T entity);

    Task DeleteAsync(Int32 id);

    Int32 Count();
}
