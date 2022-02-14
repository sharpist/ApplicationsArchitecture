namespace CleanArchitecture.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DatabaseContext<T> context;
    private readonly DbSet<T> dbSet;

    public Repository(DatabaseContext<T> context) =>
        (this.context, this.dbSet) = (context, context.Set<T>());

    public virtual async Task CreateAsync(T entity, CancellationToken cancellationToken = default) =>
        await dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);

    public virtual async Task CreateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) =>
        await dbSet.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

    public virtual IQueryable<T> ReadAll(bool disableTracking = true) =>
        disableTracking
            ? dbSet.AsNoTracking()
            : dbSet;

    public virtual IQueryable<T> ReadAll(
        Expression<Func<T, bool>> predicate, bool disableTracking = true) =>
        disableTracking
            ? dbSet.AsNoTracking().Where(predicate)
            : dbSet.Where(predicate);

    public virtual async Task<IEnumerable<TResult>> ReadAllAsync<TResult>(
        Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return orderBy is not null
            ? await orderBy(query).Select(selector).ToArrayAsync(cancellationToken).ConfigureAwait(false)
            : await query.Select(selector).ToArrayAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async ValueTask<T?> FindAsync(int id, CancellationToken cancellationToken = default) =>
        await dbSet.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default) =>
        dbSet.Update(entity);

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var typeInfo = typeof(T).GetTypeInfo();
        var key = context.Model.FindEntityType(typeInfo)?.FindPrimaryKey()?.Properties.FirstOrDefault();
        if (key is null)
        {
            return;
        }

        var property = typeInfo.GetProperty(key.Name);
        if (property is not null)
        {
            var entity = Activator.CreateInstance<T>();
            property.SetValue(entity, id);
            context.Entry(entity).State = EntityState.Deleted;
        }
        else
        {
            var entity = await FindAsync(id, cancellationToken).ConfigureAwait(false);
            if (entity is not null)
            {
                dbSet.Remove(entity);
            }
        }
    }

    public virtual bool TryGetCount(out int count) => dbSet.TryGetNonEnumeratedCount(out count) ? true : false;
}
