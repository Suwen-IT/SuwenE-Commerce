using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Suwen.Infrastructure.Repositories;
using Suwen.Persistence.Repositories;
using Persistence.Context;
using System.Collections.Concurrent;

namespace Suwen.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SuwenDbContext _dbContext;

        private readonly ConcurrentDictionary<Type, object> _readRepositories = new();
        private readonly ConcurrentDictionary<Type, object> _writeRepositories = new();

        public UnitOfWork(SuwenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadRepository<T> GetReadRepository<T>() where T : class
        {
            return (IReadRepository<T>)_readRepositories.GetOrAdd(typeof(T), 
                _ => new ReadRepository<T>(_dbContext));
        }

        public IWriteRepository<T> GetWriteRepository<T>() where T : class
        {
            return (IWriteRepository<T>)_writeRepositories.GetOrAdd(typeof(T), 
                _ => new WriteRepository<T>(_dbContext));
        }

        public int Save() => _dbContext.SaveChanges();

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        public async Task<bool> SaveChangesBoolAsync() => await _dbContext.SaveChangesAsync() > 0;

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
    }
}