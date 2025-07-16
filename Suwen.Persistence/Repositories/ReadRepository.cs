using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suwen.Infrastructure.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly SuwenDbContext _dbContext;
        public ReadRepository(SuwenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<T> Table => _dbContext.Set<T>();

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            var query = Table.AsQueryable();
            if (predicate != null) Table.Where(predicate);
            return await query.CountAsync();
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            IQueryable<T> query = Table;
            if (!enableTracking)
                query = query.AsNoTracking();
            return query.Where(predicate);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>,IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable=queryable.AsNoTracking();
            if(include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null) 
                return await orderBy(queryable).ToListAsync();

            return await queryable.ToListAsync();
        }

        public async Task<List<T>> GetAllAsyncByPaging(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>,IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
                queryable=orderBy(queryable);

            return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIdAsync(int id, bool tracking = true)
        {
            IQueryable<T> query = Table;
            if (!tracking) 
                query = query.AsNoTracking();
            
            var parameter = Expression.Parameter(typeof(T),"entity");
            var property = Expression.Property(parameter, "Id");
            var constant = Expression.Constant(id);
            var body = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T,bool>>(body, parameter);
            
            return await query.FirstOrDefaultAsync(lambda);
        }

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
        {
            IQueryable<T> query = Table;
            if (!tracking) 
                query = query.AsNoTracking();
            
            var parameter = Expression.Parameter(typeof(T),"entity");
            var property = Expression.Property(parameter, "Id");
            var constant = Expression.Constant(id);
            var body = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T,bool>>(body, parameter);
            
            return await query.FirstOrDefaultAsync(lambda);
        }
        
    }
}
