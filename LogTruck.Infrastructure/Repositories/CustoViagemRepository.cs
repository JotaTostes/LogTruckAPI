using LogTruck.Application.Common.Security;
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

        public CustoViagemRepository(AppDbContext context, ICurrentUserService currentUserService) : base(context, currentUserService)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustoViagem?>> GetByViagemIdAsync(Guid viagemId)
            => await _context.CustosViagem.Where(x => x.ViagemId == viagemId).ToListAsync();

        public async Task<CustoViagem?> GetByIdAsync(Guid id)
                        => await _context.CustosViagem.FirstOrDefaultAsync(x => x.Id == id);
        public async Task<IEnumerable<CustoViagem?>> GetCustosCompletos()
        {
            return await _context.CustosViagem
                .Include(c => c.Viagem)
                .ToListAsync();
        }

    }
}
