using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Domain.Entities;
using LogTruck.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LogTruck.Infrastructure.Repositories
{
    public class MotoristaRepository : BaseRepository<Motorista>,IMotoristaRepository
    {
        private readonly AppDbContext _context;

        public MotoristaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Motorista?> GetByIdAsync(Guid id) => await _context.Motoristas.FindAsync(id);

        public async Task<List<Motorista>> GetAllMotoristasCompletos(Guid? motoristaId = null)
        {
            var query = _context.Motoristas
                .Include(m => m.Viagens)
                    .ThenInclude(v => v.Comissao)
                .Where(m => m.Ativo)
                .AsQueryable();

            if (motoristaId.HasValue)
            {
                query = query.Where(m => m.Id == motoristaId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
