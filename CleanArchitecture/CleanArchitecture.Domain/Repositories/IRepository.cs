namespace CleanArchitecture.Domain.Repositories;

public interface IRepository<TEntity> : IDisposable
    where TEntity : class
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    IQueryable<TEntity> ReadAll(bool disableTracking = true);
    IQueryable<TEntity> ReadAll(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true);
    Task<IEnumerable<TResult>> ReadAllAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);
    ValueTask<TEntity?> FindAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
    bool TryGetCount(out int count);
}
