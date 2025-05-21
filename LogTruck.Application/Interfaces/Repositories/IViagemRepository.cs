using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface IViagemRepository : IBaseRepository<Viagem>
    {
        Task<Viagem?> GetByIdAsync(Guid id);
    }
}
