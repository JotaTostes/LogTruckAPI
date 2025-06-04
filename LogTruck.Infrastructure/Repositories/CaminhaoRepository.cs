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
    public class CaminhaoRepository : BaseRepository<Caminhao>,ICaminhaoRepository
    {
        private readonly AppDbContext _context;

        public CaminhaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Caminhao?> GetByIdAsync(Guid id)
        => await _context.Caminhoes.FindAsync(id);

        public async Task<List<Caminhao>> GetCaminhoesCompletos()
        {
            return await _context.Caminhoes
                .Include(c => c.Viagens)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
