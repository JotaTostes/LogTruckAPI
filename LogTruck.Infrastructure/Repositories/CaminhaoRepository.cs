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
    public class CaminhaoRepository : ICaminhaoRepository
    {
        private readonly AppDbContext _context;

        public CaminhaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Caminhao?> GetByIdAsync(Guid id) => await _context.Caminhoes.FindAsync(id);

        public async Task<IEnumerable<Caminhao>> GetAllAsync() => await _context.Caminhoes.ToListAsync();

        public async Task AddAsync(Caminhao caminhao) => await _context.Caminhoes.AddAsync(caminhao);

        public void Update(Caminhao caminhao) => _context.Caminhoes.Update(caminhao);

        public void Delete(Caminhao caminhao) => _context.Caminhoes.Remove(caminhao);
    }
}
