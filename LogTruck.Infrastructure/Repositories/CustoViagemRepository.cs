using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Domain.Entities;
using LogTruck.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Infrastructure.Repositories
{
    public class CustoViagemRepository : BaseRepository<CustoViagem> ,ICustoViagemRepository
    {
        private readonly AppDbContext _context;

        public CustoViagemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustoViagem?>> GetByViagemIdAsync(Guid viagemId)
            => await _context.CustosViagem.Where(x => x.ViagemId == viagemId).ToListAsync();

        public async Task<CustoViagem?> GetByIdAsync(Guid id)
                        => await _context.CustosViagem.FirstOrDefaultAsync(x => x.Id == id);
    }
}
