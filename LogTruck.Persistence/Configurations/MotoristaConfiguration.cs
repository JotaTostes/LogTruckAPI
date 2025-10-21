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
    public class MotoristaConfiguration : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder.ToTable("Motoristas");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.CNH)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasMany(m => m.Viagens)
                   .WithOne(v => v.Motorista)
                   .HasForeignKey(v => v.MotoristaId);
        }
    }
}
