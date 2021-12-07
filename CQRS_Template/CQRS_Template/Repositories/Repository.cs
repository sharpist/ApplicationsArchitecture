namespace CQRS_Template.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext<T> context;
    private readonly DbSet<T> dbSet;

    public Repository(AppDbContext<T> context)
    {
        this.context = context;
        this.dbSet = this.context.Set<T>();
    }

    public IQueryable<T> Items => context.Entities.AsQueryable();

    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<T> ReadAsync(int id)
    {
        var entity = await dbSet.FindAsync(id);
        context.Entry(entity).State = EntityState.Detached;

        return entity;
    }

    public async Task<IEnumerable<T>> ReadAsync()
    {
        return await dbSet.AsNoTracking().ToArrayAsync();
    }

    public async Task Update(T entity)
    {
        dbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task<T> DeleteAsync(int id)
    {
        var entity = await ReadAsync(id);
        if (entity is not null)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
        return entity;
    }

    public int Count() => dbSet.Count();
}
