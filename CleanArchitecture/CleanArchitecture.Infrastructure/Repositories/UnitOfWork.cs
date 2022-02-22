namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class UnitOfWork<TContext> : IUnitOfWork<TContext>
    where TContext : DatabaseContext
{
    #region fields

    private Dictionary<Type, Object>? repositories;
    private IServiceProviderIsService serviceProviderIsService;

    #endregion

    #region properties

    public TContext DbContext { get; }
    public SaveChangesResult LastSaveChangesResult { get; }

    #endregion

    public UnitOfWork(TContext context, IServiceProviderIsService serviceProviderIsService)
    {
        this.DbContext = context ?? throw new ArgumentNullException(nameof(context));
        this.serviceProviderIsService = serviceProviderIsService ?? throw new ArgumentNullException(nameof(serviceProviderIsService));
        this.LastSaveChangesResult = new SaveChangesResult();
    }

    #region methods

    public void SetAutoDetectChanges(bool value) => DbContext.ChangeTracker.AutoDetectChangesEnabled = value;

    #endregion

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (serviceProviderIsService.IsService(typeof(IRepository<TEntity>)))
        {
            var customRepo = DbContext.GetService<IRepository<TEntity>>();
            if (customRepo is not null)
            {
                return customRepo;
            }
        }

        repositories ??= new();
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

    #region finalizer

    private void Dispose(bool disposing)
    {
        lock (this)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    repositories?.Clear();
                    DbContext?.Dispose();
                }
            }
            this.disposed = true;
        }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork() => this.Dispose(false);

    private bool disposed;

    #endregion
}
