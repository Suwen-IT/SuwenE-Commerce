using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Persistence.Context;
using Suwen.Infrastructure.Repositories;
using Suwen.Persistence.Repositories;


namespace Suwen.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SuwenDbContext dbContext;
        public UnitOfWork(SuwenDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(dbContext);
        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>()=> new WriteRepository<T>(dbContext);

        public int Save() => dbContext.SaveChanges();
        public async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();

    }
}