using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
