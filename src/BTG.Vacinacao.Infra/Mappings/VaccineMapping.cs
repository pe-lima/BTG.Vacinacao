using BTG.Vacinacao.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Infra.Mappings
{
    public class VaccineMapping : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.ToTable("Vaccines");
            
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Code)
                .IsRequired()
                .HasMaxLength(6);

            builder.HasIndex(v => v.Code)
                .IsUnique();

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(v => v.CreatedAt)
                .IsRequired();
     
            builder.Property(v => v.UpdatedAt)
                .IsRequired(false);
        }
    }
}
