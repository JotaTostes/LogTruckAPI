using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface IMotoristaRepository : IBaseRepository<Motorista>
    {
        Task<Motorista?> GetByIdAsync(Guid id);
        Task<List<Motorista>> GetAllMotoristasCompletos();
    }
}
