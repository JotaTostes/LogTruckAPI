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
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        => await _context.Usuarios.FindAsync(id);
    }
}
