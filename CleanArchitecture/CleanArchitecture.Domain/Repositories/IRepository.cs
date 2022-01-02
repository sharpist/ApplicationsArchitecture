namespace CleanArchitecture.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
    Task<IEnumerable<T>> ReadAsync(CancellationToken cancellationToken = default(CancellationToken));
    IQueryable<T> Read(Expression<Func<T, Boolean>> predicate);
    Task<T?> FindAsync(Int32 id, CancellationToken cancellationToken = default(CancellationToken));
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
    Task DeleteAsync(Int32 id, CancellationToken cancellationToken = default(CancellationToken));
    Int32 Count();
}
