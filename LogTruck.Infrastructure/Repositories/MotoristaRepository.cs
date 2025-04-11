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
    public class MotoristaRepository : IMotoristaRepository
    {
        private readonly AppDbContext _context;

        public MotoristaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Motorista?> GetByIdAsync(Guid id) => await _context.Motoristas.FindAsync(id);

        public async Task<IEnumerable<Motorista>> GetAllAsync() => await _context.Motoristas.ToListAsync();

        public async Task AddAsync(Motorista motorista) => await _context.Motoristas.AddAsync(motorista);

        public void Update(Motorista motorista) => _context.Motoristas.Update(motorista);

        public void Delete(Motorista motorista) => _context.Motoristas.Remove(motorista);
    }
}
