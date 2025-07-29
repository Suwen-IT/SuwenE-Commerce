using Application.Interfaces.Repositories;

namespace Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : class;
        IWriteRepository<T> GetWriteRepository<T>() where T : class;
        int Save();
        Task<int> SaveChangesAsync();
        Task<bool>SaveChangesBoolAsync();
    }
}
