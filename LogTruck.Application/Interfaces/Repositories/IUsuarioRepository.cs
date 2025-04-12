using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task AddAsync(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(Usuario usuario);
        Task<Usuario?> GetByEmailAsync(string email);
    }
}
