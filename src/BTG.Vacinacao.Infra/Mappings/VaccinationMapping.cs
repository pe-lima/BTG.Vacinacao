using BTG.Vacinacao.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Infra.Mappings
{
    public class VaccinationMapping : IEntityTypeConfiguration<Vaccination>
    {
        public void Configure(EntityTypeBuilder<Vaccination> builder)
        {
            builder.ToTable("Vaccinations");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.ApplicationDate)
                .IsRequired();

            builder.HasOne(v => v.Person)
                .WithMany()
                .HasForeignKey(v => v.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Vaccine)
                .WithMany()
                .HasForeignKey(v => v.VaccineId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(v => v.DoseType)
                .IsRequired();
        }
    }
}
