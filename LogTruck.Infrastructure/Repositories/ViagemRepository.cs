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
    public class ViagemRepository : IViagemRepository
    {
        private readonly AppDbContext _context;

        public ViagemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Viagem?> GetByIdAsync(Guid id) => await _context.Viagens.FindAsync(id);

        public async Task<IEnumerable<Viagem>> GetAllAsync() => await _context.Viagens.ToListAsync();

        public async Task AddAsync(Viagem viagem) => await _context.Viagens.AddAsync(viagem);

        public void Update(Viagem viagem) => _context.Viagens.Update(viagem);

        public void Delete(Viagem viagem) => _context.Viagens.Remove(viagem);
    }
}
