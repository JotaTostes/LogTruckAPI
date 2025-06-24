using LogTruck.Application.Common.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Infrastructure.Repositories
{
    public class BaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ICurrentUserService _currentUserService;

        public BaseRepository(DbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _currentUserService = currentUserService;
        }

        public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            SetUsuarioAlteracaoId();
            SetAtualizadoEm();
            await _context.SaveChangesAsync();
        }

        private void SetUsuarioAlteracaoId()
        {
            var userId = _currentUserService.UserId;
            foreach (var entry in _context.ChangeTracker.Entries<T>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    var property = entry.Property("UsuarioAlteracaoId");
                    if (property != null && property.Metadata != null)
                    {
                        property.CurrentValue = userId;
                    }
                }
            }
        }

        private void SetAtualizadoEm()
        {
            foreach (var entry in _context.ChangeTracker.Entries<T>())
            {
                if (entry.State == EntityState.Modified)
                {
                    var property = entry.Property("AtualizadoEm");
                    if (property != null && property.Metadata != null)
                    {
                        property.CurrentValue = DateTime.UtcNow;
                    }
                }
            }
        }
    }
}
