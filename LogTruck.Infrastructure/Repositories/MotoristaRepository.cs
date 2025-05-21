using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Domain.Entities;
using LogTruck.Persistence.Context;

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
    }
}
