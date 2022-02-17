namespace CleanArchitecture.Domain.Data;

public interface IDatabaseContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
