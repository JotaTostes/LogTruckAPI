using LogTruck.Application.Common.Security;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Domain.Entities;
using LogTruck.Domain.Enums;
using LogTruck.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Infrastructure.Repositories
{
    public class ComissaoRepository : BaseRepository<Comissao>, IComissaoRepository
    {
        private readonly AppDbContext _context;

        public ComissaoRepository(AppDbContext context, ICurrentUserService currentUserService) : base(context, currentUserService)
        {
            _context = context;
        }

        public async Task<Comissao?> GetByIdAsync(Guid id) => await _context.Comissoes.FindAsync(id);

        public async Task<bool> ExistePorViagemIdAsync(Guid viagemId) => await _context.Comissoes.Include(c => c.Viagem)
            .AnyAsync(c => c.ViagemId == viagemId);

        public async Task<IEnumerable<Comissao>> GetComissaoCompleta()
        {
            return await _context.Comissoes
                .Include(c => c.Viagem)
                    .ThenInclude(x => x.Motorista)
                .Where(c => c.Viagem.Status != StatusViagem.Planejada && c.Viagem.Status != StatusViagem.Cancelada)
                .ToListAsync();
        }
    }
}
