namespace CleanArchitecture.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task CreateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    IQueryable<T> ReadAll(bool disableTracking = true);
    IQueryable<T> ReadAll(Expression<Func<T, bool>> predicate, bool disableTracking = true);
    Task<IEnumerable<TResult>> ReadAllAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);
    ValueTask<T?> FindAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    bool TryGetCount(out int count);
}
