using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface IViagemRepository
    {
        Task<Viagem?> GetByIdAsync(Guid id);
        Task<IEnumerable<Viagem>> GetAllAsync();
        Task AddAsync(Viagem viagem);
        void Update(Viagem viagem);
        void Delete(Viagem viagem);
    }
}
