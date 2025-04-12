using LogTruck.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Persistence.Configurations
{
    public class ViagemConfiguration : IEntityTypeConfiguration<Viagem>
    {
        public void Configure(EntityTypeBuilder<Viagem> builder)
        {
            builder.ToTable("Viagens");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.DataSaida).IsRequired();
            builder.Property(v => v.DataRetorno).IsRequired(false);

            builder.HasOne(v => v.Motorista)
                   .WithMany(m => m.Viagens)
                   .HasForeignKey(v => v.MotoristaId);

            builder.HasOne(v => v.Caminhao)
                   .WithMany(vh => vh.Viagens)
                   .HasForeignKey(v => v.CaminhaoId);

            builder.HasMany(v => v.Custos)
                   .WithOne(cv => cv.Viagem)
                   .HasForeignKey(cv => cv.ViagemId);

            builder.HasOne(v => v.Comissao)
                   .WithOne(c => c.Viagem)
                   .HasForeignKey<Comissao>(c => c.ViagemId);
        }
    }
}
