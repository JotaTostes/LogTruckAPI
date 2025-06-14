using LogTruck.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<Usuario>().HasQueryFilter(u => u.Ativo);
            modelBuilder.Entity<Motorista>().HasQueryFilter(m => m.Ativo);
            modelBuilder.Entity<Caminhao>().HasQueryFilter(c => c.Ativo);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.CriadoEm))
                        .ValueGeneratedOnAdd()
                        .IsRequired();
                }
            }
        }
    }
}
