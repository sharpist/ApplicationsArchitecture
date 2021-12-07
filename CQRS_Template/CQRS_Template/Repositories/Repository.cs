namespace CQRS_Template.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext<T> context;
    private readonly DbSet<T> dbSet;

    public Repository(AppDbContext<T> context) =>
        (this.context, this.dbSet) = (context, context.Set<T>());

    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> ReadAsync() =>
        await dbSet.AsNoTracking().ToArrayAsync();

    public IQueryable<T> Read(System.Linq.Expressions.Expression<Func<T, bool>> predicate) =>
        dbSet.AsNoTracking().Where(predicate);

    public async Task<T> FindAsync(int id) => await dbSet.FindAsync(id);

    public async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task<T> DeleteAsync(int id)
    {
        var entity = await FindAsync(id);
        if (entity is not null)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
        return entity;
    }

    public int Count() => dbSet.Count();
}
