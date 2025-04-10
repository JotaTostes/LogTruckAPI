using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogTruck.Domain.Entities;

namespace LogTruck.Persistence.Configurations
{
    public class CaminhaoConfiguration : IEntityTypeConfiguration<Caminhao>
    {
        public void Configure(EntityTypeBuilder<Caminhao> builder)
        {
            builder.ToTable("Caminhao");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Placa)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(c => c.Modelo)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(c => c.Viagens)
                   .WithOne(vg => vg.Caminhao)
                   .HasForeignKey(vg => vg.CaminhaoId);
        }
    }
}
