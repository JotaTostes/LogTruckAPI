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
    public class CustoViagemRepository : ICustoViagemRepository
    {
        private readonly AppDbContext _context;

        public CustoViagemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustoViagem?> GetByIdAsync(Guid id) => await _context.CustosViagem.FindAsync(id);

        public async Task<IEnumerable<CustoViagem>> GetAllAsync() => await _context.CustosViagem.ToListAsync();

        public async Task AddAsync(CustoViagem custo) => await _context.CustosViagem.AddAsync(custo);

        public void Update(CustoViagem custo) => _context.CustosViagem.Update(custo);

        public void Delete(CustoViagem custo) => _context.CustosViagem.Remove(custo);
    }
}
