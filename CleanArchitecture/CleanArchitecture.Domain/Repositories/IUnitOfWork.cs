namespace CleanArchitecture.Domain.Repositories;

public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : IDatabaseContext
{
    TContext DbContext { get; }
    ValueTask<int> CommitAsync(params IUnitOfWork[] unitOfWorks);
}

public interface IUnitOfWork : IAsyncDisposable
{
    SaveChangesResult LastSaveChangesResult { get; }
    void SetAutoDetectChanges(bool value);
    IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
    ValueTask<int> CommitAsync();
}
