namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DatabaseContext
{
    #region fields

    private bool disposed;
    private Dictionary<Type, Object>? repositories;

    #endregion

    #region properties

    public TContext DbContext { get; }
    public SaveChangesResult LastSaveChangesResult { get; }

    #endregion

    public UnitOfWork(TContext context)
    {
        DbContext = context ?? throw new ArgumentNullException(nameof(context));
        LastSaveChangesResult = new SaveChangesResult();
    }

    #region methods

    public void SetAutoDetectChanges(bool value) => DbContext.ChangeTracker.AutoDetectChangesEnabled = value;

    #endregion

    public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
    {
        repositories ??= new Dictionary<Type, Object>();

        if (hasCustomRepository)
        {
            var customRepo = DbContext.GetService<IRepository<TEntity>>();
            if (customRepo is not null)
            {
                return customRepo;
            }
        }

        var type = typeof(TEntity);
        if (!repositories.ContainsKey(type))
        {
            repositories[type] = new Repository<TEntity>(DbContext);
        }

        return (IRepository<TEntity>)repositories[type];
    }

    public async ValueTask<int> CommitAsync()
    {
        try
        {
            return await DbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            LastSaveChangesResult.Exception = exception;
            return 0;
        }
    }

    public async ValueTask<int> CommitAsync(params IUnitOfWork[] unitOfWorks)
    {
        var count = 0;
        foreach (var unitOfWork in unitOfWorks)
        {
            count += await unitOfWork.CommitAsync();
        }

        count += await CommitAsync();
        return count;
    }

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                repositories?.Clear();
                await DbContext.DisposeAsync();
            }
        }
        this.disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
