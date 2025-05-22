using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface IComissaoRepository : IBaseRepository<Comissao>
    {
        Task<Comissao?> GetByIdAsync(Guid id);
        Task<bool> ExistePorViagemIdAsync(Guid viagemId);
    }
}
