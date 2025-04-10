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
    public class ComissaoConfiguration : IEntityTypeConfiguration<Comissao>
    {
        public void Configure(EntityTypeBuilder<Comissao> builder)
        {
            builder.ToTable("Comissoes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.ValorCalculado)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
        }
    }
}
