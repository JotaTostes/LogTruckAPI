using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface IComissaoRepository
    {
        Task<Comissao?> GetByIdAsync(Guid id);
        Task<IEnumerable<Comissao>> GetAllAsync();
        Task AddAsync(Comissao comissao);
        void Update(Comissao comissao);
        void Delete(Comissao comissao);
    }
}
