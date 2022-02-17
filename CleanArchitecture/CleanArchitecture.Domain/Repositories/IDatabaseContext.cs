
namespace CleanArchitecture.Migrations
{
    public interface IDatabaseContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}