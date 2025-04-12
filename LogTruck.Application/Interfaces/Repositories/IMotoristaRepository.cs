using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface IMotoristaRepository
    {
        Task<Motorista?> GetByIdAsync(Guid id);
        Task<IEnumerable<Motorista>> GetAllAsync();
        Task AddAsync(Motorista motorista);
        void Update(Motorista motorista);
        void Delete(Motorista motorista);
    }
}
