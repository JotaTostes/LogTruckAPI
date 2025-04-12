using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Repositories
{
    public interface ICaminhaoRepository
    {
        Task<Caminhao?> GetByIdAsync(Guid id);
        Task<IEnumerable<Caminhao>> GetAllAsync();
        Task AddAsync(Caminhao caminhao);
        void Update(Caminhao caminhao);
        void Delete(Caminhao caminhao);
    }
}
