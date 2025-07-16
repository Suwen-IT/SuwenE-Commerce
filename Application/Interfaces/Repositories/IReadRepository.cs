using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace Application.Interfaces.Repositories
{
    public interface IReadRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false);


        Task<List<T>> GetAllAsyncByPaging(Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           bool enableTracking = false, int currentPage = 1, int pageSize = 3);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
           bool enableTracking = false);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false);

        Task<T?> GetByIdAsync(int id, bool tracking = true);
        Task<T?> GetByIdAsync(Guid id, bool tracking = true);

        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    }
}
