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
    public class ViagemRepository : BaseRepository<Viagem>,IViagemRepository
    {
        private readonly AppDbContext _context;

        public ViagemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Viagem?> GetByIdAsync(Guid id) => await _context.Viagens.FindAsync(id);

    }
}
