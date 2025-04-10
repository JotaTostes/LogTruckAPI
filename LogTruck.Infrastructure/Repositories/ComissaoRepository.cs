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
    public class ComissaoRepository : IComissaoRepository
    {
        private readonly AppDbContext _context;

        public ComissaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comissao?> GetByIdAsync(Guid id) => await _context.Comissoes.FindAsync(id);

        public async Task<IEnumerable<Comissao>> GetAllAsync() => await _context.Comissoes.ToListAsync();

        public async Task AddAsync(Comissao comissao) => await _context.Comissoes.AddAsync(comissao);

        public void Update(Comissao comissao) => _context.Comissoes.Update(comissao);

        public void Delete(Comissao comissao) => _context.Comissoes.Remove(comissao);
    }
}
