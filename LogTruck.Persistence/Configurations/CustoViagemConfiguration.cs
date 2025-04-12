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
    public class CustoViagemConfiguration : IEntityTypeConfiguration<CustoViagem>
    {
        public void Configure(EntityTypeBuilder<CustoViagem> builder)
        {
            builder.ToTable("CustosViagem");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Valor)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
        }
    }
}
