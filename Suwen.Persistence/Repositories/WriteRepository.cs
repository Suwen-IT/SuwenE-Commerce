using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suwen.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly SuwenDbContext _dbContext;
        public WriteRepository(SuwenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<T> Table => _dbContext.Set<T>();
        public async Task<bool> AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            return true;
        }
        public async Task AddRangeAsync(IList<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(T entity)
        {
            Table.Remove(entity);
            await Task.CompletedTask;
        } 
        public async Task<T> UpdateAsync(T entity)
        {
            Table.Update(entity);
            await Task.CompletedTask;
            return entity;
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync()>0;
        }
    }
}