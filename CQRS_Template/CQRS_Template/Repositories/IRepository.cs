namespace CQRS_Template.Repositories;

public interface IRepository<T>
{
    IQueryable<T> Items { get; }

    Task CreateAsync(T entity);

    Task<T> ReadAsync(Int32 id);

    Task<IEnumerable<T>> ReadAsync();

    Task Update(T entity);

    Task<T> DeleteAsync(Int32 id);

    Int32 Count();
}
