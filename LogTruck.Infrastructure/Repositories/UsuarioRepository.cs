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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        => await _context.Usuarios.FindAsync(id);

        public async Task<Usuario?> GetByEmailAsync(string email)
        => await _context.Usuarios.FirstAsync(x => x.Email == email);

        public async Task<IEnumerable<Usuario>> GetAllAsync()
            => await _context.Usuarios.ToListAsync();

        public async Task AddAsync(Usuario usuario)
            => await _context.Usuarios.AddAsync(usuario);

        public void Update(Usuario usuario)
            => _context.Usuarios.Update(usuario);

        public void Delete(Usuario usuario)
            => _context.Usuarios.Remove(usuario);
    }
}
