namespace CleanArchitecture.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DatabaseContext context;
    private readonly DbSet<TEntity> dbSet;

    public Repository(DatabaseContext context) =>
        (this.context, this.dbSet) = (context, context.Set<TEntity>());

    public virtual async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);

    public virtual async Task CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) =>
        await dbSet.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

    public virtual IQueryable<TEntity> ReadAll(bool disableTracking = true) =>
        disableTracking
            ? dbSet.AsNoTracking()
            : dbSet;

    public virtual IQueryable<TEntity> ReadAll(
        Expression<Func<TEntity, bool>> predicate, bool disableTracking = true) =>
        disableTracking
            ? dbSet.AsNoTracking().Where(predicate)
            : dbSet.Where(predicate);

    public virtual async Task<IEnumerable<TResult>> ReadAllAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = dbSet;

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

    public virtual async ValueTask<TEntity?> FindAsync(int id, CancellationToken cancellationToken = default) =>
        await dbSet.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        dbSet.Update(entity);

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var typeInfo = typeof(TEntity).GetTypeInfo();
        var key = context.Model.FindEntityType(typeInfo)?.FindPrimaryKey()?.Properties.FirstOrDefault();
        if (key is null)
        {
            return;
        }

        var property = typeInfo.GetProperty(key.Name);
        if (property is not null)
        {
            var entity = Activator.CreateInstance<TEntity>();
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
