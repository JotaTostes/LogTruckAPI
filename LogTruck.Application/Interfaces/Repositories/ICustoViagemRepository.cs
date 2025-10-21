using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface ICustoViagemRepository : IBaseRepository<CustoViagem>
    {
        Task<IEnumerable<CustoViagem?>> GetByViagemIdAsync(Guid viagemId);
        Task<CustoViagem?> GetByIdAsync(Guid id);
        Task<IEnumerable<CustoViagem?>> GetCustosCompletos();
    }
}
