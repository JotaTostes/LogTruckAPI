using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface ICustoViagemRepository
    {
        Task<CustoViagem?> GetByIdAsync(Guid id);
        Task<IEnumerable<CustoViagem>> GetAllAsync();
        Task AddAsync(CustoViagem custo);
        void Update(CustoViagem custo);
        void Delete(CustoViagem custo);
    }
}
