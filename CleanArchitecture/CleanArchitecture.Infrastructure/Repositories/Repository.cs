namespace CleanArchitecture.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DatabaseContext<T> context;
    private readonly DbSet<T> dbSet;

    public Repository(DatabaseContext<T> context) =>
        (this.context, this.dbSet) = (context, context.Set<T>());

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await dbSet.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> ReadAsync(CancellationToken cancellationToken = default) =>
        await dbSet.AsNoTracking().ToArrayAsync(cancellationToken);

    public IQueryable<T> Read(Expression<Func<T, bool>> predicate) =>
        dbSet.AsNoTracking().Where(predicate);

    public async Task<T?> FindAsync(int id, CancellationToken cancellationToken = default) =>
        await dbSet.FindAsync(new object[] { id }, cancellationToken);

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await FindAsync(id, cancellationToken);
        if (entity is not null)
        {
            if (context.Entry(entity).State is EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public bool TryGetCount(out int count) => dbSet.TryGetNonEnumeratedCount(out count) ? true : false;
}
