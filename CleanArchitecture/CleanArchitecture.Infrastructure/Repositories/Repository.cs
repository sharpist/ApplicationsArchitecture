namespace CleanArchitecture.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DatabaseContext<T> context;
    private readonly DbSet<T> dbSet;

    public Repository(DatabaseContext<T> context) =>
        (this.context, this.dbSet) = (context, context.Set<T>());

    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> ReadAsync(CancellationToken cancellationToken = default) =>
        await dbSet.AsNoTracking().ToArrayAsync(cancellationToken);

    public IQueryable<T> Read(Expression<Func<T, bool>> predicate) =>
        dbSet.AsNoTracking().Where(predicate);

    public async Task<T?> FindAsync(int id, CancellationToken cancellationToken = default) =>
        await dbSet.FindAsync(new object[] { id }, cancellationToken);

    public async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await FindAsync(id);
        if (entity is not null)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public int Count() => dbSet.Count();
}
