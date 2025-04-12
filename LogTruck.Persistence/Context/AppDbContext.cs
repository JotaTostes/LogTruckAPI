using LogTruck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Motorista> Motoristas => Set<Motorista>();
        public DbSet<Caminhao> Caminhoes => Set<Caminhao>();
        public DbSet<Viagem> Viagens => Set<Viagem>();
        public DbSet<CustoViagem> CustosViagem => Set<CustoViagem>();
        public DbSet<Comissao> Comissoes => Set<Comissao>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
