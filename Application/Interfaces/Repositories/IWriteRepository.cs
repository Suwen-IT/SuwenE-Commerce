
namespace Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task AddRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool>SaveChangesAsync();
    }
}
